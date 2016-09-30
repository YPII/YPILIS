using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class Accept
	{
		protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		protected YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
		protected YellowstonePathology.Business.Test.PanelOrder m_PanelOrderBeingAccepted;
        
        protected YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
        protected YellowstonePathology.Business.Rules.Rule m_Rule;
		protected YellowstonePathology.Business.User.SystemUser m_OrderingUser;
        protected YellowstonePathology.Business.User.SystemUser m_AcceptingUser;		
		protected YellowstonePathology.Business.Test.Model.TestCollection m_TestCollection;

		public Accept()
        {			
			this.m_TestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();
			this.m_Rule = new YellowstonePathology.Business.Rules.Rule();

            this.m_Rule.ActionList.Add(DoesUserHavePermission);
            this.m_Rule.ActionList.Add(HaltIfPanelOrderIsAccepted);
            this.m_Rule.ActionList.Add(HaltIfPanelSetOrderIsFinal);
            this.m_Rule.ActionList.Add(HaltIfAnyResultsAreEmpty);
			this.m_Rule.ActionList.Add(PatientIsLinked);
		}

		public void Execute(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus, Test.AccessionOrder accessionOrder, Test.PanelOrder panelOrder,
			YellowstonePathology.Business.User.SystemUser orderingUser, YellowstonePathology.Business.User.SystemUser acceptingUser)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelOrder.ReportNo);
			this.m_PanelOrderBeingAccepted = this.m_PanelSetOrder.PanelOrderCollection.GetByPanelOrderId(panelOrder.PanelOrderId);
			this.m_ExecutionStatus = executionStatus;
			this.m_OrderingUser = orderingUser;
            this.m_AcceptingUser = acceptingUser;
            this.m_Rule.Execute(executionStatus);
        }

		protected virtual void DoesUserHavePermission()
		{

		}

        protected virtual void HaltIfPanelSetOrderIsFinal()
        {
			if (this.m_PanelSetOrder.Final == true)
            {
                this.m_ExecutionStatus.AddMessage("Case is already final", true);
            }
        }

        protected virtual void HaltIfPanelOrderIsAccepted()
        {
            if (this.m_PanelOrderBeingAccepted.Accepted == true)
            {
                this.m_ExecutionStatus.AddMessage("Panel is already accepted.", true);
            }
        }

        protected virtual void HaltIfAnyResultsAreEmpty()
        {
			foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this.m_PanelOrderBeingAccepted.TestOrderCollection)
            {
                if (string.IsNullOrEmpty(testOrder.Result) == true)
                {
                    this.m_ExecutionStatus.AddMessage("An empty result was found.", true);
                    break;
                }
            }
        }

		protected virtual void PatientIsLinked()
		{
			if (this.m_AccessionOrder.PatientId == "0")
			{
				this.m_ExecutionStatus.AddMessage("Patient is not linked", true);
			}
		}

        protected virtual void AcceptPanel()
        {
            this.m_PanelOrderBeingAccepted.Accepted = true;
            this.m_PanelOrderBeingAccepted.AcceptedById = this.m_AcceptingUser.UserId;
			this.m_PanelOrderBeingAccepted.AcceptedDate = DateTime.Today;
            this.m_PanelOrderBeingAccepted.AcceptedTime = DateTime.Now;
        }

        protected virtual void Save()
        {
        }
	}
}

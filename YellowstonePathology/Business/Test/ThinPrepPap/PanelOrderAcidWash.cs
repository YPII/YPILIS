using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.User;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
	[PersistentClass("tblPanelOrderAcidWash", "tblPanelOrder", "YPIDATA")]
	public class PanelOrderAcidWash : YellowstonePathology.Business.Test.PanelOrder
	{
		private string m_Result;

		public PanelOrderAcidWash()
		{

		}

		public PanelOrderAcidWash(string reportNo, string objectId, string panelOrderId, YellowstonePathology.Business.Panel.Model.Panel panel, int orderedById)
			: base(reportNo, objectId, panelOrderId, panel, orderedById)
		{

		}

		public override void AcceptResults(YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, Business.User.SystemUser acceptingUser)
		{
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.ReportNo);
			if (panelSetOrder.Final == true)
			{
				executionStatus.AddMessage(this.ReportNo + " is already finaled.", true);
				ruleExecutionStatus.PopulateFromLinqExecutionStatus(executionStatus);
				return;
			}

			if (this.Accepted == true)
			{
				executionStatus.AddMessage(this.ReportNo + " Acid Wash result has already been accepted.", true);
				ruleExecutionStatus.PopulateFromLinqExecutionStatus(executionStatus);
				return;
			}

			this.Accepted = true;
			this.AcceptedById = acceptingUser.UserId;
			this.AcceptedDate = DateTime.Today;
			this.AcceptedTime = DateTime.Now;

			this.Acknowledged = true;
			this.AcknowledgedById = acceptingUser.UserId;
			this.AcknowledgedDate = DateTime.Today;
			this.AcknowledgedTime = DateTime.Now;
		}

        public override void AcceptResults()
        {
            base.AcceptResults();
            this.NotifyPropertyChanged("AcceptedBy");
        }

        public override void UnacceptResults()
        {
            base.UnacceptResults();
            this.NotifyPropertyChanged("AcceptedBy");
        }

        public Rules.MethodResult IsOkToAccept()
        {
            Rules.MethodResult methodResult = new Rules.MethodResult();
            if(this.Accepted == true)
            {
                methodResult.Success = false;
                methodResult.Message = "Result has already been accepted.";
            }
            return methodResult;
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result
		{
			get { return this.m_Result; }
			set
			{
				if (this.m_Result != value)
				{
					this.m_Result = value;
					this.NotifyPropertyChanged("Result");
				}
			}
		}
	}
}

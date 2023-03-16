using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Surgical
{
    public class RulesAssignPathologistId
	{
        private static RulesAssignPathologistId m_Instance;        
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;        
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected YellowstonePathology.Business.User.SystemUserRoleDescriptionList m_PermissionList;
        protected RuleExecutionStatus m_RuleExecutionStatus;

        private RulesAssignPathologistId()            
        {
            this.m_PermissionList = new User.SystemUserRoleDescriptionList();
			this.m_PermissionList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist);
			this.m_PermissionList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Administrator);
        }

        public bool CaseIsFinal
        {
            get
            {
                return this.m_PanelSetOrder.Final;
            }
        }

        public bool CaseIsAssigned()
        {
            bool result = false;
            if (this.m_PanelSetOrder.AssignedToId != 0)
			{
                result = true;
            }			
            return result;
        }

        public void UnAssignPathologist()
        {
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);
            this.m_PanelSetOrder.AssignedToId = 0;
            this.m_AccessionOrder.CaseOwnerId = 0;
        }

        public void AssignPathologist()
        {
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);
            this.m_PanelSetOrder.AssignedToId = Business.User.SystemIdentity.Instance.User.UserId;
            this.m_AccessionOrder.CaseOwnerId = Business.User.SystemIdentity.Instance.User.UserId;
		}

        public void HandleDrNeroPeerReview()
        {
            
        }		

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
            set { this.m_PanelSetOrder = value; }
        }        

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set { this.m_AccessionOrder = value; }
		}

        public void Run(YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus)
        {
            this.m_RuleExecutionStatus = ruleExecutionStatus;
            this.Execute();
        }

        public virtual RuleExecutionStatus Execute()
        {
            bool hasPermission = Business.User.SystemIdentity.Instance.User.IsUserInRole(this.m_PermissionList);
            this.m_RuleExecutionStatus = new RuleExecutionStatus();
            if(hasPermission)
            {
                if(this.CaseIsFinal == false)
                {
                    if(this.CaseIsAssigned() == false)
                    {
                        RuleExecutionStatusItem status = new RuleExecutionStatusItem();
                        status.ExecutionHalted = false;
                        status.Description = "This case has been assigned.";
                        this.AssignPathologist();
                    }
                    else
                    {
                        RuleExecutionStatusItem status = new RuleExecutionStatusItem();
                        status.ExecutionHalted = false;
                        status.Description = "This case has been unassigned.";
                        this.UnAssignPathologist();
                    }
                }
                else
                {
                    RuleExecutionStatusItem status = new RuleExecutionStatusItem();
                    status.ExecutionHalted = true;
                    status.Description = "This case cannot be assigned because it is final.";
                }
            }
            else
            {
                RuleExecutionStatusItem status = new RuleExecutionStatusItem();
                status.ExecutionHalted = true;
                status.Description = "You do not have permission to assign this case to yourself.";
                this.m_RuleExecutionStatus.RuleExecutionStatusList.Add(status);
            }

            return this.m_RuleExecutionStatus;
        }

        public static RulesAssignPathologistId Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new RulesAssignPathologistId();
                }
                return m_Instance;
            }
        }               
	}
}

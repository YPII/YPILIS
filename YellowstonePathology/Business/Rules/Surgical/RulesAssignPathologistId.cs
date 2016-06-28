using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Surgical
{
    public class RulesAssignPathologistId : BaseRules
	{
        private static RulesAssignPathologistId m_Instance;        
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;        
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		private RulesAssignPathologistId()
            : base(typeof(RulesAssignPathologistId))
        {
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

		public bool OKToAssignSpecialDermCase()
		{
			bool result = true;
			string msg = string.Empty;
			switch (this.m_AccessionOrder.PhysicianId)
			{				
				case 58:// Dr Hawk					
                    if (!(YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId == 5088))
					{
						msg = "Dr Hawk has requested that only Dr. Emerick sign her cases.";
						result = false;
					}
					break;				
				default:
					break;
			}

			if (msg.Length > 0)
			{
				System.Windows.MessageBoxResult dialogResult = System.Windows.MessageBox.Show(msg + "\n\n Are you sure you want to reassign this case?", "Reassign Case?", System.Windows.MessageBoxButton.YesNo);
				if (dialogResult == System.Windows.MessageBoxResult.Yes)
				{
					result = true;
				}
			}
			return result;
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

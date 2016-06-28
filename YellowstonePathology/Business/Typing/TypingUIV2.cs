using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace YellowstonePathology.Business.Typing
{
	public class TypingUIV2 : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		DateTime m_CaseListDate;

		YellowstonePathology.Business.User.SystemUserCollection m_TypingUsers;
		YellowstonePathology.Business.Surgical.SurgicalOrderList m_SurgicalOrderList;

		private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;

		bool m_AuditMode = false;
		bool m_IsPossibleReportableCase = false;
		private YellowstonePathology.Business.Common.FieldEnabler m_FieldEnabler;
		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		YellowstonePathology.Business.Typing.ParagraphTemplateCollection m_ParagraphTemplateCollection;
		private YellowstonePathology.Business.View.BillingSpecimenViewCollection m_BillingSpecimenViewCollection;
		private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
		private string m_TemplateText;
        private System.Windows.Controls.TabItem m_Writer;

		public TypingUIV2(System.Windows.Controls.TabItem writer)
		{
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;

			this.m_ParagraphTemplateCollection = new ParagraphTemplateCollection();

			this.m_CaseListDate = DateTime.Today;

			this.m_TypingUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Typing, true);

			this.m_SurgicalOrderList = new YellowstonePathology.Business.Surgical.SurgicalOrderList();
			this.m_SurgicalOrderList.FillByAccessionDate(DateTime.Today);

			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);
			YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.AddAllToUserList(this.PathologistUsers, true);

			this.m_BillingSpecimenViewCollection = new View.BillingSpecimenViewCollection();

			this.m_FieldEnabler = new YellowstonePathology.Business.Common.FieldEnabler();
            this.m_Writer = writer;       
		}

		public void GetAccessionOrder(string reportNo)
		{
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo);
            if(string.IsNullOrEmpty(masterAccessionNo) == false)
            {
                this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);

                if (this.m_AccessionOrder != null)
                {
                    this.m_SurgicalTestOrder = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

                    this.m_CaseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_AccessionOrder, reportNo);

                    this.RefreshBillingSpecimenViewCollection();

                    this.NotifyPropertyChanged("");
                }
                else
                {
                    MessageBox.Show("Case Not Found.");
                }
            }
            else
            {
                MessageBox.Show("Case Not Found.");
            }
		}

        public Business.User.SystemIdentity SystemIdentity
        {
            get { return this.m_SystemIdentity; }
        }

		public void Save(bool releaseLock)
		{			            
            
		}
		
		public string TemplateText
        {
            get { return this.m_TemplateText; }
            set { this.m_TemplateText = value; }
        }

		public ParagraphTemplateCollection ParagraphTemplateCollection
		{
			get { return this.m_ParagraphTemplateCollection; }
			set { this.m_ParagraphTemplateCollection = value; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
            set
            {
                this.m_AccessionOrder = value;
                this.NotifyPropertyChanged("AccessionOrder");
            }
		}

		public YellowstonePathology.Business.Common.FieldEnabler FieldEnabler
		{
			get { return this.m_FieldEnabler; }
			set { this.m_FieldEnabler = value; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
		{
			get { return this.m_PathologistUsers; }
		}

		public void SetIsPossibleReportableCase()
		{
			YellowstonePathology.Business.Surgical.ReportableCases reportableCases = new YellowstonePathology.Business.Surgical.ReportableCases();
			this.IsPossibleReportableCase = reportableCases.IsPossibleReportableCase(this.m_SurgicalTestOrder);
		}

		public Visibility ImmediateVisibility
		{
			get
			{
				foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen ssrItem in this.m_SurgicalTestOrder.SurgicalSpecimenCollection)
				{
					if (ssrItem.ImmediatePerformed == true)
					{
						return Visibility.Visible;
					}
				}
				return Visibility.Collapsed;
			}
		}

		public DateTime CaseListDate
		{
			get { return this.m_CaseListDate; }
			set
			{
				if (value != this.m_CaseListDate)
				{
					this.m_CaseListDate = value;
					this.NotifyPropertyChanged("CaseListDate");
				}
			}
		}

		public bool IsPossibleReportableCase
		{
			get { return this.m_IsPossibleReportableCase; }
			set
			{
				if (value != this.m_IsPossibleReportableCase)
				{
					this.m_IsPossibleReportableCase = value;
					this.NotifyPropertyChanged("IsPossibleReportableCase");
				}
			}
		}

		public bool AuditMode
		{
			get { return this.m_AuditMode; }
			set
			{
				if (value != this.m_AuditMode)
				{
					this.m_AuditMode = value;
					this.NotifyPropertyChanged("AuditMode");
				}
			}
		}

		public YellowstonePathology.Business.User.SystemUserCollection TypingUsers
		{
			get { return this.m_TypingUsers; }
		}

		public string PatientName
		{
			get
			{
				return this.AccessionOrder.PatientName;
			}
		}

		public YellowstonePathology.Business.Surgical.SurgicalOrderList SurgicalOrderList
		{
			get { return m_SurgicalOrderList; }
		}

		public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder SurgicalTestOrder
		{
			get
			{
				if (this.AccessionOrder != null)
				{
					return this.m_SurgicalTestOrder;
				}
				return null;
			}

		}

		public void DeletePanelSetOrderCptCode(string panelSetOrderCPTCodeId)
		{
			YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.GetPanelSetOrderCPTCode(panelSetOrderCPTCodeId);
			if (panelSetOrderCPTCode.PostDate.HasValue == false || panelSetOrderCPTCode.PostDate == DateTime.Today)
			{
				if (MessageBoxResult.Yes == MessageBox.Show("Delete this Item?", "Delete.", MessageBoxButton.YesNo, MessageBoxImage.Question))
				{
					this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.Remove(panelSetOrderCPTCode);
				}
			}
			else
			{
				if (MessageBoxResult.Yes ==
					MessageBox.Show("This CPT Code cannot be deleted. Would you like to reverse the entry.", "Reverse Entry", MessageBoxButton.YesNo, MessageBoxImage.Question))
				{
					//this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.Reverse(panelSetOrderCPTCode);
				}
			}
			this.RefreshBillingSpecimenViewCollection();
		}

		public void RefreshBillingSpecimenViewCollection()
		{
			this.m_BillingSpecimenViewCollection.Refresh(this.m_SurgicalTestOrder.SurgicalSpecimenCollection,
					this.m_AccessionOrder.SpecimenOrderCollection, this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection, this.m_AccessionOrder.ICD9BillingCodeCollection);
		}

		public void DeleteIcd9Code(string icd9BillingId)
		{
			YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = this.m_AccessionOrder.ICD9BillingCodeCollection.GetCurrent(icd9BillingId);
			this.m_AccessionOrder.ICD9BillingCodeCollection.Remove(icd9BillingCode);
			this.RefreshBillingSpecimenViewCollection();
		}

		public void RunWorkspaceEnableRules()
		{
			if (this.m_SurgicalTestOrder.ReportNo != string.Empty)
			{
				YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
				YellowstonePathology.Business.Rules.WorkspaceEnableRules workspaceEnableRules = new Rules.WorkspaceEnableRules();
				workspaceEnableRules.Execute(this.AccessionOrder, this.m_SurgicalTestOrder, this.m_FieldEnabler, executionStatus);
			}
		}		

		public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
		}

		public YellowstonePathology.Business.View.BillingSpecimenViewCollection BillingSpecimenViewCollection
		{
			get { return this.m_BillingSpecimenViewCollection; }
		}


		public void RefreshCaseDocumentCollection(string reportNo)
		{
			this.m_CaseDocumentCollection = new Document.CaseDocumentCollection(reportNo);
			NotifyPropertyChanged("CaseDocumentCollection");
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
    }
}

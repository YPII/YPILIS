using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace YellowstonePathology.UI.Billing
{	
	public partial class BillingPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        public delegate void ShowXPSDocumentEventHandler(object sender, CustomEventArgs.FileNameReturnEventArgs e);
        public event ShowXPSDocumentEventHandler ShowXPSDocument;

        public delegate void ShowTIFDocumentEventHandler(object sender, CustomEventArgs.FileNameReturnEventArgs e);
        public event ShowTIFDocumentEventHandler ShowTIFDocument;

        public delegate void ShowICDCodeEntryEventHandler(object sender, CustomEventArgs.AccessionOrderWithTrackerReturnEventArgs e);
        public event ShowICDCodeEntryEventHandler ShowICDCodeEntry;

        public delegate void ShowCPTCodeEntryEventHandler(object sender, CustomEventArgs.PanelSetOrderReturnEventArgs e);
        public event ShowCPTCodeEntryEventHandler ShowCPTCodeEntry;

		public delegate void ShowPatientDetailPageEventHandler(object sender, EventArgs e);
		public event ShowPatientDetailPageEventHandler ShowPatientDetailPage;

        public delegate void ShowADTPageEventHandler(object sender, EventArgs e);
        public event ShowADTPageEventHandler ShowADTPage;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
		private string m_PageHeaderText;
        
        private YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection m_PanelSetOrderCPTCodeCollection;
        private YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection m_PanelSetOrderCPTCodeBillCollection;
        private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
                
        private string m_ReportNo;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;        

        public BillingPage(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{			
			this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;

            this.m_FacilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance;
            this.m_FacilityCollection.Insert(0, new Business.Facility.Model.Facility());

            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.m_PanelSetOrderCPTCodeCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection;
            this.m_PanelSetOrderCPTCodeBillCollection = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection;                        

            this.m_CaseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_ReportNo);            
			this.m_PageHeaderText = "Billing For: " + this.m_AccessionOrder.PatientDisplayName + " - " + this.m_AccessionOrder.PBirthdate.Value.ToShortDateString();            

			InitializeComponent();

			DataContext = this;
            this.Loaded += new RoutedEventHandler(BillingPage_Loaded);
            this.Unloaded += BillingPage_Unloaded;            
        }        

        private void BillingPage_Loaded(object sender, RoutedEventArgs e)
        {             
            YellowstonePathology.Business.Document.CaseDocument firstRequisition = this.m_CaseDocumentCollection.GetFirstRequisition();
            if(firstRequisition != null)
            {
                this.ShowTIFDocument(this, new CustomEventArgs.FileNameReturnEventArgs(firstRequisition.FullFileName));
            }            
        }

        private void BillingPage_Unloaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();            
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }              

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection PanelSetOrderCPTCodeCollection
        {
            get { return this.m_PanelSetOrderCPTCodeCollection; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection PanelSetOrderCPTCodeBillCollection
        {
            get { return this.m_PanelSetOrderCPTCodeBillCollection; }
        }

        public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
        {
            get { return this.m_CaseDocumentCollection; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}		

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());            
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        

        private void MenuItemDeletePanelSetOrderCPTCodes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCode.SelectedItems.Count != 0)
            {
                for (int i = this.ListViewPanelSetOrderCPTCode.SelectedItems.Count - 1; i >= 0; i--)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = (YellowstonePathology.Business.Test.PanelSetOrderCPTCode)this.ListViewPanelSetOrderCPTCode.SelectedItems[i];
                    this.m_PanelSetOrderCPTCodeCollection.Remove(panelSetOrderCPTCode);
                }
            }
        }

        private void MenuItemDeletePanelSetOrderCPTCodeBill_Click(object sender, RoutedEventArgs e)
        {            
            if (this.ListViewPanelSetOrderCPTCodeBill.SelectedItems.Count != 0)
            {
                for (int i = this.ListViewPanelSetOrderCPTCodeBill.SelectedItems.Count - 1; i >= 0; i--)
                {                    
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill)this.ListViewPanelSetOrderCPTCodeBill.SelectedItems[i];
                    this.m_PanelSetOrderCPTCodeBillCollection.Remove(panelSetOrderCPTCodeBill);                 
                }
            }         
        }        

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {                        
            if (this.IsTechnicalBillingFacilityValid() == true)
            {
                if (this.IsProfessionalBillingFacilityValid() == true)
                {
                    YellowstonePathology.Business.Billing.Model.BillableObject billableObject = Business.Billing.Model.BillableObjectFactory.GetBillableObject(this.m_AccessionOrder, this.m_ReportNo);
                    YellowstonePathology.Business.Rules.MethodResult methodResult = billableObject.Set();
                    if (methodResult.Success == false)
                    {
                        MessageBox.Show(methodResult.Message);
                    }
                }
            }            
        }

        private bool IsTechnicalBillingFacilityValid()
        {
            bool result = true;
            YellowstonePathology.Business.Facility.Model.Facility technicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(this.m_PanelSetOrder.TechnicalComponentFacilityId);
            YellowstonePathology.Business.Facility.Model.ClientBillingFacilityCollection clientBillingFacilityCollection = new YellowstonePathology.Business.Facility.Model.ClientBillingFacilityCollection();            
            return result;
        }

        private bool IsProfessionalBillingFacilityValid()
        {
            bool result = true;
            YellowstonePathology.Business.Facility.Model.Facility professionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(this.m_PanelSetOrder.ProfessionalComponentFacilityId);
            YellowstonePathology.Business.Facility.Model.ClientBillingFacilityCollection clientBillingFacilityCollection = new YellowstonePathology.Business.Facility.Model.ClientBillingFacilityCollection();            
            return result;
        }

        private void ButtonPost_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Billing.Model.BillableObject billableObject = Business.Billing.Model.BillableObjectFactory.GetBillableObject(this.m_AccessionOrder, this.m_ReportNo);
            YellowstonePathology.Business.Rules.MethodResult methodResult = billableObject.Post();
            if (methodResult.Success == false)
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        private void ButtonUnpost_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Billing.Model.BillableObject billableObject = Business.Billing.Model.BillableObjectFactory.GetBillableObject(this.m_AccessionOrder, this.m_ReportNo);
            billableObject.Unpost();
        }

        private void ButtonUnset_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Billing.Model.BillableObject billableObject = Business.Billing.Model.BillableObjectFactory.GetBillableObject(this.m_AccessionOrder, this.m_ReportNo);
            billableObject.Unset();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (this.Close != null) this.Close(this, new EventArgs());            
        }        
        
        private void ListViewDocuments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewDocuments.SelectedItem != null)
            {
                YellowstonePathology.Business.Document.CaseDocument caseDocument = (YellowstonePathology.Business.Document.CaseDocument)this.ListViewDocuments.SelectedItem;
                switch (caseDocument.Extension.ToUpper())
                {
                    case "XPS":
                        if (this.ShowXPSDocument != null) this.ShowXPSDocument(this, new CustomEventArgs.FileNameReturnEventArgs(caseDocument.FullFileName));            
                        break;
                    case "TIF":
                        if (this.ShowTIFDocument != null) this.ShowTIFDocument(this, new CustomEventArgs.FileNameReturnEventArgs(caseDocument.FullFileName));            
                        break;
                }
            }
        }

        private void ButtonICDCodes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowICDCodeEntry != null) this.ShowICDCodeEntry(this, new CustomEventArgs.AccessionOrderWithTrackerReturnEventArgs(this.m_AccessionOrder));
        }

        private void ButtonCPTCodes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowCPTCodeEntry != null) this.ShowCPTCodeEntry(this, new CustomEventArgs.PanelSetOrderReturnEventArgs(this.m_PanelSetOrder));
        }               

        private void MenuItemReverseBillTo_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCodeBill.SelectedItem != null)
            {
                foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this.ListViewPanelSetOrderCPTCodeBill.SelectedItems)
                {
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.ReverseBillTo(panelSetOrderCPTCodeBill);
                }                                                
            }
        }

        private void MenuItemReverseBillByClient_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCodeBill.SelectedItem != null)
            {
                foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this.ListViewPanelSetOrderCPTCodeBill.SelectedItems)
                {
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.ReverseBillByClient(panelSetOrderCPTCodeBill);
                }
            }
        }

        private void MenuItemReverse_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCodeBill.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill)this.ListViewPanelSetOrderCPTCodeBill.SelectedItem;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Reverse(panelSetOrderCPTCodeBill);                
            }
        }

        private void MenuItemPostSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCode.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = (YellowstonePathology.Business.Test.PanelSetOrderCPTCode)this.ListViewPanelSetOrderCPTCode.SelectedItem;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Post(panelSetOrderCPTCode);
            }
        }                

		private void ButtonPatientDetails_Click(object sender, RoutedEventArgs e)
		{
			this.ShowPatientDetailPage(this, new EventArgs());
		}

        private void MenuItemAddWithCurrentClient_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCodeBill.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill)this.ListViewPanelSetOrderCPTCodeBill.SelectedItem;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.AddWithClientId(panelSetOrderCPTCodeBill, this.m_AccessionOrder.ClientId);
            }
        }

        private void MenuItemUpdateClientId_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCode.SelectedItems.Count != 0)
            {
                foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.ListViewPanelSetOrderCPTCode.SelectedItems)
                {
                    panelSetOrderCPTCode.ClientId = AccessionOrder.ClientId;
                }
            }
        }                       

        private void ButtonADT_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == false)
            {
                this.ShowADTPage(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Cannot show ADT information when the MRN is blank");
            }
        }

        private void ButtonInsuranceCard_Click(object sender, RoutedEventArgs e)
        {
            this.CreateInsuranceCard();            
        }

        private void CreateInsuranceCard()
        {
            Business.HL7View.ADTMessages adtMessages = Business.Gateway.AccessionOrderGateway.GetADTMessages(this.m_AccessionOrder.SvhMedicalRecord);
            if (adtMessages.Messages.Count > 0)
            {
                Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_AccessionOrder.MasterAccessionNo);
                YellowstonePathology.Business.Document.ADTInsuranceDocument adtInsuranceDocument = new Business.Document.ADTInsuranceDocument(adtMessages);
                adtInsuranceDocument.SaveAsTIF(orderIdParser);
                this.m_CaseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_ReportNo);
                this.NotifyPropertyChanged("CaseDocumentCollection");
                MessageBox.Show("The insurance card was successfully created.");
            }
            else
            {
                MessageBox.Show("No ADT information was found.");
            }
        }

        private void MenuItemUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCodeBill.SelectedItems.Count != 0)
            {
                foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this.ListViewPanelSetOrderCPTCodeBill.SelectedItems)
                {
                    panelSetOrderCPTCodeBill.ClientId = AccessionOrder.ClientId;
                }
            }
        }        

        private void MenuItemAddOne_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCodeBill.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill)this.ListViewPanelSetOrderCPTCodeBill.SelectedItem;
                panelSetOrderCPTCodeBill.Quantity += 1;
            }
        }

        private void MenuItemChangeTo88342_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCode.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = (YellowstonePathology.Business.Test.PanelSetOrderCPTCode)this.ListViewPanelSetOrderCPTCode.SelectedItem;
                panelSetOrderCPTCode.CPTCode = "88342";
            }
        }
    }
}

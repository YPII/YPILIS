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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{	
	public partial class AccessionOrderPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void OrderPanelSetEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs e);
        public event OrderPanelSetEventHandler OrderPanelSet;

        public delegate void ShowSurgicalDiagnosisEventHandler(object sender, EventArgs e);
        public event ShowSurgicalDiagnosisEventHandler ShowSurgicalDiagnosis;

        public delegate void ShowSurgicalGrossDescriptionEventHandler(object sender, EventArgs e);
        public event ShowSurgicalGrossDescriptionEventHandler ShowSurgicalGrossDescription;        

        public delegate void StartAccessionedSpecimenPathHandler(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event StartAccessionedSpecimenPathHandler StartAccessionedSpecimenPath;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		public delegate void NextEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.ReportNoReturnEventArgs e);
        public event NextEventHandler Next;

        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        public delegate void ShowResultPageEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.PanelSetOrderReturnEventArgs e);
        public event ShowResultPageEventHandler ShowResultPage;

        public delegate void ShowMissingInformationPageEventHandler(object sender, EventArgs e);
        public event ShowMissingInformationPageEventHandler ShowMissingInformationPage;
		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;		

		private string m_PageHeaderText = "Report Order page";		
        
        private YellowstonePathology.Business.PanelSet.Model.PanelSetCollection m_PanelSetCollectionView;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;
        private OrderPageCaseTypeList m_CaseTypeList;

        private PageNavigationModeEnum m_PageNavigationMode;

		public AccessionOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,             
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,        
            PageNavigationModeEnum pageNavigationMode)
		{            
			this.m_AccessionOrder = accessionOrder;
            this.m_PageNavigationMode = pageNavigationMode;

			this.m_ClientOrder = clientOrder;			

            if (YellowstonePathology.Business.User.SystemIdentity.Instance.User.IsUserInRole(Business.User.SystemUserRoleDescriptionEnum.Pathologist) == true)
            {
                this.m_PanelSetCollectionView = Business.PanelSet.Model.PanelSetCollection.GetPathologistPanelSets();
            }
            else
            {
                this.m_PanelSetCollectionView = Business.PanelSet.Model.PanelSetCollection.GetHistologyPanelSets();
            }

            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            this.m_CaseTypeList = new OrderPageCaseTypeList();

			InitializeComponent();

            this.SetButtonVisibility();
			DataContext = this;

			this.Loaded += new RoutedEventHandler(AccessionOrderPage_Loaded);            
		}

        public AccessionOrderPage(ClientOrderReceivingHandler clientOrderReceivingHandler, PageNavigationModeEnum pageNavigationMode)
        {            
            this.m_AccessionOrder = clientOrderReceivingHandler.AccessionOrder;            
            this.m_PageNavigationMode = pageNavigationMode;

            this.m_ClientOrder = clientOrderReceivingHandler.ClientOrder;            

            this.m_PanelSetCollectionView = Business.PanelSet.Model.PanelSetCollection.GetHistologyPanelSets();
            this.m_FacilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();

            this.m_CaseTypeList = new OrderPageCaseTypeList();

            InitializeComponent();

            this.SetButtonVisibility();
            DataContext = this;

			this.Loaded += new RoutedEventHandler(AccessionOrderPage_Loaded);                                    
        }

        private void AccessionOrderPage_Loaded(object sender, RoutedEventArgs e)
		{
            this.SelectTestOrder();            
        }
                
        private void AccessionOrderPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "PanelSetOrderCollection")
			{
				this.ListViewPanelSetOrder.SelectedIndex = -1;
				if (this.m_AccessionOrder.PanelSetOrderCollection.Count == 1)
				{
					this.ListViewPanelSetOrder.SelectedIndex = 0;
				}
			}
		}

        private void SetButtonVisibility()
        {
            switch (this.m_PageNavigationMode)
            {
                case PageNavigationModeEnum.Inline:
                    this.ButtonBack.Visibility = System.Windows.Visibility.Visible;
                    this.ButtonClose.Visibility = System.Windows.Visibility.Visible;
                    this.ButtonNext.Visibility = System.Windows.Visibility.Visible;
                    break;
                case PageNavigationModeEnum.Standalone:
                    this.ButtonBack.Visibility = System.Windows.Visibility.Collapsed;
                    this.ButtonClose.Visibility = System.Windows.Visibility.Visible;
                    this.ButtonNext.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }
        }

        public OrderPageCaseTypeList CaseTypeList
        {
            get { return this.m_CaseTypeList; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSetCollection PanelSetCollectionView
        {
            get { return this.m_PanelSetCollectionView; }
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }                
       		
		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}				

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}        

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {			
			this.Back(this, new EventArgs());
		}		

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (IsOkToGoNext() == true)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Count == 0)
                {
                    MessageBox.Show("You need to order something before you can Finalize this case.");
                }
                else if (this.ListViewPanelSetOrder.SelectedItem != null)
                {
                    if (this.Next != null)
                    {
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                        YellowstonePathology.UI.CustomEventArgs.ReportNoReturnEventArgs returnEventArgs = new CustomEventArgs.ReportNoReturnEventArgs(((YellowstonePathology.Business.Test.PanelSetOrder)this.ListViewPanelSetOrder.SelectedItem).ReportNo);
                        this.Next(this, returnEventArgs);
                    }
                }
                else
                {
                    MessageBox.Show("You need to select the report in the list to Finalize.");
                }
            }
		}

        private bool IsOkToGoNext()
        {
            bool result = true;
            if (this.m_AccessionOrder.SpecimenOrderCollection.HasMultipleSpecimenWithThinPrepInFirstPosition() == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("This case has multiple specimen with the Thin Prep as specimen #1. Are you sure you want to continue?", "Thin Prep Conflict?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    result = false;
                }
            }
            return result;
        }

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            if (this.Close != null) this.Close(this, new EventArgs());
		}        		
        
        private void ListBoxCaseTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (this.ListBoxCaseTypes.SelectedItem != null)
            {
                string caseType = (string)this.ListBoxCaseTypes.SelectedItem;                
                switch(caseType)
                {
                    case "Histology":
                        this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetHistologyPanelSets();
                        break;
                    case "Flow Cytometry":                        
                        this.m_PanelSetCollectionView =  YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetByCaseType("Flow Cytometry");
                        break;
                    case "Molecular Genetics":
                        this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetByCaseType("Molecular");
                        break;
                    case "FISH":
                        this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetByCaseType("FISH");
                        break;
                    case "Neogenomics":
                        YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine neo = new Business.Facility.Model.NeogenomicsIrvine();
                        this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetByFacility(neo);
                        break;
                    case "ARUP":
                        YellowstonePathology.Business.Facility.Model.ARUP arup = new Business.Facility.Model.ARUP();
                        this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetByFacility(arup);
                        break;
					case "Reflex Testing":
						this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetReflexTestingPanelSets();
						break;
                    case "Pathologist":
                        this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetPathologistPanelSets();
                        break;
                    case "All Orders":
                        this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAllActive();
                        break;                        
                }                
                
                this.NotifyPropertyChanged("PanelSetCollectionView");         
            }
        }        
        
        private void HyperLinkPanelSet_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = (Hyperlink)sender;
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)hyperLink.Tag;
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo();
            testOrderInfo.PanelSet = panelSet;
            testOrderInfo.Distribute = true;

            YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs reportOrderInfoEventArgs = new CustomEventArgs.TestOrderInfoEventArgs(testOrderInfo);
			if (this.OrderPanelSet != null) this.OrderPanelSet(this, reportOrderInfoEventArgs);

            this.SelectTestOrder();
			this.NotifyPropertyChanged("PanelSetOrderCollection");
		}

        private void SelectTestOrder()
        {
            this.ListViewPanelSetOrder.SelectedIndex = -1;
            if (this.ListViewPanelSetOrder.Items.Count == 1)
            {
                this.ListViewPanelSetOrder.SelectedIndex = 0;
            }
            else if (this.ListViewPanelSetOrder.Items.Count > 1)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true && this.m_AccessionOrder.PanelSetOrderCollection.HasThinPrepPapOrder() == false 
                    && this.m_AccessionOrder.PanelSetOrderCollection.Exists(20) == false)
                {
                    this.SelectTestOrder(13);
                }
                else if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == false && this.m_AccessionOrder.PanelSetOrderCollection.HasThinPrepPapOrder() == true)
                {
                    this.SelectTestOrder(15);
                }
            }
        }

        private void SelectTestOrder(int panelSetId)
        {
            int index = 0;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.ListViewPanelSetOrder.Items)
            {
                if (panelSetOrder.PanelSetId == panelSetId)
                {
                    this.ListViewPanelSetOrder.SelectedIndex = index;
                }
                index += 1;
            }
        }        

        private void HyperLinkSurgicalDiagnosis_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();
			if (this.m_AccessionOrder != null && this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetSurgical.PanelSetId) == true)
			{
                if (this.ShowSurgicalDiagnosis != null)
                {
                    this.ShowSurgicalDiagnosis(this, new EventArgs());
                }
			}
			else
			{
				MessageBox.Show("A surgical report must be ordered first.");
			}
		}

        private void HyperLinkGrossDescription_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();
            if (this.m_AccessionOrder != null && this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetSurgical.PanelSetId) == true)
            {
                if (this.ShowSurgicalGrossDescription != null)
                {
                    this.ShowSurgicalGrossDescription(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("A surgical report must be ordered first.");
            }
        }           

        private void ListBoxSpecimen_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListBoxSpecimen.SelectedItem != null)
            {
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.ListBoxSpecimen.SelectedItem;
                if(this.StartAccessionedSpecimenPath != null) this.StartAccessionedSpecimenPath(this, new CustomEventArgs.SpecimenOrderReturnEventArgs(specimenOrder));
            }
        }

        private void HyperLinkDeleteSpecimen_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)e.Source;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)hyperlink.Tag;
            YellowstonePathology.Business.View.SpecimenSurgicalDiagnosisView specimenSurgicalDiagnosisView = Business.View.SpecimenSurgicalDiagnosisView.Parse(specimenOrder, this.m_AccessionOrder);

            if (specimenSurgicalDiagnosisView.SurgicalDiagnosisIsOrdered == false)
            {
                if (specimenOrder.AliquotOrderCollection.Count == 0)
                {
                    this.m_AccessionOrder.SpecimenOrderCollection.Remove(specimenOrder);                    
                }
                else
                {
                    MessageBox.Show("Cannot delete the Specimen because aliquots exist.");
                }
            }
            else
            {
                MessageBox.Show("Cannot delete the specimen because a Surgical Diagnosis for this specimen exists.");
            }            
        }

        private void ListViewPanelSetOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewPanelSetOrder.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = (YellowstonePathology.Business.Test.PanelSetOrder)this.ListViewPanelSetOrder.SelectedItem;
                if(this.ShowResultPage != null) this.ShowResultPage(this, new CustomEventArgs.PanelSetOrderReturnEventArgs(panelSetOrder));
            }
        }
                
        private void ButtonPrintDataSheet_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_AccessionOrder.PanelSetOrderCollection.Count > 0)
            {
                Business.Persistence.DocumentGateway.Instance.Save();
                YellowstonePathology.Document.Result.Data.AccessionOrderDataSheetData accessionOrderDataSheetData = YellowstonePathology.Business.Gateway.XmlGateway.GetAccessionOrderDataSheetData(this.m_AccessionOrder.MasterAccessionNo);
                YellowstonePathology.Document.Result.Xps.AccessionOrderDataSheet accessionOrderDataSheet = new Document.Result.Xps.AccessionOrderDataSheet(accessionOrderDataSheetData);
                System.Printing.PrintQueue printQueue = new System.Printing.LocalPrintServer().DefaultPrintQueue;

                System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
                printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
                printDialog.PrintQueue = printQueue;
                printDialog.PrintDocument(accessionOrderDataSheet.FixedDocument.DocumentPaginator, "AccessionDataSheet");
            }
            else
            {
                MessageBox.Show("You must order something before the data sheet can be printed out.");
            }
        }
    }
}

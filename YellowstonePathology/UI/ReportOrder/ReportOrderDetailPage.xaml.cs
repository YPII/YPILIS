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
using System.Windows.Shapes;
using System.IO;

namespace YellowstonePathology.UI.ReportOrder
{
	/// <summary>
	/// Interaction logic for PanelSetOrderDetails.xaml
	/// </summary>
	public partial class ReportOrderDetailPage : Window
	{
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemUserCollection m_UserCollection;
		private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private string m_ReportDocumentPath;
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public ReportOrderDetailPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{			
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_SystemIdentity = systemIdentity;
			this.m_UserCollection = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection;
            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();            

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			this.m_ReportDocumentPath = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);

			InitializeComponent();

			DataContext = this;         
            this.Closing += new System.ComponentModel.CancelEventHandler(ReportOrderDetailPage_Closing);
		}        

        private void ReportOrderDetailPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
			this.Save(false);
        }

		private void Save(bool releaseLock)
		{
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_AccessionOrder, releaseLock);			
		}

        public string PageHeaderText
        {
            get { return string.Empty; }
        }

        public string ReportDocumentPath
        {
            get { return this.m_ReportDocumentPath; }
            set { this.m_ReportDocumentPath = value; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection UserCollection
		{
			get { return this.m_UserCollection; }
		}

		public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
		{
			get { return this.m_FacilityCollection; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            
			this.Close();
		}

        private void ButtonCopyReportDocumentPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetData(DataFormats.Text, this.TextBoxReportDocumentPath.Text);
        }

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			caseDocument.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}        

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsOKToFinal() == true)
            {
				YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToFinalize();
				if (result.Success == true)
                {                    
					this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                }
				else
				{
					MessageBox.Show(result.Message);
				}
			}
        }

        private bool IsOKToFinal()
        {
            bool result = true;
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);

            if (panelSet != null && panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
            {            
                result = false;
                MessageBox.Show("This report cannot be finalized here.");            
            }

            if (this.HasCaseBeenPublished() == false && panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument)
            {
                result = false;
                MessageBox.Show("This report cannot be finalized until it has been published.");
            }            

            return result;
        }

		private void HyperLinkPublish_Click(object sender, RoutedEventArgs e)
		{
            if (this.DoesXPSDocumentExist() == true)
            {
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Normal);
                caseDocument.Render();
                caseDocument.Publish();
                MessageBox.Show("The case was successfully published.");
            }
            else
            {
                MessageBox.Show("Cannot publish this case until the XPS document is present.");
            }
		}        

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnfinalize_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnfinalize();
			if (result.Success == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
			else
			{
				MessageBox.Show(result.Message);
			}
		}

        private bool DoesXPSDocumentExist()
        {
            bool result = true;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string xpsFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
            if (System.IO.File.Exists(xpsFileName) == false)
            {                
                result = false;
            }
            return result;
        }

        private bool HasCaseBeenPublished()
        {
            bool result = true;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string tifFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
            if (System.IO.File.Exists(tifFileName) == false)
            {
                result = false;
            }
            return result;
        }

        private void ButtonShowSpecimenDialog_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.OrderedOnId != null)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);

                YellowstonePathology.UI.Login.SpecimenOrderDetailsPage specimenOrderDetailsPage = new YellowstonePathology.UI.Login.SpecimenOrderDetailsPage(this.m_AccessionOrder, specimenOrder);
                specimenOrderDetailsPage.Next += new Login.SpecimenOrderDetailsPage.NextEventHandler(SpecimenOrderDetailsPage_Next);
                specimenOrderDetailsPage.Back += new Login.SpecimenOrderDetailsPage.BackEventHandler(SpecimenOrderDetailsPage_Next);
                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.PageNavigator.Navigate(specimenOrderDetailsPage);
                this.m_LoginPageWindow.ShowDialog();
            }
        }

        private void ButtonShowSelectSpecimenDialog_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(panelSet, orderTarget, false);

            if (panelSet.HasNoOrderTarget == false)
            {
                Login.Receiving.SpecimenSelectionPage specimenSelectionPage = new Login.Receiving.SpecimenSelectionPage(this.m_AccessionOrder, testOrderInfo);
                specimenSelectionPage.Back += new Login.Receiving.SpecimenSelectionPage.BackEventHandler(SpecimenSelectionPage_Back);
                specimenSelectionPage.TargetSelected += new Login.Receiving.SpecimenSelectionPage.TargetSelectedEventHandler(OrderTargetSelectionPage_TargetSelected);

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.PageNavigator.Navigate(specimenSelectionPage);
                this.m_LoginPageWindow.ShowDialog();
            }
        }

        private void OrderTargetSelectionPage_TargetSelected(object sender, CustomEventArgs.TestOrderInfoEventArgs e)
        {
            this.m_PanelSetOrder.OrderedOnId = e.TestOrderInfo.OrderTarget.GetId();
            this.m_PanelSetOrder.OrderedOn = e.TestOrderInfo.OrderTarget.GetOrderedOnType();
            this.m_LoginPageWindow.Close();
        }

        private void SpecimenSelectionPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void SpecimenOrderDetailsPage_Next(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }
    }
}

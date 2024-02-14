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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public partial class ProviderDistributionPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText = "Provider and Distribution Page";
        private Brush m_ProviderStatusColor;

        private YellowstonePathology.Business.Audit.Model.AuditCollection m_ClientPhysicianNotSetAuditCollection;

        private Visibility m_NextButtonVisibility;
        private Visibility m_CloseButtonVisibility;
        private Visibility m_BackButtonVisibility;

        private YellowstonePathology.Business.User.SystemUserCollection m_SystemUserCollection;

        public ProviderDistributionPage(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            Visibility nextButtonVisibility,
            Visibility closeButtonVisibility,
            Visibility backButtonVisibility)
        {
            this.m_PageNavigator = pageNavigator;

            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            this.m_NextButtonVisibility = nextButtonVisibility;
            this.m_CloseButtonVisibility = closeButtonVisibility;
            this.m_BackButtonVisibility = backButtonVisibility;

            this.m_SystemUserCollection = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection;

            this.m_ClientPhysicianNotSetAuditCollection = new Business.Audit.Model.AuditCollection();
            this.m_ClientPhysicianNotSetAuditCollection.Add(new YellowstonePathology.Business.Audit.Model.ClientNotSetAudit(this.m_AccessionOrder));
            this.m_ClientPhysicianNotSetAuditCollection.Add(new YellowstonePathology.Business.Audit.Model.PhysicianNotSetAudit(this.m_AccessionOrder));
            this.m_ClientPhysicianNotSetAuditCollection.Add(new YellowstonePathology.Business.Audit.Model.DistributeToPatientAudit(this.m_AccessionOrder));

            InitializeComponent();
            this.ExpanderOptions.IsExpanded = true;

            DataContext = this;

            this.Loaded += new RoutedEventHandler(ProviderDetailPage_Loaded);
            Close += ProviderDistributionPage_Close;
        }

        public YellowstonePathology.Business.User.SystemUserCollection SystemUserCollection
        {
            get { return this.m_SystemUserCollection; }
        }

        private void ProviderDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
            {
                this.GridLeftNav.IsEnabled = false;
            }

            this.m_ClientPhysicianNotSetAuditCollection.Run();
            this.SetProviderStatusColor();
            this.ButtonProviderLookup.Focus();
        }

        private void ProviderDistributionPage_Close(object sender, EventArgs e)
        {
            //this.m_Closing = true;
        }

        public Visibility NextButtonVisibility
        {
            get { return this.m_NextButtonVisibility; }
        }

        public Visibility CloseButtonVisibility
        {
            get { return this.m_CloseButtonVisibility; }
        }

        public Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        public void SetProviderStatusColor()
        {
            if (this.m_ClientPhysicianNotSetAuditCollection.ActionRequired == true)
            {
                this.ProviderStatusColor = Brushes.PaleVioletRed;
            }
            else
            {
                this.ProviderStatusColor = Brushes.LightGreen;
            }
        }

        public Brush ProviderStatusColor
        {
            get { return this.m_ProviderStatusColor; }
            set
            {
                this.m_ProviderStatusColor = value;
                this.NotifyPropertyChanged("ProviderStatusColor");
            }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
            set
            {
                if (this.m_PanelSetOrder != value)
                {
                    this.m_PanelSetOrder = value;
                    this.NotifyPropertyChanged("PanelSetOrder");
                }
            }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        private void HyperLinkAddCopyTo_Click(object sender, RoutedEventArgs e)
        {
            PhysicianClientSearchPage physicianClientSearchPage = new PhysicianClientSearchPage(this.m_AccessionOrder, this.m_AccessionOrder.ClientId, true);
            physicianClientSearchPage.Back += new PhysicianClientSearchPage.BackEventHandler(CopyTo_PhysicianClientSearchPage_Back);
            physicianClientSearchPage.Next += new PhysicianClientSearchPage.NextEventHandler(CopyTo_PhysicianClientSearchPage_Next);
            this.m_PageNavigator.Navigate(physicianClientSearchPage);
        }

        private void CopyTo_PhysicianClientSearchPage_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void CopyTo_PhysicianClientSearchPage_Next(object sender, CustomEventArgs.PhysicianClientDistributionReturnEventArgs e)
        {            
            this.m_AccessionOrder.SetDistribution(e.PhysicianClientDistribution);
            this.m_PageNavigator.Navigate(this);
        }

        private void SetPhysicianClient(Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution)
        {
            if (physicianClientDistribution != null)
            {
                this.m_AccessionOrder.SetPhysicianClient(physicianClientDistribution);
                this.m_AccessionOrder.UpdateColorCode();

                if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder surgicalPanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                    this.m_AccessionOrder.UpdateCaseAssignment(surgicalPanelSetOrder);
                }
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientPhysicianNotSetAuditCollection.Run();

            YellowstonePathology.Business.Audit.Model.DistributionNotSetAudit distributionNotSetAudit = new Business.Audit.Model.DistributionNotSetAudit(this.m_PanelSetOrder);
            distributionNotSetAudit.Run();

            if (this.m_ClientPhysicianNotSetAuditCollection.ActionRequired == true)
            {
                MessageBox.Show(this.m_ClientPhysicianNotSetAuditCollection.Message, "Continue?", MessageBoxButton.OK);
            }
            else if (distributionNotSetAudit.ActionRequired == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(distributionNotSetAudit.Message.ToString() + " Are you sure you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (this.Close != null) this.Close(this.Close, new EventArgs());
                }
            }
            else
            {
                if (this.Close != null) this.Close(this.Close, new EventArgs());
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientPhysicianNotSetAuditCollection.Run();
            if (this.m_ClientPhysicianNotSetAuditCollection.ActionRequired == true)
            {
                MessageBox.Show("You cannot continue because the provider has not been set.", "Continue?", MessageBoxButton.OK);
            }
            else
            {
                if (this.Back != null) this.Back(this.Back, new EventArgs());
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientPhysicianNotSetAuditCollection.Run();
            if (this.m_ClientPhysicianNotSetAuditCollection.ActionRequired == true)
            {
                MessageBox.Show("You cannot continue because the provider has not been set.", "Continue?", MessageBoxButton.OK);
            }
            else
            {
                if (this.Next != null)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    this.Next(this.Next, new EventArgs());
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void HyperLinkDeleteDistribution_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTestOrderReportDistribution.SelectedItem != null)
            {
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution)this.ListViewTestOrderReportDistribution.SelectedItem;
                if (testOrderReportDistribution.Distributed == true)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("This item has been distributed. Are you sure you want to delete it.", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        this.m_PanelSetOrder.TestOrderReportDistributionCollection.Remove(testOrderReportDistribution);
                    }
                }
                else
                {
                    this.m_PanelSetOrder.TestOrderReportDistributionCollection.Remove(testOrderReportDistribution);
                }
            }
            else
            {
                MessageBox.Show("You must select an item to delete first.");
            }
        }

        private void HyperLinkShowProviderLookup_Click(object sender, RoutedEventArgs e)
        {
            PhysicianClientSearchPage physicianClientSearchPage = new PhysicianClientSearchPage(this.m_AccessionOrder, this.m_AccessionOrder.ClientId, true);
            physicianClientSearchPage.Back += new PhysicianClientSearchPage.BackEventHandler(PhysicianClientSearchPage_Back);
            physicianClientSearchPage.Next += new PhysicianClientSearchPage.NextEventHandler(PhysicianClientSearchPage_Next);
            this.m_PageNavigator.Navigate(physicianClientSearchPage);

        }

        private void PhysicianClientSearchPage_Next(object sender, CustomEventArgs.PhysicianClientDistributionReturnEventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
            this.SetPhysicianClient(e.PhysicianClientDistribution);

            YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
            multiTestDistributionHandler.Set();

            this.SetDistribution();
        }

        private void PhysicianClientSearchPage_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void HyperLinkSetDistribution_Click(object sender, RoutedEventArgs e)
        {
            this.SetDistribution();
        }

        private void HyperLinkPhysicianNotFound_Click(object sender, RoutedEventArgs e)
        {
            Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = new Business.Client.Model.PhysicianClientDistributionListItem();
            physicianClientDistribution.PhysicianId = 2371;
            physicianClientDistribution.PhysicianName = "*** Physician Not Found *** *** Physician Not Found ***";
            physicianClientDistribution.ClientId = 1007;
            physicianClientDistribution.ClientName = "** Client Not Found **";

            this.SetPhysicianClient(physicianClientDistribution);
            this.SetDistribution();
        }

        private void SetDistribution()
        {
            this.m_AccessionOrder.SetDistribution();
            this.NotifyPropertyChanged("");            
        }        

        private void HyperLinkScheduleDistributionImmediate_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Distribute == true)
            {
                if (this.ListViewTestOrderReportDistribution.SelectedItem != null)
                {
                    this.m_PanelSetOrder.SchedulePublish(DateTime.Now);
                    foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in this.ListViewTestOrderReportDistribution.SelectedItems)
                    {
                        testOrderReportDistribution.ScheduleForDistribution(DateTime.Now);
                    }
                }
                else
                {
                    MessageBox.Show("You must select one or more items to schedule.");
                }
            }
            else
            {
                MessageBox.Show("Can't schedule a distribution when the case is not marked to distribute.");
            }
        }

        private void HyperLinkScheduleDistribution15Minute_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Distribute == true)
            {
                if (this.ListViewTestOrderReportDistribution.SelectedItem != null)
                {
                    this.m_PanelSetOrder.SchedulePublish(DateTime.Now.AddMinutes(15));
                    foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in this.ListViewTestOrderReportDistribution.SelectedItems)
                    {
                        testOrderReportDistribution.ScheduleForDistribution(DateTime.Now.AddMinutes(15));
                    }
                }
                else
                {
                    MessageBox.Show("You must select one or more items to schedule.");
                }
            }
            else
            {
                MessageBox.Show("Can't schedule a distribution when the case is not marked to distribute.");
            }
        }

        private void HyperLinkUnScheduleDistribution_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTestOrderReportDistribution.SelectedItem != null)
            {
                this.m_PanelSetOrder.UnSchedulePublish();
                foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in this.ListViewTestOrderReportDistribution.SelectedItems)
                {
                    testOrderReportDistribution.UnScheduleForDistribution();
                }
            }
            else
            {
                MessageBox.Show("You must select one or more items to unschedule.");
            }
        }

        private void HyperLinkHoldDistribution_Click(object sender, RoutedEventArgs e)
        {
            this.m_PanelSetOrder.HoldDistribution = true;
        }

        private void HyperLinkReleaseDistributionHold_Click(object sender, RoutedEventArgs e)
        {
            this.m_PanelSetOrder.HoldDistribution = false;
        }

        private void HyperLinkPublish_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == true)
            {
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = Business.Document.DocumentFactory.GetDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Normal);
                YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
                YellowstonePathology.Business.Rules.MethodResult methodResult = caseDocument.DeleteCaseFiles(orderIdParser);

                if (methodResult.Success == true)
                {
                    caseDocument.Render();
                    caseDocument.Publish();
                    MessageBox.Show("The document has been published.");
                }
                else
                {
                    MessageBox.Show(methodResult.Message);
                }
            }
            else
            {
                MessageBox.Show("You cannot publish this case until it's final.");
            }
        }

        private void HyperLinkOpenDocumentFolder_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string folderPath = Business.Document.CaseDocumentPath.GetPath(orderIdParser);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
            p.StartInfo = info;
            p.Start();
        }

        private void HyperLinkAddMTDOHDistribution_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.PanelSetId == 13)
            {
                string tordId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                this.m_PanelSetOrder.TestOrderReportDistributionCollection.AddNext(tordId, tordId, this.m_PanelSetOrder.ReportNo, 3946, "Cari Williams, RN",
                    1542, "Oncology Patient Navigator", YellowstonePathology.Business.Client.Model.WebServicePhysicianClientDistribution.WEBSERVICE);

                if (string.IsNullOrEmpty(this.m_AccessionOrder.PAddress1) == false)
                {
                    string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    this.m_PanelSetOrder.TestOrderReportDistributionCollection.AddNext(testOrderReportDistributionId, testOrderReportDistributionId, this.m_PanelSetOrder.ReportNo, 2678, "Reportable Disease Physician, Infection Control",
                        1337, "Montana Department Of Health", YellowstonePathology.Business.Client.Model.MTDOHPhysicianClientDistribution.MTDOH);
                }
                else
                {
                    MessageBox.Show("You cannot add this distribution without an address.");
                }
            }
            else
            {
                MessageBox.Show("You can only add a MTDOH distribution to a surgical.");
            }
        }

        private void HyperLinkAddWYDOHDistribution_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.PanelSetId == 13)
            {
                if (string.IsNullOrEmpty(this.m_AccessionOrder.PAddress1) == false)
                {
                    string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    this.m_PanelSetOrder.TestOrderReportDistributionCollection.AddNext(testOrderReportDistributionId, testOrderReportDistributionId, this.m_PanelSetOrder.ReportNo, 2678, "Reportable Disease Physician, Infection Control",
                        1335, "Wyoming Department Of Health", YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX, "3077773419");
                }
                else
                {
                    MessageBox.Show("You cannot add this distribution without an address.");
                }
            }
            else
            {
                MessageBox.Show("You can only add a WYDOH distribution to a surgical.");
            }
        }

        private void HyperLinkAddFaxDistribution_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Distribute == true)
            {
                string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution =
                    new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution(testOrderReportDistributionId, testOrderReportDistributionId, this.m_PanelSetOrder.ReportNo, 0, null, 0, "Special Distribution",
                        YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX, null);

                TestOrderReportDistributionPage testOrderReportDistributionPage = new TestOrderReportDistributionPage(testOrderReportDistribution, this.m_PageNavigator);
                testOrderReportDistributionPage.Next += new TestOrderReportDistributionPage.NextEventHandler(AddDistribution_TestOrderReportDistributionPage_Next);
                testOrderReportDistributionPage.Back += new TestOrderReportDistributionPage.BackEventHandler(AddDistribution_TestOrderReportDistributionPage_Back);
                this.m_PageNavigator.Navigate(testOrderReportDistributionPage);

            }
        }

        private void HyperLinkAddPrintDistribution_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Distribute == true)
            {
                string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution =
                    new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution(testOrderReportDistributionId, testOrderReportDistributionId, this.m_PanelSetOrder.ReportNo, 0, null, 0, "Special Distribution",
                        YellowstonePathology.Business.Client.Model.PrintPhysicianClientDistribution.PRINT, null);

                TestOrderReportDistributionPage testOrderReportDistributionPage = new TestOrderReportDistributionPage(testOrderReportDistribution, this.m_PageNavigator);
                testOrderReportDistributionPage.Next += new TestOrderReportDistributionPage.NextEventHandler(AddDistribution_TestOrderReportDistributionPage_Next);
                testOrderReportDistributionPage.Back += new TestOrderReportDistributionPage.BackEventHandler(AddDistribution_TestOrderReportDistributionPage_Back);
                this.m_PageNavigator.Navigate(testOrderReportDistributionPage);
            }
        }

        private void AddDistribution_TestOrderReportDistributionPage_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void AddDistribution_TestOrderReportDistributionPage_Next(object sender, CustomEventArgs.TestOrderReportDistributionReturnEventArgs e)
        {
            this.m_PanelSetOrder.TestOrderReportDistributionCollection.Add(e.TestOrderReportDistribution);
            this.m_PageNavigator.Navigate(this);
        }

        private void TestOrderReportDistributionPage_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void TestOrderReportDistributionPage_Next(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void CheckBoxDistribute_Unchecked(object sender, RoutedEventArgs e)
        {
            /*
            if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.Count > 0)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistributedItems() == false)
                {
                    MessageBox.Show("The distribution for this case will be removed.");
                    this.m_PanelSetOrder.TestOrderReportDistributionCollection.Clear();
                }
            }
            */
        }

        private void HyperLinkEditDistribution_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTestOrderReportDistribution.SelectedItem != null)
            {
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution)this.ListViewTestOrderReportDistribution.SelectedItem;
                TestOrderReportDistributionPage testOrderReportDistributionPage = new TestOrderReportDistributionPage(testOrderReportDistribution, this.m_PageNavigator);
                testOrderReportDistributionPage.Next += new TestOrderReportDistributionPage.NextEventHandler(TestOrderReportDistributionPage_Next);
                testOrderReportDistributionPage.Back += new TestOrderReportDistributionPage.BackEventHandler(TestOrderReportDistributionPage_Back);
                this.m_PageNavigator.Navigate(testOrderReportDistributionPage);

            }
            else
            {
                MessageBox.Show("You must select a distribution to edit first.");
            }
        }

        private void HyperLinkAddComplete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTestOrderReportDistribution.SelectedItem != null)
            {
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution)this.ListViewTestOrderReportDistribution.SelectedItem;
                this.m_PanelSetOrder.Published = true;
                this.m_PanelSetOrder.ScheduledPublishTime = null;
                testOrderReportDistribution.Distributed = true;
                testOrderReportDistribution.TimeOfLastDistribution = DateTime.Now;
                testOrderReportDistribution.ScheduledDistributionTime = null;
            }
        }

        private void HyperLinkAddOPNDistribution_Click(object sender, RoutedEventArgs e)
        {
            string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            this.m_PanelSetOrder.TestOrderReportDistributionCollection.AddNext(testOrderReportDistributionId, testOrderReportDistributionId, this.m_PanelSetOrder.ReportNo, 3946, "Cari Williams, RN",
                1542, "Oncology Patient Navigator", YellowstonePathology.Business.Client.Model.WebServicePhysicianClientDistribution.WEBSERVICE);
        }

        private void MenuItemConvertToFax_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTestOrderReportDistribution.SelectedItem != null)
            {
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution)this.ListViewTestOrderReportDistribution.SelectedItem;
                if (string.IsNullOrEmpty(testOrderReportDistribution.FaxNumber) == false)
                {
                    testOrderReportDistribution.DistributionType = "Fax";
                }
                else
                {
                    MessageBox.Show("This distribution cannot be converted to a fax because there is no fax number.");
                }
            }
        }

        private void HyperLinkShowHl7Result_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.HL7View.IResultView resultView = Business.HL7View.ResultViewFactory.GetResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, this.m_AccessionOrder.ClientId, false, false);
            if (resultView != null)
            {
                System.Xml.Linq.XElement document = resultView.GetDocument();
                ReportDistribution.Hl7ResultDialog hl7ResultDialog = new ReportDistribution.Hl7ResultDialog(document);
                hl7ResultDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("The client does not receive an HL7 result.  Choose another report to view.");
            }
        }

        private void HyperLinkSendHL7Result_Click(object sender, RoutedEventArgs e)
        {            
            YellowstonePathology.Business.HL7View.IResultView resultView = Business.HL7View.ResultViewFactory.GetResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, this.m_AccessionOrder.ClientId, false, false);
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);            
        }

        private void HyperLinkSendHL7ResultTest_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_AccessionOrder.ClientId == 587)
            {
                YellowstonePathology.Business.HL7View.NMH.NMHResultView resultViewNMH = new Business.HL7View.NMH.NMHResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, true);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultViewNMH.Send(methodResult);
            }
            else if(this.m_AccessionOrder.ClientId == 136)
            {
                Business.HL7View.Riverstone.RiverstoneResultView resultView = new Business.HL7View.Riverstone.RiverstoneResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, true);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);
            }
            else if (this.m_AccessionOrder.ClientId == 1822)
            {
                Business.HL7View.EMA.EMAResultView resultView = new Business.HL7View.EMA.EMAResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, true);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);
            }
            else if (this.m_AccessionOrder.ClientId == 1494)
            {
                Business.HL7View.DeerLodge.DeerLodgeResultView resultView = new Business.HL7View.DeerLodge.DeerLodgeResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, false, true);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);
            }
            else
            {
                YellowstonePathology.Business.HL7View.EPIC.EPICBeakerResultView resultView = new Business.HL7View.EPIC.EPICBeakerResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, false, true);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);                
            }            
        }

        private void HyperLinkSendHL7Order_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.HL7View.Orders.ORMO01 ormo01 = new Business.HL7View.Orders.ORMO01(this.m_AccessionOrder);
            System.Xml.Linq.XElement document = new System.Xml.Linq.XElement("HL7Message");
            ormo01.ToXml(document);
        }        

        private void ExpanderOptions_Expanded(object sender, RoutedEventArgs e)
        {
            ExpanderMore.IsExpanded = false;
        }

        private void ExpanderMore_Expanded(object sender, RoutedEventArgs e)
        {
            ExpanderOptions.IsExpanded = false;
        }

        private void MenuItemDeleteDistributionLog_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewTestOrderReportDistributionLog.SelectedItem != null)
            {
                Business.ReportDistribution.Model.TestOrderReportDistributionLog rdl = (Business.ReportDistribution.Model.TestOrderReportDistributionLog)this.ListViewTestOrderReportDistributionLog.SelectedItem;
                this.m_PanelSetOrder.TestOrderReportDistributionLogCollection.Remove(rdl);
                this.NotifyPropertyChanged("TestOrderReportDistributionLogCollection");
            }
        }

        private void MenuItemSendSVHUnsolicted_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewTestOrderReportDistribution.SelectedItem != null)
            {
                Business.ReportDistribution.Model.TestOrderReportDistribution rd = (Business.ReportDistribution.Model.TestOrderReportDistribution)this.ListViewTestOrderReportDistribution.SelectedItem;
                if(rd.DistributionType == "EPIC" || rd.DistributionType == "EPIC->Fax")
                {
                    YellowstonePathology.Business.HL7View.EPIC.EPICBeakerResultView resultView = new Business.HL7View.EPIC.EPICBeakerResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, true, false);
                    YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                    resultView.Send(methodResult);
                    MessageBox.Show("The result was sent unsolicted to EPIC");
                }
                else if (rd.DistributionType == "Meditech")
                {
                    throw new Exception("Meditech is not a valid distribution anymore.");
                    //YellowstonePathology.Business.HL7View.WPH.WPHResultView resultView = new Business.HL7View.WPH.WPHResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, true, false);
                    //YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                    //resultView.Send(methodResult);
                    //MessageBox.Show("The result was sent unsolicted to EPIC");
                }
                else
                {
                    MessageBox.Show("Cannot send this unsolicted because it is not an EPIC distribution.");
                }
                
            }
            else
            {
                MessageBox.Show("Please select a distribution to send.");
            }
            
        }        

        private void MenuItemConvertToNMH_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTestOrderReportDistribution.SelectedItem != null)
            {
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution)this.ListViewTestOrderReportDistribution.SelectedItem;
                testOrderReportDistribution.DistributionType = "Meditech";
            }
        }

        private void HyperLinkSendResultText_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.m_AccessionOrder.PPhoneNumberHome) == false)
            {
                JObject jsonRequest = Business.APIRequestHelper.GetSendCovidResultTextMessage(this.m_AccessionOrder.PPhoneNumberHome, this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder.PFirstName);
                Business.APIResult apiResult = Business.APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);                
            }
        }

        private void HyperLinkSendResultEmail_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PEmailAddress) == false)
            {
                JObject jsonRequest = Business.APIRequestHelper.GetSendCovidResultEmailMessage(this.m_AccessionOrder.PEmailAddress, this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder.PFirstName);
                Business.APIResult apiResult = Business.APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);
            }
        }

        private void MenuItemSendWYDOH_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewTestOrderReportDistribution.SelectedItem != null)
            {
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution)this.ListViewTestOrderReportDistribution.SelectedItem;
                if(testOrderReportDistribution.DistributionType == "WYDOH")
                {
                    string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(testOrderReportDistribution.ReportNo);
                    Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);

                    YellowstonePathology.Business.Rules.MethodResult resultV2 = new Business.Rules.MethodResult();
                    YellowstonePathology.Business.HL7View.WYDOH.WYDOHResultViewV2 wyDOHResultViewV2 = new Business.HL7View.WYDOH.WYDOHResultViewV2(testOrderReportDistribution.ReportNo, accessionOrder);
                    wyDOHResultViewV2.CanSend(resultV2);
                    wyDOHResultViewV2.Send(resultV2);
                }
            }
        }

        private void HyperLinkSendHL7ResultTestWithPDF_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.HL7View.EPIC.EPICBeakerResultView resultView = new Business.HL7View.EPIC.EPICBeakerResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, false, true);
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);
        }

        private void HyperLinkSendWDOOH_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult resultV2 = new Business.Rules.MethodResult();
            YellowstonePathology.Business.HL7View.WYDOH.WYDOHResultViewV2 wyDOHResultViewV2 = new Business.HL7View.WYDOH.WYDOHResultViewV2(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder);
            wyDOHResultViewV2.CanSend(resultV2);
            wyDOHResultViewV2.Send(resultV2);
        }

        private void HyperLinkSendSCLPDFTest_Click(object sender, RoutedEventArgs e)
        {            
            Business.HL7View.EPIC.EPICBeakerResultViewPDF view = new Business.HL7View.EPIC.EPICBeakerResultViewPDF(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, false, false);
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            view.Send(methodResult);
            MessageBox.Show("PDF sent to SCL in Test.");
        }

        private void HyperLinkSendSCLClosingResult_Click(object sender, RoutedEventArgs e)
        {
            Business.HL7View.EPIC.EPICBeakerResultView view = new Business.HL7View.EPIC.EPICBeakerResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, false, false);
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            view.Send(methodResult);
            MessageBox.Show("Closing Result sent to SCL in Test.");
        }

        private void HyperLinkSendSCLPDFTestUnsoliceted_Click(object sender, RoutedEventArgs e)
        {
            Business.HL7View.EPIC.EPICBeakerResultViewPDF view = new Business.HL7View.EPIC.EPICBeakerResultViewPDF(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, true, false);
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            view.Send(methodResult);
            MessageBox.Show("PDF sent to SCL in Test.");
        }        

        private void HyperLinkAddHRHOPNDistribution_Click(object sender, RoutedEventArgs e)
        {
            string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            this.m_PanelSetOrder.TestOrderReportDistributionCollection.AddNext(testOrderReportDistributionId, testOrderReportDistributionId, this.m_PanelSetOrder.ReportNo, 5502, "Jenille, HRH Oncology Navigator",
                1848, "HRH Oncology Patient Navigator", YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX, "4062062472");
        }

        private void MenuItemAddWebDistribution_Click(object sender, RoutedEventArgs e)
        {
            this.m_PanelSetOrder.TestOrderReportDistributionCollection.AddWebSerivceDistribution(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder.ClientName, this.m_AccessionOrder.ClientId,
                this.m_AccessionOrder.PhysicianName, this.m_AccessionOrder.PhysicianId);
        }

        private void MenuItemDistributeNow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HyperLinkSendMTDOH_Click(object sender, RoutedEventArgs e)
        {
            Business.HL7View.CDC.MTDohResultView resultView = new Business.HL7View.CDC.MTDohResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder);
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);
        }

        private void HyperLinkDistributionCheck_Click(object sender, RoutedEventArgs e)
        {
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string pdfPath = Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);

            if(System.IO.File.Exists(pdfPath) == false)
            {
                MessageBoxResult result = MessageBox.Show("The PDF for this case does not exist, would you like to send an email to Flow?", "Missing PDF", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    SendMessageToFlow(this.m_PanelSetOrder.ReportNo, $"A PDF document for ReportNo {this.m_PanelSetOrder.ReportNo} does not exist and distribution is delayed.");
                }
            }
        }

        public static void SendMessageToFlow(string subject, string body)
        {            
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("flowstaff@ypii.com", "sid.harder@ypii.com", subject, body);                        
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.110");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);            
        }
    }
}

﻿using System;
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

            this.m_SystemUserCollection = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection;

            this.m_ClientPhysicianNotSetAuditCollection = new Business.Audit.Model.AuditCollection();
            this.m_ClientPhysicianNotSetAuditCollection.Add(new YellowstonePathology.Business.Audit.Model.ClientNotSetAudit(this.m_AccessionOrder));
            this.m_ClientPhysicianNotSetAuditCollection.Add(new YellowstonePathology.Business.Audit.Model.PhysicianNotSetAudit(this.m_AccessionOrder));

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
            bool canAddDistribution = true;
            if (e.PhysicianClientDistribution.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC ||
                e.PhysicianClientDistribution.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX)
            {
                YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanAddEpicDistribution(e.PhysicianClientDistribution);
                if (methodResult.Success == false)
                {
                    canAddDistribution = false;
                    MessageBox.Show(methodResult.Message);
                }
            }
            if (canAddDistribution == true)
            {
                this.ResetDistributionWhenIncompatible(e.PhysicianClientDistribution);
                e.PhysicianClientDistribution.SetDistribution(this.m_AccessionOrder);
            }

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
                MessageBox.Show("The provider for this case is not set. Are you sure you want to continue?", "Continue?", MessageBoxButton.OK);
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

            YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
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
            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.ClientId);
            YellowstonePathology.Business.Audit.Model.CanSetDistributionAudit canSetDistributionAudit = new Business.Audit.Model.CanSetDistributionAudit(this.m_AccessionOrder, physicianClientDistributionCollection);
            canSetDistributionAudit.Run();
            if (canSetDistributionAudit.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                physicianClientDistributionCollection.SetDistribution(this.m_PanelSetOrder, this.m_AccessionOrder);
                this.NotifyPropertyChanged("");
            }
            else
            {
                MessageBox.Show(canSetDistributionAudit.Message.ToString());
            }
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
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Normal);
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
            string folderPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
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
                if (string.IsNullOrEmpty(this.m_AccessionOrder.PAddress1) == false)
                {
                    string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    this.m_PanelSetOrder.TestOrderReportDistributionCollection.AddNext(testOrderReportDistributionId, testOrderReportDistributionId, this.m_PanelSetOrder.ReportNo, 2678, "Reportable Disease Physician, Infection Control",
                        1337, "Montana Department Of Health", YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH);
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
                        1335, "Wyoming Department Of Health", YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH);
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
                        YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX, null);

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
                        YellowstonePathology.Business.ReportDistribution.Model.DistributionType.PRINT, null);

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
                1542, "Oncology Patient Navigator", YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE);
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
            YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, this.m_AccessionOrder.ClientId, false);
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
            YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, this.m_AccessionOrder.ClientId, false);
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);
        }

        private void HyperLinkSendHL7ResultTest_Click(object sender, RoutedEventArgs e)
        {
                YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, this.m_AccessionOrder.ClientId, true);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);
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

        private YellowstonePathology.Business.Rules.MethodResult CanAddEpicDistribution(YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Success = true;
            if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC) == false)
            {
                List<string> clientGroupIds = new List<string>();
                clientGroupIds.Add("1");
                clientGroupIds.Add("2");

                YellowstonePathology.Business.Client.Model.ClientGroupClientCollection stVincentAndHRHGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(clientGroupIds);
                if (stVincentAndHRHGroup.ClientIdExists(this.m_AccessionOrder.ClientId) == true)
                {
                    if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == true || string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
                    {
                        result.Success = false;
                        result.Message = "Unable to add an EPIC distribution as the MRN or Account No is missing.";
                    }
                    else
                    {
                        YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                        YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);
                        if (panelSet.ResultDocumentSource != YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                        {
                            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(physicianClientDistribution.ClientId);
                            physicianClientDistribution.DistributionType = client.AlternateDistributionType;
                            result.Success = true;
                        }
                    }
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Only one EPIC distribution is needed.";
            }
            return result;
        }

        private void ResetDistributionWhenIncompatible(YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution)
        {
            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.ClientId);
            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem primaryDistribution = physicianClientDistributionCollection.GetPrimaryDistribution(this.m_AccessionOrder);
            YellowstonePathology.Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection incompatibleDistributionTypeCollection = new Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection();
            bool distributionsAreIncompatible = incompatibleDistributionTypeCollection.TypesAreIncompatible(primaryDistribution.DistributionType, physicianClientDistribution.DistributionType);
            if (distributionsAreIncompatible == true)
            {
                string distributionTypeSelected = physicianClientDistribution.DistributionType;
                YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(physicianClientDistribution.ClientId);
                physicianClientDistribution.DistributionType = client.AlternateDistributionType;
            }
        }
    }
}

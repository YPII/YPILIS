using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PantherOrdersDialog.xaml
    /// </summary>
    public partial class PantherOrdersDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private YellowstonePathology.Business.Test.PantherOrderList m_PantherHPVOrderList;
        private YellowstonePathology.Business.Test.PantherOrderList m_PantherNGCTOrderList;
        private YellowstonePathology.Business.Test.PantherOrderList m_PantherHPV1618OrderList;
        private YellowstonePathology.Business.Test.PantherOrderList m_PantherTrichomonasOrderList;
        private YellowstonePathology.Business.Test.PantherOrderList m_PantherWHPOrderList;
        private YellowstonePathology.Business.Test.PantherOrderList m_PantherPreviouslyRunList;
        private YellowstonePathology.Business.Test.PantherAliquotList m_PantherAliquotList;

        private ObservableCollection<YellowstonePathology.Business.Client.Model.HPVStatus> m_HPVStatusCollection;
        private DateTime m_HPVDate;
        private System.ComponentModel.BackgroundWorker m_BackgroundWorker;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public PantherOrdersDialog()
        {
            this.m_PantherAliquotList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAliquoted();
            this.m_PantherHPVOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedHPV();
            this.m_PantherNGCTOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedNGCT();
            this.m_PantherHPV1618OrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedHPV1618();
            this.m_PantherTrichomonasOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedTrichomonas();
            this.m_PantherPreviouslyRunList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersPreviouslyRun();
            this.m_PantherWHPOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalWHP();

            this.m_HPVStatusCollection = new ObservableCollection<Business.Client.Model.HPVStatus>();
            this.m_HPVDate = DateTime.Today;

            InitializeComponent();
            this.DataContext = this;

            this.Closing += PantherOrdersDialog_Closing;           
        }

        private void PantherOrdersDialog_Closing(object sender, CancelEventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherHPVOrderList
        {
            get { return this.m_PantherHPVOrderList; }
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherPreviouslyRunList
        {
            get { return this.m_PantherPreviouslyRunList; }
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherNGCTOrderList
        {
            get { return this.m_PantherNGCTOrderList; }
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherHPV1618OrderList
        {
            get { return this.m_PantherHPV1618OrderList; }
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherTrichomonasOrderList
        {
            get { return this.m_PantherTrichomonasOrderList; }
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherWHPOrderList
        {
            get { return this.m_PantherWHPOrderList; }
        }

        public YellowstonePathology.Business.Test.PantherAliquotList PantherAliquotList
        {
            get { return this.m_PantherAliquotList; }
        }

        public ObservableCollection<Business.Client.Model.HPVStatus> HPVStatusCollection
        {
            get { return this.m_HPVStatusCollection; }
        }

        public DateTime HPVDate
        {
            get { return this.m_HPVDate; }
            set
            {
                this.m_HPVDate = value;
                this.NotifyPropertyChanged("HPVDate");
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            PantherAliquotReport pantherAliquotReport = new PantherAliquotReport(this.m_PantherAliquotList);
            pantherAliquotReport.Print();
        }

        private void ButtonPrintPreviouslyRun_Click(object sender, RoutedEventArgs e)
        {
            PantherPreviouslyRunReport pantherPreviouslyRunReport = new PantherPreviouslyRunReport(this.m_PantherPreviouslyRunList);
            pantherPreviouslyRunReport.Print();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewPantherHPVOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = (YellowstonePathology.Business.Test.PantherOrderListItem)this.ListViewPantherHPVOrders.SelectedItem;

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(pantherOrderListItem.MasterAccessionNo, this);
                this.m_LoginPageWindow.Show();

                YellowstonePathology.UI.Test.HPVResultPath hpvResultPath = new Test.HPVResultPath(pantherOrderListItem.ReportNo, accessionOrder, this.m_LoginPageWindow.PageNavigator, this);
                hpvResultPath.Finish += HpvResultPath_Finish;
                hpvResultPath.Start();
            }
        }

        private void HpvResultPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void NGCTResultPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void HPV1618ResultPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void WHPResultPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ComboBoxListType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsLoaded == true)
            {
                switch (this.ComboBoxListType.SelectedIndex)
                {
                    case 0:
                        this.m_PantherHPVOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedHPV();
                        break;
                    case 1:
                        this.m_PantherHPVOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalHPV();
                        break;
                    case 2:
                        this.m_PantherHPVOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersFinalHPV();
                        break;
                }
                this.NotifyPropertyChanged("PantherHPVOrderList");
            }
        }        

        private void ResendPantherOrder(YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem, YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay)
        {
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(pantherOrderListItem.MasterAccessionNo, this);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(accessionOrder, this);
            if (accessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetThinPrep();
                if (specimenOrder.AliquotOrderCollection.HasPantherAliquot() == true)
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetPantherAliquot();
                    YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pantherOrderListItem.ReportNo);
                    YellowstonePathology.Business.HL7View.Panther.PantherOrder pantherOrder = new Business.HL7View.Panther.PantherOrder(pantherAssay, specimenOrder, aliquotOrder, accessionOrder, panelSetOrder, YellowstonePathology.Business.HL7View.Panther.PantherActionCode.NewSample);
                    pantherOrder.Send();
                    panelSetOrder.InstrumentOrderDate = DateTime.Now;                    
                }
                else
                {
                    MessageBox.Show("No Panther aliquot found.");
                }
            }
            else
            {
                MessageBox.Show("No Thin Prep Fluid Specimen Found.");
            }
        }

        private void ButtonResendHPVPantherOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPantherHPVOrders.SelectedItem != null)
            {
                foreach(YellowstonePathology.Business.Test.PantherOrderListItem item in this.ListViewPantherHPVOrders.SelectedItems)
                {                    
                    YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay = new Business.HL7View.Panther.PantherAssayHPV();
                    this.ResendPantherOrder(item, pantherAssay);
                }
                MessageBox.Show("The selected order(s) have been sent.");
            }
        }

        private void ButtonResendNGCTPantherOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPantherNGCTOrders.SelectedItem != null)
            {
                foreach (YellowstonePathology.Business.Test.PantherOrderListItem item in this.ListViewPantherNGCTOrders.SelectedItems)
                {                    
                    YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay = new Business.HL7View.Panther.PantherAssayNGCT();
                    this.ResendPantherOrder(item, pantherAssay);
                }
                MessageBox.Show("The selected order(s) have been sent.");
            }
        }

        private void ButtonResendHPV1618PantherOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPantherHPV1618Orders.SelectedItem != null)
            {
                foreach (YellowstonePathology.Business.Test.PantherOrderListItem item in this.ListViewPantherHPV1618Orders.SelectedItems)
                {                    
                    YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay = new Business.HL7View.Panther.PantherAssayHPV1618();
                    this.ResendPantherOrder(item, pantherAssay);
                }
                MessageBox.Show("The selected order(s) have been sent.");
            }
        }

        private void ContextMenuValidate_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewPantherAliquots.SelectedItem != null)
            {                
                YellowstonePathology.Business.Test.PantherAliquotListItem pantherAliquotListItem = (YellowstonePathology.Business.Test.PantherAliquotListItem)this.ListViewPantherAliquots.SelectedItem;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(pantherAliquotListItem.MasterAccessionNo, this);

                if (accessionOrder.SpecimenOrderCollection.HasPantherAliquot() == true)
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetPantherAliquot();
                    aliquotOrder.Validated = true;
                    aliquotOrder.ValidationDate = DateTime.Now;
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, this);
                }

                this.m_PantherAliquotList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAliquoted();
                this.NotifyPropertyChanged("PantherAliquotList");
            }
        }

        private void ButtonShowNGCTResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPantherNGCTOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = (YellowstonePathology.Business.Test.PantherOrderListItem)this.ListViewPantherNGCTOrders.SelectedItem;

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(pantherOrderListItem.MasterAccessionNo, this.m_LoginPageWindow);
                this.m_LoginPageWindow.Show();

                YellowstonePathology.UI.Test.NGCTResultPath ngctResultPath = new Test.NGCTResultPath(pantherOrderListItem.ReportNo, accessionOrder, this.m_LoginPageWindow.PageNavigator, this.m_LoginPageWindow);
                ngctResultPath.Finish += NGCTResultPath_Finish;
                ngctResultPath.Start();
            }
        }

        private void ComboBoxListTypeNGCT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded == true)
            {
                switch (this.ComboBoxListTypeNGCT.SelectedIndex)
                {
                    case 0:
                        this.m_PantherNGCTOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedNGCT();
                        break;
                    case 1:
                        this.m_PantherNGCTOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalNGCT();
                        break;
                    case 2:
                        this.m_PantherNGCTOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersFinalNGCT();
                        break;
                }
                this.NotifyPropertyChanged("PantherNGCTOrderList");
            }
        }

        private void ComboBoxListTypeHPV1618_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded == true)
            {
                switch (this.ComboBoxListTypeHPV1618.SelectedIndex)
                {
                    case 0:
                        this.m_PantherHPV1618OrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedHPV1618();
                        break;
                    case 1:
                        this.m_PantherHPV1618OrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalHPV1618();
                        break;
                    case 2:
                        this.m_PantherHPV1618OrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersFinalHPV1618();
                        break;
                }
                this.NotifyPropertyChanged("PantherHPV1618OrderList");
            }
        }

        private void ButtonShowHPV1618Result_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPantherHPV1618Orders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = (YellowstonePathology.Business.Test.PantherOrderListItem)this.ListViewPantherHPV1618Orders.SelectedItem;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(pantherOrderListItem.MasterAccessionNo, this);

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.Show();

                Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pantherOrderListItem.ReportNo);
                if(panelSetOrder.PanelSetId == 62)
                {
                    YellowstonePathology.UI.Test.HPV1618ResultPath hpv1618ResultPath = new Test.HPV1618ResultPath(pantherOrderListItem.ReportNo, accessionOrder, this.m_LoginPageWindow.PageNavigator, this);
                    hpv1618ResultPath.Finish += HPV1618ResultPath_Finish;
                    hpv1618ResultPath.Start();
                } else if(panelSetOrder.PanelSetId == 269)
                {
                    YellowstonePathology.UI.Test.HPV1618SolidTumorResultPath hpv1618SolidTumorPath = new Test.HPV1618SolidTumorResultPath(pantherOrderListItem.ReportNo, accessionOrder, this.m_LoginPageWindow.PageNavigator, this);
                    hpv1618SolidTumorPath.Finish += Hpv1618SolidTumorPath_Finish;
                    hpv1618SolidTumorPath.Start();
                }                
            }
        }

        private void Hpv1618SolidTumorPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ButtonShowWHPResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewWHPOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem = (YellowstonePathology.Business.Test.PantherOrderListItem)this.ListViewWHPOrders.SelectedItem;

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(pantherOrderListItem.MasterAccessionNo, this.m_LoginPageWindow);
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(accessionOrder.ClientOrderId, this.m_LoginPageWindow);
                this.m_LoginPageWindow.Show();

                Login.WomensHealthProfilePath womensHealthProfilePath = new Login.WomensHealthProfilePath(accessionOrder, clientOrder, this.m_LoginPageWindow.PageNavigator, this, Visibility.Hidden);
                womensHealthProfilePath.Finish += WHPResultPath_Finish;
                womensHealthProfilePath.Start();
            }
        }

        private void ButtonFinalizeWHP_Click(object sender, RoutedEventArgs e)
        {
            if(this.ComboBoxListTypeWHP.SelectedIndex == 0)
            {
                foreach(YellowstonePathology.Business.Test.PantherOrderListItem pantherOrderListItem in this.ListViewWHPOrders.SelectedItems)
                {
                    YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(pantherOrderListItem.MasterAccessionNo, this);
                    this.FinalWHPCase(accessionOrder, pantherOrderListItem.ReportNo);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, this);
                }

                this.m_PantherWHPOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalWHP();
                this.NotifyPropertyChanged("PantherWHPOrderList");
            }
            else
            {
                MessageBox.Show("Select WHP cases not final", "Already Final");
            }
        }

        private void ButtonSelectAllWHP_Click(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxListTypeWHP.SelectedIndex == 0)
            {
                this.ListViewWHPOrders.SelectAll();
            }
            else
            {
                MessageBox.Show("Select WHP cases not final", "Already Final");
            }
        }

        private void ComboBoxListTypeWHP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded == true)
            {
                switch (this.ComboBoxListTypeWHP.SelectedIndex)
                {
                    case 0:
                        this.m_PantherWHPOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalWHP();
                        break;
                    case 1:
                        this.m_PantherWHPOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersFinalWHP();
                        break;
                }
                this.NotifyPropertyChanged("PantherWHPOrderList");
            }
        }

        private void FinalWHPCase(Business.Test.AccessionOrder accessionOrder, string reportNo)
        {            
            YellowstonePathology.Business.Audit.Model.IsWHPAllDoneAuditCollection isWHPAllDoneAuditCollection = new Business.Audit.Model.IsWHPAllDoneAuditCollection(accessionOrder);
            isWHPAllDoneAuditCollection.Run();

            if (isWHPAllDoneAuditCollection.ActionRequired == true)
            {
                YellowstonePathology.Business.Audit.Model.ShouldWomensHealthProfileBeFinaledAudit shouldAudit = new Business.Audit.Model.ShouldWomensHealthProfileBeFinaledAudit(accessionOrder);
                shouldAudit.Run();
                if (shouldAudit.Message.ToString() == isWHPAllDoneAuditCollection.Message)
                {                    
                    YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
                    YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(accessionOrder);
                    multiTestDistributionHandler.Set();                    
                    
                    womensHealthProfileTestOrder.Finish(accessionOrder);

                    YellowstonePathology.Business.User.SystemUser user = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(5134);
                    womensHealthProfileTestOrder.FinaledById = user.UserId;
                    womensHealthProfileTestOrder.Signature = user.Signature;
                }
            }
        }

        private void ButtonShowTrichomonasResult_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonResendTrichomonasPantherOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPantherTrichomonasOrders.SelectedItem != null)
            {
                foreach (YellowstonePathology.Business.Test.PantherOrderListItem item in this.ListViewPantherTrichomonasOrders.SelectedItems)
                {
                    YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay = new Business.HL7View.Panther.PantherAssayTrich();
                    this.ResendPantherOrder(item, pantherAssay);
                }
                MessageBox.Show("The selected order(s) have been sent.");
            }
        }

        private void ComboBoxListTypeTrichomonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded == true)
            {
                switch (this.ComboBoxListTypeTrichomonas.SelectedIndex)
                {
                    case 0:
                        this.m_PantherTrichomonasOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedTrichomonas();
                        break;
                    case 1:
                        this.m_PantherTrichomonasOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalTrichomonas();
                        break;
                    case 2:
                        this.m_PantherTrichomonasOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersFinalTrichomonas();
                        break;
                }
                this.NotifyPropertyChanged("PantherTrichomonasOrderList");
            }
        }

        private void ButtonHPVDateBack_Click(object sender, RoutedEventArgs e)
        {
            this.HPVDate = this.m_HPVDate.AddDays(-1);
        }

        private void ButtonHPVDateForward_Click(object sender, RoutedEventArgs e)
        {
            this.HPVDate = this.m_HPVDate.AddDays(1);
        }

        private void ButtonFindStandingOrderHPV_Click(object sender, RoutedEventArgs e)
        {
            this.m_HPVStatusCollection.Clear();
            this.StartHPVBackgroundWorker();
        }

        private void StartHPVBackgroundWorker()
        {
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(GetRecentCytologyOrders);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }
        private void GetRecentCytologyOrders(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            Collection<YellowstonePathology.Business.Client.Model.HPVStatus> statusCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetRecentCytologyAccessionNos(this.m_HPVDate);
            foreach(YellowstonePathology.Business.Client.Model.HPVStatus hpvStatusResult in statusCollection)
            {
                YellowstonePathology.Business.Client.Model.HPVStatus hpvStatus = new Business.Client.Model.HPVStatus(hpvStatusResult);
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(hpvStatus.MasterAccessionNo, this);
                YellowstonePathology.Business.Test.HPV.HPVTest hpvTest = new Business.Test.HPV.HPVTest();
                YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest thinPrepPapTest = new Business.Test.ThinPrepPap.ThinPrepPapTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(thinPrepPapTest.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Client.Model.StandingOrder standingOrder = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(hpvStatus.HPVStandingOrderCode);
                    if (standingOrder.IsRequired(accessionOrder) == true)
                    {
                        hpvStatus.HPVRequired = true;
                    }
                    else
                    {
                        hpvStatus.HPVRequired = false;
                    }

                    if (accessionOrder.PanelSetOrderCollection.Exists(hpvTest.PanelSetId) == true)
                    {
                        hpvStatus.HPVOrdered = true;
                    }
                    else
                    {
                        hpvStatus.HPVOrdered = false;
                    }
                }
                else
                {
                    hpvStatus.HPVOrdered = false;
                    hpvStatus.HPVRequired = false;
                }

                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                this.m_BackgroundWorker.ReportProgress(1, hpvStatus);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_HPVStatusCollection.Insert(0, (YellowstonePathology.Business.Client.Model.HPVStatus)e.UserState);
                this.NotifyPropertyChanged("HPVSatusCollection");
            }));
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
           this.m_BackgroundWorker.Dispose();
        }
    }
}

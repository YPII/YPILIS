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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for ClientOrderWorkspace.xaml
    /// </summary>
    public partial class ClientOrderWorkspace : UserControl
    {
        private YellowstonePathology.UI.DocumentWorkspace m_DocumentViewer;

        private ClientOrderUI m_ClientOrderUI;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private bool m_LoadedHasRun;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private TabItem m_Writer;
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public ClientOrderWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_LoadedHasRun = false;
            this.m_Writer = writer;

            this.m_ClientOrderUI = new ClientOrderUI(this.m_Writer);
            this.m_DocumentViewer = new DocumentWorkspace();

            this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;

            InitializeComponent();

            this.TabItemDocumentWorkspace.Content = this.m_DocumentViewer;
            this.DataContext = this.m_ClientOrderUI;

            //this.LostFocus += ClientOrderWorkspace_LostFocus;
            //this.GotFocus += ClientOrderWorkspace_GotFocus;

            this.Loaded += new RoutedEventHandler(ClientOrderWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(ClientOrderWorkspace_Unloaded);
        }

        private void ClientOrderWorkspace_GotFocus(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived -= BarcodeScanPort_ContainerScanReceived;
            this.m_BarcodeScanPort.ContainerScanReceived += BarcodeScanPort_ContainerScanReceived;
        }

        private void ClientOrderWorkspace_LostFocus(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived -= BarcodeScanPort_ContainerScanReceived;
            this.m_BarcodeScanPort.ContainerScanReceived += BarcodeScanPort_ContainerScanReceived;
        }

        private void ClientOrderWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            //this.m_BarcodeScanPort.ContainerScanReceived -= BarcodeScanPort_ContainerScanReceived;
            //this.m_BarcodeScanPort.ContainerScanReceived += BarcodeScanPort_ContainerScanReceived;

            if (this.m_LoadedHasRun == false)
            {
                this.m_MainWindowCommandButtonHandler.Save += MainWindowCommandButtonHandler_Save;
            }
            this.m_LoadedHasRun = true;
        }        

        private void BarcodeScanPort_ContainerScanReceived(Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            throw new NotImplementedException();
        }

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ClientOrderWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            //this.m_BarcodeScanPort.ContainerScanReceived -= BarcodeScanPort_ContainerScanReceived;
            this.m_LoadedHasRun = false;
            this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }

        private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
        {
            if (this.m_ClientOrderUI.AccessionOrder != null)
            {
                Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_ClientOrderUI.AccessionOrder, this.m_Writer);

                if (this.m_ClientOrderUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    this.TabControlRightSide.SelectedIndex = 1;
                }
                else
                {
                    this.TabControlRightSide.SelectedIndex = 0;
                }
            }
        }

        private void ListViewClientOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewClientOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = (YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem)this.ListViewClientOrders.SelectedItem;
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = Business.Persistence.DocumentGateway.Instance.PullClientOrder(orderBrowserListItem.ClientOrderId, this.m_Writer);
                YellowstonePathology.Business.Document.ClientOrderCaseDocument clientOrderCaseDocument = new Business.Document.ClientOrderCaseDocument(clientOrder);                
                this.m_DocumentViewer.ShowDocument(clientOrderCaseDocument);                
            }
        }

        private void ListViewClientOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewClientOrders.SelectedItem != null)
            {
                this.m_BarcodeScanPort.ContainerScanReceived -= BarcodeScanPort_ContainerScanReceived;
                YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = (YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem)this.ListViewClientOrders.SelectedItem;
                YellowstonePathology.UI.Login.Receiving.ReceiveSpecimenPathStartingWithOrder path = new Login.Receiving.ReceiveSpecimenPathStartingWithOrder(orderBrowserListItem.ClientOrderId);                
                path.Start();
            }
        }

        private void ButtonClientOrderBack_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderUI.ClientOrderDate = this.m_ClientOrderUI.ClientOrderDate.AddDays(-1);
            this.m_ClientOrderUI.GetClientOrderList();
        }

        private void ButtonClientOrderForward_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderUI.ClientOrderDate = this.m_ClientOrderUI.ClientOrderDate.AddDays(1);
            this.m_ClientOrderUI.GetClientOrderList();
        }

        private void ButtonClientOrderRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderUI.GetClientOrderList();
        }

        private void ButtonHoldList_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderUI.GetHoldList();
        }

        private void TextBoxClientOrderSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.TextBoxClientOrderSearch.Text.Length >= 1)
                {
                    Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.TextBoxClientOrderSearch.Text);
                    object textSearchObject = textSearchHandler.GetSearchObject();
                    if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
                    {
                        YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = (YellowstonePathology.Business.MasterAccessionNo)textSearchObject;
                        this.m_ClientOrderUI.GetClientOrderListByMasterAccessionNo(masterAccessionNo.Value);
                    }
                    else if (textSearchObject is YellowstonePathology.Business.PatientName)
                    {
                        YellowstonePathology.Business.PatientName patientName = (YellowstonePathology.Business.PatientName)textSearchObject;
                        this.m_ClientOrderUI.GetClientOrderListByPatientName(patientName);
                    }
                }
            }
        }

        private void MenuItemAccessionAsCOVID_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewClientOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem orderBrowserListItem = (YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItem)this.ListViewClientOrders.SelectedItem;
                Business.ClientOrder.Model.ClientOrder clientOrder = Business.Persistence.DocumentGateway.Instance.PullClientOrder(orderBrowserListItem.ClientOrderId, this);
                if(clientOrder.ClientOrderDetailCollection.Count == 1 && string.IsNullOrEmpty(clientOrder.ClientOrderDetailCollection[0].ContainerId) == false)
                {
                    clientOrder.ClientOrderDetailCollection[0].DateReceived = DateTime.Now;
                    clientOrder.ClientOrderDetailCollection[0].Received = true;
                    string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetNextMasterAccessionNo();
                    string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    Business.Test.AccessionOrder accessionOrder = new YellowstonePathology.Business.Test.AccessionOrder(masterAccessionNo, objectId);

                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(accessionOrder, this.m_Writer);
                    accessionOrder.FromClientOrder(clientOrder, Business.User.SystemIdentity.Instance.User.UserId, string.Empty);
                    accessionOrder.SpecimenOrderCollection.AccessionSpecimen(masterAccessionNo, clientOrder.ClientOrderDetailCollection);
                    Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection[0];
                    specimenOrder.Description = clientOrder.ClientOrderDetailCollection[0].Description;
                    //specimenOrder.FromClientOrderDetail(clientOrder.ClientOrderDetailCollection[0]);
                    clientOrder.Accession(accessionOrder.MasterAccessionNo);

                    Business.Test.SARSCoV2.SARSCoV2Test test = new Business.Test.SARSCoV2.SARSCoV2Test();
                    Business.Interface.IOrderTarget orderTarget = accessionOrder.SpecimenOrderCollection[0];
                    Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(test, orderTarget, true);

                    YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                    accessionOrder.TakeATrip(orderTestOrderVisitor);

                    Business.Persistence.DocumentGateway.Instance.Save();

                    string reportNo = accessionOrder.PanelSetOrderCollection[0].ReportNo;

                    this.m_BarcodeScanPort.ContainerScanReceived -= BarcodeScanPort_ContainerScanReceived;

                    this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                    UI.Login.FinalizeAccession.FinalizeAccessionPath finalizeAccessionPath =
                            new UI.Login.FinalizeAccession.FinalizeAccessionPath(reportNo, this.m_LoginPageWindow.PageNavigator, accessionOrder);
                    finalizeAccessionPath.Start();
                    finalizeAccessionPath.Return += new UI.Login.FinalizeAccession.FinalizeAccessionPath.ReturnEventHandler(FinalizeAccessionPath_Return);
                    this.m_LoginPageWindow.ShowDialog();
                }                
            }
        }

        private void FinalizeAccessionPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived -= BarcodeScanPort_ContainerScanReceived;
            this.m_BarcodeScanPort.ContainerScanReceived += BarcodeScanPort_ContainerScanReceived;
            this.m_LoginPageWindow.Close();
        }

        private void TextBoxScanSimulationSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                if(string.IsNullOrEmpty(this.m_ClientOrderUI.ScanSimulation) == false)
                {
                    this.m_ClientOrderUI.GetClientOrderListByContainerId(this.m_ClientOrderUI.ScanSimulation);
                }
            }
        }       

        private void ButtonBSDOrders_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderUI.GetBSDOrderList();
        }

        private void MenuItemReconcileBSDOrders_Click(object sender, RoutedEventArgs e)
        {
            //Business.ClientOrder.Model.OrderBrowserListItemCollection
            if(this.ListViewClientOrders.SelectedItem != null)
            {
                foreach(Business.ClientOrder.Model.OrderBrowserListItem item in this.ListViewClientOrders.SelectedItems)
                {
                    Business.ClientOrder.Model.ClientOrder clientOrder = Business.Persistence.DocumentGateway.Instance.PullClientOrder(item.ClientOrderId, this);
                    Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = Business.Gateway.ClientOrderGateway.GetClientOrdersBySvhMedicalRecord(clientOrder.SvhMedicalRecord);
                    if(clientOrderCollection.Count >= 2)
                    {
                        if(clientOrderCollection.ContainsAnAccessionedOrder() == true)
                        {
                            clientOrder.Reconciled = true;
                            Business.Persistence.DocumentGateway.Instance.Push(this);
                        }                        
                    }
                }
                MessageBox.Show("Orders have been reconciled.");
                this.m_ClientOrderUI.GetBSDOrderList();
            }
        }
    }    
}

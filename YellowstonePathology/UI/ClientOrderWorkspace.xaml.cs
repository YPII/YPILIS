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

        public ClientOrderWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_LoadedHasRun = false;
            this.m_Writer = writer;

            this.m_ClientOrderUI = new ClientOrderUI(this.m_Writer);
            this.m_DocumentViewer = new DocumentWorkspace();

            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

            InitializeComponent();

            this.TabItemDocumentWorkspace.Content = this.m_DocumentViewer;
            this.DataContext = this.m_ClientOrderUI;

            this.Loaded += new RoutedEventHandler(ClientOrderWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(ClientOrderWorkspace_Unloaded);
        }

        private void ClientOrderWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.m_LoadedHasRun == false)
            {
                this.m_MainWindowCommandButtonHandler.Save += MainWindowCommandButtonHandler_Save;
            }
            this.m_LoadedHasRun = true;
        }

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ClientOrderWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
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
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(orderBrowserListItem.ClientOrderId, this.m_Writer);
                YellowstonePathology.Business.Document.ClientOrderCaseDocument clientOrderCaseDocument = new Business.Document.ClientOrderCaseDocument(clientOrder);                
                this.m_DocumentViewer.ShowDocument(clientOrderCaseDocument);                
            }
        }

        private void ListViewClientOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewClientOrders.SelectedItem != null)
            {
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
    }
}

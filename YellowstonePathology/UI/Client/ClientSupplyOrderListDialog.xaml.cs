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
using System.ComponentModel;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for ClientSupplyOrderListDialog.xaml
    /// </summary>
    public partial class ClientSupplyOrderListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection m_ClientSupplyOrderCollection;

        public ClientSupplyOrderListDialog()
        {
            InitializeComponent();
            this.DataContext = this;
            this.ComboBoxOrderSelection.SelectedIndex = 0;
        }

        public YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection ClientSupplyOrderCollection
        {
            get { return this.m_ClientSupplyOrderCollection; }
        }

        private void ListViewClientSupplyOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewClientSupplyOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)this.ListViewClientSupplyOrders.SelectedItem;
                YellowstonePathology.UI.Client.ClientSupplyOrderDialog clientSupplyOrderDialog = new ClientSupplyOrderDialog(clientSupplyOrder.ClientSupplyOrderId);
                clientSupplyOrderDialog.ShowDialog();
                this.FillClientSupplyOrderCollection();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewClientSupplyOrders.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this supply order?", "Delete?", MessageBoxButton.YesNo);
                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)this.ListViewClientSupplyOrders.SelectedItem;
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(clientSupplyOrder, this);                    
                    this.FillClientSupplyOrderCollection();
                }
            }
            else
            {
                MessageBox.Show("You must select an item before deleting.");
            }
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewClientSupplyOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)this.ListViewClientSupplyOrders.SelectedItem;
                XElement dataElement = YellowstonePathology.Business.Gateway.XmlGateway.GetClientSupplyOrderReportData(clientSupplyOrder.ClientSupplyOrderId);
                YellowstonePathology.Business.XPSDocument.Result.Data.ClientSupplyOrderReportData clientSupplyOrderReportData = new Business.XPSDocument.Result.Data.ClientSupplyOrderReportData(dataElement);
                YellowstonePathology.Business.XPSDocument.Result.Xps.ClientSupplyOrderReport clientSupplyOrderReport = new Business.XPSDocument.Result.Xps.ClientSupplyOrderReport(clientSupplyOrderReportData);
                XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
                xpsDocumentViewer.LoadDocument(clientSupplyOrderReport.FixedDocument);
                xpsDocumentViewer.ShowDialog();
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ComboBoxOrderSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.FillClientSupplyOrderCollection();
        }

        private void FillClientSupplyOrderCollection()
        {
            if (this.ComboBoxOrderSelection.SelectedItem != null)
            {
                if (this.ComboBoxOrderSelection.SelectedIndex == 0)
                {
                    this.m_ClientSupplyOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyOrderCollection();
                }
                else if (this.ComboBoxOrderSelection.SelectedIndex == 1)
                {
                    this.m_ClientSupplyOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyOrderCollectionByFinal(false);
                }

                this.NotifyPropertyChanged("ClientSupplyOrderCollection");
            }
        }
    }
}

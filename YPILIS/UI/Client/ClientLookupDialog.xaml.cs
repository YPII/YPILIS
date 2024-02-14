using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for ClientLookupDialog.xaml
    /// </summary>
    public partial class ClientLookupDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Client.Model.ClientCollection m_ClientCollection;
        private YellowstonePathology.Business.Client.Model.Client m_Client;
        

        public ClientLookupDialog()
        {            
            InitializeComponent();
            this.DataContext = this;
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Client.Model.ClientCollection ClientCollection
        {
            get { return this.m_ClientCollection; }
        }

        public YellowstonePathology.Business.Client.Model.Client Client
        {
            get { return this.m_Client; }
        }

        private void TextBoxClientName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxClientName.Text))
            {
                this.DoClientSearch(this.TextBoxClientName.Text);
            }
        }

        private void ListViewClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewClients.SelectedItem != null)
            {
                this.m_Client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                this.DialogResult = true;
                Close();
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewClients.SelectedItem != null)
            {
                this.m_Client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
                this.DialogResult = true;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("No client is selected." + Environment.NewLine +
                    "Choosing Yes will close indicating a selection of no client, choosing No will just close, choosing Cancel will keep the dialog open.",
                    "No Client selected", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
                if(result == MessageBoxResult.Yes)
                {
                    this.DialogResult = true;
                }
                if (result == MessageBoxResult.No)
                {
                    this.DialogResult = false;
                }
                if(result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }

        private void DoClientSearch(string clientName)
        {
            this.m_ClientCollection = Business.Gateway.PhysicianClientGateway.GetClientsByClientName(clientName);
            NotifyPropertyChanged("ClientCollection");
            this.ListViewClients.SelectedIndex = -1;
        }
    }
}

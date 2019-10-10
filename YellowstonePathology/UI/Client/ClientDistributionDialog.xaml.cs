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

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for ClientDistributionDialog.xaml
    /// </summary>
    public partial class ClientDistributionDialog : Window
    {
        private YellowstonePathology.Business.Client.Model.Client m_Client;
        private YellowstonePathology.Business.Client.Model.ClientDistributionCollection m_ClientDistributionCollection;

        public ClientDistributionDialog(YellowstonePathology.Business.Client.Model.Client client)
        {
            this.m_Client = client;
            this.FillClientDistributionCollection();

            InitializeComponent();

            this.DataContext = this;
        }

        public YellowstonePathology.Business.Client.Model.Client Client
        {
            get { return this.m_Client; }
        }

        public YellowstonePathology.Business.Client.Model.ClientDistributionCollection ClientDistributionCollection
        {
            get { return this.m_ClientDistributionCollection; }
        }

        private void FillClientDistributionCollection()
        {
            this.m_ClientDistributionCollection  = Business.Gateway.PhysicianClientGateway.GetClientDistributionCollection(this.m_Client.ClientId);
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

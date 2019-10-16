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
        private Business.ReportDistribution.Model.DistributionTypeList m_DistributionTypeList;
        private string m_SuggestedAlternateDistributionType;

        public ClientDistributionDialog(YellowstonePathology.Business.Client.Model.Client client)
        {
            this.m_Client = client;
            this.m_SuggestedAlternateDistributionType = this.m_Client.AlternateDistributionType;
            this.m_DistributionTypeList = new YellowstonePathology.Business.ReportDistribution.Model.DistributionTypeList();
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

        public YellowstonePathology.Business.ReportDistribution.Model.DistributionTypeList DistributionTypeList
        {
            get { return this.m_DistributionTypeList; }
        }

        public string SuggestedAlternateDistributionType
        {
            get { return this.m_SuggestedAlternateDistributionType; }
            set { this.m_SuggestedAlternateDistributionType = value; }
        }

        private void FillClientDistributionCollection()
        {
            this.m_ClientDistributionCollection  = Business.Gateway.PhysicianClientGateway.GetClientDistributionCollection(this.m_Client.ClientId);
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonShowDistributionChanges_Click(object sender, RoutedEventArgs e)
        {
            if(this.ComboboxDistributionTypes.SelectedItem != null)
            {
                string distributionType = this.ComboboxDistributionTypes.SelectedItem.ToString();
                if(distributionType != this.m_Client.DistributionType)
                {
                    if (distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC ||
                        distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX ||
                        distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA ||
                        distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH ||
                        distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW)
                    {
                        if (string.IsNullOrEmpty(this.m_SuggestedAlternateDistributionType) == false)
                        {
                            this.m_ClientDistributionCollection.UpdateDistributionType(this.m_Client, distributionType, this.m_SuggestedAlternateDistributionType);
                        }
                        else
                        {
                            MessageBox.Show("The Alternate Distribution Type must be set when the Distribution Type is " + distributionType + ".");
                        }
                    }
                    else
                    {
                        this.m_ClientDistributionCollection.UpdateDistributionType(this.m_Client, distributionType, this.m_SuggestedAlternateDistributionType);
                    }
                }
                else
                {
                    MessageBox.Show("Choose a distribution type that is not the same as the client Distribution Type");
                }
            }
            else
            {
                MessageBox.Show("Choose a new distribution type");
            }
        }
    }
}

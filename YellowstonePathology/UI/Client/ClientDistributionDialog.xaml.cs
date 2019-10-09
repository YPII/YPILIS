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
        List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> m_ToPhysicianClientDistributionViews;
        List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> m_FromPhysicianClientDistributionViews;

        public ClientDistributionDialog(YellowstonePathology.Business.Client.Model.Client client)
        {
            this.m_Client = client;
            this.m_ToPhysicianClientDistributionViews = new List<Business.Client.Model.PhysicianClientDistributionView>();
            this.m_FromPhysicianClientDistributionViews = new List<Business.Client.Model.PhysicianClientDistributionView>();
            this.SetupDistributionViews();

            InitializeComponent();

            this.DataContext = this;
        }

        public YellowstonePathology.Business.Client.Model.Client Client
        {
            get { return this.m_Client; }
        }

        public List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> ToPhysicianClientDistributionViews
        {
            get { return this.m_ToPhysicianClientDistributionViews; }
        }

        public List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> FromPhysicianClientDistributionViews
        {
            get { return this.m_FromPhysicianClientDistributionViews; }
        }

        private void SetupDistributionViews()
        {
            YellowstonePathology.Business.Domain.PhysicianClientCollection physicianClientCollection = Business.Gateway.PhysicianClientGateway.GetPhysicianClientCollectionByClientId(this.m_Client.ClientId);
            foreach (Business.Domain.PhysicianClient physicianClient in physicianClientCollection)
            {
                List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> physicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(physicianClient.PhysicianClientId);
                foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in physicianClientDistributionViewList)
                {
                    this.m_FromPhysicianClientDistributionViews.Add(physicianClientDistributionView);
                    List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> distributeToPhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetDistributionPhysicianClientDistributions(physicianClientDistributionView.PhysicianClientDistribution.PhysicianClientDistributionID, physicianClient.PhysicianClientId);
                    foreach(YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView distributeToPhysicianClientDistributionView in distributeToPhysicianClientDistributionViewList)
                    {
                        if (this.ShouldAddPhysicianClientDistributionForProcessing(m_FromPhysicianClientDistributionViews, distributeToPhysicianClientDistributionView) == true)
                        {
                            if (this.ShouldAddPhysicianClientDistributionForProcessing(m_ToPhysicianClientDistributionViews, distributeToPhysicianClientDistributionView) == true)
                            {
                                this.m_ToPhysicianClientDistributionViews.Add(distributeToPhysicianClientDistributionView);
                            }
                        }
                    }
                }
            }
        }

        private bool ShouldAddPhysicianClientDistributionForProcessing(List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> existingList, YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView viewToCheck)
        {
            bool result = false;
            bool found = false;
            foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView existingView in existingList)
            {
                if (viewToCheck.PhysicianClientDistribution.PhysicianClientDistributionID ==
                existingView.PhysicianClientDistribution.PhysicianClientDistributionID)
                {
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                result = true;
            }
            return result;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

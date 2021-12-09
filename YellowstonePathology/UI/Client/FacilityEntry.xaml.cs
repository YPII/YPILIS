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
    /// Interaction logic for FacilityEntry.xaml
    /// </summary>
    public partial class FacilityEntry : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Facility.Model.Facility m_Facility;
        private List<string> m_PaymentTypeList;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_AccessioningFacilities;
        private YellowstonePathology.Business.Client.Model.Client m_Client;
        private bool m_IsNewFacility;

        public FacilityEntry(YellowstonePathology.Business.Facility.Model.Facility facility, bool isNewFacility)
        {
            this.m_IsNewFacility = isNewFacility;
            if (isNewFacility == true)
            {
                this.m_Facility = new Business.Facility.Model.Facility();
            }
            else
            {
                this.m_Facility = Business.Persistence.DocumentGateway.Instance.PullFacility(facility.FacilityId, this);
                if (this.m_Facility.ClientId != 0)
                {
                    this.m_Client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_Facility.ClientId);
                }
            }

            this.m_PaymentTypeList = new List<string>();
            this.m_PaymentTypeList.Add("SENDER");
            this.m_PaymentTypeList.Add("THIRD_PARTY");
            this.m_PaymentTypeList.Add("RECIPIENT");

            this.m_AccessioningFacilities = Business.Facility.Model.FacilityCollection.GetAllYPFacilities();
            this.m_AccessioningFacilities.Insert(0, new Business.Facility.Model.Facility());

            InitializeComponent();
            this.DataContext = this;
            Closing += FacilityEntry_Closing;
        }

        private void FacilityEntry_Closing(object sender, CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            YellowstonePathology.Business.Facility.Model.FacilityCollection.Refresh();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Facility.Model.Facility Facility
        {
            get { return this.m_Facility; }
        }

        public List<string> PaymentTypeList
        {
            get { return this.m_PaymentTypeList; }
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection AccessioningFacilities
        {
            get { return this.m_AccessioningFacilities; }
        }

        public YellowstonePathology.Business.Client.Model.Client Client
        {
            get { return this.m_Client; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_IsNewFacility == true)
            {
                if(CanAdd() == true)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_Facility, this);
                    this.m_IsNewFacility = false;                }
            }

            if (this.CanSave() == true)
            {
                Close();
            }
        }
        
        private bool CanAdd()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.m_Facility.FacilityName) == false)
            {
                this.m_Facility.FacilityId = Business.Helper.IdHelper.CapitalConsonantsInString(m_Facility.FacilityName);
                if (string.IsNullOrEmpty(this.m_Facility.FacilityId) == true)
                {
                    result = false;
                }
                else if (YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.Exists(this.m_Facility.FacilityId) == true)
                {
                    this.m_Facility.FacilityId = null;
                    result = false;
                }

                if (result == false)
                {
                    MessageBox.Show("The name did not create a valid Id." + Environment.NewLine + "You can add consonants to the name then change the name when a valid id is created.");
                }
            }
            else
            {
                result = false;
                //MessageBox.Show("You need to enter a facility name.");
            }
            return result;
        }

        private bool CanSave()
        {
            bool result = true;
            YellowstonePathology.Business.Audit.Model.CanSaveFacilityAudit canSaveFacilityAudit = new Business.Audit.Model.CanSaveFacilityAudit(this.m_Facility);
            canSaveFacilityAudit.Run();
            if(canSaveFacilityAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                result = false;
                MessageBox.Show(canSaveFacilityAudit.Message.ToString());
            }
            return result;
        }

        private void ButtonViewClients_Click(object sender, RoutedEventArgs e)
        {
            ClientLookupDialog dlg = new UI.Client.ClientLookupDialog();
            bool? result = dlg.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                this.m_Client = dlg.Client;
                if (this.m_Client == null)
                {
                    this.m_Facility.ClientId = 0;
                }
                else
                {
                    this.m_Facility.ClientId = this.m_Client.ClientId;
                }
            }
            this.NotifyPropertyChanged("Client");
            this.NotifyPropertyChanged("Facility");
        }

        private void ComboBoxAccessioningLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_Facility.LocationAbbreviation = ((YellowstonePathology.Business.Facility.Model.Facility)this.ComboBoxAccessioningLocation.SelectedItem).LocationAbbreviation;
            this.NotifyPropertyChanged("Facility");
        }
    }
}

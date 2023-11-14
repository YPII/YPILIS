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
    /// Interaction logic for ClientFedxDialog.xaml
    /// </summary>
    public partial class ClientFedxDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_FakeAccessionNo;
        private string m_ZPLII;

        private string m_TrackingNumber;
        private string m_ShipToName;
        private string m_ShipToPhone;
        private string m_ShipToAddress1;
        private string m_ShipToAddress2;
        private string m_ShipToCity;
        private string m_ShipToState;
        private string m_ShipToZip;
        private string m_PaymentType;
        private string m_AccountNo;
        private string m_ServiceType;

        private YellowstonePathology.Business.Facility.Model.Facility m_Facility;

        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;
        private List<string> m_PaymentTypeList;

        public ClientFedxDialog(YellowstonePathology.Business.Facility.Model.Facility facility)
        {
            this.m_FakeAccessionNo = "FromClnt";
            this.m_Facility = facility;
            if(this.m_Facility != null)
            {
                this.SetShipTo(this.m_Facility);
            }

            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.Instance;

            this.m_PaymentTypeList = new List<string>();
            this.m_PaymentTypeList.Add("SENDER");
            this.m_PaymentTypeList.Add("THIRD_PARTY");
            this.m_PaymentTypeList.Add("RECIPIENT");

            InitializeComponent();

            DataContext = this;
        }

        public string TrackingNumber
        {
            get { return this.m_TrackingNumber; }
            set { this.m_TrackingNumber = value; }
        }

        public string ShipToName
        {
            get { return this.m_ShipToName; }
            set { this.m_ShipToName = value; }
        }

        public string ShipToPhone
        {
            get { return this.m_ShipToPhone; }
            set { this.m_ShipToPhone = value; }
        }

        public string ShipToAddress1
        {
            get { return this.m_ShipToAddress1; }
            set { this.m_ShipToAddress1 = value; }
        }

        public string ShipToAddress2
        {
            get { return this.m_ShipToAddress2; }
            set { this.m_ShipToAddress2 = value; }
        }

        public string ShipToCity
        {
            get { return this.m_ShipToCity; }
            set { this.m_ShipToCity = value; }
        }

        public string ShipToState
        {
            get { return this.m_ShipToState; }
            set { this.m_ShipToState = value; }
        }

        public string ShipToZip
        {
            get { return this.m_ShipToZip; }
            set { this.m_ShipToZip = value; }
        }

        public string PaymentType
        {
            get { return this.m_PaymentType; }
            set { this.m_PaymentType = value; }
        }

        public string AccountNo
        {
            get { return this.m_AccountNo; }
            set { this.m_AccountNo = value; }
        }

        public string ServiceType
        {
            get { return this.m_ServiceType; }
            set { this.m_ServiceType = value; }
        }

        public YellowstonePathology.Business.Facility.Model.Facility Facility
        {
            get { return this.m_Facility; }
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

        public List<string> PaymentTypeList
        {
            get { return this.m_PaymentTypeList; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ComboboxShipToFacility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded == true)
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectionBoxItem != null)
                {
                    this.m_Facility = (Business.Facility.Model.Facility)comboBox.SelectedItem;
                    if (this.m_Facility != null)
                    {
                        this.SetShipTo(this.m_Facility);
                    }
                }
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void HyperLinkGetTrackingNumber_Click(object sender, RoutedEventArgs e)
        {
                if (this.IsOKToGetTrackingNumber() == true)
                {
                    Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                    Business.MaterialTracking.Model.FedexShipmentRequest shipmentRequest = new Business.MaterialTracking.Model.FedexShipmentRequest(fedExAccount,
                        this.m_FakeAccessionNo, this.m_PaymentType, this.m_ServiceType, this.m_TrackingNumber, 
                        this.m_ShipToName, this.m_ShipToPhone, this.m_ShipToAddress1,
                        this.m_ShipToAddress2, this.m_ShipToCity, this.m_ShipToState,
                        this.m_ShipToZip, this.m_AccountNo);
                    Business.MaterialTracking.Model.FedexProcessShipmentReply result = shipmentRequest.RequestShipment();

                    if (result.RequestWasSuccessful == true)
                    {
                        this.m_TrackingNumber = result.TrackingNumber;
                        this.m_ZPLII = Business.Label.Model.ZPLPrinterTCP.DecodeZPLFromBase64(result.ZPLII);
                        this.NotifyPropertyChanged(string.Empty);
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
                else
                {
                    MessageBox.Show("We are unable to get the tracking number at this point because there are problems with the data.");
                }
        }

        private bool IsOKToGetTrackingNumber()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.m_TrackingNumber) == false) result = false;
            if (string.IsNullOrEmpty(this.m_ShipToAddress1) == true) result = false;
            if (string.IsNullOrEmpty(this.m_ShipToCity) == true) result = false;
            if (string.IsNullOrEmpty(this.m_ShipToState) == true) result = false;
            if (string.IsNullOrEmpty(this.m_ShipToZip) == true) result = false;
            if (string.IsNullOrEmpty(this.m_PaymentType) == true) result = false;

            if (string.IsNullOrEmpty(this.m_ShipToName) == true) result = false;
            if (string.IsNullOrEmpty(this.m_ShipToPhone) == true) result = false;
            if (string.IsNullOrEmpty(this.m_AccountNo) == true) result = false;
            if (string.IsNullOrEmpty(this.m_ServiceType) == true) result = false;
            if (string.IsNullOrEmpty(this.m_AccountNo) == false && this.m_AccountNo.Contains("-") == true) result = false;

            return result;
        }

        private void HyperLinkPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_ZPLII) == false)
            {
                if (string.IsNullOrEmpty(Business.User.UserPreferenceInstance.Instance.UserPreference.FedExLabelPrinter) == false)
                {
                    Business.Label.Model.ZPLPrinterTCP zplPrinter = new Business.Label.Model.ZPLPrinterTCP(Business.User.UserPreferenceInstance.Instance.UserPreference.FedExLabelPrinter);
                    zplPrinter.Print(this.m_ZPLII);
                }
                else
                {
                    MessageBox.Show("You need to go into User Preferences and choose your FedEx label printer before your label can be printed.");
                }
            }
            else
            {
                MessageBox.Show("The label cannot be printed until a tracking number exists.");
            }
        }

        private void HyperLinkCancelShipment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_TrackingNumber) == false)
            {
                Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                Business.MaterialTracking.Model.FedexDeleteShipmentRequest deleteShipmentRequest = new Business.MaterialTracking.Model.FedexDeleteShipmentRequest(fedExAccount, this.m_TrackingNumber);
                Business.MaterialTracking.Model.FedexDeleteShipmentReply result = deleteShipmentRequest.Post();

                if (result.RequestWasSuccessful == true)
                {
                    this.m_ZPLII = null;
                    this.m_TrackingNumber = null;
                    this.NotifyPropertyChanged(string.Empty);
                }
                else
                {
                    MessageBox.Show("There was a problem with this Request.");
                }
            }
        }

        private void SetShipTo(Business.Facility.Model.Facility facility)
        {
            this.m_ShipToName = facility.FacilityName;
            this.m_ShipToAddress1 = facility.Address1;
            this.m_ShipToAddress2 = facility.Address2;
            this.m_ShipToCity = facility.City;
            this.m_ShipToState = facility.State;
            this.m_ShipToZip = facility.ZipCode;
            this.m_ShipToPhone = facility.PhoneNumber;
            this.m_PaymentType = facility.FedexPaymentType;
            this.m_AccountNo = facility.FedexAccountNo;
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HyperLinkOptiFreight_Click(object sender, RoutedEventArgs e)
        {
            Business.MaterialTracking.Model.FedexAccountProduction fedexAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
            this.m_AccountNo = fedexAccount.OptiFreightAccountNo;
            this.m_PaymentType = "THIRD_PARTY";
            this.NotifyPropertyChanged(string.Empty);
        }
    }
}

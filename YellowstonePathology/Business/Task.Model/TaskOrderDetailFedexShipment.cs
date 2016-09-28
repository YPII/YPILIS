using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
    [PersistentClass("tblTaskOrderDetailFedexShipment", "tblTaskOrderDetail", "YPIDATA")]
    public class TaskOrderDetailFedexShipment : TaskOrderDetail
    {
        private string m_TrackingNumber;
        private string m_ShipToFacilityId;
        private bool m_LabelHasBeenPrinted;
        private string m_ZPLII;
        private string m_ShipToName;
        private string m_ShipToPhone;
        private string m_ShipToAddress1;
        private string m_ShipToAddress2;
        private string m_ShipToCity;
        private string m_ShipToState;
        private string m_ShipToZip;
        private string m_PaymentType;

        public TaskOrderDetailFedexShipment()
        {
            
        }

        public TaskOrderDetailFedexShipment(string taskOrderDetailId, string taskOrderId, string objectId, Task task) 
            : base(taskOrderDetailId, taskOrderId, objectId, task)
        {
            TaskFedexShipment fedexShipment = (TaskFedexShipment)task;
            this.m_ShipToFacilityId = fedexShipment.ShipToFacility.FacilityId;
            this.m_ShipToName = fedexShipment.ShipToFacility.FacilityName;
            this.m_ShipToAddress1 = fedexShipment.ShipToFacility.Address1;
            this.m_ShipToAddress2 = fedexShipment.ShipToFacility.Address2;
            this.m_ShipToCity = fedexShipment.ShipToFacility.City;
            this.m_ShipToState = fedexShipment.ShipToFacility.State;
            this.m_ShipToZip = fedexShipment.ShipToFacility.ZipCode;
            this.m_ShipToPhone = fedexShipment.ShipToFacility.PhoneNumber;
            this.m_PaymentType = fedexShipment.ShipToFacility.FedexPaymentType;
        }

        public void SetShipTo(Business.Facility.Model.Facility facility)
        {
            this.m_ShipToFacilityId = facility.FacilityId;
            this.m_ShipToName = facility.FacilityName;
            this.m_ShipToAddress1 = facility.Address1;
            this.m_ShipToAddress2 = facility.Address2;
            this.m_ShipToCity = facility.City;
            this.m_ShipToState = facility.State;
            this.m_ShipToZip = facility.ZipCode;
            this.m_ShipToPhone = facility.PhoneNumber;
            this.m_PaymentType = facility.FedexPaymentType;
        }

        public void SetZPLFromBase64(string encodedString)
        {
            byte[] bytes = Convert.FromBase64String(encodedString);
            string zplString = System.Text.Encoding.Default.GetString(bytes);
            this.m_ZPLII = zplString;
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TrackingNumber
        {
            get { return this.m_TrackingNumber; }
            set
            {
                if (this.m_TrackingNumber != value)
                {
                    this.m_TrackingNumber = value;
                    this.NotifyPropertyChanged("TrackingNumber");
                }
            }
        }
        
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ShipToFacilityId
        {
            get { return this.m_ShipToFacilityId; }
            set
            {
                if (this.m_ShipToFacilityId != value)
                {
                    this.m_ShipToFacilityId = value;
                    this.NotifyPropertyChanged("ShipToFacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string ZPLII
        {
            get { return this.m_ZPLII; }
            set
            {
                if (this.m_ZPLII != value)
                {
                    this.m_ZPLII = value;
                    this.NotifyPropertyChanged("ZPLII");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "null", "bit")]
        public bool LabelHasBeenPrinted
        {
            get { return this.m_LabelHasBeenPrinted; }
            set
            {
                if (this.m_LabelHasBeenPrinted != value)
                {
                    this.m_LabelHasBeenPrinted = value;
                    this.NotifyPropertyChanged("LabelHasBeenPrinted");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ShipToName
        {
            get { return this.m_ShipToName; }
            set
            {
                if (this.m_ShipToName != value)
                {
                    this.m_ShipToName = value;
                    this.NotifyPropertyChanged("ShipToName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ShipToPhone
        {
            get { return this.m_ShipToPhone; }
            set
            {
                if (this.m_ShipToPhone != value)
                {
                    this.m_ShipToPhone = value;
                    this.NotifyPropertyChanged("ShipToPhone");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ShipToAddress1
        {
            get { return this.m_ShipToAddress1; }
            set
            {
                if (this.m_ShipToAddress1 != value)
                {
                    this.m_ShipToAddress1 = value;
                    this.NotifyPropertyChanged("ShipToAddress1");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ShipToAddress2
        {
            get { return this.m_ShipToAddress2; }
            set
            {
                if (this.m_ShipToAddress2 != value)
                {
                    this.m_ShipToAddress2 = value;
                    this.NotifyPropertyChanged("ShipToAddress2");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ShipToCity
        {
            get { return this.m_ShipToCity; }
            set
            {
                if (this.m_ShipToCity != value)
                {
                    this.m_ShipToCity = value;
                    this.NotifyPropertyChanged("ShipToCity");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ShipToState
        {
            get { return this.m_ShipToState; }
            set
            {
                if (this.m_ShipToState != value)
                {
                    this.m_ShipToState = value;
                    this.NotifyPropertyChanged("ShipToState");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ShipToZip
        {
            get { return this.m_ShipToZip; }
            set
            {
                if (this.m_ShipToZip != value)
                {
                    this.m_ShipToZip = value;
                    this.NotifyPropertyChanged("ShipToZip");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string PaymentType
        {
            get { return this.m_PaymentType; }
            set
            {
                if (this.m_PaymentType != value)
                {
                    this.m_PaymentType = value;
                    this.NotifyPropertyChanged("PaymentType");
                }
            }
        }

        public bool PropertiesAreEnabled
        {
            get
            {
                bool result = false;
                if (string.IsNullOrEmpty(this.m_TrackingNumber) == true) result = true;
                return result;
            }
        }
    }
}

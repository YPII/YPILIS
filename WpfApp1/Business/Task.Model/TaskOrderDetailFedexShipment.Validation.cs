using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Task.Model
{
    public partial class TaskOrderDetailFedexShipment
    {
        private Dictionary<string, string> m_ValidationErrors;

        public void ValidateObject()
        {
            this.ValidateAccountNo();
            this.ValidateShipToPhone();
        }

        public string AccountNoBinding
        {
            get { return this.m_AccountNo; }
            set
            {
                this.m_AccountNo = value;
                this.ValidateAccountNo();
            }
        }

        public void ValidateAccountNo()
        {
            if (this.m_ValidationErrors.ContainsKey("AccountNoBinding") == true) this.m_ValidationErrors.Remove("AccountNoBinding");
            if (string.IsNullOrEmpty(this.m_AccountNo) == false && this.m_AccountNo.Contains("-") == true)
            {
                this.m_ValidationErrors.Add("AccountNoBinding", "'-' is not allowed in the AccountNo.");
            }
            this.NotifyPropertyChanged("AccountNoBinding");
        }

        public string ShipToPhoneBinding
        {
            get { return this.m_ShipToPhone; }
            set
            {
                this.m_ShipToPhone = value;
                this.ValidateShipToPhone();
            }
        }

        public void ValidateShipToPhone()
        {
            if (this.m_ValidationErrors.ContainsKey("ShipToPhoneBinding") == true) this.m_ValidationErrors.Remove("ShipToPhoneBinding");
            if (string.IsNullOrEmpty(this.m_ShipToPhone) == true)
            {
                this.m_ValidationErrors.Add("ShipToPhoneBinding", "A phone number is required.");
            }
            this.NotifyPropertyChanged("ShipToPhoneBinding");
        }

        public string Errors
        {
            get
            {
                string result = string.Empty;
                if (this.m_ValidationErrors.Count > 0)
                {
                    foreach(string value in this.m_ValidationErrors.Values)
                    {
                        result += value + Environment.NewLine;
                    }
                }
                return result;
            }
        }

        public Dictionary<string, string> ValidationErrors
        {
            get { return this.m_ValidationErrors; }
        }
    }
}

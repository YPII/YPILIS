using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CLIALicense : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_LicenseNumber;
        protected Facility m_Facility;        

        public CLIALicense(Facility cliaFacility, string cliaLicenseNumber)
        {
            this.m_Facility = cliaFacility;
            this.m_LicenseNumber = cliaLicenseNumber;










        }

        public string LicenseNumber
        {
            get { return this.m_LicenseNumber;  }
        }

        public Facility Facility
        {
            get { return this.m_Facility; }
        }

        public string GetAddressString()
        {
            StringBuilder result = new StringBuilder();            
            result.Append(this.m_Facility.GetAddressString() + " (CLIA:" + this.m_LicenseNumber + ").");
            return result.ToString();
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}

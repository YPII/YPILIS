using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View
{    
    public class PID
    {
        private Business.Patient.Model.Address m_Address;
        private string m_HomePhoneNumber;        

        public PID()
        {
            this.m_Address = new Patient.Model.Address();
        }

        public void FromHL7(string pidSegment)
        {
            string[] split = pidSegment.Split('|');

            if(string.IsNullOrEmpty(split[13]) == false)
            {
                string[] homePhoneSplit = split[13].Split('^');
                this.m_HomePhoneNumber = homePhoneSplit[0];
            }
            
            if(string.IsNullOrEmpty(split[11]) == false)
            {
                string[] addressField = split[11].Split('^');
                this.m_Address.PAddress1 = addressField[0];
                this.m_Address.PAddress2 = addressField[1];
                this.m_Address.PCity = addressField[2];
                this.m_Address.PState = addressField[3];
                this.m_Address.PZipCode = addressField[4];
            }            
        }

        public string HomePhoneNumber
        {
            get { return this.m_HomePhoneNumber; }
        }

        public Business.Patient.Model.Address Address
        {
            get { return this.m_Address; }
        }
    }
}

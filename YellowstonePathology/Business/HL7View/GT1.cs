using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.HL7View
{
    public class GT1
    {
        private string m_GuarantorFirstName;
        private string m_GuarantorLastName;
        private string m_GuarantorMiddleName;
        private string m_GuarantorAddressLine1;
        private string m_GuarantorAddressLine2;
        private string m_GuarantorCity;
        private string m_GuarantorState;
        private string m_GuarantorZip;

        public GT1()
        {

        }

        public void FromHL7(string gt1Segment)
        {
            string[] split = gt1Segment.Split('|');
            string guarantorString = split[3];
            string[] guarantorSplit = guarantorString.Split('^');

            this.m_GuarantorLastName = Business.Helper.JSONHelper.HandleValue(guarantorSplit, 0);
            this.m_GuarantorFirstName = Business.Helper.JSONHelper.HandleValue(guarantorSplit, 1);
            this.m_GuarantorMiddleName = Business.Helper.JSONHelper.HandleValue(guarantorSplit, 2);

            if (string.IsNullOrEmpty(split[5]) == false)
            {
                string[] subAddressSubfields = split[5].Split('^');

                this.m_GuarantorAddressLine1 = Business.Helper.JSONHelper.HandleValue(subAddressSubfields, 0);
                this.m_GuarantorAddressLine2 = Business.Helper.JSONHelper.HandleValue(subAddressSubfields, 1);
                this.m_GuarantorCity = Business.Helper.JSONHelper.HandleValue(subAddressSubfields, 2);
                this.m_GuarantorState = Business.Helper.JSONHelper.HandleValue(subAddressSubfields, 3);
                this.m_GuarantorZip = Business.Helper.JSONHelper.HandleValue(subAddressSubfields, 4);                
            }                        
        }

        public string GuarantorLastName
        {
            get { return this.m_GuarantorLastName; }
            set {  this.m_GuarantorLastName = value; }
        }

        public string GuarantorFirstName
        {
            get { return this.m_GuarantorFirstName; }
            set { this.m_GuarantorFirstName = value; }
        }

        public string GuarantorMiddleName
        {
            get { return this.m_GuarantorMiddleName; }
            set { this.m_GuarantorMiddleName = value; }
        }

        public string GuarantorAddressLine1
        {
            get { return this.m_GuarantorAddressLine1; }
            set { this.m_GuarantorAddressLine1 = value; }
        }

        public string GuarantorAddressLine2
        {
            get { return this.m_GuarantorAddressLine2; }
            set { this.m_GuarantorAddressLine2 = value; }
        }

        public string GuarantorCity
        {
            get { return this.m_GuarantorCity; }
            set { this.m_GuarantorCity = value; }
        }

        public string GuarantorState
        {
            get { return this.m_GuarantorState; }
            set { this.m_GuarantorState = value; }
        }

        public string GuarantorZip
        {
            get { return this.m_GuarantorZip; }
            set { this.m_GuarantorZip = value; }
        }

        public JObject ToJson()
        {
            return new JObject(
                new JProperty("guarantorLastName", Business.Helper.JSONHelper.HandleValue(this.m_GuarantorLastName)),
                new JProperty("guarantorFirstName", Business.Helper.JSONHelper.HandleValue(this.m_GuarantorFirstName)),
                new JProperty("guarantorMiddleName", Business.Helper.JSONHelper.HandleValue(this.m_GuarantorMiddleName)),
                new JProperty("guarantorAddressLine1", Business.Helper.JSONHelper.HandleValue(this.m_GuarantorAddressLine1)),
                new JProperty("guarantorAddressLine2", Business.Helper.JSONHelper.HandleValue(this.m_GuarantorAddressLine2)),
                new JProperty("guarantorCity", Business.Helper.JSONHelper.HandleValue(this.m_GuarantorCity)),
                new JProperty("guarantorState", Business.Helper.JSONHelper.HandleValue(this.m_GuarantorState)),
                new JProperty("guarantorZip", Business.Helper.JSONHelper.HandleValue(this.m_GuarantorZip))
            );
        }         
        
        public string DisplayString
        {
            get
            {
                return this.m_GuarantorLastName + ", " + this.m_GuarantorFirstName + ", " + this.m_GuarantorMiddleName + Environment.NewLine + this.m_GuarantorAddressLine1 + " " + this.m_GuarantorCity + " " + this.m_GuarantorState + " " + this.m_GuarantorZip;
            }
        }       
    }
}

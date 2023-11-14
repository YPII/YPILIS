using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
    public class FedexAccount
    {        
        //Web useraname: ypiflow
        //Web Password: Cytometry1
        //Phone 877 339 2774 2,3
        //41572608

        protected string m_URL;
        protected string m_Key;
        protected string m_Password;
        protected string m_AccountNo;
        protected string m_MeterNo;
        protected string m_OptiFreightAccountNo;      

        public FedexAccount()
        {
            
        }
        
        public string URL
        {
            get { return this.m_URL; }
        }
        
        public string Key
        {
            get { return this.m_Key; }
        }        

        public string Password
        {
            get { return this.m_Password; }
        }

        public string AccountNo
        {
            get { return this.m_AccountNo; }
        }

        public string OptiFreightAccountNo
        {
            get { return this.m_OptiFreightAccountNo; }
        }

        public string MeterNo
        {
            get { return this.m_MeterNo; }
        }
    }
}

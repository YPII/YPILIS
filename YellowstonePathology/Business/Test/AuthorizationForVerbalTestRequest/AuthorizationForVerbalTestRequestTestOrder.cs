using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest
{
    [PersistentClass("tblAuthorizationForVerbalTestRequestTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class AuthorizationForVerbalTestRequestTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_AuthorizationTestName;
        private string m_Fax;
        private string m_ContactName;

        public AuthorizationForVerbalTestRequestTestOrder()
        { }

        public AuthorizationForVerbalTestRequestTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
        {
            this.m_NoCharge = true;
        }        

        [PersistentProperty()]
        public string AuthorizationTestName
        {
            get { return this.m_AuthorizationTestName; }
            set
            {
                if (this.m_AuthorizationTestName != value)
                {
                    this.m_AuthorizationTestName = value;
                    this.NotifyPropertyChanged("AuthorizationTestName");
                }
            }
        }

        [PersistentProperty()]
        public string Fax
        {
            get { return this.m_Fax; }
            set
            {
                if (this.m_Fax != value)
                {
                    this.m_Fax = value;
                    this.NotifyPropertyChanged("Fax");
                }
            }
        }

        [PersistentProperty()]
        public string ContactName
        {
            get { return this.m_ContactName; }
            set
            {
                if (this.m_ContactName != value)
                {
                    this.m_ContactName = value;
                    this.NotifyPropertyChanged("ContactName");
                }
            }
        }

        public string FaxProxy
        {
            get { return YellowstonePathology.Business.Helper.PhoneNumberHelper.CorrectPhoneNumber(this.m_Fax); }
            set
            {
                if (this.m_Fax != value)
                {
                    this.m_Fax = value;
                    this.NotifyPropertyChanged("Fax");
                    this.NotifyPropertyChanged("FaxProxy");
                }
            }
        }

        public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
        {
            string result = "Authorization for : " + this.m_AuthorizationTestName;
            return result;
        }
    }
}

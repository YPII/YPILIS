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

        public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
        {
            string result = "Authorization for : " + this.m_AuthorizationTestName;
            return result;
        }
    }
}

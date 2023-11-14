using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class AddPQRSReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Billing.Model.PQRSCode m_PQRSCode;
		YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;

		public AddPQRSReturnEventArgs(YellowstonePathology.Business.Billing.Model.PQRSCode pqrsCode, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
            this.m_PQRSCode = pqrsCode;
            this.m_SurgicalSpecimen = surgicalSpecimen;
        }

        public YellowstonePathology.Business.Billing.Model.PQRSCode PQRSCode
        {
            get { return this.m_PQRSCode; }
        }

		public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen SurgicalSpecimen
        {
            get { return this.m_SurgicalSpecimen; }
        }
    }
}

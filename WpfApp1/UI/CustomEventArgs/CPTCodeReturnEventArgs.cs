using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class CPTCodeReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Billing.Model.CptCode m_CptCode;

        public CPTCodeReturnEventArgs(YellowstonePathology.Business.Billing.Model.CptCode cptCode)
        {
            this.m_CptCode = cptCode;
        }

        public YellowstonePathology.Business.Billing.Model.CptCode CptCode
        {
            get { return this.m_CptCode; }
        }
    }
}

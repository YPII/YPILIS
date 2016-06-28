using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class MasterAccessionNoReturnEventArgs : System.EventArgs
    {
        private string m_MasterAccessionNo;

        public MasterAccessionNoReturnEventArgs(string masterAccessionNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
        }
    }
}

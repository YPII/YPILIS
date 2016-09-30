using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class Barcode
    {
        protected string m_ID;
        protected bool m_IsValidated;        

        public string ID
        {
            get { return this.m_ID; }
            set { this.m_ID = value; }
        }

        public bool IsValidated
        {
            get { return this.m_IsValidated; }
            set { this.m_IsValidated = value; }
        }
    }
}

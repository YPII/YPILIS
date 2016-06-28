using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class BarcodeScanEventArgs : EventArgs
    {
        private BarcodeScan m_BarcodeScan;       

        public BarcodeScanEventArgs(BarcodeScan barcodeScan)
        {
            this.m_BarcodeScan = barcodeScan;
        }

        public BarcodeScan BarcodeScan
        {
            get { return this.m_BarcodeScan; }
            set { this.m_BarcodeScan = value; }
        }
    }
}

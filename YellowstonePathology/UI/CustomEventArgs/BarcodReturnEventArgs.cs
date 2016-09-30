using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class BarcodeReturnEventArgs : System.EventArgs
    {
        Business.BarcodeScanning.Barcode m_Barcode;

        public BarcodeReturnEventArgs(Business.BarcodeScanning.Barcode barcode)
        {
            this.m_Barcode = barcode;
        }

        public Business.BarcodeScanning.Barcode Barcode
        {
            get { return this.m_Barcode; }
        }
    }
}

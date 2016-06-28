using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class HistologyBlock
    {
        private string m_AliquotOrderId;
        private BarcodeVersion1 m_Barcode;

        public HistologyBlock()
        {

        }

        public BarcodeVersion1 Barcode
        {
            get { return this.m_Barcode; }
            set { this.m_Barcode = value; }
        }

        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
            set { this.m_AliquotOrderId = value; }
        }

        public void FromBarcode(BarcodeVersion1 barcode)
        {
            this.m_Barcode = barcode;
            this.AliquotOrderId = barcode.ID.Trim();
        }

        public static HistologyBlock Parse(BarcodeVersion1 barcode)
        {
            HistologyBlock result = null;
            return result;
        }
    }
}

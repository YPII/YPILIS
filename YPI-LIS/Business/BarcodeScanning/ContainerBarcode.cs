using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class ContainerBarcode : Barcode
    {
        protected BarcodePrefixEnum m_Prefix;

        public ContainerBarcode()
        {

        }

        public ContainerBarcode(string scanData)
        {
            this.m_Prefix = BarcodePrefixEnum.CTNR;
            if (string.IsNullOrEmpty(scanData) == false && scanData.Length == 40)
            {
                string guidString = scanData.Substring(4);
                this.m_IsValidated = true;
                this.m_ID = guidString;
            }
            else
            {
                this.m_IsValidated = false;
            }
        }                                                   

        public override string ToString()
        {            
            return this.m_Prefix + this.m_ID;
        }

        public static ContainerBarcode Parse()
        {
            ContainerBarcode containerBarcode = new ContainerBarcode();
            containerBarcode.m_ID = System.Guid.NewGuid().ToString().ToUpper();
            containerBarcode.m_Prefix = BarcodePrefixEnum.CTNR;
            return containerBarcode;
        }
    }
}

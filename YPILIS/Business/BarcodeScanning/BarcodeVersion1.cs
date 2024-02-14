using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class BarcodeVersion1 : Barcode
    {
        protected BarcodePrefixEnum m_Prefix;        

        public BarcodeVersion1(BarcodePrefixEnum prefix, string id)
        {            
            this.m_Prefix = prefix;
            this.m_ID = id;
        }

        public BarcodeVersion1(string scanData)
        {            
            this.m_Prefix = this.ExtractPrefix(scanData);
            this.m_IsValidated = true;
            if (this.m_Prefix != BarcodePrefixEnum.UNDEFINED)
            {
                this.m_ID = scanData.Substring(this.m_Prefix.ToString().Length).TrimStart('0');
            }
            else
            {
                this.m_ID = scanData;
            }            
        }      

        public BarcodePrefixEnum Prefix
        {
            get { return this.m_Prefix; }
            set { this.m_Prefix = value; }
        }        
             
        private BarcodePrefixEnum ExtractPrefix(string scanData)
        {
            BarcodePrefixEnum prefix = BarcodePrefixEnum.UNDEFINED;
            foreach (string enumName in System.Enum.GetNames(typeof(BarcodePrefixEnum)))
            {
                if (scanData.StartsWith(enumName) == true)
                {
                    prefix = (BarcodePrefixEnum)System.Enum.Parse(typeof(BarcodePrefixEnum), enumName);
                    break;
                }
            }
            return prefix;
        }

        public override string ToString()
        {
            return this.m_Prefix + this.m_ID;
        }  
    }
}

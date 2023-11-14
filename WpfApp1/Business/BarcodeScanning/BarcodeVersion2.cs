using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class BarcodeVersion2 : Barcode
    {
        protected BarcodePrefixEnum m_Prefix;        
        protected string m_Version;
        protected string m_CRC;        

        public BarcodeVersion2(BarcodePrefixEnum prefix, string id)
        {            
            this.m_Prefix = prefix;
            this.m_ID = id;
            this.m_CRC = CRC32.ComputeCRC(id);
            this.m_IsValidated = true;
            this.m_Version = "^2";                     
        }

        public BarcodeVersion2(string scanData)
        {            
            BarcodePrefixEnum prefix = (BarcodePrefixEnum)Enum.Parse(typeof(BarcodePrefixEnum), scanData.Substring(5, 4));
            this.m_Prefix = prefix;

            this.m_CRC = scanData.Substring(2, 3);
            this.m_ID = scanData.Substring(9).Trim();
            this.m_Version = "^2";

            string computtedCRC = CRC32.ComputeCRC(this.m_ID);
            if (computtedCRC == this.m_CRC)
            {
                this.m_IsValidated = true;
            }
            else
            {
                this.m_IsValidated = false;
            }            
        }        

        public BarcodePrefixEnum Prefix
        {
            get { return this.m_Prefix; }
            set { this.m_Prefix = value; }
        }

        public string Version
        {
            get { return this.m_Version; }
            set { this.m_Version = value; }
        }

        public string CRC
        {
            get { return this.m_CRC; }
            set { this.m_CRC = value; }
        }        

        public override string ToString()
        {
            return this.m_Version + this.m_CRC.PadRight(3, '0') + this.m_Prefix + this.m_ID;             
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class CytycBarcode
    {                
        private string m_ReportNo;
        private string m_MasterAccessionNo;
        private string m_MasterAccessionNoNumber;
        private string m_MasterAccessionNoYear;
        private string m_CRC;
        private bool m_IsValidated;
        private string m_LineOne;
        private string m_LineTwo;

        public CytycBarcode()
        {

        }

        public CytycBarcode(string scanData)
        {
            string[] lines = scanData.Trim().Split(',');
            if (lines.Length == 2)
            {
                this.m_LineOne = lines[0];
                this.m_LineTwo = lines[1];

                this.m_MasterAccessionNoYear = lines[1].Substring(2, 2);
                this.m_MasterAccessionNoNumber = lines[0].TrimStart('0');
                this.m_MasterAccessionNo = this.m_MasterAccessionNoYear + "-" + this.m_MasterAccessionNoNumber;
                this.m_ReportNo = this.m_MasterAccessionNoYear + "-" + this.m_MasterAccessionNoNumber + ".P";
                this.m_CRC = lines[1].Substring(4);
				this.m_IsValidated = YellowstonePathology.Business.BarcodeScanning.CytycCRC32.IsCRCValid(this.m_MasterAccessionNo, this.m_CRC);
            }       
        }

        public string LineOne
        {
            get { return this.m_LineOne; }
        }

        public string LineTwo
        {
            get { return this.m_LineTwo; }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }            
        }

        public bool IsValidated
        {
            get { return this.m_IsValidated; }
        }

        public static CytycBarcode Parse(string masterAccessionNo)
        {
            CytycBarcode result = new CytycBarcode();
            result.m_MasterAccessionNo = masterAccessionNo;
            result.m_MasterAccessionNoYear = "20" + masterAccessionNo.Substring(0, 2);
            result.m_MasterAccessionNoNumber = masterAccessionNo.Substring(3);
			result.m_CRC = YellowstonePathology.Business.BarcodeScanning.CytycCRC32.ComputeCrc(masterAccessionNo);
            result.m_ReportNo = result.m_MasterAccessionNo + ".P";
            result.m_LineOne = result.m_MasterAccessionNoNumber.PadLeft(7, '0');
            result.m_LineTwo = result.m_MasterAccessionNoYear + result.m_CRC;
            result.m_IsValidated = true;
            return result;
        }
    }
}

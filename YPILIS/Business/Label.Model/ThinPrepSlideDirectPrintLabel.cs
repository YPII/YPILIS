using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class ThinPrepSlideDirectPrintLabel
    {        
        private string m_MasterAccessionNo;
        private string m_CytycOCRLine1;
        private string m_CytycOCRLine2;
        private string m_PatientLastName;
        private string m_PatientFirstName;
		private YellowstonePathology.Business.BarcodeScanning.CytycBarcode m_CytycBarcode;
		private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 m_Barcode;
        private string m_Location = "YPI-Blgs";

        public ThinPrepSlideDirectPrintLabel(string masterAccessionNo, string patientLastName, string patientFirstName,
			YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode, YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode)
        {            
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_CytycOCRLine1 = cytycBarcode.LineOne;
            this.m_CytycOCRLine2 = cytycBarcode.LineTwo;
            this.m_PatientLastName = patientLastName;
            this.m_PatientFirstName = patientFirstName;
            this.m_CytycBarcode = cytycBarcode;
            this.m_Barcode = barcode;
        }

        public string GetLine()
        {                        
            StringBuilder result = new StringBuilder();
            result.Append("#");
            result.Append(this.m_CytycOCRLine1);
            result.Append("#");
            result.Append(this.m_CytycOCRLine2);
            result.Append("#");
            result.Append(this.m_PatientLastName);
            result.Append("#");
            result.Append(this.m_PatientFirstName);
            result.Append("#");
            result.Append(this.m_Barcode.ToString());
            result.Append("#");
            result.Append(this.m_Location);
            return result.ToString();
        }
    }
}

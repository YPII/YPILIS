using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlideDirectPrintLabel
    {
        private string m_ReportNo;
        private string m_SlideNumber;
        private string m_PatientLastName;
		private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 m_Barcode;
        private string m_LocationAbbreviation;

        public HistologySlideDirectPrintLabel(string reportNo, string slideNumber, string patientLastName,
			YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode, string locationAbbreviation)
        {
            this.m_ReportNo = reportNo;
            this.m_SlideNumber = slideNumber;
            this.m_PatientLastName = patientLastName;
            this.m_Barcode = barcode;
            this.m_LocationAbbreviation = locationAbbreviation;            
        }

        public string GetLine()
        {                        
            StringBuilder result = new StringBuilder("$");
            result.Append("#");
            result.Append(this.m_ReportNo);
            result.Append("#");
            result.Append(this.m_SlideNumber);
            result.Append("#");
            result.Append(this.m_PatientLastName);
            result.Append("#");
            result.Append(this.m_LocationAbbreviation);
            result.Append("#");
            result.Append(this.m_Barcode.ToString());
            return result.ToString();
        }
    }
}

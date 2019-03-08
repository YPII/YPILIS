using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class HologicSlideLabel
    {
        private string m_PatientFirstName;
        private string m_PatientLastName;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 m_Barcode;
        private YellowstonePathology.Business.BarcodeScanning.CytycBarcode m_CytycBarcode;        

        public HologicSlideLabel(string patientFirstName, string patientLastName, YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode,
            YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode)
        {
            this.m_PatientFirstName = patientFirstName;
            this.m_PatientLastName = patientLastName;
            this.m_Barcode = barcode;
            this.m_CytycBarcode = cytycBarcode;
        }


        public void AppendCommands(StringBuilder zplString, int xOffset, int yOffset)
        {
            string patientNameText = this.TruncateString(this.m_PatientLastName, 8) + ", " + this.TruncateString(this.m_PatientFirstName, 1);
            zplString.Append("^FO" + (10 + xOffset) + "," + (8 + yOffset) + "^A@N,38,38,E:OCR000.TTF^FD" + this.m_CytycBarcode.LineOne + "^FS");
            zplString.Append("^FO" + (10 + xOffset) + "," + (48 + yOffset) + "^A@N,38,38,E:OCR000.TTF^FD" + this.m_CytycBarcode.LineTwo + "^FS");
            zplString.Append("^FO" + (10 + xOffset) + "," + (90 + yOffset) + "^A@N,15,15,E:VERDANA.TTF^FD" + patientNameText + "^FS");
            zplString.Append("^FO" + (10 + xOffset) + "," + (150 + yOffset) + "^A@N,15,15,E:VERDANA.TTF^FDYPI Blgs^FS");
            string slideId = this.m_Barcode.ToString().Replace("^", "_5e");
            zplString.Append("^FO" + (110 + xOffset) + "," + (118 + yOffset) + "^BXN,3,200^AD^FH^FD" + slideId + "^FS");            
        }

        public string TruncateString(string text, int width)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text) == false)
            {
                if (text.Length > width)
                {
                    result = text.Substring(0, width);
                }
                else
                {
                    result = text;
                }
            }
            return result;
        }
    }
}

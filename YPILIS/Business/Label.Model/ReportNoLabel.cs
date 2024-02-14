using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ReportNoLabel
    {
        private string m_PatientFirstName;
        private string m_PatientLastName;
        private string m_ReportNo;
        private bool m_HighPriority;

        public ReportNoLabel(string patientFirstName, string patientLastName, string reportNo, bool highPriority)
        {
            this.m_PatientFirstName = patientFirstName;
            this.m_PatientLastName = patientLastName;
            this.m_ReportNo = reportNo;
            this.m_HighPriority = highPriority;
        }


        public void AppendCommands(StringBuilder zplString, int xOffset, int yOffset)
        {            
            string patientNameText = this.TruncateString(this.m_PatientLastName, 8) + ", " + this.TruncateString(this.m_PatientFirstName, 1);
            if (m_HighPriority == true) patientNameText = $"!{patientNameText}";
            zplString.Append("^FO" + (90 + xOffset) + "," + (30 + yOffset) + "^BXN,06,200^FDRPTN" + this.m_ReportNo + "^FS");
            zplString.Append("^FO" + (22 + xOffset) + "," + (150 + yOffset) + "^A0N,25,25^FD" + m_ReportNo + "^FS");
            zplString.Append("^FO" + (22 + xOffset) + "," + (190 + yOffset) + "^A0N,25,25^FD" + patientNameText + "^FS");
            zplString.Append("^FO" + (22 + xOffset) + "," + (230 + yOffset) + "^A0N,25,25^FDYPI Blgs^FS");             
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

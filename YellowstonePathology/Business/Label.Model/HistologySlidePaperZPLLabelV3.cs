using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlidePaperZPLLabelV3 : ZPLCommand
    {
        private string m_SlideOrderId;
        private string m_ReportNo;
        private string m_LastName;
        private string m_FirstName;      
        private string m_TestName;
        private string m_SlideLabel;
        private string m_Location;        
        private bool m_UseWetProtocol;
        private bool m_PerformedByHand;

        public HistologySlidePaperZPLLabelV3(string slideOrderId, string reportNo, string firstName, string lastName, string testName, string slideLabel, string location, bool useWetProtocol, bool performedByHand)
        {
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_LastName = lastName;
            this.m_FirstName = firstName;         
            this.m_TestName = testName;
            this.m_SlideLabel = slideLabel;
            this.m_Location = location;        
            this.m_UseWetProtocol = useWetProtocol;
            this.m_PerformedByHand = performedByHand;
        }

        public string GetCommandWithOffset(int xOffset)
        {
            StringBuilder result = new StringBuilder();
            string truncatedTestName = null;
            if (this.m_TestName.Length > 16)
            {
                truncatedTestName = this.m_TestName.Substring(0, 16);
            }
            else
            {
                truncatedTestName = this.m_TestName;
            }

            if (this.m_UseWetProtocol == true) truncatedTestName = truncatedTestName + "W";
            if (this.m_PerformedByHand == true) truncatedTestName = truncatedTestName + "H";

            string patientname = null;
            if (this.m_LastName.Length > 10)
            {
                patientname = this.m_LastName.Substring(0, 10);
            }
            else
            {
                patientname = this.m_LastName;
            }

            if(string.IsNullOrEmpty(this.m_FirstName) == false)
            {
                patientname = patientname + " " + this.m_FirstName.Substring(0, 1);
            }

            //A@N,18,18,E:VERDANA.TTF            
            result.Append("^FO35,033^A0N,42,42^FD" + this.m_ReportNo + "^FS");
            result.Append("^FO35,080^BXN,03,200,24,24^FDHSLD" + this.m_SlideOrderId + "^FS");
            result.Append("^FO35,185^A0N,25,25^FD" + patientname + "^FS");
            result.Append("^FO35,220^A0N,25,25^FDYPI-Blgs^FS");
            result.Append("^FO120,85^A0N,35,35^FD" + this.m_SlideLabel + "^FS");
            result.Append("^FO122,130^A0N,25,25^FD" + truncatedTestName + "^FS");

            return result.ToString();        
        }
    }
}

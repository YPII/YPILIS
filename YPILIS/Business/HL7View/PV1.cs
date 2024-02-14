using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View
{    
    public class PV1
    {
        private string m_PatientType;

        public PV1()
        {

        }

        public void FromHL7(string pv1Segment)
        {
            string[] split = pv1Segment.Split('|');
            this.m_PatientType = split[18];
        }

        public string PatientType
        {
            get { return this.m_PatientType; }
        }

        public string ConvertedPatientType
        {
            get
            {
                string result = this.m_PatientType;
                switch (this.m_PatientType)
                {
                    case "IN":
                    case "ER":
                    case "INO":
                    case "RCR":
                        result += "(IP)";
                        break;
                    case "CLI":
                    case "SDC":
                    case "REF":
                        result += "(OP)";
                        break;
                }
                return result;
            }
        }
    }
}

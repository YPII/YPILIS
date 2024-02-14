using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PlaceOfService
    {
        private string m_Name;
        private string m_Code;
        private string m_PatientType;
        private string m_SVHPatientClass;

        public PlaceOfService(string name, string code, string patientType, string svhPatientClass)
        {
            this.m_Name = name;
            this.m_Code = code;
            this.m_PatientType = patientType;
            this.m_SVHPatientClass = svhPatientClass;
        }

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        public string PatientType
        {
            get { return this.m_PatientType; }
            set { this.m_PatientType = value; }
        }

        public string SVHPatientClass
        {
            get { return this.m_SVHPatientClass; }
            set { this.m_SVHPatientClass = value; }
        }
    }
}

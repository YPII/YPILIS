using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
    public class PatientName
    {
        private string  m_FirstName;
        private string m_LastName;

        public PatientName()
        {
            
        }

        public PatientName(string lastName, string firstName)
        {
            this.m_LastName = lastName;
            this.m_FirstName = firstName;
        }

        public string FirstName
        {
            get { return this.m_FirstName; }
        }

        public string LastName
        {
            get { return this.m_LastName; }
        }

        public string GetInitials()
        {
            string result = null;
            if (string.IsNullOrEmpty(this.m_FirstName) != true) result += this.m_FirstName.Substring(0, 1).ToUpper();
            if (string.IsNullOrEmpty(this.m_LastName) != true) result += this.m_LastName.Substring(0, 1).ToUpper();
            return result;
        }

        public static bool TryParse(string patientNameString, out PatientName output)
        {
            bool result = false;
            PatientName patientName = null;
            string[] names = patientNameString.Split(new char[] { ',' });

            if (names[0].Length >= 3)
            {
                patientName = new PatientName();
                result = true;
                patientName.m_LastName = names[0].Trim();
                if (names.Length > 1)
                {
                    patientName.m_FirstName = names[1].Trim();
                }
            }

            output = patientName;
            return result;
        }
    }
}

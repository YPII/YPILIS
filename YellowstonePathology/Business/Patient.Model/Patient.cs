using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Patient.Model
{
    public class Patient
    {
        private string m_PatientId;
        private string m_FirstName;
        private string m_LastName;
        private Nullable<DateTime> m_Birthdate;

        public Patient(string firstName, string lastName)
        {
            this.m_FirstName = firstName;
            this.m_LastName = lastName;
        }

        public string PatientId
        {
            get { return this.m_PatientId; }
            set { this.m_PatientId = value; }
        }

        public string FirstName
        {
            get { return this.m_FirstName; }
            set { this.m_FirstName = value; }
        }

        public string LastName
        {
            get { return this.m_LastName; }
            set { this.m_LastName = value; }
        }

        public Nullable<DateTime> Birthdate
        {
            get { return this.m_Birthdate; }
            set { this.m_Birthdate = value; }
        }

        public string GetTruncatedFirstName(int length)
        {
            string result = this.m_FirstName;
            if (this.m_FirstName.Length > length)
            {
                result = result.Substring(0, length);
            }
            return result;
        }

        public string GetTruncatedLastName(int length)
        {
            string result = this.m_LastName;
            if (this.m_LastName.Length > length)
            {
                result = result.Substring(0, length);
            }
            return result;
        }
        
        public static string GetLastFirstDisplayName(string firstName, string lastName)
        {
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrEmpty(lastName) == false)
            {
                result.Append(lastName);
            }
            if (string.IsNullOrEmpty(firstName) == false)
            {
                result.Append(", " + firstName);
            }
            return result.ToString();
        }
    }
}

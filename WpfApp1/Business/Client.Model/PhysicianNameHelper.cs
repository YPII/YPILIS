using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianNameHelper
    {
        private string m_PhysicianNameSearchText;
        private string m_LastName;
        private string m_FirstName;
        private bool m_IsValid;
        private bool m_IsLastNameOnly;

        public PhysicianNameHelper(string physicianNameSearchText)
        {
            this.m_PhysicianNameSearchText = physicianNameSearchText;
            this.SetFirstName();
            this.SetLastName();
        }

        public string LastName
        {
            get { return this.m_LastName; }
        }

        public string FirstName
        {
            get { return this.m_FirstName; }
        }

        public bool IsValid
        {
            get { return this.m_IsValid; }
        }

        public bool IsLastNameOnly
        {
            get { return this.m_IsLastNameOnly; }
        }

        private void SetLastName()
        {                            
            if (this.m_PhysicianNameSearchText == string.Empty)
            {
                this.m_IsValid = false;
            }
            else
            {
                string[] spaceSplit = this.m_PhysicianNameSearchText.Split(' ');
                if (spaceSplit.Length == 2)
                {
                    this.m_LastName = spaceSplit[0];                    
                    this.m_IsValid = true;                    
                }
                else if (spaceSplit.Length >= 3)
                {
                    int indexOfLastSpace = this.m_PhysicianNameSearchText.LastIndexOf(' ');
                    this.m_LastName = this.m_PhysicianNameSearchText.Substring(indexOfLastSpace).Trim();
                }
                else
                {
                    this.m_LastName = this.m_PhysicianNameSearchText;
                    this.m_IsValid = true;
                    this.m_IsLastNameOnly = true;
                }
            }                            
        }

        private void SetFirstName()
        {                            
            if (this.m_PhysicianNameSearchText == string.Empty)
            {
                this.m_IsValid = false;
            }
            else
            {
                string[] spaceSplit = this.m_PhysicianNameSearchText.Split(' ');
                if (spaceSplit.Length == 2)
                {
                    this.m_FirstName = spaceSplit[1];
                    this.m_IsValid = true;
                }
                else if (spaceSplit.Length >= 3)
                {
                    int indexOfLastSpace = this.m_PhysicianNameSearchText.LastIndexOf(' ');
                    this.m_FirstName = this.m_PhysicianNameSearchText.Substring(0, indexOfLastSpace).Trim();
                }
                else
                {
                    this.m_IsValid = false;
                }
            }                
        }       
    }
}

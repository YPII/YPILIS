using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianClientNameHelper
    {
        private string m_PhysicianClientName;
        private string m_PhysicianName;
        private string m_ClientName;
        private bool m_IsValid;

        public PhysicianClientNameHelper(string physicianClientName)
        {
            this.m_PhysicianClientName = physicianClientName;
            this.SetPhysicianLastName();
            this.SetClientName();
        }

        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
        }

        public string ClientName
        {
            get { return this.m_ClientName; }
        }

        public bool IsValid
        {
            get { return this.m_IsValid; }
        }

        private void SetPhysicianLastName()
        {                            
            if (this.m_PhysicianClientName == string.Empty)
            {
                this.m_IsValid = false;
            }
            else
            {
                string[] spaceSplit = this.m_PhysicianClientName.Split(' ');
                if (spaceSplit.Length == 2)
                {
                    this.m_PhysicianName = spaceSplit[1];
                    this.m_IsValid = true;
                }
                else if (spaceSplit.Length >= 3)
                {
                    int indexOfLastSpace = this.m_PhysicianClientName.LastIndexOf(' ');
                    this.m_PhysicianName = this.m_PhysicianClientName.Substring(indexOfLastSpace).Trim();
                }
                else
                {
                    this.m_IsValid = false;
                }
            }                            
        }

        private void SetClientName()
        {                            
            if (this.m_PhysicianClientName == string.Empty)
            {
                this.m_IsValid = false;
            }
            else
            {
                string[] spaceSplit = this.m_PhysicianClientName.Split(' ');
                if (spaceSplit.Length == 2)
                {
                    this.m_ClientName = spaceSplit[0];
                    this.m_IsValid = true;
                }
                else if (spaceSplit.Length >= 3)
                {
                    int indexOfLastSpace = this.m_PhysicianClientName.LastIndexOf(' ');
                    this.m_ClientName = this.m_PhysicianClientName.Substring(0, indexOfLastSpace).Trim();
                }
                else
                {
                    this.m_IsValid = false;
                }
            }                
        }       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Monitor
{
    public class Courier
    {
        private string m_Name;
        private string m_Status;

        public Courier(string name, string status)
        {
            m_Name = name;
            m_Status = status;            
        }

        public string Name { get { return m_Name; } set { m_Name = value; } }
        public string Status { get { return m_Status; } set { m_Status = value; } }
    }
}

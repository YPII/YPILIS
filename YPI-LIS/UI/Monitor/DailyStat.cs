using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Monitor
{
    public class DailyStat
    {
        private string m_Name;
        private string m_Value;

        public DailyStat(string name, string value)
        {
            m_Name = name;
            m_Value = value;            
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string Value
        {
            get { return m_Value; } 
            set { m_Value = value; }
        }
    }
}

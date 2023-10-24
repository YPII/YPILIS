using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.MaterialTracking
{
    public class MaterialTrackingSummaryColumn
    {
        private string m_Name;
        private int m_Value;        

        public MaterialTrackingSummaryColumn(string name, int value)
        {
            this.m_Name = name;
            this.m_Value = value;            
        }

        public string Name
        {
            get { return this.m_Name; }
        }

        public int Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }        
    }
}

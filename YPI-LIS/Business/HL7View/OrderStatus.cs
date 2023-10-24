using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class OrderStatus
    {        
        private string m_Value;
        private string m_Description;

        public OrderStatus(string value, string description)
        {
            this.m_Value = value;
            this.m_Description = description;
        }

        public string Value
        {
            get { return this.m_Value; }
        }

        public string Description
        {
            get { return this.m_Description; }
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class MappingAttribute : System.Attribute
    {
        string m_Name;

        public MappingAttribute(string sqlName)
        {
            this.m_Name = sqlName;
        }

        public string Name
        {
            get { return this.m_Name; }
        }
    }
}
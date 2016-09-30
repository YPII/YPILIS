using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class ReflexOrderStatus
    {        
        private bool m_IsRequired;        

        public ReflexOrderStatus()
        {            
            this.m_IsRequired = false;            
        }        

        public bool IsRequired
        {
            get { return this.m_IsRequired; }
            set { this.m_IsRequired = value; }
        }               
    }
}

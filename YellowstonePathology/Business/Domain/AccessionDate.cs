using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{    
	public class WorkingDate
	{
        DateTime m_Date;
        
        public DateTime Date
        {
            get { return this.m_Date; }
            set { this.m_Date = value; }            
        }
	}
}

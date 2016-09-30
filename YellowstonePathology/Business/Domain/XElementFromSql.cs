using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{
	public class XElementFromSql : DomainBase
	{
		XElement m_Document;

		public XElementFromSql() 
        { 

        }

		public XElement Document
		{
			get { return this.m_Document; }
			set
			{
				this.m_Document = value;
				NotifyPropertyChanged("Document");
			}
		}
	}
}

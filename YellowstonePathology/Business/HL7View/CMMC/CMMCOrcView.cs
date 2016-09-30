using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.CMMC
{
	class CMMCOrcView
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_DateFormat = "yyyyMMddHHmm";
		private string m_ReportNo;

		public CMMCOrcView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
		}

		public void ToXml(XElement document)
		{
            document.Add(new XElement("ORC",
                new XElement("ORC.1",
                    new XElement("ORC.1.1", "RE")),                
                new XElement("ORC.3",
                    new XElement("ORC.3.1", m_ReportNo),
                    new XElement("ORC.3.2", "YPILIS")),
                new XElement("ORC.5",
                    new XElement("ORC.5.1", "CM")),
                new XElement("ORC.9",
                    new XElement("ORC.9.1", DateTime.Now.ToString(m_DateFormat)))));			
		}        
	}
}

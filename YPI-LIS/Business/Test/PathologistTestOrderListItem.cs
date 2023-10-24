using System;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test
{
	[XmlType("PathologistTestOrderListItem")]
	public class PathologistTestOrderListItem : ListItem
    {
		private string m_TestName;
		private string m_TestOrderDate;
		private string m_AliquotDescription;
        private string m_AliquotOrderId;
        private string m_TestOrderId;

        public PathologistTestOrderListItem()
        {
		}

		public string AliquotOrderId
		{
			get { return this.m_AliquotOrderId; }
			set { this.m_AliquotOrderId = value; }
		}

		public string TestName
		{
			get { return this.m_TestName; }
			set { this.m_TestName = value; }
		}

		public string TestOrderDate
		{
			get { return this.m_TestOrderDate; }
			set { this.m_TestOrderDate = value; }
		}

		public string AliquotDescription
		{
			get { return m_AliquotDescription; }
			set { this.m_AliquotDescription = value; }
		}

		public string TestOrderId
        {
            get { return this.m_TestOrderId; }
			set { this.m_TestOrderId = value; }
        }
	}
}

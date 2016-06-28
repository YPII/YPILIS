using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public class ShipmentListItem
	{
		#region Fields
		private string m_ShipmentId;
		private string m_ShipmentFrom;
		private string m_ShipmentTo;
		private Nullable<DateTime> m_ShipDate;
		private bool m_Shipped;
		private bool m_Received;
		private Nullable<DateTime> m_ReceivedDate;
		private int m_ReceivedById;
		private string m_ShipmentPreparedBy;
		#endregion

		public ShipmentListItem() {}

		#region Properties
		[DataMember]
		public string ShipmentId
		{
			get { return this.m_ShipmentId; }
			set { this.m_ShipmentId = value; }
		}

		[DataMember]
		public string ShipmentFrom
		{
			get { return this.m_ShipmentFrom; }
			set { this.m_ShipmentFrom = value; }
		}

		[DataMember]
		public string ShipmentTo
		{
			get { return this.m_ShipmentTo; }
			set { this.m_ShipmentTo = value; }
		}

		[DataMember]
		public Nullable<DateTime> ShipDate
		{
			get { return this.m_ShipDate; }
			set { this.m_ShipDate = value; }
		}

		[DataMember]
		public bool Shipped
		{
			get { return this.m_Shipped; }
			set { this.m_Shipped = value; }
		}

		[DataMember]
		public bool Received
		{
			get { return this.m_Received; }
			set { this.m_Received = value; }
		}

		[DataMember]
		public Nullable<DateTime> ReceivedDate
		{
			get { return this.m_ReceivedDate; }
			set { this.m_ReceivedDate = value; }
		}

		[DataMember]
		public int ReceivedById
		{
			get { return this.m_ReceivedById; }
			set { this.m_ReceivedById = value; }
		}

		[DataMember]
		public string ShipmentPreparedBy
		{
			get { return this.m_ShipmentPreparedBy; }
			set { this.m_ShipmentPreparedBy = value; }
		}
		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_ShipmentId = propertyWriter.WriteString("ShipmentId");
			this.m_ShipmentFrom = propertyWriter.WriteString("ShipmentFrom");
			this.m_ShipmentTo = propertyWriter.WriteString("ShipmentTo");
			this.m_ShipDate = propertyWriter.WriteNullableDateTime("ShipDate");
			this.m_Shipped = propertyWriter.WriteBoolean("Shipped");
			this.m_Received = propertyWriter.WriteBoolean("Received");
			this.m_ReceivedDate = propertyWriter.WriteNullableDateTime("ReceivedDate");
			this.m_ReceivedById = propertyWriter.WriteInt("ReceivedById");
			this.m_ShipmentPreparedBy = propertyWriter.WriteString("ShipmentPreparedBy");
		}
		#endregion
	}
}

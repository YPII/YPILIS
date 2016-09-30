using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	[DataContract(Namespace = "YellowstonePathology.Business.ClientOrder.Model")]
	public class ContainerIdLookupResponse
	{
		private bool m_Found;

		public ContainerIdLookupResponse()
		{
		}

		[DataMember]
		public bool Found
		{
			get { return this.m_Found; }
			set { this.m_Found = value; }
		}

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_Found = propertyWriter.WriteBoolean("Found");
		}
	}
}

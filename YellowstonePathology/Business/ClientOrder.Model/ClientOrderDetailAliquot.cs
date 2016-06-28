using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	[DataContract(Namespace = "YellowstonePathology.Business.ClientOrder.Model")]
	[PersistentClass("tblClientOrderDetailAliquot", "YPIDATA")]
	public class ClientOrderDetailAliquot
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_ClientOrderDetailAliquotId;
		private string m_ClientOrderDetailId;
		private string m_Description;

		public ClientOrderDetailAliquot()
		{
		}

		public ClientOrderDetailAliquot(string objectId, string clientOrderDetailAliquotId, string clientOrderDetailId)
		{
			this.m_ObjectId = objectId;
			this.m_ClientOrderDetailAliquotId = clientOrderDetailAliquotId;
			this.m_ClientOrderDetailId = clientOrderDetailId;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public XElement AsXML()
		{
			XElement clientOrderDetailAliquotElement = new XElement("ClientOrderDetailAliquot");
			YellowstonePathology.Business.Persistence.XmlPropertyReader clientOrderDetailAliquotPropertyWriter = new Persistence.XmlPropertyReader(this, clientOrderDetailAliquotElement);
			clientOrderDetailAliquotPropertyWriter.Write();
			return clientOrderDetailAliquotElement;
		}

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

		[PersistentPrimaryKeyProperty(false)]
		[DataMember]
		public string ClientOrderDetailAliquotId
		{
			get { return this.m_ClientOrderDetailAliquotId; }
			set
			{
				if (this.m_ClientOrderDetailAliquotId != value)
				{
					this.m_ClientOrderDetailAliquotId = value;
					this.NotifyPropertyChanged("ClientOrderDetailAliquotId");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string ClientOrderDetailId
		{
			get { return this.m_ClientOrderDetailId; }
			set
			{
				if (this.m_ClientOrderDetailId != value)
				{
					this.m_ClientOrderDetailId = value;
					this.NotifyPropertyChanged("ClientOrderDetailId");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}
	}
}

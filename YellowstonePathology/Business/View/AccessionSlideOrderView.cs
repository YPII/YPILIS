using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.View
{
	public partial class AccessionSlideOrderView : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Slide.Model.SlideOrder m_SlideOrder;

        private string m_MasterAccessionNo;
        private string m_ReportNo;
        private string m_PFirstName;
        private string m_PLastName;
        private int m_ClientId;
        private string m_ClientName;
		private int m_PhysicianId;
        private string m_PhysicianName;

		public AccessionSlideOrderView()
		{

		}

		public YellowstonePathology.Business.Slide.Model.SlideOrder SlideOrder
		{
			get { return this.m_SlideOrder; }
			set { this.m_SlideOrder = value; }
		}

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set
            {
                if (this.m_PFirstName != value)
                {
                    this.m_PFirstName = value;
                    this.NotifyPropertyChanged("PFirstName");
                }
            }
        }

        public string PLastName
        {
            get { return this.m_PLastName; }
            set
            {
                if (this.m_PLastName != value)
                {
                    this.m_PLastName = value;
                    this.NotifyPropertyChanged("PLastName");
                }
            }
        }

        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (this.m_ClientId != value)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");
                }
            }
        }

        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (this.m_ClientName != value)
                {
                    this.m_ClientName = value;
                    this.NotifyPropertyChanged("ClientName");
                }
            }
        }

		public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set
            {
                if (this.m_PhysicianId != value)
                {
                    this.m_PhysicianId = value;
                    this.NotifyPropertyChanged("PhysicianId");
                }
            }
        }

        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set
            {
                if (this.m_PhysicianName != value)
                {
                    this.m_PhysicianName = value;
                    this.NotifyPropertyChanged("PhysicianName");
                }
            }
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
            this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
            this.m_ReportNo = propertyWriter.WriteString("ReportNo");
            this.m_PFirstName = propertyWriter.WriteString("PFirstName");
            this.m_PLastName = propertyWriter.WriteString("PLastName");
            this.m_ClientId = propertyWriter.WriteInt("ClientId");
            this.m_ClientName = propertyWriter.WriteString("ClientName");
			this.m_PhysicianId = propertyWriter.WriteInt("PhysicianId");
            this.m_PhysicianName = propertyWriter.WriteString("PhysicianName");
        }

        public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
        {
        }

        public XElement ToXml()
        {            
            XElement result = new XElement("AccessionSlideOrderView");
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "MasterAccessionNo", MasterAccessionNo);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "ReportNo", ReportNo);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "PFirstName", PFirstName);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "PLastName", PLastName);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "ClientId", ClientId.ToString());
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "ClientName", ClientName);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "PhysicianId", PhysicianId);
            YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "PhysicianName", PhysicianName);
            return result;
        }	
	}
}

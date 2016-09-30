using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain
{
	public class Temp
    {
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private int m_TempId;
        private Nullable<DateTime> m_TempDate;
        private int m_Hello;

        public Temp()
        {

        }

        public int TempId
        {
            get { return this.m_TempId; }
            set { this.m_TempId = value; }
        }

        public Nullable<DateTime> TempDate
        {
            get { return this.m_TempDate; }
            set { this.m_TempDate = value; }
        }

        public int Hello
        {
            get { return this.m_Hello; }
            set { this.m_Hello = value; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		#region Serialization
		public void FromXml(XElement xml)
		{
			if (xml.Element("TempId") != null) m_TempId = Convert.ToInt32(xml.Element("TempId").Value);
			if (xml.Element("TempDate") != null) m_TempDate = DateTime.Parse(xml.Element("TempDate").Value);
			if (xml.Element("Hello") != null) m_Hello = Convert.ToInt32(xml.Element("Hello").Value);
		}

		public XElement ToXml()
		{
			XElement result = new XElement("Temp");
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "TempId", TempId.ToString());
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "TempDate", TempDate);
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.Serialize(result, "Hello", Hello.ToString());
			return result;
		}
		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			throw new NotImplementedException("WriteProperties not implemented");
		}
		#endregion

		#region ReadPropertiesMethod
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			throw new NotImplementedException("ReadProperties not implemented");
		}
		#endregion
	}
}

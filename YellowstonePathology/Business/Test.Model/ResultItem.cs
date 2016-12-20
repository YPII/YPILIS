using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YellowstonePathology.Test.Model
{
	[XmlType("ResultItem")]
	public class ResultItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private int m_ResultId;
        private string m_Result;

		public ResultItem()
		{

		}

        public ResultItem(int resultId, string result)
        {
            this.m_ResultId = resultId;
            this.m_Result = result;
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		#region Serialization
		public virtual void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not Implemented");
		}

		public virtual XElement ToXml()
		{
			throw new NotImplementedException("ToXml not Implemented");
		}
		#endregion		

		#region Properties
		public int ResultId
		{
			get { return this.m_ResultId; }
			set
			{
				if (this.m_ResultId != value)
				{
					this.m_ResultId = value;
					this.NotifyPropertyChanged("ResultId");
				}
			}
		}

		public string Result
		{
			get { return this.m_Result; }
			set
			{
				if (this.m_Result != value)
				{
					this.m_Result = value;
					this.NotifyPropertyChanged("Result");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_ResultId = propertyWriter.WriteInt("ResultId");
			this.m_Result = propertyWriter.WriteString("Result");
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

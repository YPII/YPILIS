using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public partial class OrderableTestCptCode
	{
		#region Fields
		private int m_OrderableTestCptCodeId;
		private int m_OrderableTestId;
		private string m_CptCode;
		#endregion

		#region Properties
		public int OrderableTestCptCodeId
		{
			get { return this.m_OrderableTestCptCodeId; }
			set
			{
				if(this.m_OrderableTestCptCodeId != value)
				{
					this.m_OrderableTestCptCodeId = value;
					this.NotifyPropertyChanged("OrderableTestCptCodeId");
				}
			}
		}

		public int OrderableTestId
		{
			get { return this.m_OrderableTestId; }
			set
			{
				if(this.m_OrderableTestId != value)
				{
					this.m_OrderableTestId = value;
					this.NotifyPropertyChanged("OrderableTestId");
				}
			}
		}

		public string CptCode
		{
			get { return this.m_CptCode; }
			set
			{
				if(this.m_CptCode != value)
				{
					this.m_CptCode = value;
					this.NotifyPropertyChanged("CptCode");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_OrderableTestCptCodeId = propertyWriter.WriteInt("OrderableTestCptCodeId");
			this.m_OrderableTestId = propertyWriter.WriteInt("OrderableTestId");
			this.m_CptCode = propertyWriter.WriteString("CptCode");
		}
		#endregion
	}
}

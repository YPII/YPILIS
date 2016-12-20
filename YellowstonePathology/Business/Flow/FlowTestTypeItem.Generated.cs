using System;

namespace YellowstonePathology.Business.Flow
{
	public partial class FlowTestTypeItem
	{
		bool m_IsActive;
		string m_TestAbbreviation;
		int m_TestId;
		string m_TestName;
		int m_PanelSetId;

		public bool IsActive
		{
			get { return this.m_IsActive; }
			set
			{
				if (value != this.m_IsActive)
				{
					this.m_IsActive = value;
					this.NotifyPropertyChanged("IsActive");
				}
			}
		}

		public string TestAbbreviation
		{
			get { return this.m_TestAbbreviation; }
			set
			{
				if (value != this.m_TestAbbreviation)
				{
					this.m_TestAbbreviation = value;
					this.NotifyPropertyChanged("TestAbbreviation");
				}
			}
		}

		public int TestId
		{
			get { return this.m_TestId; }
			set
			{
				if (value != this.m_TestId)
				{
					this.m_TestId = value;
					this.NotifyPropertyChanged("TestId");
				}
			}
		}

		public string TestName
		{
			get { return this.m_TestName; }
			set
			{
				if (value != this.m_TestName)
				{
					this.m_TestName = value;
					this.NotifyPropertyChanged("TestName");
				}
			}
		}

		public int PanelSetId
		{
			get { return this.m_PanelSetId; }
			set
			{
				if (value != this.m_PanelSetId)
				{
					this.m_PanelSetId = value;
					this.NotifyPropertyChanged("PanelSetId");
				}
			}
		}
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_IsActive = propertyWriter.WriteBoolean("IsActive");
			this.m_TestAbbreviation = propertyWriter.WriteString("TestAbbreviation");
			this.m_TestId = propertyWriter.WriteInt("TestId");
			this.m_TestName = propertyWriter.WriteString("TestName");
			this.m_PanelSetId = propertyWriter.WriteInt("PanelSetId");
		}
	}
}

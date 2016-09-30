using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.ComponentModel;

namespace YellowstonePathology.Business.PanelSet.Model
{
	[XmlType("PanelSetTestResultList")]
	public class PanelSetTestResultList : ObservableCollection<PanelSetTestResultListItem>
	{
		public PanelSetTestResultList()
        {
		}
	}

	public class PanelSetTestResultListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_PanelSetTestResultId;
		private int m_TestId;
		private int m_PanelSetId;
		private string m_TestName;

		public PanelSetTestResultListItem()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public int PanelSetTestResultId
		{
			get { return this.m_PanelSetTestResultId; }
			set
			{
				if (this.m_PanelSetTestResultId != value)
				{
					this.m_PanelSetTestResultId = value;
					NotifyPropertyChanged("PanelSetTestResultId");
				}
			}
		}

		public int TestId
		{
			get { return this.m_TestId; }
			set
			{
				if (this.m_TestId != value)
				{
					this.m_TestId = value;
					NotifyPropertyChanged("TestId");
				}
			}
		}

		public int PanelSetId
		{
			get { return this.m_PanelSetId; }
			set
			{
				if (this.m_PanelSetId != value)
				{
					this.m_PanelSetId = value;
					NotifyPropertyChanged("PanelSestId");
				}
			}
		}

		public string TestName
		{
			get { return this.m_TestName; }
			set
			{
				if (this.m_TestName != value)
				{
					this.m_TestName = value;
					NotifyPropertyChanged("TestName");
				}
			}
		}
	}
}

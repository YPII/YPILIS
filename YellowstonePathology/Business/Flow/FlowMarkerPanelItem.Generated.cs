using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace YellowstonePathology.Business.Flow
{
	[YellowstonePathology.Business.CustomAttributes.SqlTableAttribute("tblFlowMarkerPanel", "MarkerPanelId", SqlDbType.Int, 4, false)]
	public partial class FlowMarkerPanelItem : BaseItem
	{
		string m_Intensity = string.Empty;
		string m_Interpretation = string.Empty;
		string m_MarkerName = string.Empty;
		int m_MarkerPanelId;
		int m_PanelId;
		string m_PanelName = string.Empty;
		string m_Reference = string.Empty;

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Intensity", 50, SqlDbType.VarChar, false)]
		public string Intensity
		{
			get { return this.m_Intensity; }
			set
			{
				if (value != this.m_Intensity)
				{
					this.m_Intensity = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Intensity", this.m_Intensity);
					this.NotifyPropertyChanged("Intensity");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Interpretation", 50, SqlDbType.VarChar, false)]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				if (value != this.m_Interpretation)
				{
					this.m_Interpretation = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Interpretation", this.m_Interpretation);
					this.NotifyPropertyChanged("Interpretation");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("MarkerName", 50, SqlDbType.VarChar, false)]
		public string MarkerName
		{
			get { return this.m_MarkerName; }
			set
			{
				if (value != this.m_MarkerName)
				{
					this.m_MarkerName = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "MarkerName", this.m_MarkerName);
					this.NotifyPropertyChanged("MarkerName");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("MarkerPanelId", 4, SqlDbType.Int, false)]
		public int MarkerPanelId
		{
			get { return this.m_MarkerPanelId; }
			set
			{
				if (value != this.m_MarkerPanelId)
				{
					this.m_MarkerPanelId = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "MarkerPanelId", this.m_MarkerPanelId);
					this.NotifyPropertyChanged("MarkerPanelId");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("PanelId", 4, SqlDbType.Int, false)]
		public int PanelId
		{
			get { return this.m_PanelId; }
			set
			{
				if (value != this.m_PanelId)
				{
					this.m_PanelId = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "PanelId", this.m_PanelId);
					this.NotifyPropertyChanged("PanelId");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("PanelName", 50, SqlDbType.VarChar, false)]
		public string PanelName
		{
			get { return this.m_PanelName; }
			set
			{
				if (value != this.m_PanelName)
				{
					this.m_PanelName = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "PanelName", this.m_PanelName);
					this.NotifyPropertyChanged("PanelName");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Reference", 50, SqlDbType.VarChar, false)]
		public string Reference
		{
			get { return this.m_Reference; }
			set
			{
				if (value != this.m_Reference)
				{
					this.m_Reference = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Reference", this.m_Reference);
					this.NotifyPropertyChanged("Reference");
				}
			}
		}
	}
}

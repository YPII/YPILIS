using System;
using System.Data;

namespace YellowstonePathology.Business.Flow
{
	[YellowstonePathology.Business.CustomAttributes.SqlTableAttribute("tblMarkers", "MarkerId", SqlDbType.Int, 4)]
	public partial class MarkerItem : BaseItem
	{
		string m_CPTCode = string.Empty;
		bool m_IsMyelodysplastic;
		bool m_IsNormalMarker;
		int m_MarkerId;
		string m_MarkerName = string.Empty;
		int m_OrderFlag;
		bool m_Predictive;
		int m_QTYBill;
		string m_Type = string.Empty;

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("CPTCode", 20, SqlDbType.VarChar)]
		public string CPTCode
		{
			get { return this.m_CPTCode; }
			set
			{
				if (value != this.m_CPTCode)
				{
					this.m_CPTCode = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "CPTCode", this.m_CPTCode);
					this.NotifyPropertyChanged("CPTCode");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("IsMyelodysplastic", 1, SqlDbType.Bit)]
		public bool IsMyelodysplastic
		{
			get { return this.m_IsMyelodysplastic; }
			set
			{
				if (value != this.m_IsMyelodysplastic)
				{
					this.m_IsMyelodysplastic = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "IsMyelodysplastic", this.m_IsMyelodysplastic);
					this.NotifyPropertyChanged("IsMyelodysplastic");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("IsNormalMarker", 1, SqlDbType.Bit)]
		public bool IsNormalMarker
		{
			get { return this.m_IsNormalMarker; }
			set
			{
				if (value != this.m_IsNormalMarker)
				{
					this.m_IsNormalMarker = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "IsNormalMarker", this.m_IsNormalMarker);
					this.NotifyPropertyChanged("IsNormalMarker");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("MarkerId", 4, SqlDbType.Int)]
		public int MarkerId
		{
			get { return this.m_MarkerId; }
			set
			{
				if (value != this.m_MarkerId)
				{
					this.m_MarkerId = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "MarkerId", this.m_MarkerId);
					this.NotifyPropertyChanged("MarkerId");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("MarkerName", 50, SqlDbType.VarChar)]
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

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("OrderFlag", 4, SqlDbType.Int)]
		public int OrderFlag
		{
			get { return this.m_OrderFlag; }
			set
			{
				if (value != this.m_OrderFlag)
				{
					this.m_OrderFlag = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "OrderFlag", this.m_OrderFlag);
					this.NotifyPropertyChanged("OrderFlag");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Predictive", 1, SqlDbType.Bit)]
		public bool Predictive
		{
			get { return this.m_Predictive; }
			set
			{
				if (value != this.m_Predictive)
				{
					this.m_Predictive = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Predictive", this.m_Predictive);
					this.NotifyPropertyChanged("Predictive");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("QTYBill", 4, SqlDbType.Int)]
		public int QTYBill
		{
			get { return this.m_QTYBill; }
			set
			{
				if (value != this.m_QTYBill)
				{
					this.m_QTYBill = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "QTYBill", this.m_QTYBill);
					this.NotifyPropertyChanged("QTYBill");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute("Type", 30, SqlDbType.VarChar)]
		public string Type
		{
			get { return this.m_Type; }
			set
			{
				if (value != this.m_Type)
				{
					this.m_Type = value;
					this.SetPropertyChangedItem(this.m_PropertyChangedList, "Type", this.m_Type);
					this.NotifyPropertyChanged("Type");
				}
			}
		}
	}
}

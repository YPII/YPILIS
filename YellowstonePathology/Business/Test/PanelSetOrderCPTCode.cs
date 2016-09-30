using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test
{
	[PersistentClass("tblPanelSetOrderCPTCode", "YPIDATA")]
	public class PanelSetOrderCPTCode : INotifyPropertyChanged
	{			
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_PanelSetOrderCPTCodeId;
        private string m_ReferenceId;
		private string m_ReportNo;
        private int m_ClientId;
		private int m_Quantity;		
		private string m_CPTCode;
		private string m_Modifier;				
        private string m_CodeableType;
        private string m_CodeableDescription;
        private string m_EntryType;
		private string m_SpecimenOrderId;
        private Nullable<DateTime> m_PostDate;
        private string m_CodeType;

        public PanelSetOrderCPTCode()
        {

        }

		public PanelSetOrderCPTCode(string reportNo, string objectId, string panelSetOrderCPTCodeId)
		{
			this.m_ReportNo = reportNo;
			this.m_ObjectId = objectId;
			this.m_PanelSetOrderCPTCodeId = panelSetOrderCPTCodeId;
		}

		public string PostDateProxy
		{
			get
			{
				if (this.PostDate.HasValue == false)
				{
					return null;
				}
				else
				{
					return this.PostDate.Value.ToShortDateString();
				}
			}

			set
			{
				string strValue = value.ToString();
				if (strValue == string.Empty)
				{
					this.PostDate = null;
				}
				else
				{
					this.PostDate = DateTime.Parse(strValue);
				}
			}
		}

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string PanelSetOrderCPTCodeId
		{
            get { return this.m_PanelSetOrderCPTCodeId; }
			set
			{
                if (this.m_PanelSetOrderCPTCodeId != value)
				{
                    this.m_PanelSetOrderCPTCodeId = value;
                    this.NotifyPropertyChanged("PanelSetOrderCPTCodeId");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ReferenceId
        {
            get { return this.m_ReferenceId; }
            set
            {
                if (this.m_ReferenceId != value)
                {
                    this.m_ReferenceId = value;
                    this.NotifyPropertyChanged("ReferenceId");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if(this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
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

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "null", "int")]
		public int Quantity
		{
			get { return this.m_Quantity; }
			set
			{
				if(this.m_Quantity != value)
				{
					this.m_Quantity = value;
					this.NotifyPropertyChanged("Quantity");
				}
			}
		}								

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string CPTCode
		{
			get { return this.m_CPTCode; }
			set
			{
				if(this.m_CPTCode != value)
				{
					this.m_CPTCode = value;
					this.NotifyPropertyChanged("CPTCode");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Modifier
		{
			get { return this.m_Modifier; }
			set
			{
				if(this.m_Modifier != value)
				{
					this.m_Modifier = value;
					this.NotifyPropertyChanged("Modifier");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string CodeableDescription
		{
			get { return this.m_CodeableDescription; }
			set
			{
                if (this.m_CodeableDescription != value)
				{
                    this.m_CodeableDescription = value;
                    this.NotifyPropertyChanged("CodeableDescription");
				}
			}
		}		      

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string CodeableType
        {
            get { return this.m_CodeableType; }
            set
            {
                if (this.m_CodeableType != value)
                {
                    this.m_CodeableType = value;
                    this.NotifyPropertyChanged("CodeableType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string EntryType
        {
            get { return this.m_EntryType; }
            set
            {
                if (this.m_EntryType != value)
                {
                    this.m_EntryType = value;
                    this.NotifyPropertyChanged("EntryType");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string SpecimenOrderId
		{
			get { return this.m_SpecimenOrderId; }
			set
			{
				if (this.m_SpecimenOrderId != value)
				{
					this.m_SpecimenOrderId = value;
					this.NotifyPropertyChanged("SpecimenOrderId");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> PostDate
        {
            get { return this.m_PostDate; }
            set
            {
                if (this.m_PostDate != value)
                {
                    this.m_PostDate = value;
                    this.NotifyPropertyChanged("PostDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string CodeType
        {
            get { return this.m_CodeType; }
            set
            {
                if (this.m_CodeType != value)
                {
                    this.m_CodeType = value;
                    this.NotifyPropertyChanged("CodeType");
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
	}
}


















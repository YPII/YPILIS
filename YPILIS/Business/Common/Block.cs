using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
	public class Block : YellowstonePathology.Business.Domain.DomainBase
	{        		
		protected string m_CompanyId = "YPII";
		protected string m_ScanningPrefix = "ALQ";
		protected string m_CassetteColor;
		protected string m_BlockTitle;
		protected string m_PatientInitials;
		protected string m_BlockId;
		protected bool m_PrintRequested;
		protected bool m_Verified;
		protected string m_ReportNo;
		protected string m_MasterAccessionNo;        

		public Block()
		{
            
		}        

		public string ReportNo
		{
			get
			{
				return this.m_ReportNo;
			}
			set
			{
				if (value != this.m_ReportNo)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

		public string MasterAccessionNo
		{
			get
			{
				return this.m_MasterAccessionNo;
			}
			set
			{
				if (value != this.m_MasterAccessionNo)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

        public string BlockTitle
        {
			get { return this.m_BlockTitle; }
			set
			{
				if (value != this.m_BlockTitle)
				{
					this.m_BlockTitle = value;
					this.NotifyPropertyChanged("BlockTitle");
				}
			}
        }

		public string BlockId
		{
			get { return this.m_BlockId; }
			set
			{
				if (value != this.m_BlockId)
				{
					this.m_BlockId = value;
					this.NotifyPropertyChanged("BlockId");
				}
			}
		}

        public string PatientInitials
        {
			get { return this.m_PatientInitials; }
			set
			{
				if (value != this.m_PatientInitials)
				{
					this.m_PatientInitials = value;
					this.NotifyPropertyChanged("PatientInitials");
				}
			}
        }

        public string CassetteColor
        {
            get { return this.m_CassetteColor; }
			set
			{
				if (value != this.m_CassetteColor)
				{
					this.m_CassetteColor = value;
					this.NotifyPropertyChanged("CassetteColor");
				}
			}
        }

		public bool PrintRequested
		{
			get { return this.m_PrintRequested; }
			set
			{
				if (value != this.m_PrintRequested)
				{
					this.m_PrintRequested = value;
					this.NotifyPropertyChanged("PrintRequested");
				}
			}
		}

		public bool Verified
		{
			get { return this.m_Verified; }
			set
			{
				if (value != this.m_Verified)
				{
					this.m_Verified = value;
					this.NotifyPropertyChanged("Verified");
				}
			}
		}		

        public string CompanyId
        {
            get { return this.m_CompanyId; }
        }

        public string ScanningId
        {
            get { return this.m_ScanningPrefix + this.BlockId.ToString(); }            
        }                    
	}
}

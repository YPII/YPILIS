using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	[Table(Name = "tblIcd9Code")]
	public class Icd9Code : DomainBase
	{		
		private string m_Code;		

		public Icd9Code()
		{
			
		}				

		[Column(Storage = "m_Code", Name="ICD9Code", DbType = "VarChar(50)")]
		public string Code
		{
			get { return this.m_Code; }
			set
			{
				if ((this.m_Code != value))
				{
					this.m_Code = value;
					this.NotifyPropertyChanged("Code");
				}
			}
		}		
	}
}


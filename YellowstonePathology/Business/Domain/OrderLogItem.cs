using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{
	[PersistentClass(false, "", "YPIDATA")]
	public class OrderLogItem
	{
		private string m_ReportNo;
		private string m_Initials;
		private string m_TestName;
		private string m_Description;
		private Nullable<DateTime> m_OrderTime;
		private string m_ProcedureComment;

		public OrderLogItem()
		{
		}

		[PersistentDocumentIdProperty()]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set { this.m_ReportNo = value; }
		}

        [PersistentProperty()]
		public string Initials
		{
			get { return this.m_Initials; }
			set { this.m_Initials = value; }
		}

        [PersistentProperty()]
		public string TestName
		{
			get { return this.m_TestName; }
			set { this.m_TestName = value; }
		}

        [PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set { this.m_Description = value; }
		}

        [PersistentProperty()]
		public Nullable<DateTime> OrderTime
		{
			get { return this.m_OrderTime; }
			set { this.m_OrderTime = value; }
		}

        [PersistentProperty()]
		public string ProcedureComment
		{
			get { return this.m_ProcedureComment; }
			set { this.m_ProcedureComment = value; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{	
	public class OrderLogItem
	{
		private string m_ReportNo;
		private string m_Initials;
		private string m_TestName;
		private string m_Description;
		private Nullable<DateTime> m_OrderTime;
		private string m_ProcedureComment;
        private string m_Comment;
        private string m_LastName;
		private string m_CutBy;

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

        [PersistentProperty()]
        public string Comment
        {
            get { return this.m_Comment; }
            set { this.m_Comment = value; }
        }

        [PersistentProperty()]
        public string LastName
        {
            get { return this.m_LastName; }
            set { this.m_LastName = value; }
        }

		[PersistentProperty()]
		public string CutBy
		{
			get { return this.m_CutBy; }
			set { this.m_CutBy = value; }
		}
	}
}

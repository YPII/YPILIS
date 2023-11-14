using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace YellowstonePathology.Business.Domain.Core
{
	[Table(Name = "tblComment")]
	public class Comment : DomainBase
	{
		private int m_CommentId;
		private string m_Description;
		private string m_Comment;

		public Comment() { }

		[Column(Name = "CommentId", Storage = "m_CommentId", IsPrimaryKey = true, IsDbGenerated = true)]
		public int CommentId
		{
			get { return this.m_CommentId; }
			set
			{
				if (this.m_CommentId != value)
				{
					this.m_CommentId = value;
					this.NotifyPropertyChanged("CommentId");
				}
			}
		}

		[Column(Name = "Description", Storage = "m_Description")]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		[Column(Name = "Comment", Storage = "m_Comment")]
		public string ActualComment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("ActualComment");
				}
			}
		}
	}
}

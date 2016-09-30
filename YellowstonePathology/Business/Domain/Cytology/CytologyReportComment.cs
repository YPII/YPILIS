using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain.Cytology
{
	public partial class CytologyReportComment : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_CommentId;
		private string m_Comment;
		private string m_AbbreviatedComment;

		public CytologyReportComment()
		{

		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentProperty()]
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

		[PersistentProperty()]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[PersistentProperty()]
		public string AbbreviatedComment
		{
			get { return this.m_AbbreviatedComment; }
			set
			{
				if (this.m_AbbreviatedComment != value)
				{
					this.m_AbbreviatedComment = value;
					this.NotifyPropertyChanged("AbbreviatedComment");
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{
	public partial class ImmunoComment
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_ImmunoCommentId;
		private string m_Comment;

		public ImmunoComment()
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
		public int ImmunoCommentId
		{
			get { return this.m_ImmunoCommentId; }
			set
			{
				if (this.m_ImmunoCommentId != value)
				{
					this.m_ImmunoCommentId = value;
					this.NotifyPropertyChanged("ImmunoCommentId");
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
	}
}

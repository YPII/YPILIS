using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Cytology.Model
{
	public partial class SpecimenAdequacyComment : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private int m_CommentId;
        private string m_Comment;	

		public SpecimenAdequacyComment()
		{

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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}

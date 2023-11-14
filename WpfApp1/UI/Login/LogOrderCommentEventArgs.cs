using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
    public class LogOrderCommentEventArgs
    {
        private YellowstonePathology.Business.Interface.IOrderComment m_OrderComment;
        string m_Comment;

		public LogOrderCommentEventArgs(YellowstonePathology.Business.Interface.IOrderComment orderComment, string comment)
        {
			this.m_OrderComment = orderComment;
            this.m_Comment = comment;
        }

		public YellowstonePathology.Business.Interface.IOrderComment OrderComment
        {
			get { return this.m_OrderComment; }
        }

        public string Comment
        {
            get { return this.m_Comment; }
        }
    }
}

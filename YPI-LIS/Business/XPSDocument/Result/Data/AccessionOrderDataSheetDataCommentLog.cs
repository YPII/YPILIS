using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class AccessionOrderDataSheetDataCommentLog
    {

        private string m_LoggedBy;
        private string m_Description;
        private string m_Comment;
        private string m_LogDate;

        public AccessionOrderDataSheetDataCommentLog(Domain.OrderCommentLog orderCommentLog)
        {
            this.m_LoggedBy = string.IsNullOrEmpty(orderCommentLog.LoggedBy) ? orderCommentLog.LoggedBy : string.Empty;
            this.m_Description = string.IsNullOrEmpty(orderCommentLog.Description) ? orderCommentLog.Description : string.Empty;
            this.m_Comment = string.IsNullOrEmpty(orderCommentLog.Comment) ? orderCommentLog.Comment : string.Empty;
            this.m_LogDate = orderCommentLog.LogDate.ToShortDateString() + " " + orderCommentLog.LogDate.ToShortTimeString();
        }

        public string LoggedBy
        {
            get { return m_LoggedBy; }
        }

        public string Description
        {
            get { return m_Description; }
        }

        public string Comment
        {
            get { return m_Comment; }
        }

        public string LogDate
        {
            get { return m_LogDate; }
        }
    }
}

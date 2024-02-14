using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class TimeToFixationException
    {
        private string m_TimeToFixationExceptionId;
        private string m_Comment;

        public TimeToFixationException()
        {

        }

        public TimeToFixationException(string id, string comment)
        {
            this.m_TimeToFixationExceptionId = id;
            this.m_Comment = comment;
        }

        public string TimeToFixationExceptionId
        {
            get { return this.m_TimeToFixationExceptionId; }
            set { this.m_TimeToFixationExceptionId = value; }
        }

        public string Comment
        {
            get { return this.m_Comment; }
            set { this.m_Comment = value; }
        }
    }
}

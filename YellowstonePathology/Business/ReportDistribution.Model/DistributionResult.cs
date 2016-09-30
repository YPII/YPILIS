using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class DistributionResult
    {
        private bool m_IsComplete;
        private string m_Message;

        public DistributionResult()
        {
            this.m_IsComplete = false;
        }

        public bool IsComplete
        {
            get { return this.m_IsComplete; }
            set { this.m_IsComplete = value; }
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PQRSCode : CptCode
    {
        protected string m_ReportingDefinition;
        protected string m_FormattedReportingDefinition;
        protected string m_Comment;

        public PQRSCode()
        {
        }

        [PersistentProperty()]
        public string ReportingDefinition
        {
            get { return this.m_ReportingDefinition; }
            set { this.m_ReportingDefinition = value; }
        }

        [PersistentProperty()]
        public string Comment
        {
            get { return this.m_Comment; }
            set { this.m_Comment = value; }
        }

        public string FormattedReportingDefinition
        {
            get
            {                
                StringBuilder result = new StringBuilder(this.m_Code);
                if (this.m_Modifier != null) result.Append("-" + this.m_Modifier);
                result.Append(":  ");
                result.Append(this.m_ReportingDefinition);
                if (string.IsNullOrEmpty(this.m_Comment) == false) result.Append(Environment.NewLine + this.m_Comment);
                return result.ToString();
            }
            set { this.m_FormattedReportingDefinition = value; }
        }

        public override CptCode Clone(CptCode cptCodeIn)
        {
            PQRSCode codeIn = (PQRSCode)cptCodeIn;
            return (CptCode)codeIn.MemberwiseClone();
        }        
    }
}

using System;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Validation
{
    public class BrokenRuleItem
    {
        string m_AccessionNo = string.Empty;
        string m_RuleName = string.Empty;
        string m_Description = string.Empty;
        string m_Severity = string.Empty;

        public BrokenRuleItem()
        {

        }

        public string AccessionNo
        {
            get { return this.m_AccessionNo; }
            set { this.m_AccessionNo = value; }
        }

        public string RuleName
        {
            get { return this.m_RuleName; }
            set { this.m_RuleName = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public string Severity
        {
            get { return this.m_Severity; }
            set { this.m_Severity = value; }
        }

        public void Fill(MySqlDataReader dr)
        {
            this.AccessionNo = BaseData.GetStringValue("AccessionNo", dr);
            this.Description = BaseData.GetStringValue("Description", dr);
            this.RuleName = BaseData.GetStringValue("RuleName", dr);
            this.Severity = BaseData.GetStringValue("Severity", dr);
        }
    }
}

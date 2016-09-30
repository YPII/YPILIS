using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class ReportNoField : Interface.ISearchField
    {
        string m_XmlStatement;
        string m_ReportNo;        
        string m_SqlFieldName;
        string m_Condition;

        public ReportNoField()
        {
			this.m_XmlStatement = "po.ReportNo=\"'#REPORTNO#'\" = 1";

            //TODO: Done added to make assignment
            this.m_Condition = string.Empty;
            this.m_SqlFieldName = string.Empty;
        }        

        public string Value
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        public string Condition
        {
            get { return this.m_Condition; }            
        }

        public string SqlFieldName
        {
            get { return this.m_SqlFieldName; }            
        }

        public override string ToString()
        {
            string result = this.m_XmlStatement.Replace("#REPORTNO#", this.Value);            
            return result;
        }       
    }
}

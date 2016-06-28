using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Reports
{
    public enum ReportCategory { Billing, Surgical, Cytology, Flow, Molecular }

    public abstract class ReportBase
    {
        protected string m_ReportName;
        protected ReportCategory m_ReportCategory;

        public ReportBase()
        {

        }

        public virtual void PublishReport()
        {

        }

        public virtual void ViewReport()
        {

        }

        public string ReportName
        {
            get { return this.m_ReportName; }
            set { this.m_ReportName = value; }
        }

        public ReportCategory ReportCategory
        {
            get { return this.m_ReportCategory; }
            set { this.m_ReportCategory = value; }
        }

        public virtual void SetParameterUIVisibility(List<UIParameterListItem> uiParameterList)
        {            
            
        }
    }
}

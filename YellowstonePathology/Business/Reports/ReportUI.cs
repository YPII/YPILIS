using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Reports
{
	/*public class ReportUI
	{
        List<YellowstonePathology.Business.Reports.ReportBase> m_BillingReportList;
        List<UIParameterListItem> m_UIParameterList;
        ReportBase m_Report;

        public ReportUI()
        {
            this.m_BillingReportList = new List<ReportBase>();
            this.m_BillingReportList.Add(new YellowstonePathology.Business.Reports.Billing.YPIISummary());

            this.m_UIParameterList = new List<UIParameterListItem>();
            this.m_UIParameterList.Add(new UIParameterListItem("StartDate", System.Windows.Visibility.Collapsed));
            this.m_UIParameterList.Add(new UIParameterListItem("EndDate", System.Windows.Visibility.Collapsed));
            this.m_UIParameterList.Add(new UIParameterListItem("BillingLocation", System.Windows.Visibility.Collapsed));                        
        }

        public YellowstonePathology.Business.Reports.ReportBase Report
        {
            get { return this.m_Report; }
            set { this.m_Report = value; }
        }

        public List<UIParameterListItem> UIParameterList
        {
            get { return this.m_UIParameterList; }
        }

        public List<YellowstonePathology.Business.Reports.ReportBase> BillingReportList
        {
            get { return this.m_BillingReportList; }
        }
	}*/

    public class UIParameterListItem
    {
        string m_Name;
        System.Windows.Visibility m_Visibility;
            
        public UIParameterListItem(string parameterName, System.Windows.Visibility visibility)
        {
            this.m_Visibility = visibility;
            this.m_Name = parameterName;
        }

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public System.Windows.Visibility Visibility
        {
            get { return this.m_Visibility; }
            set { this.m_Visibility = value; }
        }
    }
}

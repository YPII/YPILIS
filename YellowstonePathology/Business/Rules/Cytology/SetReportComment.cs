using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class SetReportComment
	{
        private YellowstonePathology.Business.Rules.Rule m_Rule;
        private string m_ReportComment;
        private YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrder;

		public SetReportComment()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule(); 
            this.m_Rule.ActionList.Add(IsOkToSetReportComment);
            this.m_Rule.ActionList.Add(SetComment);            
        }        

        private void IsOkToSetReportComment()
        {
            if (this.m_PanelOrder.Accepted == true)
            {
                this.m_Rule.ExecutionStatus.AddMessage("Unable to set the selected report comment because screening is final.", true);
            }
        }

        private void SetComment()
        {
            if (string.IsNullOrEmpty(this.m_PanelOrder.ReportComment) == true)
            {
                this.m_PanelOrder.ReportComment = this.m_ReportComment;
            }
            else
            {
                this.m_PanelOrder.ReportComment += "  ";
                this.m_PanelOrder.ReportComment += this.m_ReportComment;
            }
        }

        public void Execute(string reportComment, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ReportComment = reportComment;
            this.m_PanelOrder = panelOrder;            
            this.m_Rule.Execute(executionStatus);
        }       
	}
}

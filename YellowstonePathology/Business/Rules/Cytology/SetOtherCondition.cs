using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class SetOtherCondition
	{
        YellowstonePathology.Business.Rules.Rule m_Rule;

        string m_OtherCondition;
        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrder;
        
		public SetOtherCondition()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();     
       
            this.m_Rule.ActionList.Add(IsOkToSetOtherCondition);
            this.m_Rule.ActionList.Add(SetOtherConditionComment);
            this.m_Rule.ActionList.Add(HandleEndoInWomenOlderThan40ReportComment);
            this.m_Rule.ActionList.Add(HandleECCReportComment);            
        }        

        private void IsOkToSetOtherCondition()
        {            
            if (this.m_PanelOrder.Accepted == true)
            {
                this.m_Rule.ExecutionStatus.AddMessage("Unable to set the selected other condition because screening is final.", true);
            }
        }

        private void SetOtherConditionComment()
        {
            string result = this.m_PanelOrder.OtherConditions;
            if (string.IsNullOrEmpty(result) == false)
            {
                result += " ";
            }            
            result = result + this.m_OtherCondition;
            this.m_PanelOrder.OtherConditions = result;
        }

        private void HandleEndoInWomenOlderThan40ReportComment()
        {
			string endoOtherCondition = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetOtherConditionById(7).OtherConditionText;
			string endoReportComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(44).Comment;

            if (string.IsNullOrEmpty(this.m_PanelOrder.OtherConditions) == false)
            {
                if (this.m_PanelOrder.OtherConditions.Length >= endoOtherCondition.Length)
                {
                    if (this.m_PanelOrder.OtherConditions.Contains(endoOtherCondition) == true)
                    {
                        if (string.IsNullOrEmpty(this.m_PanelOrder.ReportComment) == true || this.m_PanelOrder.ReportComment.Contains(endoReportComment) == false)
                        {
                            if (string.IsNullOrEmpty(this.m_PanelOrder.ReportComment) == false)
                            {
                                this.m_PanelOrder.ReportComment += "  ";
                            }
                            this.m_PanelOrder.ReportComment += endoReportComment;
                        }
                    }
                }
            }
        }

        private void HandleECCReportComment()
        {
            
        }

        public void Execute(string otherCondition, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_OtherCondition = otherCondition;
            this.m_PanelOrder = panelOrder;            
            this.m_Rule.Execute(executionStatus);
        }       
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapScreeningPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        protected string m_ScreeningType;
        protected bool m_IsQC;
        protected string m_ReportComment;

        public ThinPrepPapScreeningPanel()
        {
            this.m_IsQC = false;
            this.m_PanelId = 38;
            this.m_PanelName = "Cytology Screening";
            this.m_AcknowledgeOnOrder = true;
            this.m_ScreeningType = "Primary Screening";
            this.m_ResultCode = "59999";
            this.m_PanelOrderClassName = typeof(PanelOrderCytology).AssemblyQualifiedName;
        }

        public string ScreeningType
        {
            get { return this.m_ScreeningType; }
            set { this.m_ScreeningType = value; }
        }

        public bool IsQC
        {
            get { return this.m_IsQC; }
            set { this.m_IsQC = value; }
        }

        public string ReportComment
        {
            get { return this.m_ReportComment; }
        }
        
    }
}

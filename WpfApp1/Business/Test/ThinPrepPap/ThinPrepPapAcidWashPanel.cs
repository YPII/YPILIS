using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapAcidWashPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        private string m_ReportComment;

        public ThinPrepPapAcidWashPanel()
        {
            this.m_PanelId = 39;
            this.m_PanelName = "Acid Wash";
            this.m_PanelOrderClassName = typeof(PanelOrderAcidWash).AssemblyQualifiedName;
            this.m_AcknowledgeOnOrder = true;
            this.m_ReportComment = "This specimen was reprocessed using an acid wash technique to aid specimen adequacy for evaluation.  " + 
                "This procedure may increase turnaround time, but results in far fewer unsatisfactory pap tests.";
        }

        public string ReportComment
        {
            get { return this.m_ReportComment; }
            set { this.m_ReportComment = value; }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultBreast : HER2AmplificationResult
    {        
        protected string InterpretiveComment = "Human epidermal growth factor receptor 2 gene (HER2) is amplified in approximately 20% of breast cancers.  " +
                "Amplification of the HER2 gene in breast tumors is associated with a worse prognosis.  HER2 status is also predictive of response to " +
                "chemotherapeutic agents.  Dual in situ hybridization (ISH) studies for HER2 amplification were performed on the submitted sample, in " +
                "accordance with current ASCO/CAP guidelines. For this patient, the HER2:Chr17 ratio was *RATIO* and average HER2 copy number per cell " +
                "was *HER2COPY* (*CELLSCOUNTED* nuclei examined).  Therefore, HER2 status is *HER2STATUS*.";

        public HER2AmplificationResultBreast(PanelSetOrderCollection panelSetOrderCollection, HER2AmplificationByISHTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
            this.m_ReportReference = "Wolff AC, Hammond MEH, Hicks DG, et al. Recommendations for Human Epidermal Growth Factor Receptor 2 Testing in " +
                "Breast Cancer. Arch Pathol Lab Med. doi: 10.5858/arpa.2013-0953-SA.";
        }

        public HER2AmplificationResultBreast(PanelSetOrderCollection panelSetOrderCollection, HER2AnalysisSummary.HER2AnalysisSummaryTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
            this.m_ReportReference = "Wolff AC, Hammond MEH, Hicks DG, et al. Recommendations for Human Epidermal Growth Factor Receptor 2 Testing in " +
                "Breast Cancer. Arch Pathol Lab Med. doi: 10.5858/arpa.2013-0953-SA.";
        }        

        public override void SetISHResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_ResultComment = null;
            this.m_ResultDescription = "Ratio = " + this.m_HER2AmplificationByISHTestOrder.Her2Chr17Ratio;
            base.SetISHResults(specimenOrder);
        }        

        public override void SetSummaryResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_ResultComment = null;
            this.m_ResultDescription = "Ratio = " + this.m_HER2AmplificationByISHTestOrder.Her2Chr17Ratio;
            base.SetSummaryResults(specimenOrder);
        }

        protected void SetInterpretiveCommentValues(string ratio, string cellsCounted, string status, double? her2Copy)
        {
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*RATIO*", ratio);
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*CELLSCOUNTED*", cellsCounted);
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", status);
            if (her2Copy.HasValue == true)
            {
                this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2COPY*", her2Copy.Value.ToString());
            }
        }
    }
}

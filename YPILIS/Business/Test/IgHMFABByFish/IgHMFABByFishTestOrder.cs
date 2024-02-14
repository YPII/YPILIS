using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.IgHMFABByFish
{
    [PersistentClass("tblIgHMFABByFishTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class IgHMFABByFishTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Result;
        private string m_Interpretation;
        private string m_ProbeSetDetail;
        private string m_Method;
        private string m_ASRComment;
        private string m_NucleiScored;
        private string m_ProbeComment;

        public IgHMFABByFishTestOrder()
        {

        }

        public IgHMFABByFishTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_ASRComment = "All controls were within expected ranges.  " +
                "NeoGenomics Laboratories FISH test uses either FDA cleared and/or analyte specific reagent (ASR) probes. This test was developed and " +
                "its performance characteristics determined by NeoGenomics Laboratories in Ft. Myers FL. It has not been cleared or approved by the U.S. " +
                "Food and Drug Administration (FDA). The FDA has determined that such clearance or approval is not necessary. This test is used for " +
                "clinical purposes and should not be regarded as investigational or for research. This laboratory is regulated under CLIA '88 as " +
                "qualified to perform high complexity testing.Interphase FISH does not include examination of the entire chromosomal complement. " +
                "Clinically significant anomalies detectable by routine banded cytogenetic analysis may still be present. Consider reflex banded " +
                "cytogenetic analysis.";
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "200", "null", "varchar")]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "2000", "null", "varchar")]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "2000", "null", "varchar")]
        public string ProbeSetDetail
        {
            get { return this.m_ProbeSetDetail; }
            set
            {
                if (this.m_ProbeSetDetail != value)
                {
                    this.m_ProbeSetDetail = value;
                    this.NotifyPropertyChanged("ProbeSetDetail");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string Method
        {
            get { return this.m_Method; }
            set
            {
                if (this.m_Method != value)
                {
                    this.m_Method = value;
                    this.NotifyPropertyChanged("Method");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ASRComment
        {
            get { return this.m_ASRComment; }
            set
            {
                if (this.m_ASRComment != value)
                {
                    this.m_ASRComment = value;
                    this.NotifyPropertyChanged("ASRComment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string NucleiScored
        {
            get { return this.m_NucleiScored; }
            set
            {
                if (this.m_NucleiScored != value)
                {
                    this.m_NucleiScored = value;
                    this.NotifyPropertyChanged("NucleiScored");
                }
            }
        }

        [PersistentProperty()]
        public string ProbeComment
        {
            get { return this.m_ProbeComment; }
            set
            {
                if (this.m_ProbeComment != value)
                {
                    this.m_ProbeComment = value;
                    this.NotifyPropertyChanged("ProbeComment");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.Append("Result: ");
            result.AppendLine(this.m_Result);
            result.AppendLine("Interpretation: ");
            result.AppendLine(this.m_Interpretation);
            return result.ToString();
        }
    }
}
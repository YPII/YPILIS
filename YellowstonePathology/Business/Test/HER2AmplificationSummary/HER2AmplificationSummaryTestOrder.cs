using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Audit.Model;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    [PersistentClass("tblHER2AmplificationSummaryTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AmplificationSummaryTestOrder : PanelSetOrder, Interface.IHER2ISH
    {
        protected string m_Result;
        protected int m_CellsCounted;
        protected int m_TotalChr17SignalsCounted;
        protected int m_TotalHer2SignalsCounted;
        protected int m_Her2byIHCOrder;
        protected int m_NumberOfObservers;
        protected bool m_IncludeImmunoRecommendedComment;
        protected bool m_IncludeResultComment;
        protected bool m_IncludePolysomyComment;
        protected string m_PolysomyPercent;
        protected string m_Chr17SignalRangeLow;
        protected string m_Chr17SignalRangeHigh;
        protected string m_Her2SignalRangeLow;
        protected string m_Her2SignalRangeHigh;
        protected string m_CommentLabel;
        protected string m_SampleAdequacy;
        protected string m_ProbeSignalIntensity;
        protected string m_TechComment;
        protected string m_ResultComment;
        protected string m_InterpretiveComment;
        protected string m_ResultDescription;
        protected string m_SourceBlock;
        protected string m_GeneticHeterogeneity;
        protected string m_Her2Chr17ClusterRatio;
        protected string m_ReportReference;
        protected string m_Indicator;
        protected string m_Method;
        protected bool m_NotInterpretable;
        protected string m_ASRComment;
        protected string m_FixationComment;
        protected bool m_HER2ByIHCRequired;
        protected bool m_RecountRequired;
        private int m_CellsRecount;
        private int m_TotalChr17SignalsRecount;
        private int m_TotalHer2SignalsRecount;
        private string m_IHCScore;

        public HER2AmplificationSummaryTestOrder()
        { }

        public HER2AmplificationSummaryTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_Method = "This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved " +
                "Inform HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on " +
                "paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 " +
                "and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).";
            this.m_ASRComment = "This test was performed using a US FDA approved DNA probe kit, modified to report results according to " +
                "ASCO/CAP guidelines, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the " +
                "responsibility for test performance";
        }

        [PersistentProperty()]
        public bool IncludeImmunoRecommendedComment
        {
            get { return this.m_IncludeImmunoRecommendedComment; }
            set
            {
                if (this.m_IncludeImmunoRecommendedComment != value)
                {
                    this.m_IncludeImmunoRecommendedComment = value;
                    this.NotifyPropertyChanged("IncludeImmunoRecommendedComment");
                }
            }
        }

        [PersistentProperty()]
        public bool IncludeResultComment
        {
            get { return this.m_IncludeResultComment; }
            set
            {
                if (this.m_IncludeResultComment != value)
                {
                    this.m_IncludeResultComment = value;
                    this.NotifyPropertyChanged("IncludeResultComment");
                }
            }
        }

        [PersistentProperty()]
        public bool IncludePolysomyComment
        {
            get { return this.m_IncludePolysomyComment; }
            set
            {
                if (this.m_IncludePolysomyComment != value)
                {
                    this.m_IncludePolysomyComment = value;
                    this.NotifyPropertyChanged("IncludePolysomyComment");
                }
            }
        }

        [PersistentProperty()]
        public int CellsCounted
        {
            get { return this.m_CellsCounted; }
            set
            {
                if (this.m_CellsCounted != value)
                {
                    this.m_CellsCounted = value;
                    this.NotifyPropertyChanged("CellsCounted");
                }
            }
        }

        [PersistentProperty()]
        public int TotalChr17SignalsCounted
        {
            get { return this.m_TotalChr17SignalsCounted; }
            set
            {
                if (this.m_TotalChr17SignalsCounted != value)
                {
                    this.m_TotalChr17SignalsCounted = value;
                    this.NotifyPropertyChanged("TotalChr17SignalsCounted");
                }
            }
        }

        [PersistentProperty()]
        public int TotalHer2SignalsCounted
        {
            get { return this.m_TotalHer2SignalsCounted; }
            set
            {
                if (this.m_TotalHer2SignalsCounted != value)
                {
                    this.m_TotalHer2SignalsCounted = value;
                    this.NotifyPropertyChanged("TotalHer2SignalsCounted");
                }
            }
        }

        [PersistentProperty()]
        public int Her2byIHCOrder
        {
            get { return this.m_Her2byIHCOrder; }
            set
            {
                if (this.m_Her2byIHCOrder != value)
                {
                    this.m_Her2byIHCOrder = value;
                    this.NotifyPropertyChanged("Her2byIHCOrder");
                }
            }
        }

        [PersistentProperty()]
        public int NumberOfObservers
        {
            get { return this.m_NumberOfObservers; }
            set
            {
                if (this.m_NumberOfObservers != value)
                {
                    this.m_NumberOfObservers = value;
                    this.NotifyPropertyChanged("NumberOfObservers");
                }
            }
        }

        [PersistentProperty()]
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
        public string PolysomyPercent
        {
            get { return this.m_PolysomyPercent; }
            set
            {
                if (this.m_PolysomyPercent != value)
                {
                    this.m_PolysomyPercent = value;
                    this.NotifyPropertyChanged("PolysomyPercent");
                }
            }
        }

        [PersistentProperty()]
        public string Chr17SignalRangeLow
        {
            get { return this.m_Chr17SignalRangeLow; }
            set
            {
                if (this.m_Chr17SignalRangeLow != value)
                {
                    this.m_Chr17SignalRangeLow = value;
                    this.NotifyPropertyChanged("Chr17SignalRangeLow");
                }
            }
        }

        [PersistentProperty()]
        public string Chr17SignalRangeHigh
        {
            get { return this.m_Chr17SignalRangeHigh; }
            set
            {
                if (this.m_Chr17SignalRangeHigh != value)
                {
                    this.m_Chr17SignalRangeHigh = value;
                    this.NotifyPropertyChanged("Chr17SignalRangeHigh");
                }
            }
        }

        [PersistentProperty()]
        public string Her2SignalRangeLow
        {
            get { return this.m_Her2SignalRangeLow; }
            set
            {
                if (this.m_Her2SignalRangeLow != value)
                {
                    this.m_Her2SignalRangeLow = value;
                    this.NotifyPropertyChanged("Her2SignalRangeLow");
                }
            }
        }

        [PersistentProperty()]
        public string Her2SignalRangeHigh
        {
            get { return this.m_Her2SignalRangeHigh; }
            set
            {
                if (this.m_Her2SignalRangeHigh != value)
                {
                    this.m_Her2SignalRangeHigh = value;
                    this.NotifyPropertyChanged("Her2SignalRangeHigh");
                }
            }
        }

        [PersistentProperty()]
        public string CommentLabel
        {
            get { return this.m_CommentLabel; }
            set
            {
                if (this.m_CommentLabel != value)
                {
                    this.m_CommentLabel = value;
                    this.NotifyPropertyChanged("CommentLabel");
                }
            }
        }

        [PersistentProperty()]
        public string ResultDescription
        {
            get { return this.m_ResultDescription; }
            set
            {
                if (this.m_ResultDescription != value)
                {
                    this.m_ResultDescription = value;
                    this.NotifyPropertyChanged("ResultDescription");
                }
            }
        }

        [PersistentProperty()]
        public string SampleAdequacy
        {
            get { return this.m_SampleAdequacy; }
            set
            {
                if (this.m_SampleAdequacy != value)
                {
                    this.m_SampleAdequacy = value;
                    this.NotifyPropertyChanged("SampleAdequacy");
                }
            }
        }

        [PersistentProperty()]
        public string ProbeSignalIntensity
        {
            get { return this.m_ProbeSignalIntensity; }
            set
            {
                if (this.m_ProbeSignalIntensity != value)
                {
                    this.m_ProbeSignalIntensity = value;
                    this.NotifyPropertyChanged("ProbeSignalIntensity");
                }
            }
        }

        [PersistentProperty()]
        public string TechComment
        {
            get { return this.m_TechComment; }
            set
            {
                if (this.m_TechComment != value)
                {
                    this.m_TechComment = value;
                    this.NotifyPropertyChanged("TechComment");
                }
            }
        }

        [PersistentProperty()]
        public string ResultComment
        {
            get { return this.m_ResultComment; }
            set
            {
                if (this.m_ResultComment != value)
                {
                    this.m_ResultComment = value;
                    this.NotifyPropertyChanged("ResultComment");
                }
            }
        }

        [PersistentProperty()]
        public string InterpretiveComment
        {
            get { return this.m_InterpretiveComment; }
            set
            {
                if (this.m_InterpretiveComment != value)
                {
                    this.m_InterpretiveComment = value;
                    this.NotifyPropertyChanged("InterpretiveComment");
                }
            }
        }

        [PersistentProperty()]
        public string GeneticHeterogeneity
        {
            get { return this.m_GeneticHeterogeneity; }
            set
            {
                if (this.m_GeneticHeterogeneity != value)
                {
                    this.m_GeneticHeterogeneity = value;
                    this.NotifyPropertyChanged("GeneticHeterogeneity");
                }
            }
        }

        [PersistentProperty()]
        public string Her2Chr17ClusterRatio
        {
            get { return this.m_Her2Chr17ClusterRatio; }
            set
            {
                if (this.m_Her2Chr17ClusterRatio != value)
                {
                    this.m_Her2Chr17ClusterRatio = value;
                    this.NotifyPropertyChanged("Her2Chr17ClusterRatio");
                }
            }
        }

        [PersistentProperty()]
        public string ReportReference
        {
            get { return this.m_ReportReference; }
            set
            {
                if (this.m_ReportReference != value)
                {
                    this.m_ReportReference = value;
                    this.NotifyPropertyChanged("ReportReference");
                }
            }
        }

        [PersistentProperty()]
        public string SourceBlock
        {
            get { return this.m_SourceBlock; }
            set
            {
                if (this.m_SourceBlock != value)
                {
                    this.m_SourceBlock = value;
                    this.NotifyPropertyChanged("SourceBlock");
                }
            }
        }

        [PersistentProperty()]
        public string Indicator
        {
            get { return this.m_Indicator; }
            set
            {
                if (this.m_Indicator != value)
                {
                    this.m_Indicator = value;
                    this.NotifyPropertyChanged("Indicator");
                }
            }
        }

        [PersistentProperty()]
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
        public bool NotInterpretable
        {
            get { return this.m_NotInterpretable; }
            set
            {
                if (this.m_NotInterpretable != value)
                {
                    this.m_NotInterpretable = value;
                    this.NotifyPropertyChanged("NotInterpretable");
                }
            }
        }

        [PersistentProperty()]
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
        public string FixationComment
        {
            get { return this.m_FixationComment; }
            set
            {
                if (this.m_FixationComment != value)
                {
                    this.m_FixationComment = value;
                    this.NotifyPropertyChanged("FixationComment");
                }
            }
        }

        [PersistentProperty()]
        public bool HER2ByIHCRequired
        {
            get { return this.m_HER2ByIHCRequired; }
            set
            {
                if (this.m_HER2ByIHCRequired != value)
                {
                    this.m_HER2ByIHCRequired = value;
                    this.NotifyPropertyChanged("HER2ByIHCRequired");
                }
            }
        }

        [PersistentProperty()]
        public bool RecountRequired
        {
            get { return this.m_RecountRequired; }
            set
            {
                if (this.m_RecountRequired != value)
                {
                    this.m_RecountRequired = value;
                    this.NotifyPropertyChanged("RecountRequired");
                }
            }
        }

        [PersistentProperty()]
        public int CellsRecount
        {
            get { return this.m_CellsRecount; }
            set
            {
                if (this.m_CellsRecount != value)
                {
                    this.m_CellsRecount = value;
                    this.NotifyPropertyChanged("CellsRecount");
                }
            }
        }

        [PersistentProperty()]
        public int TotalChr17SignalsRecount
        {
            get { return this.m_TotalChr17SignalsRecount; }
            set
            {
                if (this.m_TotalChr17SignalsRecount != value)
                {
                    this.m_TotalChr17SignalsRecount = value;
                    this.NotifyPropertyChanged("TotalChr17SignalsRecount");
                }
            }
        }

        [PersistentProperty()]
        public int TotalHer2SignalsRecount
        {
            get { return this.m_TotalHer2SignalsRecount; }
            set
            {
                if (this.m_TotalHer2SignalsRecount != value)
                {
                    this.m_TotalHer2SignalsRecount = value;
                    this.NotifyPropertyChanged("TotalHer2SignalsRecount");
                }
            }
        }

        [PersistentProperty()]
        public string IHCScore
        {
            get { return this.m_IHCScore; }
            set
            {
                if (this.m_IHCScore != value)
                {
                    this.m_IHCScore = value;
                    this.NotifyPropertyChanged("IHCScore");
                }
            }
        }

        public int CellCountToUse
        {
            get { return this.m_RecountRequired == true ? this.m_CellsRecount : this.m_CellsCounted; }
            set
            {
                if (this.m_RecountRequired == true) this.CellsRecount = value;
                else this.CellsCounted = value;
            }
        }

        private int TotalHer2SignalsCountToUse
        {
            get { return this.m_RecountRequired == true ? this.m_TotalHer2SignalsRecount : this.m_TotalHer2SignalsCounted; }
            set
            {
                if (this.m_RecountRequired == true) this.TotalHer2SignalsRecount = value;
                else this.TotalHer2SignalsCounted = value;
            }
        }

        private int TotalChr17SignalsCountToUse
        {
            get { return this.m_RecountRequired == true ? this.m_TotalChr17SignalsRecount : this.m_TotalChr17SignalsCounted; }
            set
            {
                if (this.m_RecountRequired == true) this.TotalChr17SignalsRecount = value;
                else this.TotalChr17SignalsCounted = value;
            }
        }

        public Nullable<double> Her2Chr17Ratio
        {
            get
            {
                Nullable<double> ratio = null;
                if (TotalHer2SignalsCountToUse > 0 && TotalChr17SignalsCountToUse > 0)
                {
                    double dratio = (double)TotalHer2SignalsCountToUse / (double)TotalChr17SignalsCountToUse;
                    ratio = Convert.ToDouble(Math.Round((dratio), 2));
                }
                return ratio;
            }
            set { }
        }

        public Nullable<double> AverageHer2NeuSignal
        {
            get
            {
                Nullable<double> result = null;
                if (TotalHer2SignalsCountToUse > 0 && CellCountToUse > 0)
                {
                    double dratio = (double)TotalHer2SignalsCountToUse / (double)CellCountToUse;
                    result = Convert.ToDouble(Math.Round((dratio), 2));
                }
                return result;
            }
            set { }
        }

        public string AverageChr17Signal
        {
            get
            {
                string ratio = "Unable to calculate";
                if (TotalChr17SignalsCountToUse > 0 && CellCountToUse > 0)
                {
                    double dratio = (double)TotalChr17SignalsCountToUse / (double)CellCountToUse;
                    ratio = Convert.ToString(Math.Round((dratio), 2));
                }
                return ratio;
            }
            set { }
        }

        public string AverageHer2Chr17Signal
        {
            get
            {
                string ratio = "Unable to calculate";
                Nullable<double> dratio = this.AverageHer2Chr17SignalAsDouble;
                if (dratio.HasValue)
                {
                    ratio = Convert.ToString(Math.Round((dratio.Value), 2));
                }
                return ratio;
            }
            set { }
        }

        public Nullable<double> AverageHer2Chr17SignalAsDouble
        {
            get
            {
                Nullable<double> dratio = null;
                if (TotalChr17SignalsCountToUse > 0 && TotalHer2SignalsCountToUse > 0 && CellCountToUse > 0)
                {
                    dratio = ((double)TotalHer2SignalsCountToUse / (double)CellCountToUse) / ((double)TotalChr17SignalsCountToUse / (double)CellCountToUse);
                }
                return dratio;
            }
        }

        public int TotalChr17SignalsCountedUI
        {
            get { return this.m_TotalChr17SignalsCounted; }
            set
            {
                if (this.m_TotalChr17SignalsCounted != value)
                {
                    this.TotalChr17SignalsCounted = value;
                    NotifyPropertyChanged("Her2Chr17Ratio");
                    NotifyPropertyChanged("TotalChr17SignalsCountedUI");
                    NotifyPropertyChanged("AverageChr17Signal");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public int TotalChr17SignalsRecountUI
        {
            get { return this.m_TotalChr17SignalsRecount; }
            set
            {
                if (this.m_TotalChr17SignalsRecount != value)
                {
                    this.m_TotalChr17SignalsRecount = value;
                    NotifyPropertyChanged("Her2Chr17Ratio");
                    NotifyPropertyChanged("TotalChr17SignalsCountedUI");
                    NotifyPropertyChanged("AverageChr17Signal");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public int TotalHer2SignalsRecountUI
        {
            get { return this.m_TotalHer2SignalsRecount; }
            set
            {
                if (this.m_TotalHer2SignalsRecount != value)
                {
                    this.m_TotalHer2SignalsRecount = value;
                    NotifyPropertyChanged("Her2Chr17Ratio");
                    NotifyPropertyChanged("TotalHer2SignalsCountedUI");
                    NotifyPropertyChanged("AverageHer2NeuSignal");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public int TotalHer2SignalsCountedUI
        {
            get { return this.m_TotalHer2SignalsCounted; }
            set
            {
                if (this.m_TotalHer2SignalsCounted != value)
                {
                    this.TotalHer2SignalsCounted = value;
                    NotifyPropertyChanged("Her2Chr17Ratio");
                    NotifyPropertyChanged("TotalHer2SignalsCountedUI");
                    NotifyPropertyChanged("AverageHer2NeuSignal");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public int CellsCountedUI
        {
            get { return this.m_CellsCounted; }
            set
            {
                if (this.m_CellsCounted != value)
                {
                    this.CellsCounted = value;
                    NotifyPropertyChanged("CellsCountedUI");
                    NotifyPropertyChanged("AverageHer2NeuSignal");
                    NotifyPropertyChanged("AverageChr17Signal");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public int CellsRecountUI
        {
            get { return this.m_CellsRecount; }
            set
            {
                if (this.m_CellsRecount != value)
                {
                    this.m_CellsRecount = value;
                    NotifyPropertyChanged("CellsCountedUI");
                    NotifyPropertyChanged("AverageHer2NeuSignal");
                    NotifyPropertyChanged("AverageChr17Signal");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string result = "HER2 Amplification Summary: " + this.Result;
            return result;
        }

        public AuditResult IsOkToSetResults(AccessionOrder accessionOrder)
        {
            AuditResult result = new AuditResult();
            result.Status = AuditStatusEnum.OK;
            if (this.m_Accepted == true)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = "The results may not be set because they have already been accepted." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(this.m_Indicator) == true)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message += "The indication must be set before results can be set." + Environment.NewLine;
            }

            if (this.m_NotInterpretable == false)
            {
                if (this.TotalHer2SignalsCountToUse == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    string whichCount = this.m_RecountRequired == true ? "The Total Her2 Signals Recount " : "The Total Her2 Signals Counted ";
                    result.Message += whichCount + "must be set before results can be set." + Environment.NewLine;
                }
                if (this.m_TotalChr17SignalsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    string whichCount = this.m_RecountRequired == true ? "The Total Chr17 Signals Recount " : "The Total Chr17 Signals Counted ";
                    result.Message += whichCount + "must be set before results can be set." + Environment.NewLine;
                }
                if (this.m_CellsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    string whichCount = this.m_RecountRequired == true ? "The Cells Recount " : "The Cells Counted ";
                    result.Message += "must be set before results can be set." + Environment.NewLine;
                }
                if (this.m_GeneticHeterogeneity == HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInClusters)
                {
                    if (string.IsNullOrEmpty(this.m_Her2Chr17ClusterRatio) == true)
                    {
                        result.Status = AuditStatusEnum.Failure;
                        result.Message += "The Her2Chr/17 Cluster Ratio must be set before results can be set." + Environment.NewLine;
                    }
                }
            }
            return result;
        }

        public void SetResults(AccessionOrder accessionOrder)
        {
            HER2AmplificationByISH.HER2AmplificationResultCollection her2AmplificationResultCollection = new HER2AmplificationByISH.HER2AmplificationResultCollection(accessionOrder.PanelSetOrderCollection, this.m_ReportNo);
            HER2AmplificationByISH.HER2AmplificationResult her2AmplificationResult = her2AmplificationResultCollection.FindMatch();
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.OrderedOn, this.OrderedOnId);
            her2AmplificationResult.SetResults(specimenOrder);
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Indicator) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "The result may not be accepted because the indication is not set.";
                }
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "The result may not be accepted because the result is not set.";
                }
            }
            return result;
        }

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            AuditResult result = new AuditResult();
            result.Status = AuditStatusEnum.OK;
            Rules.MethodResult methodResult = base.IsOkToFinalize();
            if (methodResult.Success == false)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = methodResult.Message;
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to final as the result is not set.";
                }
            }

            return result;
        }

        public void SetValues(AccessionOrder accessionOrder)
        {
            HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();
            Her2AmplificationByIHC.Her2AmplificationByIHCTest ihcTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();

            HER2AmplificationByISH.HER2AmplificationByISHTestOrder ishTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_OrderedOnId, true);
            this.m_CellsCounted = ishTestOrder.CellsCounted;
            this.m_TotalChr17SignalsCounted = ishTestOrder.TotalChr17SignalsCounted;
            this.m_TotalHer2SignalsCounted = ishTestOrder.TotalHer2SignalsCounted;
            this.m_Her2byIHCOrder = ishTestOrder.Her2byIHCOrder;
            this.m_NumberOfObservers = ishTestOrder.NumberOfObservers;
            this.m_IncludeImmunoRecommendedComment = ishTestOrder.IncludeImmunoRecommendedComment;
            this.m_IncludeResultComment = ishTestOrder.IncludeResultComment;
            this.m_IncludePolysomyComment = ishTestOrder.IncludePolysomyComment;
            this.m_PolysomyPercent = ishTestOrder.PolysomyPercent;
            this.m_Chr17SignalRangeLow = ishTestOrder.Chr17SignalRangeLow;
            this.m_Chr17SignalRangeHigh = ishTestOrder.Chr17SignalRangeHigh;
            this.m_Her2SignalRangeLow = ishTestOrder.Her2SignalRangeLow;
            this.m_Her2SignalRangeHigh = ishTestOrder.Her2SignalRangeHigh;
            this.m_CommentLabel = ishTestOrder.CommentLabel;
            this.m_SampleAdequacy = ishTestOrder.SampleAdequacy;
            this.m_ProbeSignalIntensity = ishTestOrder.ProbeSignalIntensity;
            this.m_TechComment = ishTestOrder.TechComment;
            this.m_ResultComment = ishTestOrder.ResultComment;
            this.m_InterpretiveComment = ishTestOrder.InterpretiveComment;
            this.m_ResultDescription = ishTestOrder.ResultDescription;
            this.m_SourceBlock = ishTestOrder.SourceBlock;
            this.m_GeneticHeterogeneity = ishTestOrder.GeneticHeterogeneity;
            this.m_Her2Chr17ClusterRatio = ishTestOrder.Her2Chr17ClusterRatio;
            this.m_ReportReference = ishTestOrder.ReportReference;
            this.m_Indicator = ishTestOrder.Indicator;
            this.m_Method = ishTestOrder.Method;
            this.m_NotInterpretable = ishTestOrder.NotInterpretable;
            this.m_ASRComment = ishTestOrder.ASRComment;
            this.m_FixationComment = ishTestOrder.FixationComment;
            this.m_HER2ByIHCRequired = ishTestOrder.HER2ByIHCRequired;
            this.m_RecountRequired = ishTestOrder.RecountRequired;

            Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC ihcTestOrder = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ihcTest.PanelSetId, this.m_OrderedOnId, true);
            this.m_IHCScore = ihcTestOrder.Score;

            if (accessionOrder.PanelSetOrderCollection.Exists(recountTest.PanelSetId, this.m_OrderedOnId, true) == true)
            {
                HER2AmplificationRecount.HER2AmplificationRecountTestOrder recountTestOrder = (HER2AmplificationRecount.HER2AmplificationRecountTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(recountTest.PanelSetId, this.m_OrderedOnId, true);
                this.m_CellsRecount = recountTestOrder.CellsCounted;
                this.m_TotalChr17SignalsRecount = recountTestOrder.Chr17SignalsCounted;
                this.m_TotalHer2SignalsRecount = recountTestOrder.Her2SignalsCounted;
            }

            this.m_Distribute = true;
        }
    }
}

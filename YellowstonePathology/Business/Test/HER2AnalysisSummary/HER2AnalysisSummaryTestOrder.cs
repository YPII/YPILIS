using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Audit.Model;

namespace YellowstonePathology.Business.Test.HER2AnalysisSummary
{
    [PersistentClass("tblHER2AmplificationSummaryTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AnalysisSummaryTestOrder : PanelSetOrder
    {
        protected string m_Result;
        protected int m_NumberOfObservers;
        protected string m_CommentLabel;
        protected string m_TechComment;
        protected string m_ResultComment;
        protected string m_InterpretiveComment;
        protected string m_ResultDescription;
        protected string m_ReportReference;
        protected bool m_RecountRequired;

        private double? m_Her2Chr17Ratio;
        private double? m_AverageHer2NeuSignal;
        private string m_AverageChr17Signal;
        private string m_AverageHer2Chr17Signal;
        private double? m_AverageHer2Chr17SignalAsDouble;


        protected int m_CellsCounted;
        protected int m_TotalChr17SignalsCounted;
        protected int m_TotalHer2SignalsCounted;

        public HER2AnalysisSummaryTestOrder()
        { }

        public HER2AnalysisSummaryTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
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

        public int CellsCounted
        {
            get { return this.m_CellsCounted; }
        }

        public int TotalHer2SignalsCounted
        {
            get { return this.m_TotalHer2SignalsCounted; }
        }

        public int TotalChr17SignalsCounted
        {
            get { return this.m_TotalChr17SignalsCounted; }
        }

        public Nullable<double> Her2Chr17Ratio
        {
            get { return this.m_Her2Chr17Ratio; }
        }

        public Nullable<double> AverageHer2NeuSignal
        {
            get { return this.m_AverageHer2NeuSignal; }
        }

        public string AverageChr17Signal
        {
            get { return this.m_AverageChr17Signal; }
        }

        public string AverageHer2Chr17Signal
        {
            get { return this.m_AverageHer2Chr17Signal; }
        }

        public Nullable<double> AverageHer2Chr17SignalAsDouble
        {
            get { return this.m_AverageHer2Chr17SignalAsDouble; }
        }

        public void SetValues(int cellsCounted, int her2Signals, int chr17Signals)
        {
            this.m_CellsCounted = cellsCounted;
            this.m_TotalHer2SignalsCounted = her2Signals;
            this.m_TotalChr17SignalsCounted = chr17Signals;

            this.m_Her2Chr17Ratio = null;
            this.m_AverageHer2NeuSignal = null;
            this.m_AverageChr17Signal = "Unable to calculate";
            this.m_AverageHer2Chr17Signal = "Unable to calculate";
            this.m_AverageHer2Chr17SignalAsDouble = null;

            if (this.m_TotalHer2SignalsCounted > 0 && this.m_TotalChr17SignalsCounted > 0)
            {
                double dratio = (double)m_TotalHer2SignalsCounted / (double)m_TotalChr17SignalsCounted;
                this.m_Her2Chr17Ratio = Convert.ToDouble(Math.Round((dratio), 2));

                if (this.m_CellsCounted > 0)
                {
                    this.m_AverageHer2Chr17SignalAsDouble = ((double)TotalHer2SignalsCounted / (double)this.m_CellsCounted) / ((double)TotalChr17SignalsCounted / (double)this.m_CellsCounted);
                    this.m_AverageHer2Chr17Signal = Convert.ToString(Math.Round((this.m_AverageHer2Chr17SignalAsDouble.Value), 2));

                    dratio = (double)m_TotalHer2SignalsCounted / (double)this.m_CellsCounted;
                    this.m_AverageHer2NeuSignal = Convert.ToDouble(Math.Round((dratio), 2));

                    dratio = (double)TotalChr17SignalsCounted / (double)this.m_CellsCounted;
                    this.m_AverageChr17Signal = Convert.ToString(Math.Round((dratio), 2));
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

            HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();
            Her2AmplificationByIHC.Her2AmplificationByIHCTest ihcTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();

            if(accessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId) == true)
            {
                HER2AmplificationByISH.HER2AmplificationByISHTestOrder ishTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_OrderedOnId, true);
                if (ishTestOrder.Final == false)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "The " + ishTest.PanelSetName + " must be final before results can be set." + Environment.NewLine;
                }
            }
            else
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message += "A " + ishTest.PanelSetName + " must be ordered and final before results can be set." + Environment.NewLine;
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(ihcTest.PanelSetId, this.OrderedOnId, true) == true)
            {
                Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC testOrder = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ihcTest.PanelSetId, this.OrderedOnId, true);
                if (testOrder.Final == false)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "The " + ihcTest.PanelSetName + " must be final before results can be set." + Environment.NewLine;
                }
                else
                {
                    if (testOrder.Score == "2+")
                    {
                        if (accessionOrder.PanelSetOrderCollection.Exists(recountTest.PanelSetId, this.OrderedOnId, true) == true)
                        {
                            HER2AmplificationRecount.HER2AmplificationRecountTestOrder recountTestOrder = (HER2AmplificationRecount.HER2AmplificationRecountTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(recountTest.PanelSetId, this.OrderedOnId, true);
                            if (recountTestOrder.Final == false)
                            {
                                result.Status = AuditStatusEnum.Failure;
                                result.Message += "The " + recountTest.PanelSetName + " must be final before results can be set." + Environment.NewLine;
                            }
                        }
                        else
                        {
                            result.Status = AuditStatusEnum.Failure;
                            result.Message += "A " + recountTest.PanelSetName + " must be ordered and final before results can be set." + Environment.NewLine;
                        }
                    }
                }
            }
            else
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message += "A " + ihcTest.PanelSetName + " must be ordered and final before results can be set." + Environment.NewLine;
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (this.m_Accepted == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "The results may not be set because they have already been accepted." + Environment.NewLine;
                }

                if (this.TotalHer2SignalsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    string whichCount = this.m_RecountRequired == true ? "The Total Her2 Signals Recount " : "The Total Her2 Signals Counted ";
                    result.Message += whichCount + "must be set before results can be set." + Environment.NewLine;
                }
                if (this.TotalChr17SignalsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    string whichCount = this.m_RecountRequired == true ? "The Total Chr17 Signals Recount " : "The Total Chr17 Signals Counted ";
                    result.Message += whichCount + "must be set before results can be set." + Environment.NewLine;
                }
                if (this.CellsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    string whichCount = this.m_RecountRequired == true ? "The Cells Recount " : "The Cells Counted ";
                    result.Message += "must be set before results can be set." + Environment.NewLine;
                }
            }

            return result;
        }

        public void SetResults(AccessionOrder accessionOrder)
        {

            HER2AmplificationByISH.HER2AmplificationResultCollection her2AmplificationResultCollection = new HER2AmplificationByISH.HER2AmplificationResultCollection(accessionOrder.PanelSetOrderCollection, this);
            HER2AmplificationByISH.HER2AmplificationResult her2AmplificationResult = her2AmplificationResultCollection.FindSummaryMatch();
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.OrderedOn, this.OrderedOnId);
            her2AmplificationResult.SetSummaryResults(specimenOrder);

            this.NotifyPropertyChanged(string.Empty);
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "The result may not be accepted because the result is not set.";
                }
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (this.m_NumberOfObservers == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "The result may not be accepted because the Number of Observers must be greater than 0.";
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

    }
}

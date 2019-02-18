using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResult : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected HER2AmplificationByISHTestOrder m_HER2AmplificationByISHTestOrder;
        protected Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrderHer2AmplificationByIHC;
        protected HER2AmplificationRecount.HER2AmplificationRecountTestOrder m_HER2AmplificationRecountTestOrder;
        protected HER2AmplificationResultEnum m_Result;

        protected string m_InterpretiveComment;
        protected string m_ResultComment;
        protected string m_ResultDescription;
        protected string m_ReportReference;
        protected string m_FixationOutOfBoundsComment = "The specimen fixation time does not meet ASCO CAP guidelines (6 to 72 hours), which " +
            "may cause false negative results.  Repeat testing on an alternate specimen that meets fixation time guidelines is recommended, " +
            "if available.";
        protected string m_FixationColdSchemaComment = "The time from specimen procurement to fixation in formalin (cold ischemia time) exceeds " +
            "one hour, which may cause false negative results.  Repeat testing on an alternate specimen that meets ASCO CAP guidelines for cold " +
            "ischemia time is recommended, if available.";

        public HER2AmplificationResult(PanelSetOrderCollection panelSetOrderCollection, string reportNo)
        {
            Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();
            HER2AmplificationRecount.HER2AmplificationRecountTest her2AmplificationRecountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();

            this.m_HER2AmplificationByISHTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(reportNo);
            //if (panelSetOrderCollection.Exists(her2AmplificationByISHTest.PanelSetId) == true)
            //{
            //    this.m_HER2AmplificationByISHTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationByISHTest.PanelSetId);
            //}

            if (panelSetOrderCollection.Exists(her2AmplificationByIHCTest.PanelSetId) == true)
            {
                this.m_PanelSetOrderHer2AmplificationByIHC = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationByIHCTest.PanelSetId);
            }
            if(panelSetOrderCollection.Exists(her2AmplificationRecountTest.PanelSetId) == true)
            {
                this.m_HER2AmplificationRecountTestOrder = (HER2AmplificationRecount.HER2AmplificationRecountTestOrder)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationRecountTest.PanelSetId);
            }
        }

        public virtual bool IsAMatch()
        {
            return false;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected void HandleIHC()
        {
            if (this.m_PanelSetOrderHer2AmplificationByIHC != null && this.m_PanelSetOrderHer2AmplificationByIHC.Accepted == true)
            {
                if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("0") || this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("1+"))
                {
                    this.m_Result = HER2AmplificationResultEnum.Negative;
                    this.m_HER2AmplificationByISHTestOrder.RecountRequired = false;
                }
                else if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("2+"))
                {
                    this.m_HER2AmplificationByISHTestOrder.RecountRequired = true;
                }
                else if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("3+"))
                {
                    this.m_Result = HER2AmplificationResultEnum.Positive;
                    this.m_HER2AmplificationByISHTestOrder.RecountRequired = false;
                }
            }
        }
        public bool IsRecountNeeded()
        {
            bool result = false;
            if (this.m_HER2AmplificationByISHTestOrder.RecountRequired == true && this.m_HER2AmplificationByISHTestOrder.NumberOfObservers < 3) result = true;
            return result;
        }

        public virtual void SetResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            if (this.m_HER2AmplificationByISHTestOrder.HER2ByIHCRequired == true)
            {
                this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine + "HER2 immunohistochemistry will be performed and " +
                    "results will be issued in an addendum to the original surgical pathology report.";
            }

            if (this.m_HER2AmplificationByISHTestOrder.GeneticHeterogeneity == HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInCells)
            {
                this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine +
                    "However, this tumor exhibits genetic heterogeneity in HER2 gene amplification in scattered individual cells.  The " +
                    "clinical significance and potential clinical benefit of trastuzumab is uncertain when " +
                    this.m_HER2AmplificationByISHTestOrder.Indicator.ToLower() +
                    " carcinoma demonstrates genetic heterogeneity." + Environment.NewLine + Environment.NewLine;
                this.m_ResultComment = "This tumor exhibits genetic heterogeneity in HER2 gene amplification in scattered individual cells.  The clinical " +
                    "significance and potential clinical benefit of trastuzumab is uncertain when " +
                    this.m_HER2AmplificationByISHTestOrder.Indicator.ToLower() +
                    " carcinoma demonstrates genetic heterogeneity";
            }
            else if (this.m_HER2AmplificationByISHTestOrder.GeneticHeterogeneity == HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInClusters)
            {
                this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine +
                    "However, this tumor exhibits genetic heterogeneity in HER2 gene amplification in small cell clusters. The HER2/Chr17 " +
                    "ratio in the clusters is " +
                    this.m_HER2AmplificationByISHTestOrder.Her2Chr17ClusterRatio +
                    ".  The clinical significance and potential clinical benefit of trastuzumab is uncertain when " +
                    this.m_HER2AmplificationByISHTestOrder.Indicator.ToLower() +
                    " carcinoma demonstrates genetic heterogeneity." + Environment.NewLine + Environment.NewLine;
                this.m_ResultComment = "This tumor exhibits genetic heterogeneity in HER2 gene amplification in small cell clusters.  The clinical significance " +
                    "and potential clinical benefit of trastuzumab is uncertain when " +
                    this.m_HER2AmplificationByISHTestOrder.Indicator.ToLower() +
                    " carcinoma demonstrates genetic heterogeneity.";
            }

            this.m_HER2AmplificationByISHTestOrder.Result = this.m_Result.ToString();
            this.m_HER2AmplificationByISHTestOrder.ResultComment = this.m_ResultComment;
            this.m_HER2AmplificationByISHTestOrder.InterpretiveComment = this.m_InterpretiveComment.TrimEnd();
            this.m_HER2AmplificationByISHTestOrder.ResultDescription = this.m_ResultDescription;
            this.m_HER2AmplificationByISHTestOrder.CommentLabel = null;
            this.m_HER2AmplificationByISHTestOrder.ReportReference = this.m_ReportReference;
            this.m_HER2AmplificationByISHTestOrder.NoCharge = false;

            if (specimenOrder.FixationDuration > 72 || specimenOrder.FixationDuration < 6)
            {
                specimenOrder.FixationComment = m_FixationOutOfBoundsComment;
            }
        }

        public static void AcceptResults(HER2AmplificationByISHTestOrder testOrder)
        {
            testOrder.Accept();
            if (testOrder.PanelOrderCollection.GetUnacceptedPanelCount() > 0)
            {
                YellowstonePathology.Business.Test.PanelOrder panelOrder = testOrder.PanelOrderCollection.GetUnacceptedPanelOrder();
                panelOrder.AcceptResults();
            }
        }

        public static void UnacceptResults(HER2AmplificationByISHTestOrder testOrder)
        {
            testOrder.Unaccept();
            if (testOrder.PanelOrderCollection.GetAcceptedPanelCount() > 0)
            {
                YellowstonePathology.Business.Test.PanelOrder panelOrder = testOrder.PanelOrderCollection.GetLastAcceptedPanelOrder();
                panelOrder.UnacceptResults();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSERule: INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        public const string LSEColonReferences = "Practice EoGAi, Group PW. Recommendations from the EGAPP Working Group; genetic testing strategies in newly diagnosed " +
            "individuals with colorectal cancer aimed at reducing morbidity and mortality from Lynch Syndrome in relatives. Genet Med. 2009 January; 11(1): 35-41.";

        public const string LSEGYNReferences = "Meyer L, Broaddus R, Lu K. Endometrial cancer and Lynch syndrome: clinical and pathologic considerations. Cancer Control. 2009;16(1):14–22";
        public const string LSEGENReferences = "Le DT, Durham, JN, Smith KN et al.  Mismatch-repair deficiency predicts response of solid tumors to PD-1 blockade.  Science. 2017 July 28; 357(6349): 409-413.";

        public static string IHCMethod = YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method;
		public static string IHCBRAFMethod = "IHC: " + YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method + " BRAF: " + YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.Method;
		public static string IHCBRAFMLHMethod = "IHC: " + YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method + " BRAF: " + YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.Method + " MLH: " + YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisResult.Method;
        public static string IHCMLHMethod = "IHC: " + YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method + " MLH: " + YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisResult.Method;

        public static string GeneralIndication = "Assess tumor for mismatch repair deficiency to determine eligibility for PD-1 blockade therapy; screening for Lynch Syndrome.";
        public static string IHCAllIntactResult = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";

        protected string m_Indication;
        protected string m_ResultName;

		public LSERule()
		{
		}

        public string ResultName
        {
            get { return this.m_ResultName; }
            set { this.m_ResultName = value; }
        }

        public string Indication
        {
            get { return this.m_Indication; }
            set { this.m_Indication = value; }
        }

        public virtual void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            throw new Exception("SetResults Not implemented in LSERule.");
        }

        public bool IncludeInIndicationCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            bool result = false;
            if(panelSetOrderLynchSyndromeEvaluation.LynchSyndromeEvaluationType == LSEType.NOTSET)
            {
                result = true;
            }
            else if (this.m_Indication == panelSetOrderLynchSyndromeEvaluation.LynchSyndromeEvaluationType)
            {
                result = true;
            }
            return result;
        }

        public virtual bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            throw new Exception("IncludeInIHCCollection Not implemented  in LSERule.");
        }

        protected string BuildLossResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            string result = null;
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                result = "Loss of nuclear expression of ";
                List<string> results = new List<string>();
                if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEIHCResult.LossDescription) results.Add("MLH1");
                if (panelSetOrderLynchSyndromeIHC.MSH2Result == LSEIHCResult.LossDescription) results.Add("MSH2");
                if (panelSetOrderLynchSyndromeIHC.MSH6Result == LSEIHCResult.LossDescription) results.Add("MSH6");
                if (panelSetOrderLynchSyndromeIHC.PMS2Result == LSEIHCResult.LossDescription) results.Add("PMS2");

                var joinedResults = string.Join(", ", results);
                if (results.Count > 1)
                {
                    int posOfLastComma = joinedResults.LastIndexOf(",");
                    joinedResults = joinedResults.Remove(posOfLastComma, 1);
                    joinedResults = joinedResults.Insert(posOfLastComma, " and");
                }

                string multiLoss = "proteins. ";
                if (results.Count > 1) multiLoss = "protein. ";
                result = result + joinedResults + " mismatch repair " + multiLoss;
            }
            return result;
        }

        protected string BuildBRAFResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            string result = string.Empty;
            Rules.MethodResult brafResult = this.HasFinalBRAFResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);

            if (brafResult.Success == true)
            {
                result = Environment.NewLine + "BRAF mutation " + brafResult.Message.ToUpper();
            }
            return result;
        }

        protected string BuildMethResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            string result = string.Empty;
            Rules.MethodResult methResult = this.HasFinalMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);

            if (methResult.Success == true)
            {
                result = Environment.NewLine + "MLH1 promoter methylation " + methResult.Message.ToUpper();
            }
            return result;
        }

        protected string BuildMethod(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            string result = string.Empty;
            Rules.MethodResult brafResult = this.HasFinalBRAFResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            Rules.MethodResult methResult = this.HasFinalMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);

            if (brafResult.Success == true && methResult.Success == true) result = IHCBRAFMLHMethod;
            else if (brafResult.Success == true && methResult.Success == false) result = IHCBRAFMethod;
            else if (brafResult.Success == false && methResult.Success == true) result = IHCMLHMethod;
            else result = IHCMethod;

            return result;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public virtual Audit.Model.AuditResult IsOkToSetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            Audit.Model.AuditResult result = panelSetOrderLynchSyndromeEvaluation.IsOkToSetResults();
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                Rules.MethodResult hasIHCResult = this.HasFinalIHCResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
                if(hasIHCResult.Success == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = hasIHCResult.Message;
                }
            }
                return result;
        }

        protected Rules.MethodResult HasFinalIHCResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            Rules.MethodResult result = new Rules.MethodResult();
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                if (panelSetOrderLynchSyndromeIHC.Final == false)
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the " + panelSetLynchSyndromeIHCPanel.PanelSetName + " is not final.";
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to set results as a " + panelSetLynchSyndromeIHCPanel.PanelSetName + " must be ordered.";
            }

            return result;
        }

        protected Rules.MethodResult HasFinalBRAFResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            Rules.MethodResult result = new Rules.MethodResult();
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafMutationAnalysisTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (panelSetOrderBraf.Final == true)
                {
                    result.Success = true;
                    result.Message = panelSetOrderBraf.GetSummaryResult();
                }
                else
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the " + brafV600EKTest.PanelSetName + " is not final.";
                }
            }
            else if (accessionOrder.PanelSetOrderCollection.Exists(brafMutationAnalysisTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafMutationAnalysisTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (brafMutationAnalysisTestOrder.Final == true)
                {
                    result.Success = true;
                    result.Message = brafMutationAnalysisTestOrder.GetSummaryResult();
                }
                else
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the " + brafMutationAnalysisTest.PanelSetName + " is not final.";
                }
            }
            else if (accessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (panelSetOrderRASRAF.Final == true)
                {
                    result.Success = true;
                    result.Message = panelSetOrderRASRAF.GetBrafSummaryResult();
                }
                else
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the " + rasRAFPanelTest.PanelSetName + " is not final.";
                }
            }

            if (result.Success == false && string.IsNullOrEmpty(result.Message) == true) result.Message = "Unable to set results as a BRAF has not been ordered.";

            return result;
        }

        protected Rules.MethodResult HasFinalMethResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            Rules.MethodResult result = new Rules.MethodResult();

            YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                if (panelSetOrderMLH1MethylationAnalysis.Final == true)
                {
                    result.Success = true;
                    result.Message = panelSetOrderMLH1MethylationAnalysis.GetSummaryResult();
                }
                else
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the " + panelSetMLH1.PanelSetName + " is not final.";
                         
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to set results as a " + panelSetMLH1.PanelSetName + " has not been ordered.";
            }

            return result;
        }
    }
}
﻿using System;
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

        public static string GeneralIndication = "Assess tumor for mismatch repair deficiency to determine eligibility for PD-1 blockade therapy; screening for Lynch Syndrome.";
        public static string IHCAllIntactResult = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";

        protected bool m_BRAFRequired;
        protected bool m_MethRequired;
        protected string m_Indication;
        protected string m_ResultName;

        protected LSEResultEnum m_PMS2Result;
		protected LSEResultEnum m_MSH6Result;
		protected LSEResultEnum m_MSH2Result;
		protected LSEResultEnum m_MLH1Result;

        protected string m_BRAFResult;
        protected string m_MethResult;
        //protected string m_AdditionalTesting;

		protected string m_Interpretation;
		protected string m_Result;
        protected string m_Method;
        protected string m_References;

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

        public LSEResultEnum PMS2Result
		{
			get { return this.m_PMS2Result; }
			set { this.m_PMS2Result = value; }
		}

		public LSEResultEnum MSH6Result
		{
			get { return this.m_MSH6Result; }
			set { this.m_MSH6Result = value; }
		}

		public LSEResultEnum MSH2Result
		{
			get { return this.m_MSH2Result; }
			set { this.m_MSH2Result = value; }
		}

        public LSEResultEnum MLH1Result
		{
			get { return this.m_MLH1Result; }
			set { this.m_MLH1Result = value; }
		}        

        public string BRAFResult
        {
            get { return this.m_BRAFResult; }
            set { this.m_BRAFResult = value; }
        }

        public string MethResult
        {
            get { return this.m_MethResult; }
            set { this.m_MethResult = value; }
        }

        public bool BRAFRequired
        {
            get { return this.m_BRAFRequired; }
            set { this.m_BRAFRequired = value; }
        }

        public bool MethRequired
        {
            get { return this.m_MethRequired; }
            set { this.m_MethRequired = value; }
        }

        public string Interpretation
		{
			get { return this.m_Interpretation; }
		}

		public string Result
		{
			get { return this.m_Result; }
		}

        public string Method
        {
            get { return this.m_Method; }
        }

        public string References
        {
            get { return this.m_References; }
        }

        public virtual void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            panelSetOrderLynchSyndromeEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromeEvaluation.Result = this.m_Result;
            panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth = this.BRAFRequired;
            panelSetOrderLynchSyndromeEvaluation.Method = this.m_Method;
            panelSetOrderLynchSyndromeEvaluation.ReportReferences = this.m_References;			
        }

        /*public virtual void SetResultsV2(PanelSetOrderLynchSyndromeEvaluation psoLSE)
        {

            
            if(this.AreAnyLoss() == false)
            {
                this.m_Interpretation = IHCAllIntactResult;
            }
            else
            {
                this.m_Interpretation = this.BuildLossInterpretation();
            }
            

            
            if(this.m_BrafResult != LSEResultEnum.NotPerformed)
            {
                this.m_Interpretation = this.m_Interpretation + Environment.NewLine + "BRAF mutation V600E ";
                if (this.m_BrafResult == LSEResultEnum.Detected) this.m_Interpretation += "DETECTED.";
                if (this.m_BrafResult == LSEResultEnum.NotDetected) this.m_Interpretation += "NOT DETECTED.";
            }

            if (this.m_MethResult != LSEResultEnum.NotPerformed)
            {
                this.m_Interpretation = this.m_Interpretation + Environment.NewLine + "MLH1 promoter methylation ";
                if (this.m_MethResult == LSEResultEnum.Detected) this.m_Interpretation += "DETECTED.";
                if (this.m_MethResult == LSEResultEnum.NotDetected) this.m_Interpretation += "NOT DETECTED.";
            }
            
        }*/

        public virtual bool IsIHCMatch(IHCResult ihcResult)
        {
            bool result = false;
            if (this.MLH1Result == ihcResult.MLH1Result.LSEResult &&
                this.MSH2Result == ihcResult.MSH2Result.LSEResult &&
                this.MSH6Result == ihcResult.MSH6Result.LSEResult &&
                this.PMS2Result == ihcResult.PMS2Result.LSEResult)
            {
                result = true;
            }
            return result;
        }

        public string BuildLossResult()
        {
            string result = "Loss of nuclear expression of ";            
            List<string> results = new List<string>();
            if (this.m_MLH1Result == LSEResultEnum.Loss) results.Add("MLH1");
            if (this.m_MSH2Result == LSEResultEnum.Loss) results.Add("MSH2");
            if (this.m_MSH6Result == LSEResultEnum.Loss) results.Add("MSH6");
            if (this.m_PMS2Result == LSEResultEnum.Loss) results.Add("PMS2");

            var joinedResults = string.Join(", ", results);
            if(results.Count > 1)
            {
                int posOfLastComma = joinedResults.LastIndexOf(",");
                joinedResults = joinedResults.Remove(posOfLastComma, 1);
                joinedResults = joinedResults.Insert(posOfLastComma, " and");
            }
            
            result = result + joinedResults + " mismatch repair proteins. ";
            return result;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Audit.Model.AuditResult IsOkToSetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            Audit.Model.AuditResult result = panelSetOrderLynchSyndromeEvaluation.IsOkToSetResults();
            if(result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if(this.BRAFRequired == true)
                {
                    Rules.MethodResult methodResult = this.HasFinalBRAFResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
                    if(methodResult.Success == false)
                    {
                        result.Status = Audit.Model.AuditStatusEnum.Failure;
                        result.Message = methodResult.Message;
                    }
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (this.MethRequired == true)
                {
                    Rules.MethodResult methodResult = this.HasFinalMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
                    if (methodResult.Success == false)
                    {
                        result.Status = Audit.Model.AuditStatusEnum.Failure;
                        result.Message = methodResult.Message;
                    }
                }
            }

            return result;
        }

        private Rules.MethodResult HasFinalBRAFResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            Rules.MethodResult result = new Rules.MethodResult();
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafMutationAnalysisTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (panelSetOrderBraf.Final == false)
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the BRAF V600E/K Mutation Analysis is not final.";
                }
            }
            else if (accessionOrder.PanelSetOrderCollection.Exists(brafMutationAnalysisTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafMutationAnalysisTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (brafMutationAnalysisTestOrder.Final == false)
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the BRAF Mutation Analysis is not final.";
                }
            }
            else if (accessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
            {
                YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                if (panelSetOrderRASRAF.Final == false)
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the RAS/RAF Panel is not final.";
                }
            }

            if (result.Success == false && string.IsNullOrEmpty(result.Message) == true) result.Message = "Unable to set results as a BRAF has not been ordered.";

            return result;
        }

        private Rules.MethodResult HasFinalMethResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            Rules.MethodResult result = new Rules.MethodResult();

            YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                if (panelSetOrderMLH1MethylationAnalysis.Final == false)
                {
                    result.Success = false;
                    result.Message = "Unable to set results as the MLH1 Methylation Analysis is not final.";
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to set results as a MLH1 Methylation Analysis has not been ordered.";
            }

            return result;
        }
    }
}
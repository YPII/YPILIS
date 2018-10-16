using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
	[PersistentClass("tblEGFRToALKReflexAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class EGFRToALKReflexAnalysisTestOrder : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan, YellowstonePathology.Business.Interface.ISolidTumorTesting
	{
        public static string QNSStatement = "Quantity not sufficient to complete all testing.";

		private string m_Method;
		private string m_Interpretation;
        private string m_TumorNucleiPercentage;
        private bool m_QNSForALK;
        private bool m_QNSForROS1;
        private string m_PDL122C3Result;
        private string m_EGFRMutationAnalysisResult;
        private string m_ROS1ByFISHResult;
        private string m_ALKForNSCLCByFISHResult;
        private string m_BRAFMutationAnalysisResult;
        private string m_PDL1SP142Result;
        private string m_EGFRMutationAnalysisComment;
        private string m_PDL1SP142StainPercent;
        private bool m_QNS;

        public EGFRToALKReflexAnalysisTestOrder() 
        {
            
        }

		public EGFRToALKReflexAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            
		}

        public override void OrderInitialTests(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3Test.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoPDL122C3 = new TestOrderInfo(pdl122C3Test, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorPDL122C3 = new Visitor.OrderTestOrderVisitor(testOrderInfoPDL122C3);
                accessionOrder.TakeATrip(orderTestOrderVisitorPDL122C3);
            }

            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoEGFR = new TestOrderInfo(egfrMutationAnalysisTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorEGFR = new Visitor.OrderTestOrderVisitor(testOrderInfoEGFR);
                accessionOrder.TakeATrip(orderTestOrderVisitorEGFR);
            }

            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoRos1 = new TestOrderInfo(ros1ByfishTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorRos1 = new Visitor.OrderTestOrderVisitor(testOrderInfoRos1);
                accessionOrder.TakeATrip(orderTestOrderVisitorRos1);
            }

            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(alkTest.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoALK = new TestOrderInfo(alkTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorALK = new Visitor.OrderTestOrderVisitor(testOrderInfoALK);
                accessionOrder.TakeATrip(orderTestOrderVisitorALK);
            }

            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(brafTest.PanelSetId, orderTarget.GetId(), true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoBRAF = new TestOrderInfo(brafTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorBRAF = new Visitor.OrderTestOrderVisitor(testOrderInfoBRAF);
                accessionOrder.TakeATrip(orderTestOrderVisitorBRAF);
            }
        }

        [PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				this.m_Method = value;
				NotifyPropertyChanged("Method");
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				this.m_Interpretation = value;
				NotifyPropertyChanged("Interpretation");
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool QNSForALK
        {
            get { return this.m_QNSForALK; }
            set
            {
                this.m_QNSForALK = value;
                NotifyPropertyChanged("QNSForALK");
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool QNSForROS1
        {
            get { return this.m_QNSForROS1; }
            set
            {
                this.m_QNSForROS1 = value;
                NotifyPropertyChanged("QNSForROS1");
            }
        }

        [PersistentProperty()]
        public string PDL122C3Result
        {
            get { return this.m_PDL122C3Result; }
            set
            {
                this.m_PDL122C3Result = value;
                NotifyPropertyChanged("PDL122C3Result");
            }
        }

        [PersistentProperty()]
        public string EGFRMutationAnalysisResult
        {
            get { return this.m_EGFRMutationAnalysisResult; }
            set
            {
                this.m_EGFRMutationAnalysisResult = value;
                NotifyPropertyChanged("EGFRMutationAnalysisResult");
            }
        }

        [PersistentProperty()]
        public string ROS1ByFISHResult
        {
            get { return this.m_ROS1ByFISHResult; }
            set
            {
                this.m_ROS1ByFISHResult = value;
                NotifyPropertyChanged("ROS1ByFISHResult");
            }
        }

        [PersistentProperty()]
        public string ALKForNSCLCByFISHResult
        {
            get { return this.m_ALKForNSCLCByFISHResult; }
            set
            {
                this.m_ALKForNSCLCByFISHResult = value;
                NotifyPropertyChanged("ALKForNSCLCByFISHResult");
            }
        }
        
        [PersistentProperty()]
        public string BRAFMutationAnalysisResult
        {
            get { return this.m_BRAFMutationAnalysisResult; }
            set
            {
                this.m_BRAFMutationAnalysisResult = value;
                NotifyPropertyChanged("BRAFMutationAnalysisResult");
            }
        }

        [PersistentProperty()]
        public string PDL1SP142Result
        {
            get { return this.m_PDL1SP142Result; }
            set
            {
                this.m_PDL1SP142Result = value;
                NotifyPropertyChanged("PDL1SP142Result");
            }
        }

        [PersistentProperty()]
        public string EGFRMutationAnalysisComment
        {
            get { return this.m_EGFRMutationAnalysisComment; }
            set
            {
                this.m_EGFRMutationAnalysisComment = value;
                NotifyPropertyChanged("EGFRMutationAnalysisComment");
            }
        }

        [PersistentProperty()]
        public string PDL1SP142StainPercent
        {
            get { return this.m_PDL1SP142StainPercent; }
            set
            {
                this.m_PDL1SP142StainPercent = value;
                NotifyPropertyChanged("PDL1SP142StainPercent");
            }
        }

        [PersistentProperty()]
        public bool QNS
        {
            get { return this.m_QNS; }
            set
            {
                this.m_QNS = value;
                NotifyPropertyChanged("QNS");
            }
        }

        public YellowstonePathology.Business.Audit.Model.AuditResult IsOkToSetResults()
        {
            Audit.Model.AuditResult result = new Audit.Model.AuditResult();
            result.Status = Audit.Model.AuditStatusEnum.OK;

            if (this.Accepted == true)
            {
                result.Status = Audit.Model.AuditStatusEnum.Failure;
                result.Message += UnableToSetPreviousResults;
            }
            return result;
        }

        public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("EGFR Mutation Analysis");
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
			{
                YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
				result.AppendLine(egfrMutationAnalysisTestOrder.ToResultString(accessionOrder));
			}
			result.AppendLine();

			result.AppendLine("ALK For NSCLC By FISH");
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkForNSCLCByFISHTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
			if(accessionOrder.PanelSetOrderCollection.Exists(alkForNSCLCByFISHTest.PanelSetId) == true)
			{
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);
				result.AppendLine(alkForNSCLCByFISHTestOrder.ToResultString(accessionOrder));
			}
			result.AppendLine();

			result.AppendLine("Interpretation:");
			result.AppendLine(this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}

        public void SetResults(PanelSetOrderCollection panelSetOrderCollection)
        {
            StringBuilder interpretation = new StringBuilder();
            StringBuilder method = new StringBuilder();
            StringBuilder references = new StringBuilder();

            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142Test pdl1SP142Test = new PDL1SP142.PDL1SP142Test();

            YellowstonePathology.Business.PanelSet.Model.PanelSetALKRetired panelSetALKRetired = new PanelSet.Model.PanelSetALKRetired();

            if (panelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
            {
                Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)panelSetOrderCollection.GetPanelSetOrder(egfrMutationAnalysisTest.PanelSetId);
                interpretation.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.Interpretation);
                references.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.ReportReferences);
                method.AppendLine("EGFR: " + egfrMutationAnalysisTestOrder.Method);
                this.m_EGFRMutationAnalysisResult = egfrMutationAnalysisTestOrder.Result;
                this.m_EGFRMutationAnalysisComment = egfrMutationAnalysisTestOrder.Comment;
            }

            if (panelSetOrderCollection.Exists(alkTest.PanelSetId) == true)
            {
                Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkTestOrder = (ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(alkTest.PanelSetId);
                references.AppendLine();
                references.AppendLine("ALK: " + alkTestOrder.ReportReferences);

                interpretation.AppendLine();
                interpretation.AppendLine("ALK: " + alkTestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine("ALK: " + alkTestOrder.Method);
                this.m_ALKForNSCLCByFISHResult = alkTestOrder.Result;
            }
            else if (panelSetOrderCollection.Exists(panelSetALKRetired.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrderReportedSeparately alkForNSCLCByFISHTestOrderReportedSeparately = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrderReportedSeparately();
                this.m_ALKForNSCLCByFISHResult = alkForNSCLCByFISHTestOrderReportedSeparately.Result;
            }

            if (panelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId) == true)
            {
                Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (ROS1ByFISH.ROS1ByFISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(ros1ByfishTest.PanelSetId);
                interpretation.AppendLine();
                interpretation.AppendLine("ROS1: " + ros1ByFISHTestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine("ROS1: " + ros1ByFISHTestOrder.Method);
                this.m_ROS1ByFISHResult = ros1ByFISHTestOrder.Result;
            }

            if (panelSetOrderCollection.Exists(pdl1SP142Test.PanelSetId) == true)
            {
                Test.PDL1SP142.PDL1SP142TestOrder pdl1SP142TestOrder = (PDL1SP142.PDL1SP142TestOrder)panelSetOrderCollection.GetPanelSetOrder(pdl1SP142Test.PanelSetId);
                interpretation.AppendLine();
                interpretation.AppendLine(pdl1SP142TestOrder.PanelSetName + ": " + pdl1SP142TestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine(pdl1SP142TestOrder.PanelSetName + ": " + pdl1SP142TestOrder.Method);
                this.m_PDL1SP142Result = pdl1SP142TestOrder.Result;
                this.m_PDL1SP142StainPercent = pdl1SP142TestOrder.StainPercent;
            }

            if (panelSetOrderCollection.Exists(pdl122C3Test.PanelSetId) == true)
            {
                Test.PDL122C3.PDL122C3TestOrder pdl122C3TestOrder = (PDL122C3.PDL122C3TestOrder)panelSetOrderCollection.GetPanelSetOrder(pdl122C3Test.PanelSetId);
                interpretation.AppendLine();
                interpretation.AppendLine(pdl122C3TestOrder.PanelSetName + ": " + pdl122C3TestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine(pdl122C3TestOrder.PanelSetName + ": " + pdl122C3TestOrder.Method);
                this.m_PDL122C3Result = pdl122C3TestOrder.Result;
            }

            if (panelSetOrderCollection.Exists(brafTest.PanelSetId) == true)
            {
                Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafTestOrder = (BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)panelSetOrderCollection.GetPanelSetOrder(brafTest.PanelSetId);
                interpretation.AppendLine();
                interpretation.AppendLine(brafTestOrder.PanelSetName + ": " + brafTestOrder.Interpretation);

                method.AppendLine();
                method.AppendLine(brafTestOrder.PanelSetName + ": " + brafTestOrder.Method);
                this.m_BRAFMutationAnalysisResult = brafTestOrder.Result;
            }

            char[] lineFeedCharacters = { '\r', '\n' };
            this.Interpretation = interpretation.ToString().TrimEnd(lineFeedCharacters);
            this.Method = method.ToString().TrimEnd(lineFeedCharacters);
            this.ReportReferences = references.ToString().TrimEnd(lineFeedCharacters);
            this.NotifyPropertyChanged(string.Empty);
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.TumorNucleiPercentage) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = "The results cannot be accepted because the Tumor Nuclei Percentage has no value.";
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreComponentTestOrdersFinal(accessionOrder, result);
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreTestResultsPresent(accessionOrder, result);
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoComponentTestResultsMatchPreviousResults(accessionOrder, this, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskAccept;
                }
            }

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToFinalize(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.TumorNucleiPercentage) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = "The results cannot be finalized because the Tumor Nuclei Percentage has no value.";
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreComponentTestOrdersFinal(accessionOrder, result);
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.AreTestResultsPresent(accessionOrder, result);
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoComponentTestResultsMatchPreviousResults(accessionOrder, this, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskFinal;
                }
            }
            return result;
        }

        private void DoComponentTestResultsMatchPreviousResults(AccessionOrder accessionOrder, EGFRToALKReflexAnalysisTestOrder panelSetOrder, Audit.Model.AuditResult result)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142Test pdl1SP142Test = new PDL1SP142.PDL1SP142Test();

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3Test.PanelSetId) == true)
            {
                Test.PDL122C3.PDL122C3TestOrder pdl122C3TestOrder = (PDL122C3.PDL122C3TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pdl122C3Test.PanelSetId);
                if (pdl122C3TestOrder.Result != panelSetOrder.PDL122C3Result)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(pdl122C3TestOrder.PanelSetName, pdl122C3TestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
            {
                Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder  egfrMutationAnalysisTestOrder = (EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(egfrMutationAnalysisTest.PanelSetId);
                if (egfrMutationAnalysisTestOrder.Result != panelSetOrder.EGFRMutationAnalysisResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(egfrMutationAnalysisTestOrder.PanelSetName, egfrMutationAnalysisTestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId) == true)
            {
                Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (ROS1ByFISH.ROS1ByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ros1ByfishTest.PanelSetId);
                if (ros1ByFISHTestOrder.Result != panelSetOrder.ROS1ByFISHResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(ros1ByFISHTestOrder.PanelSetName, ros1ByFISHTestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(alkTest.PanelSetId) == true)
            {
                Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkTestOrder = (ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(alkTest.PanelSetId);
                if (alkTestOrder.Result != panelSetOrder.ALKForNSCLCByFISHResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(alkTestOrder.PanelSetName, alkTestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(brafTest.PanelSetId) == true)
            {
                Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafTestOrder = (BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafTest.PanelSetId);
                if (brafTestOrder.Result != panelSetOrder.BRAFMutationAnalysisResult)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(brafTestOrder.PanelSetName, brafTestOrder.Result);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl1SP142Test.PanelSetId) == true)
            {
                Test.PDL1SP142.PDL1SP142TestOrder pdl1SP142TestOrder = (PDL1SP142.PDL1SP142TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pdl1SP142Test.PanelSetId);
                if (pdl1SP142TestOrder.Result != panelSetOrder.PDL1SP142Result)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Warning;
                    result.Message += MismatchMessage(pdl1SP142TestOrder.PanelSetName, pdl1SP142TestOrder.Result);
                }
            }
        }

        private void AreComponentTestOrdersFinal(AccessionOrder accessionOrder, Audit.Model.AuditResult result)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142Test pdl1SP142Test = new PDL1SP142.PDL1SP142Test();

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3Test.PanelSetId) == true)
            {
                Test.PDL122C3.PDL122C3TestOrder pdl122C3TestOrder = (PDL122C3.PDL122C3TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pdl122C3Test.PanelSetId);
                if (pdl122C3TestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(pdl122C3TestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
            {
                Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(egfrMutationAnalysisTest.PanelSetId);
                if (egfrMutationAnalysisTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(egfrMutationAnalysisTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId) == true)
            {
                Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (ROS1ByFISH.ROS1ByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ros1ByfishTest.PanelSetId);
                if (ros1ByFISHTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(ros1ByFISHTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(alkTest.PanelSetId) == true)
            {
                Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkTestOrder = (ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(alkTest.PanelSetId);
                if (alkTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(alkTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(brafTest.PanelSetId) == true)
            {
                Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafTestOrder = (BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafTest.PanelSetId);
                if (brafTestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(brafTestOrder.PanelSetName);
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl1SP142Test.PanelSetId) == true)
            {
                Test.PDL1SP142.PDL1SP142TestOrder pdl1SP142TestOrder = (PDL1SP142.PDL1SP142TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pdl1SP142Test.PanelSetId);
                if (pdl1SP142TestOrder.Final == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFinaledMessage(pdl1SP142TestOrder.PanelSetName);
                }
            }
        }

        private void AreTestResultsPresent(AccessionOrder accessionOrder, Audit.Model.AuditResult result)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest egfrMutationAnalysisTest = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest();
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByfishTest = new ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkTest = new ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142Test pdl1SP142Test = new PDL1SP142.PDL1SP142Test();

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3Test.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_PDL122C3Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("PDL122C3Result");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrMutationAnalysisTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_EGFRMutationAnalysisResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("EGFRMutationAnalysisResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(ros1ByfishTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_ROS1ByFISHResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("ROS1ByFISHResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(alkTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_ALKForNSCLCByFISHResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("ALKForNSCLCByFISHResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(brafTest.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_BRAFMutationAnalysisResult) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("BRAFMutationAnalysisResult");
                }
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(pdl1SP142Test.PanelSetId) == true)
            {
                if (string.IsNullOrEmpty(this.m_PDL1SP142Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message += NotFilledMessage("PDL1SP142Result");
                }
            }
        }

        public void DoesPDL122C3ResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_PDL122C3Result != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_PDL122C3Result);
            }
        }

        public void DoesEGFRMutationAnalysisResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_EGFRMutationAnalysisResult != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_EGFRMutationAnalysisResult);
            }
        }

        public void DoesROS1ByFISHResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_ROS1ByFISHResult != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_ROS1ByFISHResult);
            }
        }

        public void DoesALKForNSCLCByFISHResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_ALKForNSCLCByFISHResult != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_ALKForNSCLCByFISHResult);
            }
        }

        public void DoesBRAFMutationAnalysisResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_BRAFMutationAnalysisResult != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_BRAFMutationAnalysisResult);
            }
        }

        public void DoesPDL1SP142ResultMatch(string result, Audit.Model.AuditResult auditResult)
        {
            if (this.Final == true && this.m_PDL1SP142Result != result)
            {
                auditResult.Status = Audit.Model.AuditStatusEnum.Warning;
                auditResult.Message += MismatchMessage(this.PanelSetName, this.m_PDL1SP142Result);
            }
        }

        /*public override void SetPreviousResults(PanelSetOrder panelSetOrder)
        {
            EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (EGFRToALKReflexAnalysisTestOrder)panelSetOrder;
            egfrToALKReflexAnalysisTestOrder.Method = this.m_Method;
            egfrToALKReflexAnalysisTestOrder.Interpretation = this.m_Interpretation;
            egfrToALKReflexAnalysisTestOrder.TumorNucleiPercentage = this.m_TumorNucleiPercentage;
            egfrToALKReflexAnalysisTestOrder.QNSForALK = this.m_QNSForALK;
            egfrToALKReflexAnalysisTestOrder.QNSForROS1 = this.m_QNSForROS1;
            egfrToALKReflexAnalysisTestOrder.PDL122C3Result = this.m_PDL122C3Result;
            egfrToALKReflexAnalysisTestOrder.EGFRMutationAnalysisResult = this.m_EGFRMutationAnalysisResult;
            egfrToALKReflexAnalysisTestOrder.ROS1ByFISHResult = this.m_ROS1ByFISHResult;
            egfrToALKReflexAnalysisTestOrder.ALKForNSCLCByFISHResult = this.m_ALKForNSCLCByFISHResult;
            egfrToALKReflexAnalysisTestOrder.BRAFMutationAnalysisResult = this.m_BRAFMutationAnalysisResult;
            egfrToALKReflexAnalysisTestOrder.PDL1SP142Result = this.m_PDL1SP142Result;
            base.SetPreviousResults(panelSetOrder);
    }

    public override void ClearPreviousResults()
        {
            this.m_Method = null;
            this.m_Interpretation = null;
            this.m_TumorNucleiPercentage = null;
            this.m_QNSForALK = false;
            this.m_QNSForROS1 = false;
            this.m_PDL122C3Result = null;
            this.m_EGFRMutationAnalysisResult = null;
            this.m_ROS1ByFISHResult = null;
            this.m_ALKForNSCLCByFISHResult = null;
            this.m_BRAFMutationAnalysisResult = null;
            this.m_PDL1SP142Result = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToSetPreviousResults(panelSetOrder, accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoComponentTestResultsMatchPreviousResults(accessionOrder, (EGFRToALKReflexAnalysisTestOrder)panelSetOrder, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskSetPreviousResults;
                }
            }

            return result;
        }*/
    }
}

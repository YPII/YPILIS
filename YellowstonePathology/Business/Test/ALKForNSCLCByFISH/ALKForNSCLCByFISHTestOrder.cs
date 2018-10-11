using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    [PersistentClass("tblALKForNSCLCByFISHTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class ALKForNSCLCByFISHTestOrder : YellowstonePathology.Business.Test.PanelSetOrder, YellowstonePathology.Business.Interface.ISolidTumorTesting
	{
		private string m_Result;
		private string m_Interpretation;
		private string m_ReferenceRange;
		private string m_ProbeSetDetail;
		private string m_NucleiScored;
		private string m_NucleiPercent;
		private string m_Fusions;
        private string m_Method;
		private string m_ReportDisclaimer;
        private string m_ThreeFPercentage;
        private bool m_ALKGeneAmplification;
        private string m_TumorNucleiPercentage;        

        public ALKForNSCLCByFISHTestOrder()
        {

        }

		public ALKForNSCLCByFISHTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{

		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ReferenceRange
		{
			get { return this.m_ReferenceRange; }
			set
			{
				if (this.m_ReferenceRange != value)
				{
					this.m_ReferenceRange = value;
					this.NotifyPropertyChanged("ReferenceRange");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "5", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string NucleiPercent
		{
			get { return this.m_NucleiPercent; }
			set
			{
				if (this.m_NucleiPercent != value)
				{
					this.m_NucleiPercent = value;
					this.NotifyPropertyChanged("NucleiPercent");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Fusions
		{
			get { return this.m_Fusions; }
			set
			{
				if (this.m_Fusions != value)
				{
					this.m_Fusions = value;
					this.NotifyPropertyChanged("Fusions");
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
        public string ReportDisclaimer
        {
			get { return this.m_ReportDisclaimer; }
            set
            {
				if (this.m_ReportDisclaimer != value)
                {
					this.m_ReportDisclaimer = value;
                    this.NotifyPropertyChanged("ReportDisclaimer");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ThreeFPercentage
        {
            get { return this.m_ThreeFPercentage; }
            set
            {
                if (this.m_ThreeFPercentage != value)
                {
                    this.m_ThreeFPercentage = value;
                    this.NotifyPropertyChanged("ThreeFPercentage");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ALKGeneAmplification
        {
            get { return this.m_ALKGeneAmplification; }
            set
            {
                if (this.m_ALKGeneAmplification != value)
                {
                    this.m_ALKGeneAmplification = value;
                    this.NotifyPropertyChanged("ALKGeneAmplification");
                }
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

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Nuclei Scored: " + this.m_NucleiScored);
			result.AppendLine();

			result.AppendLine("Reference Range: " + this.m_ReferenceRange);
			result.AppendLine();

			result.AppendLine("Nuclei Percent: " + this.m_NucleiPercent);
			result.AppendLine();

			result.AppendLine("Fusions: " + this.m_Fusions);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			result.AppendLine("Probeset Detail: " + this.m_ProbeSetDetail);
			result.AppendLine();

            result.AppendLine("Method: " + this.m_Method);
            result.AppendLine();

			return result.ToString();
		}

        public override void SetPreviousResults(PanelSetOrder pso)
        {
            ALKForNSCLCByFISHTestOrder panelSetOrder = (ALKForNSCLCByFISHTestOrder)pso;
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.ReferenceRange = this.m_ReferenceRange;
            panelSetOrder.ProbeSetDetail = this.m_ProbeSetDetail;
            panelSetOrder.NucleiScored = this.m_NucleiScored;
            panelSetOrder.NucleiPercent = this.m_NucleiPercent;
            panelSetOrder.Fusions = this.m_Fusions;
            panelSetOrder.Method = this.Method;
            panelSetOrder.ReportDisclaimer = this.m_ReportDisclaimer;
            panelSetOrder.ThreeFPercentage = this.m_ThreeFPercentage;
            panelSetOrder.ALKGeneAmplification = this.m_ALKGeneAmplification;
            panelSetOrder.TumorNucleiPercentage = this.m_TumorNucleiPercentage;
            base.SetPreviousResults(pso);
        }

        public override void ClearPreviousResults()
        {
            this.m_Result = null;
            this.m_Interpretation = null;
            this.m_ReferenceRange = null;
            this.m_ProbeSetDetail = null;
            this.m_NucleiScored = null;
            this.m_NucleiPercent = null;
            this.m_Fusions = null;
            this.Method = null;
            this.m_ReportDisclaimer = null;
            this.m_ThreeFPercentage = null;
            this.m_ALKGeneAmplification = false;
            this.m_TumorNucleiPercentage = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToSetPreviousResults(panelSetOrder, accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                ALKForNSCLCByFISHTestOrder pso = (ALKForNSCLCByFISHTestOrder)panelSetOrder;
                this.DoesFinalSummaryResultMatch(accessionOrder, pso.Result, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskSetPreviousResults;
                }
            }

            return result;
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToAccept;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoesFinalSummaryResultMatch(accessionOrder, this.m_Result, result);
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
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToFinal;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoesFinalSummaryResultMatch(accessionOrder, this.m_Result, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskFinal;
                }
            }

            return result;
        }

        private void DoesFinalSummaryResultMatch(AccessionOrder accessionOrder, string result, Audit.Model.AuditResult auditResult)
        {
            Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest egfrToALKReflexAnalysisTest = new EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest();

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrToALKReflexAnalysisTest.PanelSetId) == true)
            {
                Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(egfrToALKReflexAnalysisTest.PanelSetId);
                egfrToALKReflexAnalysisTestOrder.DoesALKForNSCLCByFISHResultMatch(result, auditResult);
            }
        }
    }
}

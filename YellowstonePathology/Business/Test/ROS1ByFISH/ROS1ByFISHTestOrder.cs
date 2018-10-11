using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
	[PersistentClass("tblROS1ByFISHTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class ROS1ByFISHTestOrder : YellowstonePathology.Business.Test.PanelSetOrder, YellowstonePathology.Business.Interface.ISolidTumorTesting
    {        
        private string m_Result;
        private string m_Interpretation;
        private string m_ReferenceRange;
        private string m_ProbeSetDetail;
        private string m_NucleiScored;
        private string m_Method;
        private string m_ReportDisclaimer;        
        private string m_TumorNucleiPercentage;

        public ROS1ByFISHTestOrder()
        {

        }

        public ROS1ByFISHTestOrder(string masterAccessionNo, string reportNo)
        {
            this.MasterAccessionNo = masterAccessionNo;
            this.ReportNo = reportNo;            
        }

        public ROS1ByFISHTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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

		public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
		{
            return null;
		}

        public override void SetPreviousResults(PanelSetOrder pso)
        {
            ROS1ByFISHTestOrder panelSetOrder = (ROS1ByFISHTestOrder)pso;
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.ReferenceRange = this.m_ReferenceRange;
            panelSetOrder.ProbeSetDetail = this.m_ProbeSetDetail;
            panelSetOrder.NucleiScored = this.m_NucleiScored;
            panelSetOrder.Method = this.m_Method;
            panelSetOrder.ReportDisclaimer = this.m_ReportDisclaimer;
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
            this.Method = null;
            this.m_ReportDisclaimer = null;
            this.m_TumorNucleiPercentage = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToSetPreviousResults(panelSetOrder, accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                ROS1ByFISHTestOrder pso = (ROS1ByFISHTestOrder)panelSetOrder;
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
                egfrToALKReflexAnalysisTestOrder.DoesROS1ByFISHResultMatch(result, auditResult);
            }
        }
    }
}

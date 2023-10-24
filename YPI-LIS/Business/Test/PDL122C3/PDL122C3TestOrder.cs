using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.PDL122C3
{
    [PersistentClass("tblPDL122C3TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class PDL122C3TestOrder : PanelSetOrder
    {
        private string m_Result;
        private string m_StainPercent;
        private string m_Method;
        private string m_Comment;
        private string m_Interpretation;

        public PDL122C3TestOrder()
        {
        	
        }
        
        public PDL122C3TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string StainPercent
        {
            get { return this.m_StainPercent; }
            set
            {
                if (this.m_StainPercent != value)
                {
                    this.m_StainPercent = value;
                    this.NotifyPropertyChanged("StainPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
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

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            result.AppendLine("Stain Percent: " + this.m_StainPercent);
            result.AppendLine();

            return result.ToString();
        }

        public override void SetPreviousResults(PanelSetOrder pso)
        {
            PDL122C3TestOrder panelSetOrder = (PDL122C3TestOrder)pso;
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Method = this.Method;
            panelSetOrder.Comment = this.Comment;
            panelSetOrder.Interpretation = this.m_Interpretation;
            base.SetPreviousResults(pso);
        }

        public override void ClearPreviousResults()
        {
            this.m_Result = null;
            this.m_Method = null;
            this.m_Comment = null;
            this.m_Interpretation = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToSetPreviousResults(panelSetOrder, accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                PDL122C3TestOrder pdl122C3TestOrder = (PDL122C3TestOrder)panelSetOrder;
                this.DoesFinalSummaryResultMatch(accessionOrder, pdl122C3TestOrder.Result, result);
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
                if (string.IsNullOrEmpty(this.StainPercent) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = "The results cannot be accepted because the stain percent is not set.";
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
                if (string.IsNullOrEmpty(this.StainPercent) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = "The case cannot be finalized because the stain percent is not set.";
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
                egfrToALKReflexAnalysisTestOrder.DoesPDL122C3ResultMatch(result, auditResult);
            }
        }
    }
}

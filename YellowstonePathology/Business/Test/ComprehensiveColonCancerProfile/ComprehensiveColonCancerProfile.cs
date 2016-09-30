using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Rules;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	[PersistentClass("tblComprehensiveColonCancerProfile", "tblPanelSetOrder", "YPIDATA")]
	public class ComprehensiveColonCancerProfile : PanelSetOrder
	{
        private string m_Interpretation;
        private bool m_IncludeTestsPerformedOnOtherBlocks;

		public ComprehensiveColonCancerProfile() 
        {
            this.m_Interpretation = "The results are compatible with a (sporadically occurring / Lynch Syndrome-associated) tumor.  Furthermore, the tumor exhibits " +
                "(wild-type KRAS and BRAF / a ___ KRAS mutation / wild-type KRAS with BRAF V600E mutation), which indicates that the patient (may respond to anti-EGFR therapy " +
                "/ is unlikely to respond to anti-EGFR therapy).";
        }

		public ComprehensiveColonCancerProfile(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            
		}        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool IncludeTestsPerformedOnOtherBlocks
        {
            get { return this.m_IncludeTestsPerformedOnOtherBlocks; }
            set
            {
                if (this.m_IncludeTestsPerformedOnOtherBlocks != value)
                {
                    this.m_IncludeTestsPerformedOnOtherBlocks = value;
                    this.NotifyPropertyChanged("IncludeTestsPerformedOnOtherBlocks");
                }
            }
        }    

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			return "Interpretation: " + this.m_Interpretation;
		}

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult result = new AuditResult();
            if (this.Final == true)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = "This case cannot be finalized because it is already finalized.";
            }
            else
            {
                YellowstonePathology.Business.Audit.Model.ComprehensiveColonCancerProfileFinalAudit comprehensiveColonCancerProfileFinalAudit = new ComprehensiveColonCancerProfileFinalAudit(accessionOrder);
                comprehensiveColonCancerProfileFinalAudit.Run();
                result.Status = comprehensiveColonCancerProfileFinalAudit.Status;
                result.Message = comprehensiveColonCancerProfileFinalAudit.Message.ToString();
            }
            return result;
        }
    }
}

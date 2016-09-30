using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PathologistSignoutAuditCollection : AuditCollection
    {
        public PathologistSignoutAuditCollection()
        { }

        public PathologistSignoutAuditCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = accessionOrder.PanelSetOrderCollection.GetSurgical();
            this.Add(new AncillaryStudiesAreHandledAudit(surgicalTestOrder));
            this.Add(new SurgicalCaseHasQuestionMarksAudit(accessionOrder, surgicalTestOrder));
            this.Add(new SigningUserIsAssignedUserAudit(surgicalTestOrder));
            this.Add(new SvhCaseHasMRNAndAccountNoAudit(accessionOrder));
            this.Add(new CaseHasNotFoundClientAudit(accessionOrder));
            this.Add(new CaseHasNotFoundProviderAudit(accessionOrder));
            this.Add(new DistributionCanBeSetAudit(accessionOrder));
            this.Add(new CaseHasUnfinaledPeerReviewAudit(accessionOrder));
            this.Add(new GradedStainsAreHandledAudit(surgicalTestOrder));
            this.Add(new IntraoperativeConsultationCorrelationAudit(surgicalTestOrder));
            this.Add(new PapCorrelationIsRequiredAudit(accessionOrder));
            this.Add(new PQRSIsRequiredAudit(accessionOrder));
            this.Add(new NonASCIICharacterAudit(surgicalTestOrder));
            this.Add(new LynchSyndromeAudit(accessionOrder));
            this.Add(new CCCPAudit(accessionOrder));
            this.Add(new BRAFMetastaticMelanomaAudit(accessionOrder));
            this.Add(new HPV1618ForSiteAudit(accessionOrder));
            this.Add(new RASRAForMetastaticColorectalCancerAudit(accessionOrder));
            this.Add(new PNHOnBoneMarrowSpecimenAudit(accessionOrder));
            this.Add(new PDL1Audit(accessionOrder));
        }

        public AuditCollection GetAuditMessageCollection()
        {
            AuditCollection result = new AuditCollection();
            foreach(Audit audit in this)
            {
                if(audit.GetType() == typeof(AncillaryStudiesAreHandledAudit) ||
                    audit.GetType() == typeof(SurgicalCaseHasQuestionMarksAudit) ||
                    audit.GetType() == typeof(SigningUserIsAssignedUserAudit) ||
                    audit.GetType() == typeof(SvhCaseHasMRNAndAccountNoAudit) ||
                    audit.GetType() == typeof(CaseHasNotFoundClientAudit) ||
                    audit.GetType() == typeof(CaseHasNotFoundProviderAudit) ||
                    audit.GetType() == typeof(DistributionCanBeSetAudit) ||
                    audit.GetType() == typeof(CaseHasUnfinaledPeerReviewAudit) ||
                    audit.GetType() == typeof(GradedStainsAreHandledAudit) ||
                    audit.GetType() == typeof(IntraoperativeConsultationCorrelationAudit))
                {
                    result.Add(audit);
                }
            }
            return result;
        }

        public AuditCollection GetSuggestedTestAuditCollection()
        {
            AuditCollection result = new AuditCollection();
            foreach (Audit audit in this)
            {
                if (audit.GetType() == typeof(LynchSyndromeAudit) ||
                    audit.GetType() == typeof(CCCPAudit) ||
                    audit.GetType() == typeof(BRAFMetastaticMelanomaAudit) ||
                    audit.GetType() == typeof(HPV1618ForSiteAudit) ||
                    audit.GetType() == typeof(RASRAForMetastaticColorectalCancerAudit) ||
                    audit.GetType() == typeof(PNHOnBoneMarrowSpecimenAudit) ||
                    audit.GetType() == typeof(PDL1Audit))
                {
                    result.Add(audit);
                }
            }
            return result;
        }

        public PapCorrelationIsRequiredAudit GetPapCorrelationAudit()
        {
            PapCorrelationIsRequiredAudit result = null;
            foreach (Audit audit in this)
            {
                if (audit.GetType() == typeof(PapCorrelationIsRequiredAudit))
                {
                    result = audit as PapCorrelationIsRequiredAudit;
                }
            }
            return result;
        }

        public PQRSIsRequiredAudit GetPQRSIsRequiredAudit()
        {
            PQRSIsRequiredAudit result = null;
            foreach (Audit audit in this)
            {
                if (audit.GetType() == typeof(PQRSIsRequiredAudit))
                {
                    result = audit as PQRSIsRequiredAudit;
                }
            }
            return result;
        }

        public NonASCIICharacterAudit GetNonASCIICharacterAudit()
        {
            NonASCIICharacterAudit result = null;
            foreach (Audit audit in this)
            {
                if (audit.GetType() == typeof(NonASCIICharacterAudit))
                {
                    result = audit as NonASCIICharacterAudit;
                }
            }
            return result;
        }
    }
}
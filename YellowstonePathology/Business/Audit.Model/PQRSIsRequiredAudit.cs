using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PQRSIsRequiredAudit : Audit
    {
        private YellowstonePathology.Business.Surgical.PQRSMeasure m_PQRSMeasure;
        private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public PQRSIsRequiredAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            YellowstonePathology.Business.Surgical.PQRSMeasureCollection pqrsCollection = YellowstonePathology.Business.Surgical.PQRSMeasureCollection.GetAll();
            int patientAge = YellowstonePathology.Business.Helper.PatientHelper.GetAge(this.m_AccessionOrder.PBirthdate.Value);
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure in pqrsCollection)
                {
                    if (pqrsMeasure.DoesMeasureApply(surgicalTestOrder, surgicalSpecimen, patientAge) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append("A PQRS code must be applied.");
                        this.m_PQRSMeasure = pqrsMeasure;
                        this.m_SurgicalSpecimen = surgicalSpecimen;
                        break;
                    }
                }
                if (this.m_Status == AuditStatusEnum.Failure) break;
            }
        }

        public YellowstonePathology.Business.Surgical.PQRSMeasure PQRSMeasure
        {
            get { return this.m_PQRSMeasure; }
        }

        public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen SurgicalSpecimen
        {
            get { return this.m_SurgicalSpecimen; }
        }
    }
}

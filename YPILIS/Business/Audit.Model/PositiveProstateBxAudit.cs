using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PositiveProstateBxAudit : Audit
    {
        private Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private Business.Test.AccessionOrder m_AccessionOrder;

        public PositiveProstateBxAudit(Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            Business.Test.AccessionOrder accessionOrder)
        {
            this.m_SurgicalTestOrder = surgicalTestOrder;            
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            List<int> clientIdList = new List<int>();
            clientIdList.Add(1494);
            clientIdList.Add(1496);

            if (clientIdList.Contains(this.m_AccessionOrder.ClientId))
            {
                if (Matches())
                {
                    if (this.m_Message.Length == 0)
                    {
                        this.m_Message.Append("Deerlodge has requested that a call be made to the provider for all positive prostate biopsys.");                        
                    }
                    this.m_Status = AuditStatusEnum.Failure;
                }
            }            
        }

        private bool Matches()
        {
            string keyDiagnosisWord = "carcinoma";
            string keySpecimenDescriptionWord = "prostate";            

            return (this.m_SurgicalTestOrder.SurgicalSpecimenCollection.Any(x => x.Diagnosis.Contains(keyDiagnosisWord)) && 
                this.m_AccessionOrder.SpecimenOrderCollection.Any(x => x.Description.Contains(keySpecimenDescriptionWord)));                       
        }        
    }
}

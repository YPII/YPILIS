using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class MetastaticColorectalAudit : Audit
    {
        private Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private Business.Test.AccessionOrder m_AccessionOrder;

        public MetastaticColorectalAudit(Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            Business.Test.AccessionOrder accessionOrder)
        {
            this.m_SurgicalTestOrder = surgicalTestOrder;            
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            if(!this.m_AccessionOrder.PanelSetOrderCollection.Any(s => s.PanelSetId == 281))
            {
                if (Matches())
                {
                    if(this.m_Message.Length == 0)
                    {
                        this.m_Message.Append("New diagnoses of metastatic colorectal carcinoma require NeoType colorectal tumor profile testing");
                    }                    
                    this.m_Status = AuditStatusEnum.Failure;
                }
            }            
        }

        private bool Matches()
        {
            string keyDiagnosisWord = "metastatic";

            List<string> words = new List<string>();
            words.Add("colon");
            words.Add("colorectal");
            words.Add("rectal");

            return (this.m_SurgicalTestOrder.SurgicalSpecimenCollection.Any(x => x.Diagnosis.Contains(keyDiagnosisWord)) &&            
                this.m_SurgicalTestOrder.SurgicalSpecimenCollection.Any(x => words.Any(s => x.Diagnosis.Contains(s)))) ||
                (this.m_SurgicalTestOrder.SurgicalSpecimenCollection.Any(x => x.Diagnosis.Contains(keyDiagnosisWord)) &&            
                words.Any(w => (string.IsNullOrEmpty(this.m_SurgicalTestOrder.Comment) == false && this.m_SurgicalTestOrder.Comment.Contains(w))));                       
        }        
    }
}

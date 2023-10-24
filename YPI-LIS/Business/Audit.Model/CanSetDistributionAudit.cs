using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CanSetDistributionAudit : AccessionOrderAudit
    {
        private YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList m_PhysicianClientDistributionCollection;

        public CanSetDistributionAudit(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection) : base(accessionOrder)
        {
            this.m_PhysicianClientDistributionCollection = physicianClientDistributionCollection;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            if (this.m_PhysicianClientDistributionCollection.DoesEpicDistributionExist() == true)
            {
                if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == true || string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.Append("Unable to set distribution as the Account Number or the Medical Record Number is missing.");
                }
            }
        }
    }
}

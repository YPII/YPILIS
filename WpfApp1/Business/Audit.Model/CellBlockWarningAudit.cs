using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CellBlockWarningAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public CellBlockWarningAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Message.Clear();

            this.m_Status = AuditStatusEnum.OK;
            StringBuilder resultMsg = new StringBuilder();
            StringBuilder commentMsg = new StringBuilder();

            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if(specimenOrder.AliquotOrderCollection.HasCellBlock() == true)
                {
                    this.m_Status = AuditStatusEnum.Warning;
                    this.m_Message.AppendLine("Please take note that a cell block was made that you may not be aware of.");
                }
            }            
        }
    }
}

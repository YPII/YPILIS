/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 12/29/2015
 * Time: 11:57 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Audit.Model
{
	/// <summary>
	/// Description of CodyCassetteColorAudit.
	/// </summary>
	public class CodyCassetteColorAudit : Audit
	{
		private Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotCollection;
		
		public CodyCassetteColorAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.AliquotOrderCollection aliquotCollection)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_AliquotCollection = aliquotCollection;
		}

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            if(this.m_AliquotCollection.HasDirectPrintBlocks() == true)
            {
            	if(this.m_AccessionOrder.PrintMateColumnNumber == 4 && this.m_AccessionOrder.AccessioningFacilityId == "YPIBLGS")
            	{
            		this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.Append("The Accessioning Facility should be Cody when the cassette color is pink.");
            	}
            }
        }
	}
}

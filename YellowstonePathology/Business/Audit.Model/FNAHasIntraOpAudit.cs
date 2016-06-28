/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 12/29/2015
 * Time: 11:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Audit.Model
{
	/// <summary>
	/// Description of FNAHasIntraOpAudit.
	/// </summary>
	public class FNAHasIntraOpAudit : Audit
	{
		private Test.AccessionOrder m_AccessionOrder;
		
		public FNAHasIntraOpAudit(Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
		}

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            
            YellowstonePathology.Business.Test.Surgical.SurgicalTest surgicalTest = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();
            if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(surgicalTest.PanelSetId))
            {
	            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
	            if(this.m_AccessionOrder.SpecimenOrderCollection.ContainsString("Fine") == true)
	            {
		            this.m_Status = AuditStatusEnum.Failure;
	            	foreach(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
	            	{
	            		if(surgicalSpecimen.IntraoperativeConsultationResultCollection.Count > 0)
	            		{
				            this.m_Status = AuditStatusEnum.OK;
				            break;
	            		}
	            	}
	            	
	            	if(this.m_Status == AuditStatusEnum.Failure)
	            	{
	            		this.m_Message.Append("FNA without Frozen.");
	            	}
	            }
            }
        }
	}
}

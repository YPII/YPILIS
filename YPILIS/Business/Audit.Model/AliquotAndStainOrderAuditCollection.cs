/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 12/29/2015
 * Time: 12:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Audit.Model
{
	/// <summary>
	/// Description of AliquotAndStainOrderAuditCollection.
	/// </summary>
	public class AliquotAndStainOrderAuditCollection : AuditCollection
	{		
		private FNAHasIntraOpAudit m_FNAHasIntraOpAudit;
		
		public AliquotAndStainOrderAuditCollection(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.AliquotOrderCollection aliquotCollection)
		{	
			this.m_FNAHasIntraOpAudit = new FNAHasIntraOpAudit(accessionOrder);
					
			this.Add(new HasColorForDirectPrintBlocksAudit(accessionOrder, aliquotCollection));
			this.Add(new CodyCassetteColorAudit(accessionOrder, aliquotCollection));
			this.Add(this.m_FNAHasIntraOpAudit);
		}			
		
		public FNAHasIntraOpAudit FNAHasIntraOpAudit
		{
			get{return this.m_FNAHasIntraOpAudit;}
		}
	}
}

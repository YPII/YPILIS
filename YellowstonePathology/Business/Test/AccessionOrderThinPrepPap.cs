using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
	public class AccessionOrderThinPrepPap : AccessionOrder
	{
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public AccessionOrderThinPrepPap(string masterAccessionNo, string objectId) 
            : base(masterAccessionNo, objectId)
        {

        }

        public void AddDefaultChildren(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)			
		{			
			this.m_SystemIdentity = systemIdentity;			
		}

		public void SetSpecimen(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			
		}				
	}
}

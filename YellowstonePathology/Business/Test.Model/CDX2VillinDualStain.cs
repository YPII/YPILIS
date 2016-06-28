using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CDX2VillinDualStain : DualStain
	{
		public CDX2VillinDualStain()
		{
            this.m_TestId = "CDX2VN";
			this.m_TestName = "CDX-2/Villin";
			this.m_FirstTest = new CDX2();
			this.m_SecondTest = new Villin();            
		}
	}
}

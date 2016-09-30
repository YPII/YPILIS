using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class TTF1NapsinADualStain : DualStain
	{
		public TTF1NapsinADualStain()
		{
            this.m_TestId = "TTFNPSNA";
			this.m_TestName = "TTF-1/Napsin A";
			this.m_FirstTest = new TTF1();
			this.m_SecondTest = new NapsinA();			
		}
	}
}

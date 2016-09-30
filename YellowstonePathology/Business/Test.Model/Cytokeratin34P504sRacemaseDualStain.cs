using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Cytokeratin34P504sRacemaseDualStain : DualStain
	{
		public Cytokeratin34P504sRacemaseDualStain()
		{
            this.m_TestId = "CK34P504RM";
			this.m_TestName = "Cytokeratin 34/P504s racemase";
			this.m_FirstTest = new Cytokeratin34();
			this.m_SecondTest = new P504sRacemase();

			this.m_DepricatedFirstTestId = 227;
			this.m_DepricatedSecondTestId = 228;
		}
	}
}

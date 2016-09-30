/*
 * Created by SharpDevelop.
 * User: William.Copland
 * Date: 12/16/2015
 * Time: 12:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Test.Model
{
	/// <summary>
	/// Description of P40.
	/// </summary>
	public class P40 : ImmunoHistochemistryTest
	{
		public P40()
		{
			this.m_TestId = 359;
			this.m_TestName = "P40";
            this.m_TestAbbreviation = "P40";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}

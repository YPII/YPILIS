using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CEBPA
{
	public class CEBPAResult
	{
		protected string m_Result;
		protected string m_SNPResult;
		protected string m_Interpretation;
		protected string m_Method;
		protected string m_References;

		public CEBPAResult()
		{
		}

		public void SetResults(PanelSetOrderCEBPA panelSetOrderCEBPA)
		{
			panelSetOrderCEBPA.Result = this.m_Result;
			panelSetOrderCEBPA.SNPResult = this.m_SNPResult;
			panelSetOrderCEBPA.Interpretation = this.m_Interpretation;
			panelSetOrderCEBPA.Method = this.m_Method;
			panelSetOrderCEBPA.References = this.m_References;
		}
	}
}

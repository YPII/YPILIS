using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class IHCResult
    {
        public static string Method = "Immunohistochemistry was performed on paraffin embedded tissue using antibody clones for MLH1 (G168-15), " +
            "PMS2 (MRQ-28), MSH2 (G219-1129), and MSH6 (44). These tests were run on the Ventana Ultra automated immunohistochemical platform.";

        protected string m_ResultCode;
		protected LSEIHCResult m_MLH1Result;
		protected LSEIHCResult m_MSH2Result;
		protected LSEIHCResult m_MSH6Result;
		protected LSEIHCResult m_PMS2Result;

        public IHCResult()
        {

        }

        public virtual void SetResults(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrder)
        {
            panelSetOrder.ResultCode = this.m_ResultCode;
            panelSetOrder.MLH1Result = this.m_MLH1Result.Description;
            panelSetOrder.MSH2Result = this.m_MSH2Result.Description;
            panelSetOrder.MSH6Result = this.m_MSH6Result.Description;
			panelSetOrder.PMS2Result = this.m_PMS2Result.Description;
        }

        public string ResultCode
        {
            get { return this.m_ResultCode; }
        }

		public LSEIHCResult MLH1Result
        {
            get { return this.m_MLH1Result; }
        }

		public LSEIHCResult MSH2Result
        {
            get { return this.m_MSH2Result; }
        }

		public LSEIHCResult MSH6Result
        {
            get { return this.m_MSH6Result; }
        }

		public LSEIHCResult PMS2Result
        {
            get { return this.m_PMS2Result; }
        }
        
		public static IHCResult CreateResultFromResultCode(string resultCode)
		{
			IHCResult result = null;
			if (string.IsNullOrEmpty(resultCode) == false)
			{
				IHCResultLossOfNuclearExpressionMLH1PMS2 iHCResultLossOfNuclearExpressionMLH1PMS2 = new IHCResultLossOfNuclearExpressionMLH1PMS2();
				IHCResultLossOfNuclearExpressionMSH2MSH6 iHCResultLossOfNuclearExpressionMSH2MSH6 = new IHCResultLossOfNuclearExpressionMSH2MSH6();
				IHCResultLossOfNuclearExpressionMSH6 iHCResultLossOfNuclearExpressionMSH6 = new IHCResultLossOfNuclearExpressionMSH6();
				IHCResultLossOfNuclearExpressionPMS2 iHCResultLossOfNuclearExpressionPMS2 = new IHCResultLossOfNuclearExpressionPMS2();
				IHCResultNoLossOfNuclearExpression iHCResultNoLossOfNuclearExpression = new IHCResultNoLossOfNuclearExpression();
				IHCResultLossOfNuclearExpressionMLH1 iHCResultLossOfNuclearExpressionMLH1 = new IHCResultLossOfNuclearExpressionMLH1();
                IHCResultLossOfNuclearExpressionMSH2 iHCResultLossOfNuclearExpressionMSH2 = new IHCResultLossOfNuclearExpressionMSH2();

                if (resultCode == iHCResultLossOfNuclearExpressionMLH1PMS2.ResultCode) result = iHCResultLossOfNuclearExpressionMLH1PMS2;
				else if (resultCode == iHCResultLossOfNuclearExpressionMSH2MSH6.ResultCode) result = iHCResultLossOfNuclearExpressionMSH2MSH6;
				else if (resultCode == iHCResultLossOfNuclearExpressionMSH6.ResultCode) result = iHCResultLossOfNuclearExpressionMSH6;
				else if (resultCode == iHCResultLossOfNuclearExpressionPMS2.ResultCode) result = iHCResultLossOfNuclearExpressionPMS2;
				else if (resultCode == iHCResultNoLossOfNuclearExpression.ResultCode) result = iHCResultNoLossOfNuclearExpression;
				else if (resultCode == iHCResultLossOfNuclearExpressionMLH1.ResultCode) result = iHCResultLossOfNuclearExpressionMLH1;
                else if (resultCode == iHCResultLossOfNuclearExpressionMSH2.ResultCode) result = iHCResultLossOfNuclearExpressionMSH2;
            }
			return result;
		}
    }
}

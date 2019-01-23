using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEIHCResult
	{
        public static string Intact = "Intact";
        public static string IntactDescription = "Intact nuclear expression";
        public static string Loss = "Loss";
        public static string LossDescription = "Loss of nuclear expression";

        public static string Method = "Immunohistochemistry was performed on paraffin embedded tissue using antibody clones for MLH1 (G168-15), " +
            "PMS2 (MRQ-28), MSH2 (G219-1129), and MSH6 (44). These tests were run on the Ventana Ultra automated immunohistochemical platform.";        

		protected string m_Description;
		protected string m_IHCResultType;

		public LSEIHCResult()
		{
		}

		public string Description
		{
			get { return this.m_Description; }
		}

		public string IHCResultType
        {
			get { return this.m_IHCResultType; }
		}
	}
}

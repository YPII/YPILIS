using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
	public class SARSCoV2Result : Test.TestResult
	{
        public static string OveralResultCodePositive = "HPVPSTV";

        public static string InvalidResult = "Invalid";
        public static string DETECTED = "DETECTED";
		public static string NOTDETECTED = "NOT DETECTED";
		public static string IndeterminateResult = "Indeterminate";
		public static string QnsResult = "QNS";
		public static string LowDnaResult = "Low gDNA";
		public static string HighCVResult = "High %CV";
		public static string LowFamFozResult = "LowFamFoz";
		public static string Unsatisfactory = "Unsatisfactory";
        public static string InsuficientDNA = "Insufficient DNA to perform analysis";
        public static string UnsatSpecimenComment = "HPV testing of unsatisfactory specimens may yield false negative results.  Recommend repeat HPV testing.";               

		public SARSCoV2Result()
		{
			
		}
	}
}

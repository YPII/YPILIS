using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Cytology.Model
{
	public static class CytologyResultCode
	{
        public static bool IsDiagnosisAscusAgusLsilHsil(string resultCode)
        {
            bool result = false;
            if (IsDiagnosisASCUS(resultCode) == true) result = true;
            if (IsDiagnosisAGUS(resultCode) == true) result = true;
            if (IsDiagnosisLSIL(resultCode) == true) result = true;
            if (IsDiagnosisHGSIL(resultCode) == true) result = true;
            return result;
        }

        public static bool IsDiagnosisAscusLsilHsil(string resultCode)
        {
            bool result = false;
            if (IsDiagnosisASCUS(resultCode) == true) result = true;            
            if (IsDiagnosisLSIL(resultCode) == true) result = true;
            if (IsDiagnosisHGSIL(resultCode) == true) result = true;
            return result;
        }

        public static bool IsDiagnosisAscusLsil(string resultCode)
        {
            bool result = false;
            if (IsDiagnosisASCUS(resultCode) == true) result = true;
            if (IsDiagnosisLSIL(resultCode) == true) result = true;            
            return result;
        }

        public static bool IsDiagnosisAscusAgus(string resultCode)
        {
            bool result = false;            
            if (IsDiagnosisASCUS(resultCode) == true) result = true;
            if (IsDiagnosisAGUS(resultCode) == true) result = true;            
            return result;
        }

        public static bool IsResultCodeNormal(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                if (resultCode.Substring(3, 2) == "01")
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsResultCodeUnsat(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                if (resultCode.Substring(1, 1) == "2")
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsResultCodeTZoneAbsent(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                if (resultCode.Substring(1, 2) == "10")
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsResultCodeTZonePresent(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                if (resultCode.Substring(1, 2) == "11")
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsResultCodeReactive(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                if (resultCode.Substring(3, 2) == "02")
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsDiagnosisASCUS(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {                
                int diagnosisPart = Convert.ToInt16(resultCode.Substring(3, 2));
                if (diagnosisPart == 3)
                {
                    result = true;
                }
            }
            return result;
        }

		public static bool IsDiagnosisASCH(string resultCode)
		{
			bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                int diagnosisPart = Convert.ToInt16(resultCode.Substring(3, 2));
                if (diagnosisPart == 4)
                {
                    result = true;
                }
            }
			return result;
		}

		public static bool IsDiagnosisLSIL(string resultCode)
		{
			bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                int diagnosisPart = Convert.ToInt16(resultCode.Substring(3, 2));
                if (diagnosisPart == 5)
                {
                    result = true;
                }
            }
			return result;
		}

		public static bool IsDiagnosisHGSIL(string resultCode)
		{
			bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                int diagnosisPart = Convert.ToInt16(resultCode.Substring(3, 2));
                if (diagnosisPart == 6)
                {
                    result = true;
                }
            }
			return result;
		}

		public static bool IsDiagnosisAGUS(string resultCode)
		{
			bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                int diagnosisPart = Convert.ToInt16(resultCode.Substring(3, 2));
                if (diagnosisPart == 7)
                {
                    result = true;
                }
            }
			return result;
		}

        public static bool IsDiagnosisGreaterThanThree(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                int diagnosisPart = Convert.ToInt16(resultCode.Substring(3, 2));
                if (diagnosisPart > 3 && diagnosisPart <= 11)
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsDiagnosisThreeOrBetter(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                int diagnosisPart = Convert.ToInt16(resultCode.Substring(3, 2));
                if (diagnosisPart >= 3 && diagnosisPart <= 11)
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsDiagnosisTwoOrBetter(string resultCode)
        {
            bool result = false;
            if (string.IsNullOrEmpty(resultCode) == false)
            {
                int diagnosisPart = Convert.ToInt16(resultCode.Substring(3, 2));
                if (diagnosisPart >= 2 && diagnosisPart <= 11)
                {
                    result = true;
                }
            }
            return result;
        }

        public static string ChangeResultCode(string resultCode, SpecimenAdequacy specimenAdequacy)
        {
            StringBuilder result = new StringBuilder(resultCode);
            if (specimenAdequacy != null)
            {
                if (resultCode.Substring(0, 1) == "5")
                {
                    if (resultCode.Length == 5)
                    {
                        string stringToReplace = resultCode.Substring(1, 2);
                        result.Replace(stringToReplace, specimenAdequacy.ResultCode, 1, 2);
                    }
                    else
                    {
                        throw new Exception("Cytology Result Codes must be 5 characters long");
                    }
                }
                else
                {
                    throw new Exception("This is not a Cytology Result Code.");
                }
            }
            return result.ToString();
        }

        public static string ChangeResultCode(string resultCode, ScreeningImpression screeningImpression)
        {
            StringBuilder result = new StringBuilder(resultCode);
            if (resultCode.Substring(0, 1) == "5")
            {
                if (resultCode.Length == 5)
                {
                    string stringToReplace = resultCode.Substring(3, 2);
                    result.Replace(stringToReplace, screeningImpression.ResultCode, 3, 2);
                }
                else
                {
                    throw new Exception("Cytology Result Codes must be 5 characters long");
                }
            }
            else
            {
                throw new Exception("This is not a Cytology Result Code.");
            }
            return result.ToString();
        }

        public static string CytologySpecimenAdequacyCode(string resultCode)
        {
            string result = string.Empty;
            if (resultCode.Substring(0, 1) == "5")
            {
                if (resultCode.Length == 5)
                {
                    result = resultCode.Substring(1, 2);
                }
                else
                {
                    throw new Exception("Cytology Result Codes must be 5 characters long");
                }
            }
            else
            {
                throw new Exception("This is not a Cytology Result Code.");
            }
            return result;
        }

        public static string CytologyScreeningImpressionCode(string resultCode)
        {
            string result = string.Empty;
            if (resultCode.Substring(0, 1) == "5")
            {
                if (resultCode.Length == 5)
                {
                    result = resultCode.Substring(3, 2);
                }
                else
                {
                    throw new Exception("Cytology Result Codes must be 5 characters long");
                }
            }
            else
            {
                throw new Exception("This is not a Cytology Result Code.");
            }
            return result;
        }
	}
}

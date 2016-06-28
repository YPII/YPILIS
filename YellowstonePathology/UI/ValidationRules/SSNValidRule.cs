using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace YellowstonePathology.UI.ValidationRules
{
	class SSNValidRule : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{

			string msg = "OK";
			bool ok = true;
			if (value == null)
			{
				return new ValidationResult(ok, msg);
			}

			string ssn = value.ToString();

			switch (ssn.Length)
			{
				case 0 :
					break;
				case 9:
					{
						Regex regex = new Regex(@"\d{9}");
						Match match = regex.Match(ssn);
						if (!match.Success)
						{
							ok = false;
							msg = "SSN is 9 numbers (nnnnnnnnn or nnn-nn-nnnn)";
						}
						break;
					}
				case 11:
					{
						Regex regex = new Regex(@"\d{3}\-\d{2}\-\d{4}");
						Match match = regex.Match(ssn);
						if (!match.Success)
						{
							ok = false;
							msg = "SSN is 9 numbers (nnnnnnnnn or nnn-nn-nnnn)";
						}
						break;
					}
				default:
					ok = false;
					msg = "SSN is 9 numbers (nnnnnnnnn or nnn-nn-nnnn)";
					break;
			}
			return new ValidationResult(ok, msg);
		}
	}
}

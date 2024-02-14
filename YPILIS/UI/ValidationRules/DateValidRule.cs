using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.ValidationRules
{  
	public class ShortDateValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			string strValue = value.ToString();
			if (strValue == string.Empty)
			{
				return new ValidationResult(true, "OK");
			}

			string fmtString = strValue;
			DateTime dt;
			if (strValue.Length == 8)
			{
				if (strValue.IndexOf("/") == -1)
				{
					fmtString = strValue.Insert(2, "/");
					fmtString = fmtString.Insert(5, "/");
				}
			}

			bool ok = DateTime.TryParse(fmtString, out dt);
			return new ValidationResult(ok, "Invalid Date");
		}
	}

	public class ShortDateTimeValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			string strValue = value.ToString();
			if (strValue == string.Empty)
			{
				return new ValidationResult(true, "OK");
			}

			DateTime dt;
			string fmtString = strValue;
			if (strValue.IndexOf("/") == -1)
			{
				if (strValue.IndexOf(" ") == 8 || strValue.Length == 8)
				{
					fmtString = fmtString.Insert(2, "/");
					fmtString = fmtString.Insert(5, "/");
				}
			}

			bool ok = DateTime.TryParse(fmtString, out dt);
			return new ValidationResult(ok, "Invalid Datetime");
		}
	}

	public class BirthDateValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			string msg = "OK";
			string strValue = value.ToString();
			if (strValue == string.Empty)
			{
				return new ValidationResult(true, msg);
			}

			string fmtString = strValue;
			DateTime dt;
			if (strValue.Length == 8)
			{
				if (strValue.IndexOf("/") == -1)
				{
					fmtString = strValue.Insert(2, "/");
					fmtString = fmtString.Insert(5, "/");
				}
			}

			bool ok = DateTime.TryParse(fmtString, out dt);
			if (ok)
			{
				int diff = dt.CompareTo(DateTime.Today);
				if(diff > 0)
				{
					ok = false;
					msg = "Birth date may not be in the future";
				}
				else
				{
					diff = dt.AddYears(120).CompareTo(DateTime.Today);
					if(diff < 0)
					{
						ok = false;
						msg = "Birth date over 120 year ago";
					}
				}
			}
			else
			{
				msg = "Invalid Date";
			}
			return new ValidationResult(ok, msg);
		}
	}

	public class BirthDateOver65Validation : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			string msg = "OK";
			string strValue = value.ToString();
			if (strValue == string.Empty)
			{
				return new ValidationResult(true, msg);
			}

			string fmtString = strValue;
			DateTime dt;
			if (strValue.Length == 8)
			{
				if (strValue.IndexOf("/") == -1)
				{
					fmtString = strValue.Insert(2, "/");
					fmtString = fmtString.Insert(5, "/");
				}
			}

			bool ok = DateTime.TryParse(fmtString, out dt);
			if (ok)
			{
				int diff = dt.AddYears(65).CompareTo(DateTime.Today);
				if (diff <= 0)
				{
					System.Windows.MessageBox.Show("This patient is 65 or more years old.  Check to see if an ABN is needed.", "Possible ABN", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
			return new ValidationResult(ok, msg);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YellowstonePathology.UI.Converter
{	
	public class CytologyLoginClinicalHistoryConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
			{
				return value;
			}

			string clinical = value.ToString();
			if (clinical.IndexOf("hy ") != -1)
			{
				clinical = clinical.Replace("hy ", "Hysterectomy. ");
			}
			if (clinical.IndexOf("pab ") != -1)
			{
				clinical = clinical.Replace("pab ", "Previous abnormal pap");
			}
			if (clinical.IndexOf("ab ") != -1)
			{
				clinical = clinical.Replace("ab ", "Abnormal bleeding. ");
			}
			if (clinical.IndexOf("bcp ") != -1)
			{
				clinical = clinical.Replace("bcp ", "Birth control pills. ");
			}
            if (clinical.IndexOf("bc ") != -1)
            {
                clinical = clinical.Replace("bc ", "Birth control: ");
            }
			if (clinical.IndexOf("hth ") != -1)
			{
				clinical = clinical.Replace("hth ", "Hormone therapy. ");
			}
			if (clinical.IndexOf("pnp ") != -1)
			{
				clinical = clinical.Replace("pnp ", "Previous normal pap");
			}
            if (clinical.IndexOf("prp ") != -1)
            {
                clinical = clinical.Replace("prp ", "Previous pap: ");
            }
			if (clinical.IndexOf("pb ") != -1)
			{
				clinical = clinical.Replace("pb ", "Previous biopsy");
			}
			if (clinical.IndexOf("pn ") != -1)
			{
				clinical = clinical.Replace("pn ", "Prenatal. ");
			}
			if (clinical.IndexOf("pp ") != -1)
			{
				clinical = clinical.Replace("pp ", "Postpartum. ");
			}
			if (clinical.IndexOf("pm ") != -1)
			{
				clinical = clinical.Replace("pm ", "Postmenopausal. ");
			}
			if (clinical.IndexOf("lmp") != -1)
			{
				clinical = clinical.Replace("lmp", "LMP:");
			}
			if (clinical.IndexOf("ns ") != -1)
			{
				clinical = clinical.Replace("ns ", "Not specified. ");
			}
            if (clinical.IndexOf("cp ") != -1)
            {
                clinical = clinical.Replace("cp ", "Cervix present. ");
            }
			value = PutSlashes(clinical);
			return value;
		}

		private string PutSlashes(string text)
		{
			int intCount = 0;
			string numbers = "";
			string answer = text;
			foreach (char ch in text)
			{
				int result;
				bool isInt = Int32.TryParse(ch.ToString(), out result);
				if (isInt == true)
				{
					numbers = numbers + result.ToString();
					intCount += 1;
				}
				else
				{
					intCount = 0;
					numbers = "";
				}
				string replace1 = "";
				if (intCount == 4)
				{
					replace1 = numbers.Insert(2, "/");
					answer = answer.Replace(numbers, replace1);
				}
				string replace2 = "";
				if (intCount == 6)
				{
					replace2 = numbers.Insert(2, "/").Insert(5, "/");
					numbers = numbers.Insert(2, "/");
					answer = answer.Replace(numbers, replace2);
				}
			}
			return answer;
		}
	}
}

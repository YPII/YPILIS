using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
	public class CrcLabel
	{
		public static string LabelLine1(string masterAccessionNo)
		{
			StringBuilder sb = new StringBuilder(masterAccessionNo.Substring(3));			
			while (sb.Length < 7)
			{
				sb.Insert(0, '0');
			}
			return sb.ToString();
		}

		public static string LabelLine2(string masterAccessionNo, string crc)
		{
			string ts = crc;
			while (ts.Length < 3)
			{
				ts = "0" + ts;
			}

			StringBuilder sb = new StringBuilder("20");
			sb.Append(masterAccessionNo.Substring(0, 2));
			sb.Append(ts);
			return sb.ToString();
		}
	}
}

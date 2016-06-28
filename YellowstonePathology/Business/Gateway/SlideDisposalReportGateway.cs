using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Gateway
{
	public class SlideDisposalReportGateway
	{
		public static XElement GetCytologySlideDisposalReport(DateTime disposalDate)
		{
			XElement result = null;
			SqlCommand cmd = new SqlCommand("pCytologySlideDisposalReport");
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add("@DisposalDate", System.Data.SqlDbType.DateTime).Value = disposalDate;

			StringBuilder xmlString = new StringBuilder();
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (XmlReader xmlReader = cmd.ExecuteXmlReader())
				{
					while (xmlReader.Read())
					{
						xmlString.Append(xmlReader.ReadOuterXml());
					}
				}
			}

			if (xmlString.Length > 0)
			{
				result = XElement.Parse(xmlString.ToString());
			}
			return result;
		}

		public static XElement GetSpecimenDisposalReport(DateTime disposalDate)
		{
			XElement result = null;
			SqlCommand cmd = new SqlCommand("pDailySpecimenDisposalReport");
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add("@DisposalDate", System.Data.SqlDbType.DateTime).Value = disposalDate;

			StringBuilder xmlString = new StringBuilder();
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (XmlReader xmlReader = cmd.ExecuteXmlReader())
				{
					while (xmlReader.Read())
					{
						xmlString.Append(xmlReader.ReadOuterXml());
					}
				}
			}

			if (xmlString.Length > 0)
			{
				result = XElement.Parse(xmlString.ToString());
			}
			return result;
		}
	}
}

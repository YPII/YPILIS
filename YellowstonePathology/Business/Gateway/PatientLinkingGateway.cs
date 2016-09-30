using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class PatientLinkingGateway
	{
		public static ObservableCollection<YellowstonePathology.Business.Patient.Model.PatientLinkingListItem> GetPatientLinkingList(YellowstonePathology.Business.Patient.Model.PatientLinkingListItem patientLinkingListItem)
		{
			ObservableCollection<YellowstonePathology.Business.Patient.Model.PatientLinkingListItem> result = new ObservableCollection<Patient.Model.PatientLinkingListItem>();
			SqlCommand cmd = new SqlCommand("pGetPatientLinking");
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = patientLinkingListItem.MasterAccessionNo;
			cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = patientLinkingListItem.PFirstName;
			cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = patientLinkingListItem.PLastName;
			cmd.Parameters.Add("@PSSN", SqlDbType.VarChar).Value = DBNull.Value;
			if (string.IsNullOrEmpty(patientLinkingListItem.PSSN) == false)
			{
				cmd.Parameters["@PSSN"].Value = patientLinkingListItem.PSSN;
			}
			cmd.Parameters.Add("@PatientId", SqlDbType.VarChar).Value = DBNull.Value;
			if (string.IsNullOrEmpty(patientLinkingListItem.PatientId) == false)
			{
				cmd.Parameters["@PatientId"].Value = patientLinkingListItem.PatientId;
			}
			cmd.Parameters.Add("@PBirthdate", SqlDbType.DateTime).Value = patientLinkingListItem.PBirthdate.Value;

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Patient.Model.PatientLinkingListItem item = new YellowstonePathology.Business.Patient.Model.PatientLinkingListItem();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Business.Domain.Persistence.DataReaderPropertyWriter(dr);
						item.WriteProperties(propertyWriter);
						result.Add(item);
					}
				}
			}
			return result;
		}

		public static string GetNewPatientId()
		{
			SqlCommand cmd = new SqlCommand("Insert into tblPatient DEFAULT VALUES; SELECT * from tblPatient where PatientId = IDENT_CURRENT('tblPatient')");
			cmd.CommandType = CommandType.Text;
			string patientId = null;
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						patientId = dr["PatientId"].ToString();
					}
				}
			}
			return patientId;
		}
	}
}

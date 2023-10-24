using System;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class PatientLinkingGateway
	{
		public static ObservableCollection<YellowstonePathology.Business.Patient.Model.PatientLinkingListItem> GetPatientLinkingList(YellowstonePathology.Business.Patient.Model.PatientLinkingListItem patientLinkingListItem)
		{
			ObservableCollection<YellowstonePathology.Business.Patient.Model.PatientLinkingListItem> result = new ObservableCollection<Patient.Model.PatientLinkingListItem>();
			MySqlCommand cmd = new MySqlCommand("pGetPatientLinkingV2");
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("MasterAccessionNo", patientLinkingListItem.MasterAccessionNo);
			cmd.Parameters.AddWithValue("PFirstName", patientLinkingListItem.PFirstName);
			cmd.Parameters.AddWithValue("PLastName", patientLinkingListItem.PLastName);
			cmd.Parameters.AddWithValue("PSSN", DBNull.Value);
			if (string.IsNullOrEmpty(patientLinkingListItem.PSSN) == false)
			{
				cmd.Parameters["PSSN"].Value = patientLinkingListItem.PSSN;
			}
			cmd.Parameters.AddWithValue("PatientId", DBNull.Value);
			if (string.IsNullOrEmpty(patientLinkingListItem.PatientId) == false)
			{
				cmd.Parameters["PatientId"].Value = patientLinkingListItem.PatientId;
			}
			cmd.Parameters.AddWithValue("PBirthdate", patientLinkingListItem.PBirthdate.Value);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Patient.Model.PatientLinkingListItem item = new YellowstonePathology.Business.Patient.Model.PatientLinkingListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(item, dr);
                        propertyWriter.WriteProperties();
						result.Add(item);
					}
				}
			}
			return result;
		}

		public static string GetNewPatientId()
		{
			MySqlCommand cmd = new MySqlCommand("Insert into tblPatient (PatientId) values(0); SELECT * from tblPatient where PatientId = LAST_INSERT_ID();");
			cmd.CommandType = CommandType.Text;
			string patientId = null;
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
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

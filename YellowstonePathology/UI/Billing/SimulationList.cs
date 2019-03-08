using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.UI.Billing
{	
	public class SimulationList : ObservableCollection<SimulationListItem>
    {        
		public SimulationList()
		{

		}  
        
        public void SetInsuranceBackgrounColor()
        {
            foreach(SimulationListItem item in this)
            {
                item.SetInsuranceBackgroundColor();
            }
        }        
        
        public void HandlePatientTypeSimulation()
        {
            foreach (SimulationListItem item in this)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Select copt.PatientClass, copt.AssignedPatientLocation, copt.PatientType " +
                    "from tblClientOrder co " +
                    "join tblClientOrderPatientType copt on co.ExternalOrderId = copt.ExternalOrderId " +
                    "where MasterAccessionNo = @MasterAccessionNo limit 1";
                cmd.Parameters.AddWithValue("@MasterAccessionNo", item.MasterAccessionNo);

                using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {
                    cn.Open();
                    cmd.Connection = cn;
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            item.PatientTypeSim = dr["PatientType"].ToString();
                            item.PatientClass = dr["PatientClass"].ToString();
                            item.AssignedPatientLocation = dr["AssignedPatientLocation"].ToString();
                        }
                    }
                }
            }            
        }      

        public static SimulationList GetList(DateTime finalDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT distinct a.MasterAccessionNo, pso.ReportNo, a.ClientId, a.ClientName, pso.PanelSetName, a.PatientType, a.PrimaryInsurance PrimaryInsuranceManual, psocpt.PostDate, a.SVHMedicalRecord MedicalRecord, pso.FinalDate " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "left outer join tblPanelSetOrderCPTCodeBill psocpt on pso.ReportNo = psocpt.ReportNo " +
                "Left outer Join tblSystemUser su on pso.OrderedById = su.UserId " +
                "WHERE a.SvhMedicalRecord like 'V%' and pso.PanelSetId = 13 and pso.FinalDate = @finalDate;";

            cmd.Parameters.AddWithValue("@finalDate", finalDate);
            return BuildList(cmd);            
        }                

        private static SimulationList BuildList(MySqlCommand cmd)
        {
            SimulationList result = new SimulationList();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SimulationListItem item = new SimulationListItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }
            return result;
        }
    }
}

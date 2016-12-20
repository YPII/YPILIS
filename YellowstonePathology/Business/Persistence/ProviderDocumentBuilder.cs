using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class ProviderDocumentBuilder : DocumentBuilder
    {
        private MySqlCommand m_SQLCommand;

        public ProviderDocumentBuilder(int physicianId)
        {
            this.m_SQLCommand = new MySqlCommand();
            this.m_SQLCommand.CommandText = "select * From tblPhysician where PhysicianId = @PhysicianId";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Domain.Physician physician = new Domain.Physician();
            this.Build(physician);
            return physician;
        }

        private void Build(YellowstonePathology.Business.Domain.Physician physician)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                m_SQLCommand.Connection = cn;

                using (MySqlDataReader dr = m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }
    }
}

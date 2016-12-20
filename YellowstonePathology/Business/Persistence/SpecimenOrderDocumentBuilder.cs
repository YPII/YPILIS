using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SpecimenOrderDocumentBuilder : DocumentBuilder
    {
        MySqlCommand m_SQLCommand;

        public SpecimenOrderDocumentBuilder()
        {
            
        }

        public void SetSqlByContainerId(string containerId)
        {
            this.m_SQLCommand = new MySqlCommand("select * from tblSpecimenOrder where ContainerId = @ContainerId; Select * from tblAliquotOrder where specimenOrderId in (select specimenOrderId from tblSpecimenOrder where ContainerId = @ContainerId)");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@ContainerId", SqlDbType.VarChar).Value = containerId;
        }

        public void SetSqlByAliquotOrderId(string aliquotOrderId)
        {
            this.m_SQLCommand = new MySqlCommand("select * from tblSpecimenOrder where SpecimenOrderId in (Select SpecimenOrderId from tblAliquotOrder where aliquotOrderId = @AliquotOrderId); Select * from tblAliquotOrder where AliquotOrderid = @AliquotOrderId");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@AliquotOrderId", SqlDbType.VarChar).Value = aliquotOrderId;
        }

        public void SetSqlBySpecimenOrderId(string specimenOrderId)
        {
            this.m_SQLCommand = new MySqlCommand("select * from tblSpecimenOrder where SpecimenOrderId = @SpecimenOrderId; Select * from tblAliquotOrder where specimenOrderId in (select specimenOrderId from tblSpecimenOrder where SpecimenOrderId = @SpecimenOrderId)");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@SpecimenOrderId", SqlDbType.VarChar).Value = specimenOrderId;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
            this.Build(specimenOrder);
            return specimenOrder;
        }

        private void Build(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {            
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {                        
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        Business.Test.AliquotOrder aliquotOrder = new Test.AliquotOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(aliquotOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        specimenOrder.AliquotOrderCollection.Add(aliquotOrder);
                    }
                }
            }
        }        
    }
}

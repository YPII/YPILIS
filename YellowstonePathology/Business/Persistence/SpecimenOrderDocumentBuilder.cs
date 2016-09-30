using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Persistence
{
    public class SpecimenOrderDocumentBuilder : DocumentBuilder
    {
        SqlCommand m_SQLCommand;

        public SpecimenOrderDocumentBuilder()
        {
            
        }

        public void SetSqlByContainerId(string containerId)
        {
            this.m_SQLCommand = new SqlCommand("select * from tblSpecimenOrder where ContainerId = @ContainerId");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@ContainerId", SqlDbType.VarChar).Value = containerId;
        }

        public void SetSqlByAliquotOrderId(string aliquotOrderId)
        {
            this.m_SQLCommand = new SqlCommand("select * from tblSpecimenOrder where SpecimenOrderId in (Select SpecimenOrderId from tblAliquotOrder where aliquotOrderId = @AliquotOrderId)");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@AliquotOrderId", SqlDbType.VarChar).Value = aliquotOrderId;
        }

        public void SetSqlBySpecimenOrderId(string specimenOrderId)
        {
            this.m_SQLCommand = new SqlCommand("select * from tblSpecimenOrder where SpecimenOrderId = @SpecimenOrderId");
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
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (SqlDataReader dr = this.m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {                        
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }        
    }
}

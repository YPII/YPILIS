using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class GenericDocumentBuilder : DocumentBuilder
    {
        private SqlCommand m_SQLCommand;
        private Type m_Type;

        public GenericDocumentBuilder(SqlCommand sqlCommand, Type type)
        {
            this.m_SQLCommand = sqlCommand;
            this.m_Type = type;
        }

        public override object BuildNew()
        {
            object result = Activator.CreateInstance(this.m_Type);
            this.Build(result);
            return result;
        }

        private void Build(object o)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                m_SQLCommand.Connection = cn;

                using (SqlDataReader dr = m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(o, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }
    }
}

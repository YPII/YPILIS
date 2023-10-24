using System;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class GenericDocumentBuilder : DocumentBuilder
    {
        private MySqlCommand m_MySqlCommand;
        private Type m_Type;

        public GenericDocumentBuilder(MySqlCommand sqlCommand, Type type)
        {
            this.m_MySqlCommand = sqlCommand;
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
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                m_MySqlCommand.Connection = cn;

                using (MySqlDataReader dr = m_MySqlCommand.ExecuteReader())
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

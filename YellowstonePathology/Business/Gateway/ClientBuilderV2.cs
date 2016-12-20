using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class ClientBuilderV2
    {
        YellowstonePathology.Business.Client.Model.Client m_Client;

        public ClientBuilderV2()
        {
        }

        public YellowstonePathology.Business.Client.Model.Client Client
        {
            get { return this.m_Client; }
        }

        public void Build(MySqlCommand cmd)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        this.m_Client = new YellowstonePathology.Business.Client.Model.Client();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(this.m_Client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                    if (dr.IsClosed == false)
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
                            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientLocation, dr);
                            sqlDataReaderPropertyWriter.WriteProperties();
                            this.m_Client.ClientLocationCollection.Add(clientLocation);
                        }
                    }
                }
            }
        }
    }
}

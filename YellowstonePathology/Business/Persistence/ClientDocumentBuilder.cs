using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class ClientDocumentBuilder : DocumentBuilder
    {
        private MySqlCommand m_SQLCommand;

        public ClientDocumentBuilder(int clientId)
        {            
            this.m_SQLCommand = new MySqlCommand();
            this.m_SQLCommand.CommandText = "SELECT * FROM tblClient where ClientId = @ClientId; " +
                "SELECT * from tblClientLocation where ClientId = @ClientId;";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.AddWithValue("@ClientId", clientId);
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Client.Model.Client client = new Client.Model.Client();
            this.BuildClient(client);
            return client;
        }

        private void BuildClient(YellowstonePathology.Business.Client.Model.Client client)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }                    
                }
            }
        }        
    }
}

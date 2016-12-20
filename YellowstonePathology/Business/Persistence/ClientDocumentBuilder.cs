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
            //this.m_SQLCommand.CommandText = "SELECT c.*, (SELECT * from tblClientLocation where ClientId = c.ClientId order by Location for xml path('ClientLocation'), type) ClientLocationCollection " +
            //    "FROM tblClient c where c.ClientId = @ClientId for xml Path('Client'), type";
            this.m_SQLCommand.CommandText = "SELECT * FROM tblClient where ClientId = @ClientId " +
                "SELECT * from tblClientLocation where ClientId = @ClientId";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@ClientId",  SqlDbType.Int).Value = clientId;
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
                    if (dr.IsClosed == false)
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
                            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientLocation, dr);
                            sqlDataReaderPropertyWriter.WriteProperties();
                            client.ClientLocationCollection.Add(clientLocation);
                        }
                    }
                }
            }
        }

        /*private void BuildClient(YellowstonePathology.Business.Client.Model.Client client)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (XmlReader xmlReader = this.m_SQLCommand.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        XElement result = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);
                        YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(result, client);
                        xmlPropertyWriter.Write();

                        BuildLocation(client, result);
                    }
                }
            }
        }


        private void BuildLocation(YellowstonePathology.Business.Client.Model.Client client, XElement clientElement)
        {
            client.ClientLocationCollection.Clear();
            List<XElement> clientLocationElements = (from item in clientElement.Elements("ClientLocationCollection")
                                                     select item).ToList<XElement>();
            foreach (XElement clientLocationElement in clientLocationElements.Elements("ClientLocation"))
            {
                YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(clientLocationElement, clientLocation);
                xmlPropertyWriter.Write();
                client.ClientLocationCollection.Add(clientLocation);
            }
        }*/
    }
}

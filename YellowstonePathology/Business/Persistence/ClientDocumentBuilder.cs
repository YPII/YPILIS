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
    public class ClientDocumentBuilder : DocumentBuilder
    {
        private SqlCommand m_SQLCommand;

        public ClientDocumentBuilder(int clientId)
        {            
            this.m_SQLCommand = new SqlCommand();
            this.m_SQLCommand.CommandText = "SELECT c.*, (SELECT * from tblClientLocation where ClientId = c.ClientId order by Location for xml path('ClientLocation'), type) ClientLocationCollection " +
                "FROM tblClient c where c.ClientId = @ClientId for xml Path('Client'), type";
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
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
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
        }
    }
}

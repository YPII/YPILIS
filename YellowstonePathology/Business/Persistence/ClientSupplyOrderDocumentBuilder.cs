using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class ClientSupplyOrderDocumentBuilder : DocumentBuilder
    {
        MySqlCommand m_SQLCommand;

        /*public ClientSupplyOrderDocumentBuilder(string clientSupplyOrderId)
        {
            this.m_SQLCommand = new MySqlCommand();
            this.m_SQLCommand.CommandText = "SELECT cso.*, (SELECT * from tblClientSupplyOrderDetail where clientsupplyorderid = " +
                "cso.ClientSupplyOrderId for xml path('ClientSupplyOrderDetail'), type) ClientSupplyOrderDetailCollection " +
                "FROM tblClientSupplyOrder cso where cso.ClientSupplyOrderId = @ClientSupplyOrderId for xml Path('ClientSupplyOrder'), type";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@ClientSupplyOrderId", SqlDbType.VarChar).Value = clientSupplyOrderId;
        }*/

        public ClientSupplyOrderDocumentBuilder(string clientSupplyOrderId)
        {
            this.m_SQLCommand = new MySqlCommand();
            this.m_SQLCommand.CommandText = "SELECT * from tblClientSupplyOrder where ClientSupplyOrderId = @ClientSupplyOrderId " +
                "Select * from tblClientSupplyOrderDetail where clientSupplyOrderId = @ClientSupplyOrderId";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@ClientSupplyOrderId", SqlDbType.VarChar).Value = clientSupplyOrderId;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = new Client.Model.ClientSupplyOrder();
            this.BuildClientSupplyOrder(clientSupplyOrder);
            return clientSupplyOrder;
        }

        private void BuildClientSupplyOrder(YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientSupplyOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }

                    dr.NextResult();
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail clientSupplyOrderDetail = new YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientSupplyOrderDetail, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        clientSupplyOrder.ClientSupplyOrderDetailCollection.Add(clientSupplyOrderDetail);
                    }
                }
            }
        }
        /*private void BuildClientSupplyOrder(YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder)
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
                        YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(result, clientSupplyOrder);
                        xmlPropertyWriter.Write();

                        BuildClientSupplyOrderDetail(clientSupplyOrder, result);
                    }
                }
            }
        }


        private void BuildClientSupplyOrderDetail(YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder, XElement clientSupplyOrderElement)
        {
            clientSupplyOrder.ClientSupplyOrderDetailCollection.Clear();
            List<XElement> clientSupplyOrderDetailElements = (from item in clientSupplyOrderElement.Elements("ClientSupplyOrderDetailCollection")
                                                     select item).ToList<XElement>();
            foreach (XElement clientSupplyOrderDetailElement in clientSupplyOrderDetailElements.Elements("ClientSupplyOrderDetail"))
            {
                YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail clientSupplyOrderDetail = new YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(clientSupplyOrderDetailElement, clientSupplyOrderDetail);
                xmlPropertyWriter.Write();
                clientSupplyOrder.ClientSupplyOrderDetailCollection.Add(clientSupplyOrderDetail);
            }
        }*/
    }
}

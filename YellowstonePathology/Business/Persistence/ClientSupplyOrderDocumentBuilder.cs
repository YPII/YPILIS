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
    public class ClientSupplyOrderDocumentBuilder : DocumentBuilder
    {
        SqlCommand m_SQLCommand;

        public ClientSupplyOrderDocumentBuilder(string clientSupplyOrderId)
        {
            this.m_SQLCommand = new SqlCommand();
            this.m_SQLCommand.CommandText = "SELECT cso.*, (SELECT * from tblClientSupplyOrderDetail where clientsupplyorderid = " +
                "cso.ClientSupplyOrderId for xml path('ClientSupplyOrderDetail'), type) ClientSupplyOrderDetailCollection " +
                "FROM tblClientSupplyOrder cso where cso.ClientSupplyOrderId = @ClientSupplyOrderId for xml Path('ClientSupplyOrder'), type";
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
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
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
        }
    }
}

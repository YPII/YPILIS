using System;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class ClientOrderBuilder
    {
        MySqlCommand m_Command;
        MySqlConnection m_Connection;
        MySqlDataReader m_DataReader;
        YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        public ClientOrderBuilder(MySqlCommand cmd)
        {
            this.m_Command = cmd;
            this.m_Connection = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString);
            this.m_Connection.Open();
            this.m_Command.Connection = this.m_Connection;
            this.m_DataReader = this.m_Command.ExecuteReader();
        }

        public ClientOrderBuilder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, MySqlCommand cmd)
        {
            this.m_ClientOrder = clientOrder;
            this.m_Command = cmd;
            this.m_Connection = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString);
            this.m_Connection.Open();
            this.m_Command.Connection = this.m_Connection;
            this.m_DataReader = this.m_Command.ExecuteReader();
            this.m_DataReader.NextResult();
        }

        public Nullable<int> GetPanelSetId()
        {
            Nullable<int> panelSetId = null;
            while (this.m_DataReader.Read())
            {
                if (this.m_DataReader["PanelSetId"] != DBNull.Value)
                {
                    panelSetId = Convert.ToInt32(this.m_DataReader["PanelSetId"].ToString());
                }
            }
            this.m_DataReader.NextResult();
            return panelSetId;
        }

        public void Build(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {
            this.m_ClientOrder = clientOrder;
            this.Build();
        }

        public void Build()
        {
            BuildClientOrder();
            this.m_DataReader.NextResult();
            this.m_ClientOrder.ClientOrderDetailCollection.Clear();
            BuildClientOrderDetailCollection(this.m_ClientOrder.ClientOrderDetailCollection);
        }

        public void BuildClientOrder()
        {                        
            while (this.m_DataReader.Read())
            {
                YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(this.m_ClientOrder, this.m_DataReader);
                propertyWriter.WriteProperties();
            }

            this.m_DataReader.NextResult();
            while (this.m_DataReader.Read())
            {
                YellowstonePathology.Business.Client.Model.ClientLocation clientLocation = new YellowstonePathology.Business.Client.Model.ClientLocation();
                YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(clientLocation, this.m_DataReader);
                propertyWriter.WriteProperties();
                this.m_ClientOrder.ClientLocation = clientLocation;
            }            
        }

        public void BuildClientOrderDetailCollection(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection)
        {
            int clientOrderDetailCount = 0;
            while (this.m_DataReader.Read())
            {
                clientOrderDetailCount = Convert.ToInt32(this.m_DataReader["ClientOrderDetailCount"].ToString());
            }

            if (clientOrderDetailCount > 0)
            {
                for (int i = 0; i < clientOrderDetailCount; i++)
                {
                    this.m_DataReader.NextResult();

                    string orderTypeCode = null;
                    while (this.m_DataReader.Read())
                    {
                        orderTypeCode = this.m_DataReader["OrderTypeCode"].ToString();
                    }

                    this.m_DataReader.NextResult();

                    YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailFactory.GetClientOrderDetail(orderTypeCode, Persistence.PersistenceModeEnum.UpdateChangedProperties);
                    while (this.m_DataReader.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientOrderDetail, this.m_DataReader);
                        propertyWriter.WriteProperties();
                        clientOrderDetailCollection.Add(clientOrderDetail);
                    }
                }
            }
            else
            {
                this.m_DataReader.NextResult();
                this.m_DataReader.NextResult();
            }
        }
    }
}

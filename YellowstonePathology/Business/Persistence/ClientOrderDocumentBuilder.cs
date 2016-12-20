using System;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class ClientOrderDocumentBuilder : DocumentBuilder
    {
        MySqlCommand m_SQLCommand;

        public ClientOrderDocumentBuilder(MySqlCommand sqlCommand)
        {
            this.m_SQLCommand = sqlCommand;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Gateway.ClientOrderBuilder builder = new Gateway.ClientOrderBuilder(this.m_SQLCommand);
            Nullable<int> panelSetId = builder.GetPanelSetId();
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder result = YellowstonePathology.Business.ClientOrder.Model.ClientOrderFactory.GetClientOrder(panelSetId);
            builder.Build(result);
            return result;
        }

    }
}

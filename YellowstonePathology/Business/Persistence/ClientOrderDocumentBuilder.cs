using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class ClientOrderDocumentBuilder : DocumentBuilder
    {
        SqlCommand m_SQLCommand;

        public ClientOrderDocumentBuilder(SqlCommand sqlCommand)
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

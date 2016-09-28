using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class AODocumentBuilder : DocumentBuilder
    {        
        private SqlCommand m_SQLCommand;

        public AODocumentBuilder(string masterAccessionNo, bool obtainLock)
        {                        
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;

            this.m_SQLCommand = new SqlCommand();
            //m_SQLCommand.CommandText = "gwGetAccessionByMasterAccessionNo_A9";
            this.m_SQLCommand.CommandText = "prcGetAccessionOrder";
            m_SQLCommand.CommandType = CommandType.StoredProcedure;
            m_SQLCommand.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;            
        }

        public override object BuildNew()
        {         
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();
            //YellowstonePathology.Business.Gateway.AccessionOrderBuilder builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilder();
            YellowstonePathology.Business.Gateway.AccessionOrderBuilderV2 builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilderV2();
            builder.Build(this.m_SQLCommand, result);
            return result;            
        }

        public override void Refresh(object o)
        {
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)o;
            //YellowstonePathology.Business.Gateway.AccessionOrderBuilder builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilder();
            YellowstonePathology.Business.Gateway.AccessionOrderBuilderV2 builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilderV2();
            builder.Build(this.m_SQLCommand, accessionOrder);
        }
    }
}

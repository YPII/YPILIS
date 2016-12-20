using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class AODocumentBuilder : DocumentBuilder
    {        
        private MySqlCommand m_SQLCommand;

        public AODocumentBuilder(string masterAccessionNo, bool obtainLock)
        {                        
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;

            this.m_SQLCommand = new MySqlCommand();
            this.m_SQLCommand.CommandText = "prcGetAccessionOrder";
            m_SQLCommand.CommandType = CommandType.StoredProcedure;
            m_SQLCommand.Parameters.AddWithValue("MasterAccessionNo", masterAccessionNo);
        }

        public override object BuildNew()
        {         
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();
            YellowstonePathology.Business.Gateway.AccessionOrderBuilderV2 builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilderV2();
            builder.Build(this.m_SQLCommand, result);
            return result;            
        }

        public override void Refresh(object o)
        {
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)o;
            YellowstonePathology.Business.Gateway.AccessionOrderBuilderV2 builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilderV2();
            builder.Build(this.m_SQLCommand, accessionOrder);
        }
    }
}

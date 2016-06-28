using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlCommandHelper
    {
        public static T ExecuteCollectionCommand<T>(SqlCommand sqlCommand)
        {
            StringBuilder xmlString = new StringBuilder();
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                sqlCommand.Connection = cn;
                using (XmlReader xmlReader = sqlCommand.ExecuteXmlReader())
                {
                    while (xmlReader.Read())
                    {
                        xmlString.Append(xmlReader.ReadOuterXml());
                    }
                }
            }
            return Domain.Persistence.SerializationHelper.DeserializeCollection<T>(xmlString.ToString());
        }
    }
}

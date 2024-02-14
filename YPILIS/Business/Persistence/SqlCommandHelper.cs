using System;
using System.Text;
using System.Xml;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlCommandHelper
    {
        public static T ExecuteCollectionCommand<T>(MySqlCommand sqlCommand)
        {
            StringBuilder xmlString = new StringBuilder();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
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

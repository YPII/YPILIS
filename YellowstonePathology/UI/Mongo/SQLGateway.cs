using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.UI.Mongo
{
    public class SQLGateway
    {
        public const string ConnectionString = "Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True";

        private DateTime m_StartDate;
        private DateTime m_EndDate;        

        public SQLGateway(DateTime startDate, DateTime endDate)
        {
            this.m_StartDate = startDate;
            this.m_EndDate = endDate;
        }

        public static List<object> GetObjectList(Type objectType)
        {
            List<object> result = new List<object>();
            string tableName = "tbl" + objectType.Name;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from " + tableName;            
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        object item = Activator.CreateInstance(objectType);
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }            
            return result;
        }        
    }
}

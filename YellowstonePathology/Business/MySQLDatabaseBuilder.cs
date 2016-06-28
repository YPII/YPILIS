using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace YellowstonePathology.Business
{
    public class MySQLDatabaseBuilder
    {
        public MySQLDatabaseBuilder()
        {

        }

        public void Build()
        {
            string sql = this.BuildCreateTableCommand(typeof(YellowstonePathology.Business.Test.AccessionOrder));
        }

        private string BuildCreateTableCommand(Type type)
        {
            string result ="Create Table tbl" + type.Name + "(";

            PropertyInfo[] properties = type.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentProperty)) || Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentPrimaryKeyProperty))).ToArray();

            for (int i = 0; i < properties.Length - 1; i++)
            {
                PropertyInfo property = properties[i];
                result = result + property.Name + " ";

                Type dataType = property.PropertyType;
                if (dataType == typeof(string))
                {
                    result = result + "TEXT, ";
                }
                else if (dataType == typeof(int))
                {
                    result = result + "INT, ";
                }
                else if (dataType == typeof(double))
                {
                    result = result + "DOUBLE, ";
                }
                else if (dataType == typeof(Nullable<int>))
                {
                    result = result + "INT, ";
                }
                else if (dataType == typeof(DateTime))
                {
                    result = result + "DATETIME, ";
                }
                else if (dataType == typeof(bool))
                {
                    result = result + "BIT, ";
                }
                else if (dataType == typeof(Nullable<bool>))
                {
                    result = result + "BIT, ";
                }
                else if (dataType == typeof(Nullable<DateTime>))
                {
                    result = result + "DATETIME, ";
                }
                else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                }                
            }

            if (result.Length != 0)
            {
                result = result.Remove(result.Length - 2, 2);
            }

            result += ");";

            return result;
        }
    }
}

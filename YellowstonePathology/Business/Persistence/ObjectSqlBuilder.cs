using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectSqlBuilder
    {        

        public ObjectSqlBuilder(Type typeToBuild, string primaryKeyValue)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            this.BuildCommand(cmd, typeToBuild, primaryKeyValue);     
            object result = this.ExcuteCommandAndBuild(cmd, typeToBuild);
        }        

        private void BuildCommand(SqlCommand cmd, Type typeToBuild, string primaryKeyValue)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)typeToBuild.GetCustomAttributes(typeof(PersistentClass), false).Single();
            PropertyInfo primaryKeyPropertyInfo = typeToBuild.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            StringPropertyBridge primaryKeyPropertyBridge = new StringPropertyBridge(primaryKeyPropertyInfo, primaryKeyValue);
            
            primaryKeyPropertyBridge.SetSqlParameter(cmd);
            cmd.CommandText += "Select * from " + persistentClassAttribute.StorageName + " where " + primaryKeyPropertyInfo.Name + " = " + primaryKeyPropertyBridge.AtName + "; ";

            this.BuildPersistentChildCollectionCommands(typeToBuild, cmd);
        }

        private void BuildPersistentChildCollectionCommands(Type parentType, SqlCommand cmd)
        {        
            List<PropertyInfo> childCollectionProperties = parentType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();            
            foreach (PropertyInfo propertyInfo in childCollectionProperties)
            {
                Type collectionType = propertyInfo.PropertyType;

            }            
        }

        private object ExcuteCommandAndBuild(SqlCommand cmd, Type topLevelType)
        {
            object result = null;

            using (SqlConnection cn = new SqlConnection("Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True"))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {                    
                    while (dr.Read())
                    {
                        result = Activator.CreateInstance(topLevelType);
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }

            return result;
        }
    }
}

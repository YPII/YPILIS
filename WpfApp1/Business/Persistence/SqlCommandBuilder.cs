using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlCommandBuilder
    {
        private Type m_Type;
        private string m_PrimaryKeyValue;        

        public SqlCommandBuilder(Type type, string primaryKeyValue)
        {
            this.m_Type = type;
            this.m_PrimaryKeyValue = primaryKeyValue;
        }

        public MySqlCommand Build()
        {
            MySqlCommand cmd = new MySqlCommand();
            PersistentClass persistentClassAttribute = (PersistentClass)this.m_Type.GetCustomAttributes(typeof(PersistentClass), false).Single();            

            PropertyInfo primaryKeyPropertyInfo = this.m_Type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            StringPropertyBridge primaryKeyPropertyBridge = new StringPropertyBridge(primaryKeyPropertyInfo, this.m_PrimaryKeyValue);
            primaryKeyPropertyBridge.SetSqlParameter(cmd);

            SqlSelectClause sqlSelect = new SqlSelectClause(this.m_Type);            

            SqlParameterClause sqlParameterStatement = new SqlParameterClause(this.m_Type, this.m_PrimaryKeyValue);
            SqlStatement sqlStatement = new SqlStatement(sqlParameterStatement, sqlSelect, this.m_Type);
            
            cmd.CommandText = sqlStatement.ToString();
            return cmd;
        }

        private void HandlChildCollections(Type type, MySqlCommand cmd, SqlSelectClause sqlSelect)
        {
            List<PropertyInfo> childCollectionPropertList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
            foreach (PropertyInfo propertyInfo in childCollectionPropertList)
            {
                Type collectionType = propertyInfo.PropertyType;
                Type baseType = collectionType.BaseType;
                Type genericType = baseType.GetGenericArguments()[0];

                SqlSelectClause thisSqlSelect = new SqlSelectClause(collectionType);
                SqlStatement sqlStatement = new SqlStatement(thisSqlSelect, type, genericType);
                sqlSelect.SqlStatements.Add(sqlStatement);

                this.HandlChildCollections(genericType, cmd, thisSqlSelect);
            }
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;

namespace YellowstonePathology.Business.CustomAttributes
{
    public class SqlListItemFieldAttribute : Attribute
    {
        string m_FieldName;
        SqlDbType m_SqlDbType;

        public SqlListItemFieldAttribute(string fieldName, SqlDbType sqpDbType)
        {
            this.m_FieldName = fieldName;
            this.m_SqlDbType = sqpDbType;
        }

        public string FieldName
        {
            get { return this.m_FieldName; }
            set { this.m_FieldName = value; }
        }

        public SqlDbType SqlDbType
        {
            get { return this.m_SqlDbType; }
            set { this.m_SqlDbType = value; }
        }
    }
}

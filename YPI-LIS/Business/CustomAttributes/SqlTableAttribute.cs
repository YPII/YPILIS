using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;

namespace YellowstonePathology.Business.CustomAttributes
{
    public class SqlTableAttribute : Attribute
    {
        string m_TableName;
        string m_KeyFieldName;        
        SqlDbType m_KeyFieldSqlDbType;
        int m_KeyFieldLength;
        bool m_HasFieldsToSpellCheck;

        public SqlTableAttribute(string tableName, string keyFieldName, SqlDbType keyFieldSqlDbType, int keyFieldLength)
        {
            this.m_TableName = tableName;
            this.m_KeyFieldName = keyFieldName;
            this.m_KeyFieldSqlDbType = keyFieldSqlDbType;
        }

        public SqlTableAttribute(string tableName, string keyFieldName, SqlDbType keyFieldSqlDbType, int keyFieldLength, bool hasFieldsToSpellCheck)
        {
            this.m_TableName = tableName;
            this.m_KeyFieldName = keyFieldName;
            this.m_KeyFieldSqlDbType = keyFieldSqlDbType;
            this.m_HasFieldsToSpellCheck = hasFieldsToSpellCheck;
        }

        public string TableName
        {
            get { return this.m_TableName; }
            set { this.m_TableName = value; }
        }

        public string KeyFieldName
        {
            get { return this.m_KeyFieldName; }
            set { this.m_KeyFieldName = value; }
        }

        public SqlDbType KeyFieldSqlDbType
        {
            get { return this.m_KeyFieldSqlDbType; }
            set { this.m_KeyFieldSqlDbType = value; }
        }

        public int KeyFieldLength
        {
            get { return this.m_KeyFieldLength; }
            set { this.m_KeyFieldLength = value; }
        }

        public bool HasFieldsToSpellCheck
        {
            get { return this.m_HasFieldsToSpellCheck; }
            set { this.m_HasFieldsToSpellCheck = value; }
        }
    }
}

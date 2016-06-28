using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;

namespace YellowstonePathology.Business.CustomAttributes
{
    public class SqlFieldAttribute : Attribute
    {
        string m_FieldName;
        int m_FieldLength;        
        SqlDbType m_SqlDbType;
        bool m_NeedsSpellCheck;

        public SqlFieldAttribute(string fieldName, int fieldLength, SqlDbType sqlDbType) 
        {
            this.m_FieldName = fieldName;
            this.m_FieldLength = fieldLength;
            this.m_SqlDbType = sqlDbType;
        }

        public SqlFieldAttribute(string fieldName, int fieldLength, SqlDbType sqlDbType, bool needsSpellCheck)
        {
            this.m_FieldName = fieldName;
            this.m_FieldLength = fieldLength;
            this.m_SqlDbType = sqlDbType;
            this.m_NeedsSpellCheck = needsSpellCheck;
        }

        public string FieldName
        {
            get { return this.m_FieldName; }
            set { this.m_FieldName = value; }
        }

        public int FieldLength
        {
            get { return this.m_FieldLength; }
            set { this.m_FieldLength = value; }
        }

        public SqlDbType SqlDbType
        {
            get { return this.m_SqlDbType; }
            set { this.m_SqlDbType = value; }
        }

        public bool NeedsSpellCheck
        {
            get { return this.m_NeedsSpellCheck; }
            set { this.m_NeedsSpellCheck = value; }
        }
    }
}

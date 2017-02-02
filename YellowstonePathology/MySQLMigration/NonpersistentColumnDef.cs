using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentColumnDef
    {
        private string m_ColumnName;
        private string m_ColumnType;
        private string m_ColumnWidth;
        private string m_DefaultValue;
        private bool m_IsNull;
        private string m_ColumnDefinition;

        public NonpersistentColumnDef()
        {
            this.m_ColumnDefinition = string.Empty;
        }

        public NonpersistentColumnDef(string columnName, string columnType, string columnWidth, string defaultValue, bool isNull)
        {
            this.m_ColumnName = columnName;
            this.m_ColumnType = columnType;
            this.m_ColumnWidth = columnWidth;
            this.m_DefaultValue = defaultValue;
            this.m_IsNull = isNull;

            this.m_ColumnDefinition = NonpersistentColumnDef.GetColumnDefinition(this.m_ColumnName, this.m_ColumnType, this.m_ColumnWidth,
                this.m_DefaultValue, this.m_IsNull);
        }

        public string ColumnDefinition
        {
            get { return this.m_ColumnDefinition; }
        }

        public string ColumnName
        {
            get { return this.m_ColumnName; }
        }

        public string ColumnType
        {
            get { return this.m_ColumnType; }
        }

        public string ColumnWidth
        {
            get { return this.m_ColumnWidth; }
        }

        public string DefaultValue
        {
            get { return this.m_DefaultValue; }
        }

        public bool IsNull
        {
            get { return this.m_IsNull; }
        }

        public static string GetColumnDefinition(string columnName, string columnType, string columnWidth, string defaultValue, bool isNull)
        {
            StringBuilder def = new StringBuilder();
            def.Append("`");
            def.Append(columnName);
            def.Append("` ");
            def.Append(columnType);
            if (string.IsNullOrEmpty(columnWidth) == false)
            {
                def.Append("(");
                def.Append(columnWidth);
                def.Append(")");
            }
            if (isNull == false) def.Append(" NOT NULL");
            if (string.IsNullOrEmpty(defaultValue) == false)
            {
                def.Append(" DEFAULT ");
                def.Append(defaultValue);
            }

            return def.ToString();
        }
    }
}

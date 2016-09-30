using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Persistence
{
    public class PersistentDataColumnProperty : System.Attribute
    {
        private bool m_IsNullable;
        private string m_ColumnLength;
        private string m_DefaultValue;
        private string m_DataType;

        public PersistentDataColumnProperty(bool isNullable, string columnLength, string defaultValue, string dataType)
        {
            this.m_IsNullable = isNullable;
            this.m_ColumnLength = columnLength;
            this.m_DefaultValue = defaultValue;
            this.m_DataType = dataType;
        }

        public bool IsNullable
        {
            get { return this.m_IsNullable; }
        }

        public string ColumnLength
        {
            get { return this.m_ColumnLength; }
        }

        public string DefaultValue
        {
            get { return this.m_DefaultValue; }
        }

        public string DataType
        {
            get { return this.m_DataType; }
        }
    }
}

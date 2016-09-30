using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business
{
    public class PropertyChangedItem
    {
        string m_PropertyName;
        object m_OriginalValue;

        public PropertyChangedItem(string propertyName, object originalValue)
        {
            this.m_PropertyName = propertyName;
            this.m_OriginalValue = originalValue;
        }

        public string PropertyName
        {
            get { return this.m_PropertyName; }
            set { this.m_PropertyName = value; }
        }

        public object OriginalValue
        {
            get { return this.m_OriginalValue; }
            set { this.m_OriginalValue = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Common
{
    public class SpellCheckListItem
    {
        PropertyInfo m_Property;
        private object m_Object;

        public SpellCheckListItem()
        {

        }

        public static SpellCheckListItem CreateSpellCheckListItem(PropertyInfo property, object dataObject)
        {
            SpellCheckListItem item = new SpellCheckListItem();
            item.Property = property;
            item.PropertyObject = dataObject;
            return item;
        }

        public PropertyInfo Property
        {
            get { return this.m_Property; }
            set { this.m_Property = value; }
        }

        public object PropertyObject
        {
            get { return m_Object; }
            set { m_Object = value; }
        }
    }    
}

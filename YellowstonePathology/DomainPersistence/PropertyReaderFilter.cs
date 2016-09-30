using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class PropertyReaderFilter
    {
        protected List<string> m_PropertiesToRead;

        public PropertyReaderFilter()
        {
			this.m_PropertiesToRead = new List<string>();
        }

        public bool OKToRead(string propertyName)
        {
			bool result = false;
			foreach (string property in this.m_PropertiesToRead)
			{
				if (property.ToUpper() == propertyName.ToUpper())
				{
					result = true;
					break;
				}
			}
            return result;
        }
    }
}

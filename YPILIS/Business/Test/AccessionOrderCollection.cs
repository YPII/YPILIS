using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test
{	
    public class AccessionOrderCollection : ObservableCollection<AccessionOrder>
	{
        public void AddIfNotExists(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            if (this.Exists(accessionOrder.MasterAccessionNo) == false)
            {
                this.Add(accessionOrder);
            }
        }
      
		public bool ContainerExists(string containerId)
		{
			bool result = false;
			foreach (AccessionOrder accessionOrder in this)
			{
				if (accessionOrder.Exists(containerId) == true)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public bool Exists(string masterAccessionNo)
		{
			bool result = false;
			foreach (AccessionOrder accessionOrder in this)
			{
				if (accessionOrder.MasterAccessionNo == masterAccessionNo)
				{
					result = true;
					break;
				}
			}
			return result;
		}

        public AccessionOrder GetAccessionOrder(string masterAccessionNo)
        {
            AccessionOrder result = null;
            foreach (AccessionOrder accessionOrder in this)
            {
                if (accessionOrder.MasterAccessionNo == masterAccessionNo)
                {
                    result = accessionOrder;
                    break;
                }
            }
            return result;
        }
        
        public void Remove(string masterAccessionNo)
        {
            foreach (AccessionOrder accessionOrder in this)
            {
                if (accessionOrder.MasterAccessionNo == masterAccessionNo)
                {
                	Remove(accessionOrder);
                    break;
                }
            }
        }
    }
}

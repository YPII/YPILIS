using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class PhysicianClientList : ObservableCollection<YellowstonePathology.Business.Domain.PhysicianClient>
	{
        public PhysicianClientList(List<YellowstonePathology.Business.Domain.PhysicianClient> physicianClientList)
        {
            foreach (YellowstonePathology.Business.Domain.PhysicianClient physicianClient in physicianClientList)
            {
                this.Add(physicianClient);
            }
        }

		public PhysicianClientList()
		{
		}
	}
}

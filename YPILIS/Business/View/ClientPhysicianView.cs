using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.View
{
	public class ClientPhysicianView : YellowstonePathology.Business.Client.Model.Client
	{
		private ObservableCollection<Domain.Physician> m_Physicians;

		public ClientPhysicianView()
		{
			this.m_Physicians = new ObservableCollection<Domain.Physician>();
		}

		public ObservableCollection<Domain.Physician> Physicians
		{
			get { return this.m_Physicians; }
		}

		public bool PhysicianExists(int physicianId)
		{
			bool result = false;
			foreach (Domain.Physician physician in this.Physicians)
			{
				if (physician.PhysicianId == physicianId)
				{
					result = true;
					break;
				}
			}
			return result;
		}
	}
}

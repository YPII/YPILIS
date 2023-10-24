using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
	public class ClientLocationCollection : ObservableCollection<ClientLocation>
	{
		private ClientLocation m_CurrentLocation;

		public ClientLocationCollection()
		{
		}

		public ClientLocation GetClientLocationById(int clientLocationId)
		{
			ClientLocation result = null;
			foreach (ClientLocation clientLocation in this)
			{
				if (clientLocation.ClientLocationId == clientLocationId)
				{
					result = clientLocation;
					break;
				}
			}
			return result;
		}

		public void SetCurrentLocation(int clientLocationId)
		{
			this.m_CurrentLocation = this.GetClientLocationById(clientLocationId);
		}

        public void SetCurrentLocationToMedicalRecordsOrFirst()
        {            
            foreach (ClientLocation clientLocation in this)
            {
                if (clientLocation.Location == "Medical Records")
                {
                    this.m_CurrentLocation = clientLocation;
                    break;
                }
            }

            if (this.m_CurrentLocation == null)
            {
                this.m_CurrentLocation = this[0];
            }
        }

		public ClientLocation CurrentLocation
		{
			get { return this.m_CurrentLocation; }
		}

        public bool Exists(string location)
        {
            bool result = false;
            foreach (ClientLocation clientLocation in this)
            {
                if (clientLocation.Location == "Medical Records")
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}

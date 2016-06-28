using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
	public class ClientCollection : ObservableCollection<Client>
	{
        public ClientCollection()
        {

        }

        public bool Exists(int clientId)
        {
            bool result = false;
            foreach (Client client in this)
            {
                if (client.ClientId == clientId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }    
}

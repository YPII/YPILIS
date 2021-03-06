﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class ClientGroupClientCollection : ObservableCollection<ClientGroupClient>
    {
        public ClientGroupClientCollection()
        {

        } 

        public bool IsInGroup(int clientId, string clientGroupId)
        {
            bool result = false;
            foreach (ClientGroupClient clientGroupClient in this)
            {
                if (clientGroupClient.ClientId == clientId && clientGroupClient.ClientGroupId == clientGroupId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        
        public bool ClientIdExists(int clientId)
        {
            bool result = false;
            foreach(ClientGroupClient clientGroupClient in this)
            {
                if(clientGroupClient.ClientId == clientId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }       
    }
}

using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    [PersistentClass("tblClientGroupClient", "YPIDATA")]
    public class ClientGroupClient
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ClientGroupClientId;
        private int m_ClientId;
        private string m_ClientGroupId;
        
        public ClientGroupClient()
        {

        }

        public ClientGroupClient(string clientGroupClientId, int clientId, string clientGroupId)
        {
            this.m_ClientGroupClientId = clientGroupClientId;
            this.m_ClientId = clientId;
            this.m_ClientGroupId = clientGroupId;
        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string ClientGroupClientId
        {
            get { return this.m_ClientGroupClientId; }
            set
            {
                if (value != this.m_ClientGroupClientId)
                {
                    this.m_ClientGroupClientId = value;
                    this.NotifyPropertyChanged("ClientGroupClientId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (value != this.m_ClientId)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ClientGroupId
        {
            get { return this.m_ClientGroupId; }
            set
            {
                if (value != this.m_ClientGroupId)
                {
                    this.m_ClientGroupId = value;
                    this.NotifyPropertyChanged("ClientGroupId");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}

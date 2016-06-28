using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Client
{
    public partial class ClientGroupEntry : Window, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private YellowstonePathology.Business.Client.Model.ClientGroup m_ClientGroup;
		private YellowstonePathology.Business.Client.Model.ClientCollection m_MembersClientCollection;
        private YellowstonePathology.Business.Client.Model.ClientCollection m_SearchClientCollection;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public ClientGroupEntry(YellowstonePathology.Business.Client.Model.ClientGroup clientGroup)
        {                                
            this.m_ClientGroup = clientGroup;
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
            this.m_MembersClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientCollectionByClientGroupId(this.m_ClientGroup.ClientGroupId);
            
            InitializeComponent();

            this.DataContext = this;
            Closing += ProviderEntry_Closing;
        }

        private void ProviderEntry_Closing(object sender, CancelEventArgs e)
        {
            this.Save(false);            
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}     
        
        public YellowstonePathology.Business.Client.Model.ClientGroup ClientGroup
        {
            get { return this.m_ClientGroup; }
        }   

		public YellowstonePathology.Business.Client.Model.ClientCollection MembersClientCollection
        {
			get { return this.m_MembersClientCollection; }
		}

        public YellowstonePathology.Business.Client.Model.ClientCollection SearchClientCollection
        {
            get { return this.m_SearchClientCollection; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{            
            Close();            
		}        

        private void Save(bool releaseLock)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void ButtonAddToGroup_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewSearchClient.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewSearchClient.SelectedItem;
                if (this.m_MembersClientCollection.Exists(client.ClientId) == false)
                {                    
                    int clientGroupClientId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestClientGroupClientId() + 1;
                    string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    YellowstonePathology.Business.Client.Model.ClientGroupClient clientGroupClient = new Business.Client.Model.ClientGroupClient(objectId, clientGroupClientId, client.ClientId, this.m_ClientGroup.ClientGroupId);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(clientGroupClient, this);                    

                    this.m_MembersClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientCollectionByClientGroupId(this.m_ClientGroup.ClientGroupId);
                    this.NotifyPropertyChanged("MembersClientCollection");
                }
            }
        }

        private void ButtonRemoveFromGroup_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewMembers.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewMembers.SelectedItem;
                YellowstonePathology.Business.Gateway.PhysicianClientGateway.DeleteClientGroupClient(client.ClientId, this.m_ClientGroup.ClientGroupId);
                this.m_MembersClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientCollectionByClientGroupId(this.m_ClientGroup.ClientGroupId);
                this.NotifyPropertyChanged("MembersClientCollection");
            }
        }        

        private void TextBoxClientNameSearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxClientNameSearchText.Text.Length > 0)
            {
                this.m_SearchClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(this.TextBoxClientNameSearchText.Text);
                this.NotifyPropertyChanged("SearchClientCollection");
            }
        }
    }
}

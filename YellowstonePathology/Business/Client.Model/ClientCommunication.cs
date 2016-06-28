using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    public class ClientCommunication
    {
        public event PropertyChangedEventHandler PropertyChanged;	

        private string m_ObjectId;
        private string m_EmailAddress;
        private string m_Message;        

        public ClientCommunication()
        {

        }

		public ClientCommunication(string objectId)
		{
			this.m_ObjectId = objectId;
		}

        [PersistentDocumentIdProperty()]
        public string ObjectId
        {
            get { return this.m_ObjectId; }
            set
            {
                if (this.m_ObjectId != value)
                {
                    this.m_ObjectId = value;
                    this.NotifyPropertyChanged("ObjectId");
                }
            }
        }

        [PersistentDocumentIdProperty()]
        public string EmailAddress
        {
            get { return this.m_EmailAddress; }
            set
            {
                if (this.m_EmailAddress != value)
                {
                    this.m_EmailAddress = value;
                    this.NotifyPropertyChanged("EmailAddress");
                }
            }
        }

        [PersistentDocumentIdProperty()]
        public string Message
        {
            get { return this.m_Message; }
            set
            {
                if (this.m_Message != value)
                {
                    this.m_Message = value;
                    this.NotifyPropertyChanged("Message");
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

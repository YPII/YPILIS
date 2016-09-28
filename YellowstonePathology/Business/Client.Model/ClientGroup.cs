using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;
using YellowstonePathology.Business.Persistence;


namespace YellowstonePathology.Business.Client.Model
{
    [PersistentClass("tblClientGroup", "YPIDATA")]
    public class ClientGroup : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ObjectId;
        private int m_ClientGroupId;
        private string m_GroupName;

        public ClientGroup()
        {

        }

        [PersistentDocumentIdProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ObjectId
        {
            get { return this.m_ObjectId; }
            set
            {
                if (value != this.m_ObjectId)
                {
                    this.m_ObjectId = value;
                    this.NotifyPropertyChanged("ObjectId");
                }
            }
        }

        [PersistentPrimaryKeyProperty(true)]
        [PersistentDataColumnProperty(false, "11", "null", "int")]
        public int ClientGroupId
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string GroupName
        {
            get { return this.m_GroupName; }
            set
            {
                if (value != this.m_GroupName)
                {
                    this.m_GroupName = value;
                    this.NotifyPropertyChanged("GroupName");
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

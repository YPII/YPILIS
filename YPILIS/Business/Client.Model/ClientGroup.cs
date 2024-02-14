using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;


namespace YellowstonePathology.Business.Client.Model
{
    [PersistentClass("tblClientGroup", "YPIDATA")]
    public class ClientGroup : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ClientGroupId;
        private string m_GroupName;

        public ClientGroup()
        {

        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
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

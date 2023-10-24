using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Billing
{
    public class PanelSetCptCodeItem : INotifyPropertyChanged
    {
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        int m_PanelSetId;
        string m_PanelSetName;

        CptCodeItemCollection m_CptCodeItemCollection;

        public PanelSetCptCodeItem()
        {
            this.m_CptCodeItemCollection = new CptCodeItemCollection();
        }

        public CptCodeItemCollection CptCodeItemCollection
        {
            get { return this.m_CptCodeItemCollection; }
        }

        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                if (this.m_PanelSetId != value)
                {
                    this.m_PanelSetId = value;
                    this.NotifyPropertyChanged("PanelSetId");
                }
            }
        }

        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                if (this.m_PanelSetName != value)
                {
                    this.m_PanelSetName = value;
                    this.NotifyPropertyChanged("PanelSetName");
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

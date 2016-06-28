using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.MaterialTracking.Model
{	
    public class MaterialTrackingScannedItemView : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;        

        private string m_MaterialId;
        private string m_MaterialType;
        private string m_MasterAccessionNo;
        private string m_PLastName;
        private string m_PFirstName;
        private string m_MaterialLabel;

        public MaterialTrackingScannedItemView()
        {
            
        }        

        [PersistentProperty()]
        public string MaterialId
        {
            get { return this.m_MaterialId; }
            set
            {
                if (this.m_MaterialId != value)
                {
                    this.m_MaterialId = value;
                    this.NotifyPropertyChanged("MaterialId");
                }
            }
        }

        [PersistentProperty()]
        public string MaterialType
        {
            get { return this.m_MaterialType; }
            set
            {
                if (this.m_MaterialType != value)
                {
                    this.m_MaterialType = value;
                    this.NotifyPropertyChanged("MaterialType");
                }
            }
        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set
            {
                if (this.m_PLastName != value)
                {
                    this.m_PLastName = value;
                    this.NotifyPropertyChanged("PLastName");
                }
            }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set
            {
                if (this.m_PFirstName != value)
                {
                    this.m_PFirstName = value;
                    this.NotifyPropertyChanged("PFirstName");
                }
            }
        }

        [PersistentProperty()]
        public string MaterialLabel
        {
            get { return this.m_MaterialLabel; }
            set
            {
                if (this.m_MaterialLabel != value)
                {
                    this.m_MaterialLabel = value;
                    this.NotifyPropertyChanged("MaterialLabel");
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

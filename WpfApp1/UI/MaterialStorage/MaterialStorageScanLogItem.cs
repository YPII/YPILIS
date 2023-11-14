using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI.MaterialStorage
{
    [PersistentClass(true, "tblMaterialStorageScanLog", "YPIDATA")]

    internal class MaterialStorageScanLogItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_MaterialStorageScanLogId;
        private string m_MaterialId;
        private string m_MaterialAddress;
        private string m_MaterialType;
        private string m_ScanAction;
        private DateTime m_ScanDate;
        private string m_ScannedById;
        private string m_ScannedByName;

        public MaterialStorageScanLogItem()
        {

        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string MaterialStorageScanLogId
        {
            get { return m_MaterialStorageScanLogId; }
            set
            {
                OnPropertyChanged();
                m_MaterialStorageScanLogId = value;
            }
        }


        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string MaterialId
        {
            get { return m_MaterialId; }
            set
            {
                OnPropertyChanged();
                m_MaterialId = value;
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string MaterialAddress
        {
            get { return m_MaterialAddress; }
            set
            {
                OnPropertyChanged();
                m_MaterialAddress = value;
            }
        }


        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string MaterialType
        {
            get { return m_MaterialType; }
            set
            {
                OnPropertyChanged();
                m_MaterialType = value;
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string ScanAction
        {
            get { return m_ScanAction; }
            set
            {
                OnPropertyChanged();
                m_ScanAction = value;
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "100", "null", "datetime")]
        public DateTime ScanDate
        {
            get { return m_ScanDate; }
            set
            {
                OnPropertyChanged();
                m_ScanDate = value;
            }
        }


        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string ScannedById
        {
            get { return m_ScannedById; }
            set
            {
                OnPropertyChanged();
                m_ScannedById = value;
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string ScannedByName
        {
            get { return m_ScannedByName; }
            set
            {
                m_ScannedByName = value;
                OnPropertyChanged();
            }
        }

        void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class SlideStorage
    {
        public string m_SlideStorageId;
        private BarcodeVersion1 m_Barcode;

        private string m_StorageName;
        private string m_StorageAbbreviation;
        private string m_CabinetNumber;
        private string m_DrawerNumber;
        private string m_Facility;
        private string m_StartNumber;
        private string m_EndNumber;
        private string m_Year;     

        public SlideStorage(string storageName, string storageAbbreviation, string cabinetNumber, string drawerNumber, string facility, string year, string startNumber, string endNumber)
        {
            this.m_StorageName = storageName;
            this.m_StorageAbbreviation = storageAbbreviation;
            this.m_CabinetNumber = cabinetNumber;
            this.m_DrawerNumber = drawerNumber;
            this.m_Facility = facility;
            this.m_Year = year;
            this.m_StartNumber = startNumber;
            this.m_EndNumber = endNumber;
            this.m_SlideStorageId = (BarcodePrefixEnum.SLST + this.m_Facility + this.m_StorageAbbreviation + "C" + this.m_CabinetNumber + "D" + this.m_DrawerNumber).ToUpper();
            this.m_Barcode = new BarcodeVersion1(BarcodePrefixEnum.SLST, this.m_SlideStorageId);
        }                    

        public BarcodeVersion1 Barcode
        {
            get { return this.m_Barcode; }
            set { this.m_Barcode = value; }
        }

        public string SlideStorageId
        {
            get { return this.m_SlideStorageId; }
            set { this.m_SlideStorageId = value;}
        }

        public string StorageName
        {
            get { return this.m_StorageName; }
            set { this.m_StorageName = value; }
        }

        public string StorageAbbreviation
        {
            get { return this.m_StorageAbbreviation; }
            set { this.m_StorageAbbreviation = value; }
        }

        public string CabinetNumber
        {
            get { return this.m_CabinetNumber; }
            set { this.m_CabinetNumber = value; }
        }

        public string DrawerNumber
        {
            get { return this.m_DrawerNumber; }
            set { this.m_DrawerNumber = value; }
        }

        public string Facility
        {
            get { return this.m_Facility; }
            set { this.m_Facility = value; }
        }

        public string Year
        {
            get { return this.m_Year; }
            set { this.m_Year = value; }
        }

        public string StartNumber
        {
            get { return this.m_StartNumber; }
            set { this.m_StartNumber = value; }
        }

        public string EndNumber
        {
            get { return this.m_EndNumber; }
            set { this.m_EndNumber = value; }
        }
    }
}

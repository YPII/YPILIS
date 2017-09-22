﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{
    [PersistentClass("tblTaskOrder", "YPIDATA")]
    public class VentanaBenchMark : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected int m_BarcodeNumber;
        protected string m_StainerType;
        protected string m_StainName;
        protected string m_Procedure;
        protected string m_ProtocolName;
        protected string m_YPITestId;
        protected string m_StainModifier;
        protected bool m_UseWetProtocol;
        protected string m_ProtocolColor;

        public VentanaBenchMark()
        {

        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int BarcodeNumber
        {
            get { return this.m_BarcodeNumber; }
            set
            {
                if (this.m_BarcodeNumber != value)
                {
                    this.m_BarcodeNumber = value;
                    this.NotifyPropertyChanged("BarcodeNumber");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string StainerType
        {
            get { return this.m_StainerType; }
            set
            {
                if (this.m_StainerType != value)
                {
                    this.m_StainerType = value;
                    this.NotifyPropertyChanged("StainerType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string StainName
        {
            get { return this.m_StainName; }
            set
            {
                if (this.m_StainName != value)
                {
                    this.m_StainName = value;
                    this.NotifyPropertyChanged("StainName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string Procedure
        {
            get { return this.m_Procedure; }
            set
            {
                if (this.m_Procedure != value)
                {
                    this.m_Procedure = value;
                    this.NotifyPropertyChanged("Procedure");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string ProtocolName
        {
            get { return this.m_ProtocolName; }
            set
            {
                if (this.m_ProtocolName != value)
                {
                    this.m_ProtocolName = value;
                    this.NotifyPropertyChanged("ProtocolName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string YPITestId
        {
            get { return this.m_YPITestId; }
            set
            {
                if (this.m_YPITestId != value)
                {
                    this.m_YPITestId = value;
                    this.NotifyPropertyChanged("YPITestId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string StainModifier
        {
            get { return this.m_StainModifier; }
            set
            {
                if (this.m_StainModifier != value)
                {
                    this.m_StainModifier = value;
                    this.NotifyPropertyChanged("StainModifier");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool UseWetProtocol
        {
            get { return this.m_UseWetProtocol; }
            set
            {
                if (this.m_UseWetProtocol != value)
                {
                    this.m_UseWetProtocol = value;
                    this.NotifyPropertyChanged("UseWetProtocol");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public string ProtocolColor
        {
            get { return this.m_ProtocolColor; }
            set
            {
                if (this.m_ProtocolColor != value)
                {
                    this.m_ProtocolColor = value;
                    this.NotifyPropertyChanged("ProtocolColor");
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

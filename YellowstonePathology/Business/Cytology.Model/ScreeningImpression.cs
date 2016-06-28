using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Cytology.Model
{
    public partial class ScreeningImpression : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ResultCode;
        private string m_Description;
        private string m_Abbreviation;

        public ScreeningImpression()
        {
            
        }

		[PersistentProperty()]
        public string ResultCode
        {
            get { return this.m_ResultCode; }
            set
            {
                if (this.m_ResultCode != value)
                {
                    this.m_ResultCode = value;
                    this.NotifyPropertyChanged("ResultCode");
                }
            }
        }

		[PersistentProperty()]
		public string Description
        {
            get { return this.m_Description; }
            set
            {
                if (this.m_Description != value)
                {
                    this.m_Description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

		[PersistentProperty()]
		public string Abbreviation
        {
            get { return this.m_Abbreviation; }
            set
            {
                if (this.m_Abbreviation != value)
                {
                    this.m_Abbreviation = value;
                    this.NotifyPropertyChanged("Abbreviation");
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

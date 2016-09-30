using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Panel.Model
{	
	public class Panel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;		

        protected int m_PanelId;
        protected string m_PanelName;
        protected bool m_AcknowledgeOnOrder;
        protected string m_ResultCode;
        protected string m_PanelOrderClassName;

        protected YellowstonePathology.Business.Test.Model.TestCollection m_TestCollection;

        public Panel()
        {
			this.m_TestCollection = new YellowstonePathology.Business.Test.Model.TestCollection();
            this.m_AcknowledgeOnOrder = false;
            this.m_PanelOrderClassName = typeof(YellowstonePathology.Business.Test.PanelOrder).AssemblyQualifiedName;
		}

		public YellowstonePathology.Business.Test.Model.TestCollection TestCollection
		{
			get { return m_TestCollection; }
		}			
		
		public int PanelId
		{
			get { return this.m_PanelId; }
			set
			{
				if (this.m_PanelId != value)
				{
					this.m_PanelId = value;
					NotifyPropertyChanged("PanelId");
				}
			}
		}

		public string PanelName
		{
			get { return this.m_PanelName; }
			set
			{
				if (this.m_PanelName != value)
				{
					this.m_PanelName = value;
					NotifyPropertyChanged("PanelName");
				}
			}
		}

        public bool AcknowledgeOnOrder
        {
            get { return this.m_AcknowledgeOnOrder; }
            set 
            {
                if (this.m_AcknowledgeOnOrder != value)
                {
                    this.m_AcknowledgeOnOrder = value;
                    this.NotifyPropertyChanged("AcknowledgeOnOrder");
                }
            }
        }

        public string ResultCode
        {
            get { return this.m_ResultCode; }
            set
            {
                if (this.m_ResultCode != value)
                {
                    this.m_ResultCode = value;
                    NotifyPropertyChanged("ResultCode");
                }
            }
        }
        
        public string PanelOrderClassName
        {
            get { return this.m_PanelOrderClassName; }
            set
            {
                if (this.m_PanelOrderClassName != value)
                {
                    this.m_PanelOrderClassName = value;
                    this.NotifyPropertyChanged("PanelOrderClassName");
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

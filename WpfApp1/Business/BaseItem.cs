using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business
{
    [Serializable()]
	public class BaseItem : INotifyPropertyChanged
    {
		protected DataSet m_DataSet;
 		protected int m_Id;
		protected MySqlCommand m_Cmd;
		protected bool m_IsLoadedFromParent;
		protected string m_TblName;		        

        public delegate void PropertyChangedNotificationHandler(String info);        
        public event PropertyChangedEventHandler PropertyChanged;

		[XmlIgnore()]
		public List<Validation.ValidationRuleItem> m_ValidationRules;
		[XmlIgnore()]
		public Validation.BrokenRuleCollection m_BrokenRules;

		[XmlIgnore()]
		protected bool m_IsFilling = false;
		[XmlIgnore()]
		protected List<PropertyChangedItem> m_PropertyChangedList;

		[XmlIgnore()]
		protected bool m_IsEnabled = true;
		[XmlIgnore()]
		protected bool m_LockAquired = true;

        public BaseItem()
        {
            this.m_ValidationRules = new List<Validation.ValidationRuleItem>();
            this.m_BrokenRules = new Validation.BrokenRuleCollection();
            this.m_PropertyChangedList = new List<PropertyChangedItem>();            

			m_Cmd = new MySqlCommand();
			m_DataSet = new DataSet();
			m_TblName = string.Empty;
			m_Id = -1;
			m_IsLoadedFromParent = false;
        }

		public BaseItem(DataSet dataSet, int id)
		{
			m_Cmd = new MySqlCommand();
			m_DataSet = dataSet;
			m_TblName = string.Empty;
			m_Id = id;
			m_IsLoadedFromParent = true;
		}

		[XmlIgnore()]
		public string TableName
		{
			get { return m_TblName; }
			set { m_TblName = value; }
		}

		[XmlIgnore()]
		public List<PropertyChangedItem> PropertyChangedList
        {
            get { return this.m_PropertyChangedList; }
        }

		[XmlIgnore()]
		public bool IsFilling
        {
            get { return this.m_IsFilling; }
            set { this.m_IsFilling = value; }
        }

		[XmlIgnore()]
		public bool IsEnabled
        {
            get { return this.m_IsEnabled; }
            set 
            {
                if (value != this.m_IsEnabled)
                {
                    this.m_IsEnabled = value;
                    this.NotifyPropertyChanged("IsEnabled");
                }
            }
        }

        public virtual void Fill(MySqlDataReader dr)
        {
            BaseData.Fill(this, dr);
        }

        public virtual void SaveOld(object data)
        {
            if (data != null)
            {
                BaseData.Update(data);
            }
        }

        /*public virtual void Save()
        {
			if (m_DataSet != null)
			{
				DBAccess.DataActions.SyncData(m_DataSet);
			}            
        }*/

        public virtual void Insert()
        {
            BaseData.Insert(this);
        }

		[XmlIgnore()]
		public Validation.BrokenRuleCollection BrokenRulesCollection
        {
            get { return this.m_BrokenRules; }
        }

        public void ValidateProperty(object value, string propertyName)
        {
            this.m_BrokenRules.Clear();
            foreach (Validation.ValidationRuleItem item in this.m_ValidationRules)
            {
                if (item.PropertyName == propertyName)
                {
                    item.Validate(value, this.m_BrokenRules);
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

        protected void SetPropertyChangedItem(List<PropertyChangedItem> propertyChangedList, string propertyName, object originalValue)
        {
            if (this.m_IsFilling == false)
            {                
                bool hasBeenAdded = false;
                foreach (PropertyChangedItem item in propertyChangedList)
                {
                    if (item.PropertyName == propertyName)
                    {
                        hasBeenAdded = true;
                        break;
                    }
                }
                if (hasBeenAdded == false)
                {
                    PropertyChangedItem newChange = new PropertyChangedItem(propertyName, originalValue);
                    propertyChangedList.Add(newChange);
                }
            }
        }
	}  
}

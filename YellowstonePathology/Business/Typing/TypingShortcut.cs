using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Typing
{
	[PersistentClass("tblTypingShortcut", "YPIDATA")]
    public class TypingShortcut : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;        
		string m_Shortcut = string.Empty;		
		string m_Text = string.Empty;
		string m_Type = string.Empty;
		int m_UserId;

		public TypingShortcut()
		{
		}

		public TypingShortcut(string objectId)
		{
			this.m_ObjectId = objectId;
		}

		//[PersistentDocumentIdProperty()]
        [PersistentPrimaryKeyProperty(false)]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Shortcut
		{
			get { return this.m_Shortcut; }
			set
			{
				if (value != this.m_Shortcut)
				{
					this.m_Shortcut = value;					
					this.NotifyPropertyChanged("Shortcut");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "8000", "null", "varchar")]
		public string Text
		{
			get { return this.m_Text; }
			set
			{
				if (value != this.m_Text)
				{
					this.m_Text = value;					
					this.NotifyPropertyChanged("Text");
                    this.NotifyPropertyChanged("ShortText");
                }
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "'Global'", "varchar")]
		public string Type
		{
			get { return this.m_Type; }
			set
			{
				if (value != this.m_Type)
				{
					this.m_Type = value;					
					this.NotifyPropertyChanged("Type");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "5075", "int")]
		public int UserId
		{
			get { return this.m_UserId; }
			set
			{
				if (value != this.m_UserId)
				{
					this.m_UserId = value;					
					this.NotifyPropertyChanged("UserId");
				}
			}
		}
        
        public string ShortText
        {
            get
            {
                string text = string.Empty;
                if (this.Text.Length > 50)
                {
                    text = this.m_Text.Substring(0, 50);
                }
                else
                {
                    text = this.m_Text;
                }

                text = text.Replace('\n', ' ');
                text = text.Replace('\r', ' ');
                return text;
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

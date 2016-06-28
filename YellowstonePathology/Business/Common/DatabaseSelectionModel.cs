using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace YellowstonePathology.Business.Common
{
	public class DatabaseSelectionModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private bool m_PwOK;

		public DatabaseSelectionModel()
		{
			m_PwOK = false;
		}

		public string ConnectionString
		{
			get { return Properties.Settings.Default.CurrentConnectionString; }
			set
			{
				switch(value)
				{
					case "Test":
						Properties.Settings.Default.CurrentConnectionString = Properties.Settings.Default.TestConnectionString;
						break;
					case "Development":
						Properties.Settings.Default.CurrentConnectionString = Properties.Settings.Default.DevelopConnectionString;
						break;
					default:
						Properties.Settings.Default.CurrentConnectionString = Properties.Settings.Default.ProductionConnectionString;
						break;
				}
				Properties.Settings.Default.Save();
			}
		}

		public bool ProductionSelected
		{
			get { return Properties.Settings.Default.CurrentConnectionString == Properties.Settings.Default.ProductionConnectionString; }
			set
			{
				if (value)
				{
					TestSelected = false;
					DevelopmentSelected = false;
				}
				NotifyPropertyChanged("ProductionSelected"); }
		}

		public bool TestSelected
		{
			get { return Properties.Settings.Default.CurrentConnectionString == Properties.Settings.Default.TestConnectionString; }
			set
			{
				if (value)
				{
					ProductionSelected = false;
					DevelopmentSelected = false;
				}
				NotifyPropertyChanged("TestSelected");
			}
		}

		public bool DevelopmentSelected
		{
			get { return Properties.Settings.Default.CurrentConnectionString == Properties.Settings.Default.DevelopConnectionString; }
			set
			{
				if (value)
				{
					ProductionSelected = false;
					TestSelected = false;
				}
				NotifyPropertyChanged("DevelopmentSelected");
			}
		}

		public bool PasswordOK
		{
			get { return m_PwOK; }
			set
			{
				m_PwOK = value;
				NotifyPropertyChanged("PasswordOK");
			}
		}

		public void CheckPassword(string pwString)
		{
			if (pwString == Properties.Settings.Default.ChangeDBPassword)
			{
				PasswordOK = true;
			}
			else
			{
				PasswordOK = false;
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Monitor
{
	public partial class CytologyScreeningMonitorPage : UserControl, INotifyPropertyChanged, IMonitorPage
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection m_CytologyScreeningCollection;

        public CytologyScreeningMonitorPage()
		{         
            InitializeComponent();
            this.DataContext = this;
		}

        private void LoadData()
        {
            YellowstonePathology.Business.Calendar.HolidayCollection holidays = Business.Calendar.HolidayCollection.GetByDateRange(new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1), new DateTime(DateTime.Today.AddYears(1).Year, 12, 31));
            YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection cytologyScreeningCollection = Business.Gateway.AccessionOrderGateway.GetPendingCytologyScreening();            
            cytologyScreeningCollection.SetState(holidays);
            cytologyScreeningCollection = cytologyScreeningCollection.SortByState();
            this.m_CytologyScreeningCollection = cytologyScreeningCollection;

            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.LoadData();
        }

        public YellowstonePathology.Business.Monitor.Model.CytologyScreeningCollection CytologyScreeningCollection
        {
            get { return this.m_CytologyScreeningCollection; }
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

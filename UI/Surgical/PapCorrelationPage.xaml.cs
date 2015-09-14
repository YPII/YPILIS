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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for PapCorrelationPage.xaml
    /// </summary>
    public partial class PapCorrelationPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private YellowstonePathology.Business.Patient.Model.PatientHistoryList m_PatientHistoryList;
        private string m_PageHeaderText;

        public PapCorrelationPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalTestOrder = surgicalTestOrder;
            this.m_ObjectTracker = objectTracker;
            this.m_PageHeaderText = "Pap Correlation Page";
            this.m_PatientHistoryList = new YellowstonePathology.Business.Patient.Model.PatientHistoryList();
            this.m_PatientHistoryList.SetFillCommandByAccessionNo(m_SurgicalTestOrder.ReportNo);
            this.m_PatientHistoryList.Fill();

            InitializeComponent();

            this.DataContext = this;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save()
        {
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
        }

        public void UpdateBindingSources()
        {

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder SurgicalTestOrder
        {
            get { return this.m_SurgicalTestOrder; }
        }

        public YellowstonePathology.Business.Patient.Model.PatientHistoryList PatientHistoryList
        {
            get { return this.m_PatientHistoryList; }
        }

        private void ListViewAccessions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewAccessions.SelectedItem != null)
            {
                this.m_SurgicalTestOrder.PapCorrelationAccessionNo = ((YellowstonePathology.Business.Patient.Model.PatientHistoryListItem)this.ListViewAccessions.SelectedItem).ReportNo;
            }
        }
    }
}

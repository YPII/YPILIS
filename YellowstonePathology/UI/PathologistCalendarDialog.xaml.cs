using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PathologistCalendar.xaml
    /// </summary>
    public partial class PathologistCalendarDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.PathologistCalendarCollection m_PathologistCalendarCollection;
        private DateTime m_StartDate;
        private List<string> m_CalendarStatusList;

        public PathologistCalendarDialog()
        {
            this.m_StartDate = DateTime.Today.AddDays(-(DateTime.Today.Day - 1));
            this.m_PathologistCalendarCollection = YellowstonePathology.Business.PathologistCalendarCollection.Load(this.m_StartDate, this.m_StartDate.AddMonths(1).AddDays(-1));
            this.m_CalendarStatusList = new List<string>();
            this.m_CalendarStatusList.Add("In");
            this.m_CalendarStatusList.Add("Out");

            InitializeComponent();
            DataContext = this;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.PathologistCalendarCollection PathologistCalendarCollection
        {
            get { return this.m_PathologistCalendarCollection; }
        }

        public DateTime StartDate
        {
            get { return this.m_StartDate; }
            set { this.m_StartDate = value; }
        }

        public List<string> CalendarStatusList
        {
            get { return this.m_CalendarStatusList; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.m_PathologistCalendarCollection.Save();
            this.Close();
        }

        private void ButtonAddMonth_Click(object sender, RoutedEventArgs e)
        {
            this.m_PathologistCalendarCollection.AddMonth(this.m_StartDate);
        }

        private void ButtonGetCalendar_Click(object sender, RoutedEventArgs e)
        {
            this.m_PathologistCalendarCollection.Save();
            this.m_PathologistCalendarCollection = YellowstonePathology.Business.PathologistCalendarCollection.Load(this.m_StartDate, this.m_StartDate.AddMonths(1).AddDays(-1));
            this.NotifyPropertyChanged("PathologistCalendarCollection");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for YPHolidayDialog.xaml
    /// </summary>
    public partial class YPHolidayDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_StartDate;
        private DateTime m_EndDate;
        private YellowstonePathology.Business.YPHolidayCollection m_YPHolidayCollection;

        public YPHolidayDialog()
        {
            this.m_StartDate = new DateTime(DateTime.Today.Year, 1, 1);
            this.m_EndDate = new DateTime(DateTime.Today.Year, 12, 31);
            this.m_YPHolidayCollection = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(this.m_StartDate, this.m_EndDate);

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

        public YellowstonePathology.Business.YPHolidayCollection YPHolidayCollection
        {
            get { return this.m_YPHolidayCollection; }
            set
            {
                this.m_YPHolidayCollection = value;
                this.NotifyPropertyChanged("YPHolidayCollection");
            }
        }

        public DateTime StartDate
        {
            get { return this.m_StartDate; }
            set
            {
                this.m_StartDate = value;
                this.NotifyPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get { return this.m_EndDate; }
            set
            {
                this.m_EndDate = value;
                this.NotifyPropertyChanged("EndDate");
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_YPHolidayCollection = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(this.m_StartDate, this.m_EndDate);
            this.NotifyPropertyChanged("YPHolidayCollection");
        }

        private void DeleteHoliday_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewHolidays_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewHolidays.SelectedItem != null)
            {
                YellowstonePathology.Business.YPHoliday holiday = (YellowstonePathology.Business.YPHoliday)this.ListViewHolidays.SelectedItem;
                YPHolidayEditDialog dlg = new UI.YPHolidayEditDialog(holiday);
                dlg.ShowDialog();
                this.m_YPHolidayCollection = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(this.m_StartDate, this.m_EndDate);
                this.NotifyPropertyChanged("YPHolidayCollection");
            }
        }

        private void HyperLinkAddHoliday_Click(object sender, RoutedEventArgs e)
        {
            YPHolidayEditDialog dlg = new UI.YPHolidayEditDialog(null);
            dlg.ShowDialog();
            this.m_YPHolidayCollection = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(this.m_StartDate, this.m_EndDate);
            this.NotifyPropertyChanged("YPHolidayCollection");
        }

        private void HyperLinkAddStandardHolidays_Click(object sender, RoutedEventArgs e)
        {
            int year = DateTime.Today.Year;
            YellowstonePathology.Business.YPHolidayCollection holidays = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(new DateTime(year, 1, 1), new DateTime(year, 12, 31));
            while(holidays.Count > 0)
            {
                year++;
                holidays = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(new DateTime(year, 1, 1), new DateTime(year, 12, 31));                
            }

            holidays = YellowstonePathology.Business.Helper.DateTimeExtensions.GetHolidays(year);
            foreach (YellowstonePathology.Business.YPHoliday holiday in holidays)
            {
                holiday.Save();
            }

            if(this.m_StartDate.Year > holidays[0].HolidayDate.Year)
            {
                this.m_StartDate = holidays[0].HolidayDate;
                this.NotifyPropertyChanged("StartDate");
            }

            if(this.m_EndDate.Year < holidays[holidays.Count - 1].HolidayDate.Year)
            {
                this.m_EndDate = holidays[holidays.Count - 1].HolidayDate;
                this.NotifyPropertyChanged("EndDate");
            }

            this.m_YPHolidayCollection = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(this.m_StartDate, this.m_EndDate);
            this.NotifyPropertyChanged("YPHolidayCollection");
        }
    }
}

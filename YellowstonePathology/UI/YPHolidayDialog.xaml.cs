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

        private int m_Year;
        private YellowstonePathology.Business.YPHolidayCollection m_YPHolidayCollection;

        public YPHolidayDialog()
        {
            this.m_Year = DateTime.Today.Year;
            this.GetHolidays();

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

        public int Year
        {
            get { return this.m_Year; }
            set
            {
                this.m_Year = value;
                this.NotifyPropertyChanged("Year");
            }
        }


        private void GetHolidays()
        {
            this.m_YPHolidayCollection = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(new DateTime(this.m_Year, 1, 1), new DateTime(this.m_Year, 12, 31));
            this.NotifyPropertyChanged("YPHolidayCollection");
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ContextMenuAddDayAfter_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewHolidays.SelectedItem != null)
            {
                YellowstonePathology.Business.YPHoliday holiday = (YellowstonePathology.Business.YPHoliday)this.ListViewHolidays.SelectedItem;
                DateTime dateToChange = new DateTime(holiday.HolidayDate.Year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
                if (dateToChange.DayOfWeek == DayOfWeek.Friday)
                {
                    dateToChange = dateToChange.AddDays(3);
                }
                else
                {
                    dateToChange = dateToChange.AddDays(1);
                }

                YellowstonePathology.Business.YPHoliday holidayToAdd = new Business.YPHoliday("Day after " + holiday.HolidayName, dateToChange, false);
                if (this.CanSave(holidayToAdd) == true)
                {
                    holidayToAdd.Save();
                }
                this.GetHolidays();
            }
        }

        private void ContextMenuAddDayBefore_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewHolidays.SelectedItem != null)
            {
                YellowstonePathology.Business.YPHoliday holiday = (YellowstonePathology.Business.YPHoliday)this.ListViewHolidays.SelectedItem;
                DateTime dateToChange = new DateTime(holiday.HolidayDate.Year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
                if (dateToChange.DayOfWeek == DayOfWeek.Monday)
                {
                    dateToChange = dateToChange.AddDays(-3);
                }
                else
                {
                    dateToChange = dateToChange.AddDays(-1);
                }

                YellowstonePathology.Business.YPHoliday holidayToAdd = new Business.YPHoliday("Day before " + holiday.HolidayName, dateToChange, false);
                if (this.CanSave(holidayToAdd) == true)
                {
                    holidayToAdd.Save();
                }
                this.GetHolidays();
            }
        }

        private void ContextMenuDeleteHoliday_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewHolidays.SelectedItem != null)
            {
                YellowstonePathology.Business.YPHoliday holiday = (YellowstonePathology.Business.YPHoliday)this.ListViewHolidays.SelectedItem;
                MessageBoxResult messageBoxResult = MessageBox.Show("Delete " + holiday.HolidayName + "?", "Delete Holiday", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.m_YPHolidayCollection.DeleteHoliday(holiday);
                    this.NotifyPropertyChanged("YPHolidayCollection");
                }
            }
        }

        private void ListViewHolidays_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewHolidays.SelectedItem != null)
            {
                YellowstonePathology.Business.YPHoliday holiday = (YellowstonePathology.Business.YPHoliday)this.ListViewHolidays.SelectedItem;
                YPHolidayEditDialog dlg = new UI.YPHolidayEditDialog(holiday);
                dlg.ShowDialog();
                this.GetHolidays();
            }
        }

        private void HyperLinkAddStandardHolidays_Click(object sender, RoutedEventArgs e)
        {
            int year = DateTime.Today.Year;
            YellowstonePathology.Business.YPHolidayCollection holidays = YellowstonePathology.Business.YPHolidayCollection.GetByDateRange(new DateTime(this.m_Year, 1, 1), new DateTime(this.m_Year, 12, 31));
            if(holidays.Count == 0)
            {
                holidays = YellowstonePathology.Business.Helper.DateTimeExtensions.GetHolidays(year);
                foreach (YellowstonePathology.Business.YPHoliday holiday in holidays)
                {
                    holiday.Save();
                }
            }

            this.GetHolidays();
        }

        private void HyperLinkChangeWorkDay_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewHolidays.SelectedItems.Count > 0)
            {
                foreach (YellowstonePathology.Business.YPHoliday holiday in this.ListViewHolidays.SelectedItems)
                {
                    holiday.IsAWorkDay = !holiday.IsAWorkDay;
                    holiday.Save();
                }

                this.GetHolidays();
            }
            else
            {
                MessageBox.Show("Select a holiday to change.");
            }
        }

        private bool CanSave(Business.YPHoliday holiday)
        {
            bool result = true;

            return result;
        }
    }
}

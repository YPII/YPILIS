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
using System.ComponentModel;

namespace YellowstonePathology.UI.Calendar
{
    /// <summary>
    /// Interaction logic for CalendarDialog.xaml
    /// </summary>
    public partial class CalendarDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Calendar.PathologistCalendarDayCollection m_PathologistCalendarDayCollection;
        private DateTime m_StartDate;
        private List<string> m_CalendarStatusList;
        private List<string> m_MonthList;
        private List<int> m_Years;
        private int m_SelectedYear;
        private YellowstonePathology.Business.Calendar.HolidayCollection m_HolidayCollection;

        public CalendarDialog()
        {
            this.m_StartDate = DateTime.Today.AddDays(-(DateTime.Today.Day - 1));
            this.m_PathologistCalendarDayCollection = Business.Calendar.PathologistCalendarDayCollection.Load(this.m_StartDate, this.m_StartDate.AddMonths(1).AddDays(-1));

            this.m_CalendarStatusList = new List<string>();
            this.m_CalendarStatusList.Add("Billings");
            this.m_CalendarStatusList.Add("Bozeman");
            this.m_CalendarStatusList.Add("Out");

            this.m_MonthList = new List<string>();
            this.m_MonthList.Add("January");
            this.m_MonthList.Add("February");
            this.m_MonthList.Add("March");
            this.m_MonthList.Add("April");
            this.m_MonthList.Add("May");
            this.m_MonthList.Add("June");
            this.m_MonthList.Add("July");
            this.m_MonthList.Add("August");
            this.m_MonthList.Add("September");
            this.m_MonthList.Add("October");
            this.m_MonthList.Add("November");
            this.m_MonthList.Add("December");

            this.m_Years = new List<int>();
            int yr = DateTime.Today.AddYears(-2).Year;
            for (int idx = 0; idx < 6; idx++)
            {
                yr++;
                this.m_Years.Add(yr);
            }

            InitializeComponent();

            DataContext = this;

            this.ComboBoxYears.SelectedIndex = 1;
            this.ComboBoxPathologistYears.SelectedIndex = 1;
            this.ComboBoxPathologistMonth.SelectedIndex = DateTime.Today.Month - 1;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Calendar.PathologistCalendarDayCollection PathologistCalendarDayCollection
        {
            get { return this.m_PathologistCalendarDayCollection; }
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

        public YellowstonePathology.Business.Calendar.HolidayCollection HolidayCollection
        {
            get { return this.m_HolidayCollection; }
            set
            {
                this.m_HolidayCollection = value;
                this.NotifyPropertyChanged("HolidayCollection");
            }
        }

        public List<int> Years
        {
            get { return this.m_Years; }
        }

        public List<string> MonthList
        {
            get { return this.m_MonthList; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.m_PathologistCalendarDayCollection.Save();
            this.Close();
        }

        private void ButtonAddMonth_Click(object sender, RoutedEventArgs e)
        {
            this.m_PathologistCalendarDayCollection.AddMonth(this.m_StartDate);
        }

        private void ButtonGetPathologistCalendar_Click(object sender, RoutedEventArgs e)
        {
            this.m_PathologistCalendarDayCollection.Save();
            this.m_PathologistCalendarDayCollection = Business.Calendar.PathologistCalendarDayCollection.Load(this.m_StartDate, this.m_StartDate.AddMonths(1).AddDays(-1));

            foreach (Business.Calendar.PathologistCalendarDay day in this.m_PathologistCalendarDayCollection)
            {
                //day.DrRozelleStatus = new Business.Calendar.PathologistCalendarStatus("Dr Rozelle", "Billings");
            }

            this.NotifyPropertyChanged("PathologistCalendarDayCollection");
        }

        private void ComboBoxPathologistMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxPathologistMonth.SelectedIndex > -1 && this.ComboBoxPathologistYears.SelectedIndex > -1)
            {
                int month = this.ComboBoxPathologistMonth.SelectedIndex + 1;
                int year = (int)this.ComboBoxPathologistYears.SelectedItem;
                this.m_StartDate = new DateTime(year, month, 1);
            }
        }

        private void ComboBoxPathologistYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxPathologistMonth.SelectedIndex > -1 && this.ComboBoxPathologistYears.SelectedIndex > -1)
            {
                int month = this.ComboBoxPathologistMonth.SelectedIndex + 1;
                int year = (int)this.ComboBoxPathologistYears.SelectedItem;
                this.m_StartDate = new DateTime(year, month, 1);
            }
        }

        private void GetHolidays()
        {
            this.m_HolidayCollection = Business.Calendar.HolidayCollection.GetByDateRange(new DateTime(this.m_SelectedYear, 1, 1), new DateTime(this.m_SelectedYear, 12, 31));
            this.NotifyPropertyChanged("HolidayCollection");
        }

        private void ButtonAddStandardHolidays_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Calendar.HolidayCollection holidays = Business.Calendar.HolidayCollection.GetByDateRange(new DateTime(this.m_SelectedYear, 1, 1), new DateTime(this.m_SelectedYear, 12, 31));
            if (holidays.Count == 0)
            {
                holidays = Business.Helper.DateTimeExtensions.GetHolidays(this.m_SelectedYear);
                foreach (YellowstonePathology.Business.Calendar.Holiday holiday in holidays)
                {
                    holiday.Save();
                }
            }

            this.GetHolidays();
        }

        private void ContextMenuAddDayAfter_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewHolidays.SelectedItem != null)
            {
                YellowstonePathology.Business.Calendar.Holiday holiday = (YellowstonePathology.Business.Calendar.Holiday)this.ListViewHolidays.SelectedItem;
                DateTime dateToChange = new DateTime(holiday.HolidayDate.Year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
                if (dateToChange.DayOfWeek == DayOfWeek.Friday)
                {
                    dateToChange = dateToChange.AddDays(3);
                }
                else
                {
                    dateToChange = dateToChange.AddDays(1);
                }

                YellowstonePathology.Business.Calendar.Holiday holidayToAdd = new Business.Calendar.Holiday(string.Empty, dateToChange, false);
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
                YellowstonePathology.Business.Calendar.Holiday holiday = (YellowstonePathology.Business.Calendar.Holiday)this.ListViewHolidays.SelectedItem;
                DateTime dateToChange = new DateTime(holiday.HolidayDate.Year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
                if (dateToChange.DayOfWeek == DayOfWeek.Monday)
                {
                    dateToChange = dateToChange.AddDays(-3);
                }
                else
                {
                    dateToChange = dateToChange.AddDays(-1);
                }

                YellowstonePathology.Business.Calendar.Holiday holidayToAdd = new Business.Calendar.Holiday(string.Empty, dateToChange, false);
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
                YellowstonePathology.Business.Calendar.Holiday holiday = (YellowstonePathology.Business.Calendar.Holiday)this.ListViewHolidays.SelectedItem;
                MessageBoxResult messageBoxResult = MessageBox.Show("Delete " + holiday.DisplayDate + "?", "Delete Holiday", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.m_HolidayCollection.DeleteHoliday(holiday);
                    this.NotifyPropertyChanged("HolidayCollection");
                }
            }
        }

        private void ContextMenuIsWorkDay_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewHolidays.SelectedItem != null)
            {
                YellowstonePathology.Business.Calendar.Holiday holiday = (YellowstonePathology.Business.Calendar.Holiday)this.ListViewHolidays.SelectedItem;
                {
                    holiday.IsAWorkDay = true;
                    holiday.Save();
                }

                this.GetHolidays();
            }
            else
            {
                MessageBox.Show("Select a holiday to change.");
            }
        }

        private void ContextMenuIsNotWorkDay_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewHolidays.SelectedItem != null)
            {
                YellowstonePathology.Business.Calendar.Holiday holiday = (YellowstonePathology.Business.Calendar.Holiday)this.ListViewHolidays.SelectedItem;
                {
                    holiday.IsAWorkDay = false;
                    holiday.Save();
                }

                this.GetHolidays();
            }
            else
            {
                MessageBox.Show("Select a holiday to change.");
            }
        }

        private bool CanSave(Business.Calendar.Holiday holiday)
        {
            bool result = true;
            if (this.m_HolidayCollection.Exists(holiday.HolidayDate) == true)
            {
                result = false;
                MessageBox.Show("There already is a holiday on that date.");
            }
            return result;
        }

        private void ComboBoxYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxYears.SelectedItem != null)
            {
                this.m_SelectedYear = (int)ComboBoxYears.SelectedItem;
                this.GetHolidays();
            }
        }
    }
}

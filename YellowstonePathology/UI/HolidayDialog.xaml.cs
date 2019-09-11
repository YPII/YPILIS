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
    public partial class HolidayDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<int> m_Years;
        private int m_SelectedYear;
        private YellowstonePathology.Business.HolidayCollection m_HolidayCollection;

        public HolidayDialog()
        {
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
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.HolidayCollection HolidayCollection
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


        private void GetHolidays()
        {
            this.m_HolidayCollection = YellowstonePathology.Business.HolidayCollection.GetByDateRange(new DateTime(this.m_SelectedYear, 1, 1), new DateTime(this.m_SelectedYear, 12, 31));
            this.NotifyPropertyChanged("HolidayCollection");
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAddStandardHolidays_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.HolidayCollection holidays = YellowstonePathology.Business.HolidayCollection.GetByDateRange(new DateTime(this.m_SelectedYear, 1, 1), new DateTime(this.m_SelectedYear, 12, 31));
            if (holidays.Count == 0)
            {
                holidays = YellowstonePathology.Business.Helper.DateTimeExtensions.GetHolidays(this.m_SelectedYear);
                foreach (YellowstonePathology.Business.Holiday holiday in holidays)
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
                YellowstonePathology.Business.Holiday holiday = (YellowstonePathology.Business.Holiday)this.ListViewHolidays.SelectedItem;
                DateTime dateToChange = new DateTime(holiday.HolidayDate.Year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
                if (dateToChange.DayOfWeek == DayOfWeek.Friday)
                {
                    dateToChange = dateToChange.AddDays(3);
                }
                else
                {
                    dateToChange = dateToChange.AddDays(1);
                }

                YellowstonePathology.Business.Holiday holidayToAdd = new Business.Holiday(string.Empty, dateToChange, false);
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
                YellowstonePathology.Business.Holiday holiday = (YellowstonePathology.Business.Holiday)this.ListViewHolidays.SelectedItem;
                DateTime dateToChange = new DateTime(holiday.HolidayDate.Year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
                if (dateToChange.DayOfWeek == DayOfWeek.Monday)
                {
                    dateToChange = dateToChange.AddDays(-3);
                }
                else
                {
                    dateToChange = dateToChange.AddDays(-1);
                }

                YellowstonePathology.Business.Holiday holidayToAdd = new Business.Holiday(string.Empty, dateToChange, false);
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
                YellowstonePathology.Business.Holiday holiday = (YellowstonePathology.Business.Holiday)this.ListViewHolidays.SelectedItem;
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
                YellowstonePathology.Business.Holiday holiday = (YellowstonePathology.Business.Holiday)this.ListViewHolidays.SelectedItem;
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
                YellowstonePathology.Business.Holiday holiday = (YellowstonePathology.Business.Holiday)this.ListViewHolidays.SelectedItem;
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

        private bool CanSave(Business.Holiday holiday)
        {
            bool result = true;
            if(this.m_HolidayCollection.Exists(holiday.HolidayDate) == true)
            {
                result = false;
                MessageBox.Show("There already is a holiday on that date.");
            }
            return result;
        }

        private void ComboBoxYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxYears.SelectedItem != null)
            {
                this.m_SelectedYear = (int)ComboBoxYears.SelectedItem;
                this.GetHolidays();
            }
        }
    }
}

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
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for YPHolidayEditDialog.xaml
    /// </summary>
    public partial class YPHolidayEditDialog : Window
    {
        private YellowstonePathology.Business.YPHoliday m_YPHoliday;
        private YellowstonePathology.Business.YPHoliday m_YPHolidayOriginal;

        public YPHolidayEditDialog(YellowstonePathology.Business.YPHoliday holiday)
        {
            this.m_YPHoliday = holiday;
            this.m_YPHolidayOriginal = new Business.YPHoliday(holiday.HolidayName, holiday.HolidayDate, holiday.IsAWorkDay);

            InitializeComponent();

            DataContext = this;
        }

        public YellowstonePathology.Business.YPHoliday YPHoliday
        {
            get { return this.m_YPHoliday; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.AskSaveChanges() == true)
            {
                if (this.CanSave() == true)
                {
                    this.m_YPHoliday.Save();
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private bool AskSaveChanges()
        {
            bool result = false;
            if (this.m_YPHolidayOriginal.HolidayDate != this.m_YPHoliday.HolidayDate)
            {
                result = true;
            }
            if (string.IsNullOrEmpty(this.m_YPHoliday.HolidayName) == true || this.m_YPHolidayOriginal.HolidayName != this.m_YPHoliday.HolidayName)
            {
                result = true;
            }
            if(this.m_YPHolidayOriginal.IsAWorkDay != this.m_YPHoliday.IsAWorkDay)
            {
                result = true;
            }

            if (result == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to save the changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if(messageBoxResult == MessageBoxResult.No)
                {
                    result = false;
                }
            }
            return result;
        }

        private bool CanSave()
        {
            bool result = true;
            if(string.IsNullOrEmpty(this.m_YPHoliday.HolidayName) == true)
            {
                MessageBox.Show("Enter a valid holiday name.");
                result = false;
            }

            if(result == true)
            {
                string jString = this.m_YPHoliday.ToJSON();
                YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Helper.JSONHelper.IsValidJSONString(jString);
                if (methodResult.Success == false)
                {
                    MessageBox.Show(methodResult.Message);
                    result = false;
                }
            }

            return result;
        }
    }
}

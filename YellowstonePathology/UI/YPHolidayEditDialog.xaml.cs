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
        private string m_HolidayName;
        private string m_HolidayDate;
        bool m_SaveAsked;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="holiday"></param>
        public YPHolidayEditDialog(YellowstonePathology.Business.YPHoliday holiday)
        {
            this.m_SaveAsked = false;
            this.m_YPHoliday = holiday;
            if (holiday != null)
            {
                this.m_HolidayDate = m_YPHoliday.HolidayDate.ToString("yyyy/MM/dd");
                this.m_HolidayName = m_YPHoliday.HolidayName;
            }

            InitializeComponent();

            DataContext = this;
        }

        public YellowstonePathology.Business.YPHoliday YPHoliday
        {
            get { return this.m_YPHoliday; }
        }

        public string HolidayDate
        {
            get { return this.m_HolidayDate; }
            set { this.m_HolidayDate = value; }
        }

        public string HolidayName
        {
            get { return this.m_HolidayName; }
            set { this.m_HolidayName = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.SaveChanges() == true)
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

        private bool SaveChanges()
        {
            bool result = false;
            if(this.m_SaveAsked == false)
            {
                this.m_SaveAsked = true;
                if (this.m_YPHoliday == null && (string.IsNullOrEmpty(this.m_HolidayDate) == false || string.IsNullOrEmpty(this.m_HolidayName) == false))
                {
                    result = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(this.m_HolidayDate) == false)
                    {
                        string dt = this.m_YPHoliday.HolidayDate.ToString("yyyy/MM/dd");
                        if (dt != this.m_HolidayDate)
                        {
                            result = true;
                        }
                    }
                    if (string.IsNullOrEmpty(this.m_HolidayName) == false && this.m_HolidayName != this.m_YPHoliday.HolidayName)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool CanSave()
        {
            bool result = true;
            if(string.IsNullOrEmpty(this.m_HolidayName) == true)
            {
                MessageBox.Show("Enter a valid holiday name.");
                result = false;
            }
            else if (string.IsNullOrEmpty(this.m_HolidayName) == true)
            {
                MessageBox.Show("Enter a valid date.");
                result = false;
            }
            else
            {
                DateTime dt;
                result = DateTime.TryParse(this.m_HolidayDate, out dt);
                if(result == false)
                {
                    MessageBox.Show("Enter a valid date.");
                }
            }

            if(result == true)
            {
                DateTime holidayDate = DateTime.Parse(this.m_HolidayDate);
                if (this.m_YPHoliday == null)
                {
                    this.m_YPHoliday = new Business.YPHoliday();
                }
                this.m_YPHoliday.HolidayDate = holidayDate;
                this.m_YPHoliday.HolidayName = this.m_HolidayName;

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

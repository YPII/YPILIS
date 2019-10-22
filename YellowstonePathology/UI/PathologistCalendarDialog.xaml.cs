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
    /// Interaction logic for PathologistCalendar.xaml
    /// </summary>
    public partial class PathologistCalendarDialog : Window
    {
        private YellowstonePathology.Business.PathologistCalendarCollection m_PathologistCalendarCollection;
        private DateTime m_StartDate;

        public PathologistCalendarDialog()
        {
            this.m_StartDate = DateTime.Today.AddDays(-(DateTime.Today.Day - 1));
            this.m_PathologistCalendarCollection = YellowstonePathology.Business.PathologistCalendarCollection.Load(this.m_StartDate, this.m_StartDate.AddMonths(1).AddDays(-1));

            InitializeComponent();
            DataContext = this;
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
            this.m_PathologistCalendarCollection = YellowstonePathology.Business.PathologistCalendarCollection.Load(this.m_StartDate, this.m_StartDate.AddMonths(1).AddDays(-1));
        }
    }
}

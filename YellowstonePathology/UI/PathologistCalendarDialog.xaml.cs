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
        public PathologistCalendarDialog()
        {
            InitializeComponent();
        }

        public YellowstonePathology.Business.PathologistCalendarCollection PathologistCalendarCollection
        {
            get { return this.m_PathologistCalendarCollection; }
        }
    }
}

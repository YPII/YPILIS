using System;
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
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for ValidationTestingDialog.xaml
    /// </summary>
    public partial class ValidationTestingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Nullable<DateTime> m_WorkDate;

        public ValidationTestingDialog()
        {
            this.m_WorkDate = DateTime.Now;
            InitializeComponent();
            this.DataContext = this;
        }

        public Nullable<DateTime> WorkDate
        {
            get { return this.m_WorkDate; }
            set { this.m_WorkDate = value; }
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.m_WorkDate.Value.ToString());
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}

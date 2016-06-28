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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string m_PatientName = "Hello World How are youl.";

        public Window1()
        {            
            InitializeComponent();
            this.DataContext = this;
        }

        public string PatientName
        {
            get { return this.m_PatientName; }
            set { this.m_PatientName = value; }
        }
    }
}

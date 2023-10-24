using System;
using System.Collections.Generic;
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
    /// Interaction logic for SpecialDistribution.xaml
    /// </summary>

    public partial class SpecialDistribution : System.Windows.Window
    {        
        public SpecialDistribution()
        {
            InitializeComponent();
        }

        public string  PhysicianName
        {
            get { return this.TextBoxPhysicianName.Text; }
            set { this.TextBoxPhysicianName.Text = value; }
        }

        public string  ClientName
        {
            get { return this.TextBoxClientName.Text; }
            set { this.TextBoxClientName.Text = value; }
        }

        public void ButtonCancel_Click(object sender, RoutedEventArgs args)
        {
            this.DialogResult = false;
        }

        public void ButtonOk_Click(object sender, RoutedEventArgs args)
        {
            this.DialogResult = true;
        }
    }
}
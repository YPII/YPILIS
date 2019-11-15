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

namespace YellowstonePathology
{
    /// <summary>
    /// Interaction logic for AxisCameraDialog.xaml
    /// </summary>
    public partial class AxisCameraDialog : Window
    {
        public AxisCameraDialog()
        {
            InitializeComponent();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("Please_Show_Gross_Camera_Dialog", "Thank You");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("Please_Hide_Gross_Camera_Dialog", "Thank You");
        }
    }
}

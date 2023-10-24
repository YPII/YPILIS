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

namespace YellowstonePathology.UI.Cutting
{
    /// <summary>
    /// Interaction logic for SlideOrderDetailDialog.xaml
    /// </summary>
    public partial class SlideOrderDetailDialog : Window
    {
        YellowstonePathology.Business.Slide.Model.SlideOrder m_SlideOrder;

        public SlideOrderDetailDialog(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {
            this.m_SlideOrder = slideOrder;
            InitializeComponent();
            this.DataContext = this.m_SlideOrder;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        
    }
}

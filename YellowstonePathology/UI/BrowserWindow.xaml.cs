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
using System.Windows.Threading;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {        
        public BrowserWindow()
        {
            InitializeComponent();                                   
            Loaded += new RoutedEventHandler(BrowserWindow_Loaded);
        }

        private void BrowserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                this.Yield(1000);                             
            }
        }

        private void Yield(long ticks)
        {
            long dtEnd = DateTime.Now.AddTicks(ticks).Ticks;
            while (DateTime.Now.Ticks < dtEnd)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Background, (DispatcherOperationCallback)delegate(object unused) { return null; }, null);
            }
        }


        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {            
            
        }
        
    }
}

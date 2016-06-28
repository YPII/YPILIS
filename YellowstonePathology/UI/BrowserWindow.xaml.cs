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
//using CefSharp;
using System.Windows.Threading;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        //CefSharp.Wpf.WebView m_WebView;

        public BrowserWindow()
        {
            InitializeComponent();

            //CefSharp.Settings settings = new Settings();
            //settings.PackLoadingDisabled = true;
            //CEF.Initialize(settings);                        
            Loaded += new RoutedEventHandler(BrowserWindow_Loaded);
        }

        private void BrowserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                this.Yield(1000);
                //if (CEF.IsInitialized == true)
                //{
                //    this.m_WebView = new CefSharp.Wpf.WebView();                    
                //    this.MainGrid.Children.Add(this.m_WebView);
                //    this.m_WebView.Address = "http://localhost:3000/index.html";
                //    break;
                //}                
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

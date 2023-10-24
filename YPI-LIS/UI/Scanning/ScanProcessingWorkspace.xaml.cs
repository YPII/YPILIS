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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Xps;


namespace YellowstonePathology.UI.Scanning
{
    public partial class ScanProcessingWorkspace : System.Windows.Controls.UserControl
    {        
        public ScanProcessingWorkspace()
        {            
            InitializeComponent();
            this.DataContext = this;
            this.Browser.Address = "http://10.1.2.90:50071/scanfolders";
        }               
    }
}
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
        static ScanProcessingWorkspace m_Instance;

        YellowstonePathology.Business.Scanning.ScanningProcessing m_ScanProcessing;

        public ScanProcessingWorkspace()
        {
            this.m_ScanProcessing = new YellowstonePathology.Business.Scanning.ScanningProcessing();
            InitializeComponent();
            this.DataContext = this.m_ScanProcessing;            
        }

        public static ScanProcessingWorkspace Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ScanProcessingWorkspace();
                }
                return m_Instance;
            }
        }

        public void ListViewScannedFiles_PreviewKeyDown(object sender, KeyEventArgs args)
        {     
            
        }

        public void ListViewScanFolders_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.listViewScanFolders.SelectedItem != null)
            {
                YellowstonePathology.Business.DirectoryListItem item = (YellowstonePathology.Business.DirectoryListItem)this.listViewScanFolders.SelectedItem;                
            }
        }

        public void ListViewScannedFiles_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.listViewScannedFiles.SelectedItem != null)
            {
                System.Windows.Xps.Packaging.XpsDocument xpsDocument = new System.Windows.Xps.Packaging.XpsDocument(@"c:\ypiidata\xpsdocument55.xps", System.IO.FileAccess.Read);                
                this.DocumentViewer.Document = xpsDocument.GetFixedDocumentSequence();         
            }
        }

        public void ButtonExtract_Click(object sender, RoutedEventArgs args)
        {            
            
        }

        public void ButtonExtractAll_Click(object sender, RoutedEventArgs args)
        {            
            
        }

        public void ButtonFlipAll_Click(object sender, RoutedEventArgs args)
        {
            
        }

        public void ButtonDeleteFiles_Click(object sender, RoutedEventArgs args)
        {

        }

        public void ButtonProcessImages_Click(object sender, RoutedEventArgs args)
        {
 
        }
    }
}
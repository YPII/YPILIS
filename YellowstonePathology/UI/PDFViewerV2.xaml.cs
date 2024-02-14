using System;
using System.Collections.Generic;
using System.IO;
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
using RestSharp;
using System.IO;
using System.Net;
using Ghostscript.NET;
using Ghostscript.NET.Viewer;
using System.Drawing;
using System.Diagnostics;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PDFViewerV2.xaml
    /// </summary>
    public partial class PDFViewerV2 : Window
    {
        private GhostscriptVersionInfo m_LastInstalledVersion = null;
        private GhostscriptViewer m_Viewer = null;
        private Bitmap m_PdfPage = null;
        private string m_FileName;

        public PDFViewerV2(string fileName)
        {
            this.m_FileName = fileName;
            this.m_Viewer = new GhostscriptViewer();
            this.m_LastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion();
            this.m_Viewer.DisplaySize += new GhostscriptViewerViewEventHandler(Viewer_DisplaySize);
            this.m_Viewer.DisplayUpdate += new GhostscriptViewerViewEventHandler(Viewer_DisplayUpdate);
            this.m_Viewer.DisplayPage += new GhostscriptViewerViewEventHandler(Viewer_DisplayPage);

            InitializeComponent();

            this.m_Viewer.Open(fileName, this.m_LastInstalledVersion, false);            
        }

        private void Viewer_DisplaySize(object sender, GhostscriptViewerViewEventArgs e)
        {            
            this.m_PdfPage = e.Image;
        }
       
        private void Viewer_DisplayUpdate(object sender, GhostscriptViewerViewEventArgs e)
        {
            
        }
        
        private void Viewer_DisplayPage(object sender, GhostscriptViewerViewEventArgs e)
        {
            this.PdfDocumentImage.Source = bitmaptoimagesource(this.m_PdfPage);            
        }

        private BitmapImage bitmaptoimagesource(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            this.m_Viewer.ShowNextPage();
        }

        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            this.m_Viewer.ShowPreviousPage();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                Verb = "print",
                FileName = this.m_FileName
            };
            p.Start();
        }
    }
}

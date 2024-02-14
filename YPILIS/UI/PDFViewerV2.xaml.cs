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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PDFViewerV2.xaml
    /// </summary>
    public partial class PDFViewerV2 : Window
    {
        public PDFViewerV2()
        {
            InitializeComponent();
            GetImage();
            BitmapImage bitmapImage = GetImage();
            this.PagesContainer.Items.Add(bitmapImage);
        }

        public BitmapImage GetImage()
        {             
            WebRequest webRequest = WebRequest.Create("http://localhost:3000");
            var webResponse = webRequest.GetResponse();
            var stream = webResponse.GetResponseStream();
            BitmapImage bi = new BitmapImage();
            if (stream.CanRead)
            {
                Byte[] buffer = new Byte[webResponse.ContentLength];
                stream.Read(buffer, 0, buffer.Length);                
                
                bi.BeginInit();
                bi.StreamSource = stream;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();                

                stream.Close();               
            }
            return bi;
        }
    }
}

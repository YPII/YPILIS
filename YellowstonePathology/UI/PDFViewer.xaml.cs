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
using Windows.Data.Pdf;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PDFViewer.xaml
    /// </summary>
    public partial class PDFViewer : Window
    {        
        public PDFViewer()
        {                        
            InitializeComponent();
            this.GetFile();
        }

        private async void GetFile()
        {
            //Windows.Storage.IStorageFile file = Windows.Storage.ist.
            //PdfDocument doc = PdfDocument.LoadFromFileAsync()

            Windows.Storage.StorageFile xx = await Windows.Storage.StorageFile.GetFileFromPathAsync(@"c:\temp\testing.pdf");
            //.ContinueWith(t => PdfDocument.LoadFromFileAsync(t.Result).AsTask()).Unwrap()          
            //.ContinueWith(t2 => PdfToImages(pdfDrawer, t2.Result), TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}

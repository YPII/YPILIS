using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Media.Protection.PlayReady;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace YPI_PDF
{
    public static class PdfHandler
    {
        public static async Task<BitmapImage> OpenLocal(string fileName)
        {
            StorageFile pdfFile = await StorageFile.GetFileFromPathAsync(fileName);
            PdfDocument pdfDoc = await PdfDocument.LoadFromFileAsync(pdfFile);

            var page = pdfDoc.GetPage(0);
            BitmapImage bitmapImage = new BitmapImage();

            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);
                await bitmapImage.SetSourceAsync(stream);
            }
            
            return bitmapImage;
        }        
    }

}

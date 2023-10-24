using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;

namespace YPI_Business
{
    public class PdfHandler
    {
        public async static Task<List<System.Windows.Controls.Image>> LoadPdf(string filename)
        {
            StorageFile storageFile = await StorageFile.GetFileFromPathAsync(filename);
            PdfDocument pdfDocument = await PdfDocument.LoadFromFileAsync(storageFile);
            List<System.Windows.Controls.Image> imageList = new List<System.Windows.Controls.Image>();            
            
            for (uint i = 0; i < pdfDocument.PageCount; i++)
            {
                using (var pdfPage = pdfDocument.GetPage(i))
                {
                    BitmapImage bitmapImage = await PageToBitmapAsync(pdfPage);
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image
                    {
                        Source = bitmapImage,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 4, 0, 4),
                        MaxWidth = 800
                    };
                    imageList.Add(image);
                }
            }

            return imageList;
        }

        private static async Task<BitmapImage> PageToBitmapAsync(PdfPage page)
        {
            BitmapImage image = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);                
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream.AsStream();
                image.EndInit();
                return image;
            }
        }
    }
}

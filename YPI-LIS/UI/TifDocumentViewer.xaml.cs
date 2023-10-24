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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace YellowstonePathology.UI
{
    /// <summary>
	/// Interaction logic for TifDocumentViewer.xaml
    /// </summary>
    public partial class TifDocumentViewer : Window
    {
        List<BitmapSource> m_BitmapSourceList;

        public TifDocumentViewer()
        {
            this.m_BitmapSourceList = new List<BitmapSource>();
            InitializeComponent();         
        }

        public void Load(string fileName)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.IO.FileStream fileStream = System.IO.File.OpenRead(fileName);
            CopyStream(fileStream, memoryStream);
            fileStream.Close();

            TiffBitmapDecoder decoder = new TiffBitmapDecoder(memoryStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            foreach (BitmapFrame bitmapFrame in decoder.Frames)
            {
                BitmapSource bitmapSource = bitmapFrame;
                this.m_BitmapSourceList.Add(bitmapSource);

                Image image = new Image();
                image.Source = bitmapSource;
                image.Margin = new Thickness(2);
                this.StackPanelImages.Children.Add(image);
            }            
        }

        private void PrintDocument()
        {
            FixedDocument fixedDocument = new FixedDocument();
            fixedDocument.DocumentPaginator.PageSize = new Size(96 * 8.5, 96 * 11);                       

            foreach (BitmapSource bitmapSource in this.m_BitmapSourceList)
            {
                PageContent pageContent = new PageContent();

                FixedPage fixedPage = new FixedPage();
                fixedPage.Background = Brushes.White;
                fixedPage.Width = 96 * 8.5;
                fixedPage.Height = 96 * 11;

                Image image = new Image();
                image.Width = 96 * 8;
                image.Height = 96 * 10.5;
                image.Source = bitmapSource;

                FixedPage.SetLeft(image, 96 * .25);
                FixedPage.SetTop(image, 96 * .25);
                fixedPage.Children.Add((UIElement)image);

                ((IAddChild)pageContent).AddChild(fixedPage);
                fixedDocument.Pages.Add(pageContent);

                Size pageSize = new Size(96 * 8.5, 96 * 11);
                fixedPage.Measure(pageSize);
                fixedPage.Arrange(new Rect(new Point(), pageSize));
                fixedPage.UpdateLayout();
            }
            
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
            printDialog.PrintDocument(fixedDocument.DocumentPaginator, "Print TIF Document");
        }        

        private void ToolBarButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            this.PrintDocument();
        }

        private void ToolBarButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static void CopyStream(System.IO.Stream fromStream, System.IO.Stream toStream)
        {
            int Length = 65536;
            Byte[] buffer = new Byte[Length];
            int bytesRead = fromStream.Read(buffer, 0, Length);
            while (bytesRead > 0)
            {
                toStream.Write(buffer, 0, bytesRead);
                bytesRead = fromStream.Read(buffer, 0, Length);
            }
        }
    }
}

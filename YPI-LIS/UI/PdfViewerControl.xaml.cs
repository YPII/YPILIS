using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PdfViewer.xaml
    /// </summary>
    public partial class PdfViewerControl : UserControl
    {

        #region Bindable Properties

        public string PdfPath
        {
            get { return (string)GetValue(PdfPathProperty); }
            set { SetValue(PdfPathProperty, value); }
        }
        
        public static readonly DependencyProperty PdfPathProperty =
            DependencyProperty.Register("PdfPath", typeof(string), typeof(PdfViewerControl), new PropertyMetadata(null, propertyChangedCallback: OnPdfPathChanged));

        private async static void OnPdfPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PdfViewerControl pdfDrawer = (PdfViewerControl)d;
            pdfDrawer.PagesContainer.Items.Clear();            

            if (!string.IsNullOrEmpty(pdfDrawer.PdfPath))
            {                
                var path = System.IO.Path.GetFullPath(pdfDrawer.PdfPath);
                List<System.Windows.Controls.Image> imageList = await YPI_Business.PdfHandler.LoadPdf(path);
                foreach(System.Windows.Controls.Image image in imageList)
                {
                    pdfDrawer.PagesContainer.Items.Add(image);
                }
            }
        }

        #endregion

        public PdfViewerControl()
        {
            InitializeComponent();
        }        
    }
}

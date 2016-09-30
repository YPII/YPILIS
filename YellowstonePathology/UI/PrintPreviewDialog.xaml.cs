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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PrintPreviewDialog.xaml
    /// </summary>
    public partial class PrintPreviewDialog : Window
    {
        public PrintPreviewDialog()
        {
            InitializeComponent();
        }

        public IDocumentPaginatorSource Document
        {
            get { return this.DocumentViewer.Document; }
            set { this.DocumentViewer.Document = value; }
        }

    }
}

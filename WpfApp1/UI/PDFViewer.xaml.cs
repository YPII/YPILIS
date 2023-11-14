using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows;
using System.Diagnostics;
using System.Drawing.Printing;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PDFViewer.xaml
    /// </summary>
    public partial class PDFViewer : Window
    {
        public string m_FileName;

        public PDFViewer(string fileName)
        {
            this.m_FileName = fileName;
            InitializeComponent();
            this.Pdf.PdfPath = fileName;
        }              

        public string FileName
        {
            get { return m_FileName; }
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                Verb = "print",
                WorkingDirectory = "C:\\Program Files\\Adobe\\Acrobat DC\\Acrobat",
                FileName = "Acrobat.exe"
            };
            p.Start();
        }                
    }
}

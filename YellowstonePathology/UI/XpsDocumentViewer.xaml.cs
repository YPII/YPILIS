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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for XpsDocumentViewer.xaml
    /// </summary>
    public partial class XpsDocumentViewer : Window
    {
        public XpsDocumentViewer()
        {            
            InitializeComponent();
            this.Viewer.FitToWidth();
        }

        public void ViewDocument(string fileName)
        {
            if (System.IO.File.Exists(fileName) == true)
            {
                XpsDocument xpsDocument = new XpsDocument(fileName, System.IO.FileAccess.Read);
                this.Viewer.Document = xpsDocument.GetFixedDocumentSequence();                
                xpsDocument.Close();
            }
        }

		public void LoadDocument(FixedDocument fixedDocument)
		{
			this.Viewer.Document = fixedDocument;
		}
	}
}

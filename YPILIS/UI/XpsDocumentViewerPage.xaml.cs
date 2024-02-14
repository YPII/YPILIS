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
    public partial class XpsDocumentViewerPage : UserControl
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;        

        private Visibility m_BackButtonVisibility;
        private Visibility m_NextButtonVisibility;

        public XpsDocumentViewerPage(Visibility backButtonVisibility, Visibility nextButtonVisibility)
        {
            this.m_BackButtonVisibility = backButtonVisibility;
            this.m_NextButtonVisibility = nextButtonVisibility;

            InitializeComponent();

            this.DataContext = this;
            this.Viewer.FitToWidth();
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        public System.Windows.Visibility NextButtonVisibility
        {
            get { return this.m_NextButtonVisibility; }
        }

        public System.Windows.Visibility NavigationGridVisibility
        {
            get 
            {
                System.Windows.Visibility result = System.Windows.Visibility.Collapsed;
                if (this.m_BackButtonVisibility == System.Windows.Visibility.Visible || this.m_NextButtonVisibility == System.Windows.Visibility.Visible)
                {
                    result = System.Windows.Visibility.Visible;
                }
                return result;
            }
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

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());
        }
	}
}

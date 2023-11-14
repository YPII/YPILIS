using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Xps.Packaging;

namespace YellowstonePathology.Business.Document
{
	public class XpsCaseDocument : CaseDocument
	{
        private XpsDocument m_XpsDocument;

		public XpsCaseDocument()
		{

		}

        public override void Close()
        {
            var xpsUri = this.m_XpsDocument.Uri;
            var xpsPackage = System.IO.Packaging.PackageStore.GetPackage(xpsUri);
            System.IO.Packaging.PackageStore.RemovePackage(xpsUri);
            xpsPackage.Close();
        }

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{
			this.m_XpsDocument = new XpsDocument(this.FullFileName, System.IO.FileAccess.Read);
			System.Windows.Controls.DocumentViewer documentViewer = new System.Windows.Controls.DocumentViewer();
			documentViewer.Loaded += new System.Windows.RoutedEventHandler(DocumentViewer_Loaded);
			documentViewer.Document = this.m_XpsDocument.GetFixedDocumentSequence();
			contentControl.Content = documentViewer;            
		}

		private void DocumentViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Controls.DocumentViewer documentViewer = (System.Windows.Controls.DocumentViewer)sender;
			documentViewer.FitToWidth();
		}
	}
}

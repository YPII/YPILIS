using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

namespace YellowstonePathology.Business.Document
{
	public class PdfDocument : CaseDocument
	{		

		public PdfDocument()
		{

		}				

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{			
			ScrollViewer scrollViewer = new ScrollViewer();
			scrollViewer.CanContentScroll = true;
			scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
			scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

			WebBrowser webBrowser = new WebBrowser();
			scrollViewer.Content = webBrowser;
			contentControl.Content = scrollViewer;
			string url = this.FullFileName.Replace(@"\\CFileServer\AccessionDocuments\", "http://10.1.2.90:50071/documents/");
			url = url.Replace("\\", "/");
			webBrowser.Source = new Uri(url);								
		}
	}
}

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
using System.IO;
using System.Windows.Xps.Packaging;

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Interaction logic for ReportViewer.xaml
	/// </summary>
	public partial class ReportViewer : Window
	{
		XpsDocument m_Document;
		Uri m_Uri;

		public ReportViewer()
		{
			InitializeComponent();
			this.Closing += new System.ComponentModel.CancelEventHandler(ReportViewer_Closing);
		}

		private void ReportViewer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			System.IO.Packaging.PackageStore.RemovePackage(m_Uri);
			this.m_Document.Close();
		}

		public void ShowDocument(string file)
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(file);
			string fileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
			fileName += file + ".xps";

			FileStream fileStream = File.OpenRead(fileName);
			byte[] bytes = new byte[fileStream.Length];
			fileStream.Read(bytes, 0, bytes.Length);
			fileStream.Close();
			MemoryStream memoryStream = new MemoryStream(bytes);

			string tempPath = "pack://" + file + ".xps";
			System.IO.Packaging.Package package = System.IO.Packaging.Package.Open(memoryStream);
			m_Uri = new Uri(tempPath);
			System.IO.Packaging.PackageStore.AddPackage(m_Uri, package);
			m_Document = new XpsDocument(package, System.IO.Packaging.CompressionOption.Maximum, tempPath);
			FixedDocumentSequence fixedDocumentSequence = m_Document.GetFixedDocumentSequence();

			this.DocumentViewerReports.Document = fixedDocumentSequence as IDocumentPaginatorSource;
		}

		private void DocumentViewerReports_Loaded(object sender, RoutedEventArgs e)
		{
			this.Width = 900;
			this.Height = 900;
		}
	}
}

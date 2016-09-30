using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Document
{
	public class AccessionOrderCaseDocument : CaseDocument
	{
		private string m_ReportNo;

		public AccessionOrderCaseDocument(string reportNo, string masterAccessionNo)
		{
			m_ReportNo = reportNo;
			MasterAccessionNo = masterAccessionNo;
			FullFileName = string.Empty;
			FileName = "Accession Data Sheet.xps";
			FilePath = string.Empty;
			CaseDocumentType = "AccessionOrderDataSheet";
		}

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{
			YellowstonePathology.Document.Result.Data.AccessionOrderDataSheetData accessionOrderDataSheetData = YellowstonePathology.Business.Gateway.XmlGateway.GetAccessionOrderDataSheetData(this.MasterAccessionNo);
			YellowstonePathology.Document.Result.Xps.AccessionOrderDataSheet accessionOrderDataSheet = new YellowstonePathology.Document.Result.Xps.AccessionOrderDataSheet(accessionOrderDataSheetData);
			System.Windows.Controls.DocumentViewer documentViewer = new System.Windows.Controls.DocumentViewer();
			documentViewer.Loaded += new System.Windows.RoutedEventHandler(DocumentViewer_Loaded);
			documentViewer.Document = accessionOrderDataSheet.FixedDocument;
			contentControl.Content = documentViewer;
		}

		private void DocumentViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Controls.DocumentViewer documentViewer = (System.Windows.Controls.DocumentViewer)sender;
			documentViewer.FitToWidth();
		}
	}
}

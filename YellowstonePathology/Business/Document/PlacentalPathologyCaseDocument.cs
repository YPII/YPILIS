using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Document
{
	public class PlacentalPathologyCaseDocument : CaseDocument
	{
		private string m_PlacentalClientOrderId;

		public PlacentalPathologyCaseDocument(string clientOrderId)
		{
			this.m_PlacentalClientOrderId = clientOrderId;
			FullFileName = string.Empty;
			FileName = "Placental Pathology Sheet.xps";
			FilePath = string.Empty;
			CaseDocumentType = "PlacentalPathologySheet";
		}

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{
			YellowstonePathology.Document.Result.Data.PlacentalPathologyQuestionnaireData placentalPathologyData = YellowstonePathology.Business.Gateway.XmlGateway.GetPlacentalPathologyQuestionnaire(this.m_PlacentalClientOrderId, writer);
			if (placentalPathologyData != null)
			{
				YellowstonePathology.Document.PlacentalPathologyQuestionnaire placentalPathologyQuestionnare = new YellowstonePathology.Document.PlacentalPathologyQuestionnaire(placentalPathologyData);
				System.Windows.Controls.DocumentViewer documentViewer = new System.Windows.Controls.DocumentViewer();
				documentViewer.Loaded += new System.Windows.RoutedEventHandler(DocumentViewer_Loaded);
				documentViewer.Document = placentalPathologyQuestionnare.FixedDocument;
				contentControl.Content = documentViewer;
			}
			else
			{
				contentControl.Content = null;
				System.Windows.MessageBox.Show("Placental Questionaire is not available");
			}
		}

		private void DocumentViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Controls.DocumentViewer documentViewer = (System.Windows.Controls.DocumentViewer)sender;
			documentViewer.FitToWidth();
		}
	}
}

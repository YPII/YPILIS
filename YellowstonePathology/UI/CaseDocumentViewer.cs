using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class CaseDocumentViewer
    {		
		public void View(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
			YellowstonePathology.Business.Interface.ICaseDocument doc = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(accessionOrder, panelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			doc.Render();
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(panelSetOrder.ReportNo);

			string fileName = string.Empty;
            if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument ||
                panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.RetiredTestDocument)
			{
				fileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
			}
			else
			{
				fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			}

			switch (doc.NativeDocumentFormat)
			{
				case Business.Document.NativeDocumentFormatEnum.Word:
					YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
					break;
				case Business.Document.NativeDocumentFormatEnum.XPS:
					YellowstonePathology.UI.XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
					xpsDocumentViewer.ViewDocument(fileName);
					xpsDocumentViewer.ShowDialog();
					break;
			}
		}
    }
}

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
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
			YellowstonePathology.Business.Interface.ICaseDocument doc = Business.Document.DocumentFactory.GetDocument(accessionOrder, panelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			doc.Render();
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(panelSetOrder.ReportNo);

			string fileName = string.Empty;
            if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument ||
                panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.RetiredTestDocument)
			{
				fileName = Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
			}
			else
			{
                fileName = doc.SaveFileName;
			}

			switch (doc.NativeDocumentFormat)
			{
				case Business.Document.NativeDocumentFormatEnum.Word:
                    YellowstonePathology.Business.Document.CaseDocument.OpenWordDoc(fileName);
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

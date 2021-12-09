using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class DocumentFactory
	{
        public static YellowstonePathology.Business.Interface.ICaseDocument GetDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, ReportSaveModeEnum reportSaveMode)
        {
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
            Type caseDocumentType = Type.GetType(panelSet.WordDocumentClassName);
            YellowstonePathology.Business.Interface.ICaseDocument document = (YellowstonePathology.Business.Interface.ICaseDocument)Activator.CreateInstance(caseDocumentType, accessionOrder, panelSetOrder, reportSaveMode);
            return document;
        }        
	}
}

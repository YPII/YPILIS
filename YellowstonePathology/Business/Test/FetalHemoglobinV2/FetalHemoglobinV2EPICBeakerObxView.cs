using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.FetalHemoglobinV2
{
    public class FetalHemoglobinV2EPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public FetalHemoglobinV2EPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
			FetalHemoglobinV2TestOrder panelSetOrder = (FetalHemoglobinV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			
			this.AddNextObxElementBeaker("Test Name", panelSetOrder.PanelSetName, document, "F");
			this.AddNextObxElementBeaker("HB-f", panelSetOrder.HbFPercent, document, "F");
            this.AddNextObxElementBeaker("Fetal Maternal Bleed", panelSetOrder.FetalBleed + " mL", document, "F");
            this.AddNextObxElementBeaker("Rh Immune globulin", panelSetOrder.RhImmuneGlobulin + " dose(s)", document, "F");
            this.AddNextObxElementBeaker("Report Comment", panelSetOrder.ReportComment, document, "F");
            this.AddNextObxElementBeaker("Antibodies used in this analysis", "Fetal Hemoglobin (Hb-F)", document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen Description", specimenOrder.Description, document, "F");
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");
            this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");
            this.AddNextObxElementBeaker("References", panelSetOrder.ReportReferences, document, "F");

            this.AddNextObxElementBeaker("Reference Values", panelSetOrder.HbFReferenceRange, document, "F");
            this.AddNextObxElementBeaker("Notification Comment", panelSetOrder.NotificationComment, document, "F");
            this.AddNextObxElementBeaker("Test Development", panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElementBeaker("Location Performed", panelSetOrder.GetLocationPerformedComment(), document, "F");

            this.AddAmendments(document);			
		}
    }
}

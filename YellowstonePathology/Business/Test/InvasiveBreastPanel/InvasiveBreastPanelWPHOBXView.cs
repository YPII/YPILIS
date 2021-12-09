using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.InvasiveBreastPanel
{
    public class InvasiveBreastPanelWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public InvasiveBreastPanelWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        { }


        public override void ToXml(XElement document)
        {
            InvasiveBreastPanel panelSetOrder = (InvasiveBreastPanel)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            InvasiveBreastPanelResult invasiveBreastPanelResult = new InvasiveBreastPanelResult(this.m_ReportNo, this.m_AccessionOrder);

            this.AddHeader(document, panelSetOrder, panelSetOrder.PanelSetName);
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Testing Summary: ", document, "F");

            if (invasiveBreastPanelResult.HasSurgical == true)
            {
                this.HandleLongString("Surgical Diagnosis: " + invasiveBreastPanelResult.SurgicalSpecimen.Diagnosis, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (string.IsNullOrEmpty(invasiveBreastPanelResult.HER2ResultString) == false)
            {
                this.AddNextObxElement("HER 2: " + invasiveBreastPanelResult.HER2ResultString, document, "F");
            }

            if (string.IsNullOrEmpty(invasiveBreastPanelResult.ERResultString) == false)
            {
                this.AddNextObxElement("Estrogen Receptor: " + invasiveBreastPanelResult.ERResultString, document, "F");
            }

            if (string.IsNullOrEmpty(invasiveBreastPanelResult.PRResultString) == false)
            {
                this.AddNextObxElement("Progesterone Receptor: " + invasiveBreastPanelResult.PRResultString, document, "F");
            }
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");

            this.AddAmendments(document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}

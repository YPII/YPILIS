using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Trichomonas
{
    public class TrichomonasWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public TrichomonasWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            TrichomonasTestOrder panelSetOrder = (TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "Trichomonas Vaginalis");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddAmendments(document);

            string method = "DNA was extracted from the patient's specimen using an automated method.  Real time PCR amplification was performed for organism detection and identification.";
            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(method, document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}

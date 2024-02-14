using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.DeerLodge
{
    public class DeerLodgePv1View
    {
        public DeerLodgePv1View()
        {

        }

        public void ToXml(XElement document)
        {
            //PV1||OUTPATIENT|CCMDLLAB^^^CCDLH||||||||||||||||10400830688

            XElement pv1Element = new XElement("PV1");
            document.Add(pv1Element);

            XElement pv12Element = new XElement("PV1.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.2.1", "OUTPATIENT", pv12Element);
            pv1Element.Add(pv12Element);

            XElement pv13Element = new XElement("PV1.3");            
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.3.1", "CCMDLLAB", pv13Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.3.4", "CCDLH", pv13Element);
            pv1Element.Add(pv13Element);
            
            XElement pv119Element = new XElement("PV1.19");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.19.1", "10400830688", pv119Element);
            pv1Element.Add(pv119Element);
        }
    }
}
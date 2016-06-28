using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICFT1View
    {
        string m_DateFormat = "yyyyMMdd";
        YellowstonePathology.Business.Billing.Model.CptCode m_CptCode;
        DateTime m_BillDate;
        DateTime m_PostDate;
        string m_Quantity;
        YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public EPICFT1View(YellowstonePathology.Business.Billing.Model.CptCode cptCode, DateTime billDate, DateTime postDate, string quantity,
            YellowstonePathology.Business.Domain.Physician orderingPhysician)
        {
            this.m_CptCode = cptCode;
            this.m_BillDate = billDate;
            this.m_PostDate = postDate;
            this.m_Quantity = quantity;
            this.m_OrderingPhysician = orderingPhysician;
        }

        public void ToXml(XElement document)
        {
            //    1 2 3     4       5     6     7           8        9 0 123456789      20                    1 2  23 4     25 
            // FT1||||20160523|20160523|CG|760502971|WDCUL$WOUND CULT||1||||||||||1962440800^^^^^^^^NPI^^^^NPI|||M3591||760502971
            // FT1||||20160523|20160523|CG|760101585|RENW$RENAL PANEL||1||||||||||1629286430^^^^^^^^NPI^^^^NPI|||M1019||760101585
            XElement ft1Element = new XElement("FT1", "FT1");
            document.Add(ft1Element);

            XElement ft14Element = new XElement("FT1.4", this.m_BillDate.ToString(this.m_DateFormat));
            ft1Element.Add(ft14Element);

            XElement ft15Element = new XElement("FT1.5", this.m_PostDate.ToString(this.m_DateFormat));
            ft1Element.Add(ft15Element);

            XElement ft16Element = new XElement("FT1.6", "CG");
            ft1Element.Add(ft16Element);

            XElement ft17Element = new XElement("FT1.7", this.m_CptCode.SVHCDMCode);
            ft1Element.Add(ft17Element);

            XElement ft18Element = new XElement("FT1.8", this.m_CptCode.SVHCDMDescription);
            ft1Element.Add(ft18Element);

            XElement ft110Element = new XElement("FT1.10", this.m_Quantity);
            ft1Element.Add(ft110Element);

            XElement ft120Element = new XElement("FT1.20");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.1", this.m_OrderingPhysician.Npi.ToString(), ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.2", this.m_OrderingPhysician.LastName, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.3", this.m_OrderingPhysician.FirstName, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.4", this.m_OrderingPhysician.MiddleInitial, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.5", string.Empty, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.6", string.Empty, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.7", string.Empty, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.8", string.Empty, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.9", "NPI", ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.10", string.Empty, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.11", string.Empty, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.12", string.Empty, ft120Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("FT1.20.13", "NPI", ft120Element);
            ft1Element.Add(ft120Element);

            XElement ft123Element = new XElement("FT1.23", "?????");
            ft1Element.Add(ft123Element);

            XElement ft125Element = new XElement("FT1.25", this.m_CptCode.SVHCDMCode);
            ft1Element.Add(ft125Element);
        }
    }
}

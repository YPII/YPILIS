using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Orders
{
    public class GT1
    {
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public GT1(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public void ToXml(XElement document)
        {
            XElement gt1Element = new XElement("GT1");
            document.Add(gt1Element);
        }
    }
}

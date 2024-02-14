using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Orders
{
    public class IN1
    {
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public IN1(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public void ToXml(XElement document)
        {
            XElement in1Element = new XElement("IN1");
            document.Add(in1Element);
        }
    }
}

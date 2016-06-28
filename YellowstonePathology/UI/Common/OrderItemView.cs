using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Common
{
    public class OrderItemView
    {
        private XElement m_OrderItemDocument;        
        private List<XElement> m_OrderItemElementList;                

        public OrderItemView()
        {
            System.Reflection.Assembly assembly = this.GetType().Assembly;
            XmlReader orderItemXmlReader = XmlReader.Create(assembly.GetManifestResourceStream("YellowstonePathology.UI.Common.OrderItem.xml"));
            this.m_OrderItemDocument = XElement.Load(orderItemXmlReader);
            
            this.m_OrderItemElementList = new List<XElement>();            
            this.FlattenTree(this.m_OrderItemDocument);            
        }

        public XElement OrderItemDocument
        {
            get { return this.m_OrderItemDocument; }
        }

        public void ClearSelectedItems()
        {            
            this.ClearSelectedItems(this.m_OrderItemDocument);
        }

        private void ClearSelectedItems(XElement xElement)
        {
            if (xElement != null)
            {
                List<XElement> xElementList = (from el in xElement.Elements("OrderItem") select el).ToList<XElement>();
                foreach (XElement element in xElementList)
                {
                    element.Element("Order").Value = "False";
                    if (element.Element("Comment") != null)
                    {
                        element.Element("Comment").Value = string.Empty;
                    }
                    ClearSelectedItems(element.Element("OrderItems"));
                }
            }
        }

        public YellowstonePathology.Business.Test.Model.TestCollection GetSelectedTests()
        {
            YellowstonePathology.Business.Test.Model.TestCollection testCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();
            YellowstonePathology.Business.Test.Model.TestCollection result = new YellowstonePathology.Business.Test.Model.TestCollection();
            foreach (XElement element in this.m_OrderItemElementList)
            {
                if (element.Element("ItemType").Value == "Test")
                {
                    if (element.Element("Order").Value == "True")
                    {                        
                        int testId = Convert.ToInt32(element.Element("Id").Value);
                        YellowstonePathology.Business.Test.Model.Test test = testCollection.GetTest(testId);
                        string testOrderComment = element.Element("Comment").Value;
                        test.OrderComment = testOrderComment;
                        result.Add(test);
                    }
                }
            }
            return result;
        }

        public List<YellowstonePathology.Business.Test.Model.DualStain> GetSelectedDualStains()
        {
            YellowstonePathology.Business.Test.Model.TestCollection testCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();
            List<YellowstonePathology.Business.Test.Model.DualStain> result = new List<YellowstonePathology.Business.Test.Model.DualStain>();
            YellowstonePathology.Business.Test.Model.DualStainCollection dualStainCollection = YellowstonePathology.Business.Test.Model.DualStainCollection.GetAll();

            foreach (XElement element in this.m_OrderItemElementList)
            {
                if (element.Element("ItemType").Value == "DualStain")
                {
                    if (element.Element("Order").Value == "True")
                    {
                        int testId = Convert.ToInt32(element.Element("Id").Value);
                        YellowstonePathology.Business.Test.Model.DualStain dualStain = dualStainCollection.Get(testId);
                        string testOrderComment = element.Element("Comment").Value;
                        dualStain.OrderComment = testOrderComment;
                        result.Add(dualStain);
                    }
                }
            }
            return result;
        }

        private void FlattenTree(XElement xElement)
        {
            if (xElement != null)
            {
                List<XElement> xElementList = (from el in xElement.Elements("OrderItem") select el).ToList<XElement>();
                foreach (XElement element in xElementList)
                {
                    this.m_OrderItemElementList.Add(element);
                    FlattenTree(element.Element("OrderItems"));
                }
            }
        }          
       
        public void SelectAnElement(XElement elementToSelect)
        {
            foreach (XElement stainElement in elementToSelect.Element("OrderItems").Descendants("OrderItem"))
            {
                if (stainElement.Element("Orderable").Value == "True")
                {
                    stainElement.Element("Order").Value = "True";
                }
            }            
        }

        public void UnSelectAnElement(XElement elementToUnselect)
        {
            foreach (XElement stainElement in elementToUnselect.Element("OrderItems").Descendants("OrderItem"))
            {
                if (stainElement.Element("Orderable").Value == "True")
                {
                    stainElement.Element("Order").Value = "False";
                }
            }
        } 
    }
}

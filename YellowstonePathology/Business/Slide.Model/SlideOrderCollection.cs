using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Slide.Model
{
    public class SlideOrderCollection : SlideOrderCollection_Base
    {        
        public SlideOrderCollection()
        {
            
        }

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string slideOrderId = element.Element("SlideOrderId").Value;
                    if (this[i].SlideOrderId == slideOrderId)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }

        public bool TestOrderExists(string testOrderId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in this)
            {
                if (slideOrder.TestOrderId == testOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.Model.TestOrder_Base GetTestOrder(string testOrderId)
        {
            YellowstonePathology.Business.Test.Model.TestOrder_Base result = null;
            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in this)
            {
                if (slideOrder.TestOrderId == testOrderId)
                {
                    result = slideOrder.TestOrder;
                    break;
                }
            }
            return result;
        }        

        public bool IsLastSlide(string slideOrderId)
        {
            bool result = false;
            if (this[this.Count - 1].SlideOrderId == slideOrderId)
            {
                result = true;
            }
            return result;
        }

        public List<SlideOrder> GetSlideOrderByAliquotOrderId(string aliquotOrderId)
        {
            List<SlideOrder> result = new List<SlideOrder>();
            foreach (SlideOrder slideOrder in this)
            {
                if (slideOrder.AliquotOrderId == aliquotOrderId)
                {
                    result.Add(slideOrder);                    
                }
            }
            return result;
        }

        public SlideOrder GetSlideOrderByTestOrderId(string testOrderId)
        {
            SlideOrder result = null;
            foreach (SlideOrder slideOrder in this)
            {
                if (slideOrder.TestOrderId == testOrderId)
                {
                    result = slideOrder;
                    break;
                }
            }
            return result;
        }        
    }
}

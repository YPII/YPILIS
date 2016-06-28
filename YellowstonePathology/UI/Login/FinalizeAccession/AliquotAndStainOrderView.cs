using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public class AliquotAndStainOrderView
    {
        private XElement m_View;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public AliquotAndStainOrderView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;
            this.Build();
        }

		public void Refresh(bool preserveCurrentlySelectedItems, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
			this.m_PanelSetOrder = panelSetOrder;
            XElement oldView = this.m_View;
            this.Build();
            if (preserveCurrentlySelectedItems == true)
            {                
                this.SelectedItemsFromOldView(oldView);
            }            
        }

        public XElement View
        {
            get { return this.m_View; }
        }

        private bool IsSpecimenOrderSelected(string specimenOrderId, XElement view)
        {
            bool result = false;
            foreach (XElement specimenElement in view.Elements("SpecimenOrder"))
            {
                if (specimenElement.Element("SpecimenOrderId").Value == specimenOrderId)
                {
                    bool selected = Convert.ToBoolean(specimenElement.Element("IsSelected").Value);
                    if (selected == true)
                    {
                        result = true;
                        break;
                    }
                }
            }    
            return result;
        }

        private bool DoesAliquotOrderExist(string aliquotOrderId, XElement view)
        {
            bool result = false;
            foreach (XElement specimenElement in view.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotOrderElement in specimenElement.Elements("AliquotOrder"))
                {
                    if (aliquotOrderElement.Element("AliquotOrderId").Value == aliquotOrderId)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        private bool IsAliquotOrderSelected(string aliquotOrderId, XElement view)
        {
            bool result = false;
            foreach (XElement specimenElement in view.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotOrderElement in specimenElement.Elements("AliquotOrder"))
                {
                    if (aliquotOrderElement.Element("AliquotOrderId").Value == aliquotOrderId)
                    {
                        bool selected = Convert.ToBoolean(aliquotOrderElement.Element("IsSelected").Value);
                        if (selected == true)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private bool IsTestOrderSelected(string testOrderId, XElement view)
        {
            bool result = false;
            foreach (XElement testOrderElement in view.Elements("TestOrder"))
            {
                if (testOrderElement.Element("TestOrderId").Value == testOrderId)
                {
                    bool selected = Convert.ToBoolean(testOrderElement.Element("IsSelected").Value);
                    if (selected == true)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        private void SelectedItemsFromOldView(XElement oldView)
        {
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                string specimenOrderId = specimenElement.Element("SpecimenOrderId").Value;
                if (this.IsSpecimenOrderSelected(specimenOrderId, oldView) == true)
                {
                    specimenElement.Element("IsSelected").SetValue("true");
                }
                foreach (XElement aliquotOrderElement in specimenElement.Elements("AliquotOrder"))
                {
                    string aliquotOrderId = aliquotOrderElement.Element("AliquotOrderId").Value;
                    if (this.DoesAliquotOrderExist(aliquotOrderId, oldView) == true)
                    {
                        if (this.IsAliquotOrderSelected(aliquotOrderId, oldView) == true)
                        {
                            aliquotOrderElement.Element("IsSelected").SetValue("true");
                        }
                    }                    
                    foreach (XElement testElement in aliquotOrderElement.Elements("TestOrder"))
                    {
                        string testOrderId = testElement.Element("TestOrderId").Value;
                        if(this.IsTestOrderSelected(specimenOrderId, oldView) == true)
                        {
                            testElement.Element("IsSelected").SetValue("true");
                        }
                    }
                }
            }     
        }

        private void Build()
        {
            this.m_View = new XElement("SpecimenOrders");
			if (this.m_PanelSetOrder != null)
			{
				foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
				{
					XElement specimenElement = new XElement("SpecimenOrder");
					XElement specimenIsSelectedElement = new XElement("IsSelected", false);
                    XElement specimenClientAccessionedElement = new XElement("ClientAccessioned", specimenOrder.ClientAccessioned);
					XElement specimenOrderIdElement = new XElement("SpecimenOrderId", specimenOrder.SpecimenOrderId);
					XElement specimenDescriptionElement = new XElement("Description", specimenOrder.Description);
					XElement specimenDescriptionStringElement = new XElement("DescriptionString", specimenOrder.GetSpecimenDescriptionString());

					specimenElement.Add(specimenIsSelectedElement);
                    specimenElement.Add(specimenClientAccessionedElement);                    
					specimenElement.Add(specimenOrderIdElement);
					specimenElement.Add(specimenDescriptionElement);
					specimenElement.Add(specimenDescriptionStringElement);
					this.m_View.Add(specimenElement);

					foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
					{
						XElement aliquotElement = new XElement("AliquotOrder");
						XElement aliquotIdElement = new XElement("AliquotOrderId", aliquotOrder.AliquotOrderId);
                        XElement aliquotClientAccessionedElement = new XElement("ClientAccessioned", aliquotOrder.ClientAccessioned);
						XElement aliquotLabelElement = new XElement("Label", aliquotOrder.Display); ;
						XElement aliquotTypeElement = new XElement("Type", aliquotOrder.AliquotType);
						XElement aliquotIdentificationType = new XElement("LabelType", aliquotOrder.LabelType);
						XElement aliquotIsSelectedElement = new XElement("IsSelected", false);
                        XElement aliquotEmbeddingInstructionsElement = new XElement("EmbeddingInstructions", aliquotOrder.EmbeddingInstructions);

                        aliquotElement.Add(aliquotLabelElement);                        
						aliquotElement.Add(aliquotIdElement);
                        aliquotElement.Add(aliquotClientAccessionedElement);
						aliquotElement.Add(aliquotTypeElement);
						aliquotElement.Add(aliquotIsSelectedElement);
						aliquotElement.Add(aliquotIdentificationType);
                        aliquotElement.Add(aliquotEmbeddingInstructionsElement);
                        specimenElement.Add(aliquotElement);
                        
                        foreach (YellowstonePathology.Business.Test.Model.TestOrder_Base testOrderBase in aliquotOrder.TestOrderCollection)
                        {
                            YellowstonePathology.Business.Test.Model.TestOrder testOrder = this.m_PanelSetOrder.GetTestOrder(testOrderBase.TestOrderId);
                            if (testOrder != null)
                            {
                                XElement testElement = new XElement("TestOrder");
                                XElement testOrderIdElement = new XElement("TestOrderId", testOrder.TestOrderId.ToString());                                
                                XElement testNameElement = new XElement("TestName", testOrder.TestName);
                                XElement testIsSelectedElement = new XElement("IsSelected", false);

                                testElement.Add(testOrderIdElement);
                                testElement.Add(testIsSelectedElement);
                                testElement.Add(testNameElement);

                                foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in testOrder.SlideOrderCollection)
                                {
                                    XElement slideElement = new XElement("SlideOrder",
                                        new XElement("SlideOrderId", slideOrder.SlideOrderId.ToString()),
                                        new XElement("IsSelected", false),
                                        new XElement("ClientAccessioned", slideOrder.ClientAccessioned),
                                        new XElement("Label", slideOrder.Label));
                                    testElement.Add(slideElement);
                                }                            
                                aliquotElement.Add(testElement);
                            }
                        }                        					    
					}
				}
			}
        }

        public void SetSpecimenChecks(bool check)
        {
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                specimenElement.Element("IsSelected").SetValue(check.ToString().ToLower());
            }            
        }

        public void SetAliquotChecks(bool check)
        {
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    aliquotElement.Element("IsSelected").SetValue(check.ToString());
                }
            }            
        }

        public void SetTestChecks(bool check)
        {
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    foreach (XElement testElement in aliquotElement.Elements("TestOrder"))
                    {
                        testElement.Element("IsSelected").SetValue(check.ToString());
                    }
                }
            }            
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection GetSelectedSpecimen()
        {
			YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection result = new Business.Specimen.Model.SpecimenOrderCollection();
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                bool selected = Convert.ToBoolean(specimenElement.Element("IsSelected").Value);
                if (selected == true)
                {
                    string specimenOrderId = specimenElement.Element("SpecimenOrderId").Value;
					YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderById(specimenOrderId);
                    result.Add(specimenOrder);
                }
            }
            return result;
        }

        public void SetEmbeddingComments()
        {
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotElement.Element("AliquotOrderId").Value);
                    aliquotOrder.EmbeddingInstructions = aliquotElement.Element("EmbeddingInstructions").Value;
                }
            }
        }

        public YellowstonePathology.Business.Test.AliquotOrderCollection GetAliquotsWithSelectedTests()
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection result = new Business.Test.AliquotOrderCollection();
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    foreach (XElement testElement in aliquotElement.Elements("TestOrder"))
                    {
                        bool selected = Convert.ToBoolean(testElement.Element("IsSelected").Value);
                        if (selected == true)
                        {
							foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                            {
                                if (specimenOrder.SpecimenOrderId == specimenElement.Element("SpecimenOrderId").Value)
                                {
                                    foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                                    {
                                        if (aliquotOrder.AliquotOrderId.ToString() == aliquotElement.Element("AliquotOrderId").Value)
                                        {
                                            result.Add(aliquotOrder);
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.AliquotOrderCollection GetSelectedAliquots()
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection result = new Business.Test.AliquotOrderCollection();
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    bool selected = Convert.ToBoolean(aliquotElement.Element("IsSelected").Value);
                    if (selected == true)
                    {
						foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                        {
                            if (specimenOrder.SpecimenOrderId == specimenElement.Element("SpecimenOrderId").Value)
                            {
                                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                                {
                                    if (aliquotOrder.AliquotOrderId.ToString() == aliquotElement.Element("AliquotOrderId").Value)
                                    {
                                        result.Add(aliquotOrder);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Slide.Model.SlideOrderCollection GetSelectedSlideOrders()
        {
            YellowstonePathology.Business.Slide.Model.SlideOrderCollection result = new YellowstonePathology.Business.Slide.Model.SlideOrderCollection();
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    foreach (XElement testElement in aliquotElement.Elements("TestOrder"))
                    {
                        foreach (XElement slideElement in testElement.Elements("SlideOrder"))
                        {
                            bool selected = Convert.ToBoolean(slideElement.Element("IsSelected").Value);
                            if (selected == true)
                            {
								foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                                {
                                    if (specimenOrder.SpecimenOrderId == specimenElement.Element("SpecimenOrderId").Value)
                                    {
                                        foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                                        {
                                            if (aliquotOrder.AliquotOrderId == aliquotElement.Element("AliquotOrderId").Value)
                                            {
                                                foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in aliquotOrder.SlideOrderCollection)
                                                {
                                                    if (slideOrder.SlideOrderId == slideElement.Element("SlideOrderId").Value)
                                                    {
                                                        result.Add(slideOrder);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.AliquotOrderCollection GetAliquotCollection()
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection result = new Business.Test.AliquotOrderCollection();
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
					foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                    {
                        if (specimenOrder.SpecimenOrderId == specimenElement.Element("SpecimenOrderId").Value)
                        {
                            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                            {
                                if (aliquotOrder.AliquotOrderId.ToString() == aliquotElement.Element("AliquotOrderId").Value)
                                {
                                    result.Add(aliquotOrder);
                                    break;
                                }
                            }
                            break;
                        }
                    }                    
                }
            }
            return result;
        }

        public bool HaveSlidesBeenMadeForTestOrder(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {
            bool result = false;
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    foreach (XElement testOrderElement in aliquotElement.Elements("TestOrder"))
                    {
                        string testOrderId = testOrderElement.Element("TestOrderId").Value;
                        if (testOrderId == testOrder.TestOrderId)
                        {
                            if (testOrderElement.Element("SlideOrder") != null)
                            {
                                result = true;
                                return result;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.Model.TestOrderCollection GetSelectedTestOrders()
        {
            YellowstonePathology.Business.Test.Model.TestOrderCollection result = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    foreach (XElement testOrderElement in aliquotElement.Elements("TestOrder"))
                    {
                        bool selected = Convert.ToBoolean(testOrderElement.Element("IsSelected").Value);
                        if (selected == true)
                        {
                            string testOrderId = testOrderElement.Element("TestOrderId").Value;
                            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_PanelSetOrder.PanelOrderCollection)
                            {                                
                                foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                                {
                                    if (testOrderId == testOrder.TestOrderId)
                                    {
                                        result.Add(testOrder);                                        
                                        break;
                                    }
                                }                                
                            }
                        }
                    }
                }
            }
            return result;
        }

        public List<string> GetSelectedSlideOrderIds()
        {
            List<string> result = new List<string>();
            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    foreach (XElement testElement in aliquotElement.Elements("TestOrder"))
                    {
                        string testOrderId = testElement.Element("TestOrderId").Value;
                        foreach (XElement slideElement in testElement.Elements("SlideOrder"))
                        {
                            bool selected = Convert.ToBoolean(slideElement.Element("IsSelected").Value);
                            if (selected == true)
                            {                                
                                string slideOrderId = slideElement.Element("SlideOrderId").Value;
                                result.Add(slideOrderId);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private bool TestHasSlide()
        {
            bool result = false;

            foreach (XElement specimenElement in this.m_View.Elements("SpecimenOrder"))
            {
                foreach (XElement aliquotElement in specimenElement.Elements("AliquotOrder"))
                {
                    bool selected = Convert.ToBoolean(aliquotElement.Element("IsSelected").Value);
                    foreach (XElement testElement in aliquotElement.Elements("TestOrder"))
                    {
                        selected = selected ? selected : Convert.ToBoolean(testElement.Element("IsSelected").Value);
                        if (selected == true)
                        {
                            if (testElement.Elements("SlideOrder").Count() > 0)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                    if (result == true)
                    {
                        break;
                    }
                }
                if (result == true)
                {
                    break;
                }
            }
            return result;
        }
    }
}

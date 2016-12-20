using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class AccessionOrderBuilder
	{		        
		public AccessionOrderBuilder()
		{
            
		}		

		public void Build(MySqlCommand cmd, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            XElement document = null;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        document = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);
                    }
                }
            }

            if (document != null)
            {                 
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(document, accessionOrder);
				xmlPropertyWriter.Write();

                accessionOrder.AccessionLock.MasterAccessionNo = accessionOrder.MasterAccessionNo;
                BuildSpecimenOrder(accessionOrder, document);                
				BuildTaskOrder(accessionOrder, document);
				BuildIcdBillingCode(accessionOrder, document);
                BuildPanelSetOrder(accessionOrder, document);				                
            }                        
		}        		

		private void BuildSpecimenOrder(Test.AccessionOrder accessionOrder, XElement accessionOrderElement)
		{            
            accessionOrder.SpecimenOrderCollection.IsLoading = true;
			List<XElement> specimenOrderElements = (from item in accessionOrderElement.Elements("SpecimenOrderCollection")
													select item).ToList<XElement>();

            accessionOrder.SpecimenOrderCollection.RemoveDeleted(specimenOrderElements.Elements("SpecimenOrder"));
			foreach (XElement specimenOrderElement in specimenOrderElements.Elements("SpecimenOrder"))
			{
                string specimenOrderId = specimenOrderElement.Element("SpecimenOrderId").Value;
                Specimen.Model.SpecimenOrder specimenOrder = null;

                if (accessionOrder.SpecimenOrderCollection.Exists(specimenOrderId) == true)
                {
                    specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrder(specimenOrderId);
                }
                else
                {
                    specimenOrder = new Specimen.Model.SpecimenOrder();
                    accessionOrder.SpecimenOrderCollection.Add(specimenOrder);
                }
				
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(specimenOrderElement, specimenOrder);
				xmlPropertyWriter.Write();
				BuildAliquotOrderLeftSide(specimenOrder, specimenOrderElement);
				
			}
            accessionOrder.SpecimenOrderCollection.IsLoading = false;
		}

		private void BuildAliquotOrderLeftSide(Specimen.Model.SpecimenOrder specimenOrder, XElement specimenOrderElement)
		{
			List<XElement> aliquotOrderElements = (from item in specimenOrderElement.Elements("AliquotOrderCollection")
												   select item).ToList<XElement>();

            specimenOrder.AliquotOrderCollection.RemoveDeleted(aliquotOrderElements.Elements("AliquotOrder"));
			foreach (XElement aliquotOrderElement in aliquotOrderElements.Elements("AliquotOrder"))
			{
                string aliquotOrderId = aliquotOrderElement.Element("AliquotOrderId").Value;
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = null;
                if (specimenOrder.AliquotOrderCollection.Exists(aliquotOrderId) == true)
                {
                    aliquotOrder = specimenOrder.AliquotOrderCollection.Get(aliquotOrderId);
                }
                else
                {
                    aliquotOrder = new Test.AliquotOrder();
                    specimenOrder.AliquotOrderCollection.Add(aliquotOrder);
                }
				
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(aliquotOrderElement, aliquotOrder);
				xmlPropertyWriter.Write();
              
                BuildAliquotOrderTestOrder(aliquotOrder, aliquotOrderElement);
                BuildAliquotOrderSlideOrderCollection(aliquotOrder, aliquotOrderElement);				
			}
		}

        private void BuildTestOrderAliquotOrder(YellowstonePathology.Business.Test.Model.TestOrder testOrder, XElement testOrderElement)
        {
            XElement aliquotOrderElement = testOrderElement.Element("AliquotOrder");
            if (aliquotOrderElement != null)
            {
                YellowstonePathology.Business.Test.AliquotOrder_Base aliquotOrder = new Test.AliquotOrder_Base();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(aliquotOrderElement, aliquotOrder);
				xmlPropertyWriter.Write();
				testOrder.AliquotOrder = aliquotOrder;
            }
        }

        private void BuildSlideOrderTestOrder(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder, XElement slideOrderElement)
        {
            XElement testOrderElement = slideOrderElement.Element("TestOrder");
            if (testOrderElement != null)
            {
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(testOrderElement, testOrder);
				xmlPropertyWriter.Write();
                slideOrder.TestOrder = testOrder;
            }
        }

        private void BuildAliquotOrderTestOrder(Test.AliquotOrder aliquotOrder, XElement aliquotOrderElement)
        {
            List<XElement> aliquotOrderElements = (from item in aliquotOrderElement.Elements("TestOrderCollection")
                                                   select item).ToList<XElement>();
            aliquotOrder.TestOrderCollection.RemoveDeleted(aliquotOrderElements.Elements("TestOrder"));
            foreach (XElement testOrderElement in aliquotOrderElements.Elements("TestOrder"))
            {
                string testOrderId = testOrderElement.Element("TestOrderId").Value;
                YellowstonePathology.Business.Test.Model.TestOrder_Base testOrder = null;
                if (aliquotOrder.TestOrderCollection.Exists(testOrderId) == true)
                {
                    testOrder = aliquotOrder.TestOrderCollection.GetBase(testOrderId);
                }
                else
                {
                    testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
                    aliquotOrder.TestOrderCollection.Add(testOrder);
                }
                
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(testOrderElement, testOrder);
				xmlPropertyWriter.Write();
            }
        }		

        private void BuildTestOrderSlideOrderCollection(YellowstonePathology.Business.Test.Model.TestOrder testOrder, XElement testOrderElement)
        {
            List<XElement> slideOrderElements = (from item in testOrderElement.Elements("SlideOrderCollection")
                                                           select item).ToList<XElement>();

            testOrder.SlideOrderCollection.RemoveDeleted(slideOrderElements.Elements("SlideOrder"));
            foreach (XElement slideOrderElement in slideOrderElements.Elements("SlideOrder"))
            {
                string slideOrderId = slideOrderElement.Element("SlideOrderId").Value;
                YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = new YellowstonePathology.Business.Slide.Model.SlideOrder();
                if (testOrder.SlideOrderCollection.Exists(slideOrderId) == true)
                {
                    slideOrder = testOrder.SlideOrderCollection.Get(slideOrderId);
                }
                else
                {
                    slideOrder = new YellowstonePathology.Business.Slide.Model.SlideOrder();
                    testOrder.SlideOrderCollection.Add(slideOrder);
                }

                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(slideOrderElement, slideOrder);
                xmlPropertyWriter.Write();                
            }
        }

        private void BuildAliquotOrderSlideOrderCollection(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, XElement aliquotOrderElement)
        {
            List<XElement> slideOrderElements = (from item in aliquotOrderElement.Elements("SlideOrderCollection")
                                                 select item).ToList<XElement>();

            aliquotOrder.SlideOrderCollection.RemoveDeleted(slideOrderElements.Elements("SlideOrder"));
            foreach (XElement slideOrderElement in slideOrderElements.Elements("SlideOrder"))
            {
                string slideOrderId = slideOrderElement.Element("SlideOrderId").Value;
                YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = null;
                if (aliquotOrder.SlideOrderCollection.Exists(slideOrderId) == true)
                {
                    slideOrder = aliquotOrder.SlideOrderCollection.Get(slideOrderId);
                }
                else
                {
                    slideOrder = new YellowstonePathology.Business.Slide.Model.SlideOrder();
                    aliquotOrder.SlideOrderCollection.Add(slideOrder);
                }

                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(slideOrderElement, slideOrder);
                xmlPropertyWriter.Write();
                
            }
        }   

		private void BuildPanelSetOrder(Test.AccessionOrder accessionOrder, XElement accessionOrderElement)
		{
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();

			List<XElement> panelSetOrderElements = (from psoc in accessionOrderElement.Elements("PanelSetOrderCollection")
														select psoc).ToList<XElement>();

            accessionOrder.PanelSetOrderCollection.RemoveDeleted(panelSetOrderElements.Elements("PanelSetOrder"));
			foreach (XElement panelSetOrderElement in panelSetOrderElements.Elements("PanelSetOrder"))
			{
                int panelSetId = Convert.ToInt32(panelSetOrderElement.Element("PanelSetId").Value);
                string reportNo = panelSetOrderElement.Element("ReportNo").Value;
                Test.PanelSetOrder panelSetOrder = null;
                if (accessionOrder.PanelSetOrderCollection.Exists(reportNo) == true)
                {
                    panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
                }
                else
                {
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetId);
                    panelSetOrder = Test.PanelSetOrderFactory.CreatePanelSetOrder(panelSet);
                    accessionOrder.PanelSetOrderCollection.Add(panelSetOrder);
                }
				
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelSetOrderElement, panelSetOrder);
				xmlPropertyWriter.Write();

                YellowstonePathology.Business.Builder.PanelSetOrderBuilder builder = YellowstonePathology.Business.Builder.PanelSetOrderBuilderFactory.GetBuilder(panelSetId);
				builder.Build(panelSetOrder, panelSetOrderElement);

                this.SetOrderedOnDescription(panelSetOrder, accessionOrder);
				BuildAmendment(panelSetOrder, panelSetOrderElement);
                BuildPanelSetOrderCPTCode(panelSetOrder, panelSetOrderElement);
                BuildPanelSetOrderCPTCodeBill(panelSetOrder, panelSetOrderElement);
                BuildTestOrderReportDistribution(panelSetOrder, panelSetOrderElement);
                BuildTestOrderReportDistributionLog(panelSetOrder, panelSetOrderElement);
				BuildPanelOrder(panelSetOrder, panelSetOrderElement);
				BuildSurgicalSpecific(accessionOrder, panelSetOrder, panelSetOrderElement);
			}
		}

        private void SetOrderedOnDescription(Test.PanelSetOrder panelSetOrder, Test.AccessionOrder accessionOrder)
        {            
            if (panelSetOrder.OrderedOn != null)
            {
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
                if (specimenOrder != null)
                {
                    switch (panelSetOrder.OrderedOn)
                    {
                        case YellowstonePathology.Business.OrderedOn.Specimen:
                        case YellowstonePathology.Business.OrderedOn.ThinPrepFluid:
                            panelSetOrder.OrderedOnDescription = specimenOrder.Description;
                            break;
                        case YellowstonePathology.Business.OrderedOn.Aliquot:
                            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetAliquotOrder(panelSetOrder.OrderedOnId);
                            panelSetOrder.OrderedOnDescription = specimenOrder.Description + " - " + aliquotOrder.Label;
                            break;
                        default:
                            throw new Exception("Must be Specimen or Aliquot");
                    }
                }
            }            
        }

		private void BuildAmendment(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
		{
			List<XElement> amendmentElements = (from item in panelSetOrderElement.Elements("AmendmentCollection")
												 select item).ToList<XElement>();
			foreach (XElement amendmentElement in amendmentElements.Elements("Amendment"))
			{
                string amendmentId = amendmentElement.Element("AmendmentId").Value;
                YellowstonePathology.Business.Amendment.Model.Amendment amendment = null;
                if (panelSetOrder.AmendmentCollection.Exists(amendmentId) == true)
                {
                    amendment = panelSetOrder.AmendmentCollection.GetAmendment(amendmentId);
                }
                else
                {
                    amendment = new YellowstonePathology.Business.Amendment.Model.Amendment();
                    panelSetOrder.AmendmentCollection.Add(amendment);
                }

                
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(amendmentElement, amendment);
				xmlPropertyWriter.Write();
			}
		}

        private void BuildTestOrderReportDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> testOrderReportDistributionElements = (from item in panelSetOrderElement.Elements("TestOrderReportDistributionCollection")
                                                           select item).ToList<XElement>();
            panelSetOrder.TestOrderReportDistributionCollection.RemoveDeleted(testOrderReportDistributionElements.Elements("TestOrderReportDistribution"));
            foreach (XElement testOrderReportDistributionElement in testOrderReportDistributionElements.Elements("TestOrderReportDistribution"))
            {
                string testOrderReportDistributionId = testOrderReportDistributionElement.Element("TestOrderReportDistributionId").Value;
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = null;
                if (panelSetOrder.TestOrderReportDistributionCollection.Exists(testOrderReportDistributionId) == true)
                {
                    testOrderReportDistribution = panelSetOrder.TestOrderReportDistributionCollection.Get(testOrderReportDistributionId);
                }
                else
                {
                    testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                    panelSetOrder.TestOrderReportDistributionCollection.Add(testOrderReportDistribution);
                }
                
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(testOrderReportDistributionElement, testOrderReportDistribution);
                xmlPropertyWriter.Write();                
            }
        }

        private void BuildTestOrderReportDistributionLog(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> testOrderReportDistributionLogElements = (from item in panelSetOrderElement.Elements("TestOrderReportDistributionLogCollection")
                                                                      select item).ToList<XElement>();

            panelSetOrder.TestOrderReportDistributionLogCollection.RemoveDeleted(testOrderReportDistributionLogElements.Elements("TestOrderReportDistributionLog"));
            foreach (XElement testOrderReportDistributionLogElement in testOrderReportDistributionLogElements.Elements("TestOrderReportDistributionLog"))
            {
                string testOrderReportDistributionLogId = testOrderReportDistributionLogElement.Element("TestOrderReportDistributionLogId").Value;
                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLog testOrderReportDistributionLog = null;
                if (panelSetOrder.TestOrderReportDistributionLogCollection.Exists(testOrderReportDistributionLogId) == true)
                {
                    testOrderReportDistributionLog = panelSetOrder.TestOrderReportDistributionLogCollection.Get(testOrderReportDistributionLogId);
                }
                else
                {
                    testOrderReportDistributionLog = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLog();
                    panelSetOrder.TestOrderReportDistributionLogCollection.Add(testOrderReportDistributionLog);
                }

                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(testOrderReportDistributionLogElement, testOrderReportDistributionLog);
                xmlPropertyWriter.Write();
            }
        }

        private void BuildPanelSetOrderCPTCode(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> panelSetOrderCPTCodeElements = (from item in panelSetOrderElement.Elements("PanelSetOrderCPTCodeCollection")
                                                           select item).ToList<XElement>();
            panelSetOrder.PanelSetOrderCPTCodeCollection.RemoveDeleted(panelSetOrderCPTCodeElements.Elements("PanelSetOrderCPTCode"));
            foreach (XElement panelSetOrderCPTCodeElement in panelSetOrderCPTCodeElements.Elements("PanelSetOrderCPTCode"))
            {
                string panelSetOrderCPTCodeId = panelSetOrderCPTCodeElement.Element("PanelSetOrderCPTCodeId").Value;
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = null;
                if (panelSetOrder.PanelSetOrderCPTCodeCollection.Exists(panelSetOrderCPTCodeId) == true)
                {
                    panelSetOrderCPTCode = panelSetOrder.PanelSetOrderCPTCodeCollection.Get(panelSetOrderCPTCodeId);
                }
                else
                {
                    panelSetOrderCPTCode = new Test.PanelSetOrderCPTCode();
                    panelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
                }

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelSetOrderCPTCodeElement, panelSetOrderCPTCode);
				xmlPropertyWriter.Write();
            }
        }

        private void BuildPanelSetOrderCPTCodeBill(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> panelSetOrderCPTCodeBillElements = (from item in panelSetOrderElement.Elements("PanelSetOrderCPTCodeBillCollection")
                                                           select item).ToList<XElement>();
            panelSetOrder.PanelSetOrderCPTCodeBillCollection.RemoveDeleted(panelSetOrderCPTCodeBillElements.Elements("PanelSetOrderCPTCodeBill"));
            foreach (XElement panelSetOrderCPTCodeBillElement in panelSetOrderCPTCodeBillElements.Elements("PanelSetOrderCPTCodeBill"))
            {
                string panelSetOrderCPTCodeBillId = panelSetOrderCPTCodeBillElement.Element("PanelSetOrderCPTCodeBillId").Value;
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = null;
                if (panelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(panelSetOrderCPTCodeBillId) == true)
                {
                    panelSetOrderCPTCodeBill = panelSetOrder.PanelSetOrderCPTCodeBillCollection.Get(panelSetOrderCPTCodeBillId);
                }
                else
                {
                    panelSetOrderCPTCodeBill = new Test.PanelSetOrderCPTCodeBill();
                    panelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
                }
                
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelSetOrderCPTCodeBillElement, panelSetOrderCPTCodeBill);
				xmlPropertyWriter.Write();
                
            }
        }

		private void BuildPanelOrder(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
		{			
			List<XElement> panelOrderElements = (from poc in panelSetOrderElement.Elements("PanelOrderCollection")
												 select poc).ToList<XElement>();

            YellowstonePathology.Business.Panel.Model.PanelCollection panelCollection = YellowstonePathology.Business.Panel.Model.PanelCollection.GetAll();
            panelSetOrder.PanelOrderCollection.RemoveDeleted(panelOrderElements.Elements("PanelOrder"));
			foreach (XElement panelOrderElement in panelOrderElements.Elements("PanelOrder"))
			{
                string panelOrderId = panelOrderElement.Element("PanelOrderId").Value;
                Test.PanelOrder panelOrder = null;
                if (panelSetOrder.PanelOrderCollection.Exists(panelOrderId) == true)
                {
                    panelOrder = panelSetOrder.Get(panelOrderId);
                }
                else
                {
                    int panelId = Convert.ToInt32(panelOrderElement.Element("PanelId").Value);
                    YellowstonePathology.Business.Panel.Model.Panel panel = panelCollection.GetPanel(panelId);

                    panelOrder = Test.PanelOrderFactory.GetPanelOrder(panel);
                    panelSetOrder.PanelOrderCollection.Add(panelOrder);
                }

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(panelOrderElement, panelOrder);
				xmlPropertyWriter.Write();
                BuildTestOrderRightSide(panelOrder, panelOrderElement);
			}
		}

		private void BuildTestOrderRightSide(Test.PanelOrder panelOrder, XElement panelOrderElement)
		{
			List<XElement> testOrderElements = (from item in panelOrderElement.Elements("TestOrderCollection")
												select item).ToList<XElement>();

            panelOrder.TestOrderCollection.RemoveDeleted(testOrderElements.Elements("TestOrder"));
			foreach (XElement testOrderElement in testOrderElements.Elements("TestOrder"))
			{
                string testOrderId = testOrderElement.Element("TestOrderId").Value;
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = null;
                if (panelOrder.TestOrderCollection.Exists(testOrderId) == true)
                {
                    testOrder = panelOrder.TestOrderCollection.Get(testOrderId);
                }
                else
                {
                    testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
                    panelOrder.TestOrderCollection.Add(testOrder);
                }
				
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(testOrderElement, testOrder);
				xmlPropertyWriter.Write();

                BuildTestOrderAliquotOrder(testOrder, testOrderElement);
                BuildTestOrderSlideOrderCollection(testOrder, testOrderElement);
			}
		}				

		public void SetSurgicalAuditAmendment(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
		{
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
			{
				panelSetOrder.SurgicalAuditCollection.SetAmendmentReference(amendment);
			}
		}

		public void SetSurgicalSpecimenSpecimenOrder(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
		{
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrder.SurgicalSpecimenCollection)
			{
				foreach (Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
				{
					if (specimenOrder.SpecimenOrderId == surgicalSpecimen.SpecimenOrderId)
					{
						surgicalSpecimen.SpecimenOrder = specimenOrder;
						break;
					}
				}				
			}
		}

		public void SetSurgicalSpecimenAuditSpecimenOrder(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
		{
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in panelSetOrder.SurgicalAuditCollection)
			{
				foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit surgicalSpecimenAudit in surgicalAudit.SurgicalSpecimenAuditCollection)
				{
					Specimen.Model.SpecimenOrder specimenOrder = (from so in accessionOrder.SpecimenOrderCollection
																where so.SpecimenOrderId == surgicalSpecimenAudit.SpecimenOrderId
																  select so).Single<Specimen.Model.SpecimenOrder>();
					surgicalSpecimenAudit.SpecimenOrder = specimenOrder;
				}
			}         
		}

		public void SetSurgicalSpecimenOrderItemCollection(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder)
		{

			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrder.SurgicalSpecimenCollection)
			{
				foreach (Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
				{
					if (specimenOrder.SpecimenOrderId == surgicalSpecimen.SpecimenOrderId)
					{
						panelSetOrder.SpecimenOrderCollection.IsLoading = true;
						panelSetOrder.SpecimenOrderCollection.Add(specimenOrder);
						panelSetOrder.SpecimenOrderCollection.IsLoading = false;
					}
				}
			}            
		}

		private void BuildTaskOrder(Test.AccessionOrder accessionOrder, XElement accessionOrderElement)
		{
			List<XElement> taskOrderElements = (from item in accessionOrderElement.Elements("TaskOrderCollection")
														 select item).ToList<XElement>();

            accessionOrder.TaskOrderCollection.RemoveDeleted(taskOrderElements.Elements("TaskOrder"));
			foreach (XElement taskOrderElement in taskOrderElements.Elements("TaskOrder"))
			{
                string taskOrderId = taskOrderElement.Element("TaskOrderId").Value;
                YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = null;

                if (accessionOrder.TaskOrderCollection.Exists(taskOrderId) == true)
                {
                    taskOrder = accessionOrder.TaskOrderCollection.GetTaskOrder(taskOrderId);
                }
                else
                {
                    taskOrder = new YellowstonePathology.Business.Task.Model.TaskOrder();
                    accessionOrder.TaskOrderCollection.Add(taskOrder);
                }

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(taskOrderElement, taskOrder);
				xmlPropertyWriter.Write();
				this.BuildTaskOrderDetail(taskOrder, taskOrderElement);
			}
		}

		private void BuildTaskOrderDetail(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder, XElement taskOrderElement)
		{
			List<XElement> taskOrderDetailElements = (from item in taskOrderElement.Elements("TaskOrderDetailCollection")
												select item).ToList<XElement>();
            taskOrder.TaskOrderDetailCollection.RemoveDeleted(taskOrderDetailElements.Elements("TaskOrderDetail"));
			foreach (XElement taskOrderDetailElement in taskOrderDetailElements.Elements("TaskOrderDetail"))
			{
                string taskOrderDetailId = taskOrderDetailElement.Element("TaskOrderDetailId").Value;
                YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = null;
                if (taskOrder.TaskOrderDetailCollection.Exists(taskOrderDetailId) == true)
                {
                    taskOrderDetail = taskOrder.TaskOrderDetailCollection.Get(taskOrderDetailId);
                }
                else
                {
                    string taskId = taskOrderDetailElement.Element("TaskId").Value;
                    taskOrderDetail = YellowstonePathology.Business.Task.Model.TaskOrderDetailFactory.GetTaskOrderDetail(taskId);
                    taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
                }

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(taskOrderDetailElement, taskOrderDetail);
				xmlPropertyWriter.Write();
			}
		}

		private void BuildIcdBillingCode(Test.AccessionOrder accessionOrder, XElement accessionOrderElement)
		{
			List<XElement> icd9BillingCodeElements = (from item in accessionOrderElement.Elements("ICD9BillingCodeCollection")
													 select item).ToList<XElement>();

            accessionOrder.ICD9BillingCodeCollection.RemoveDeleted(icd9BillingCodeElements.Elements("ICD9BillingCode"));
			foreach (XElement icd9BillingCodeElement in icd9BillingCodeElements.Elements("ICD9BillingCode"))
			{
                string icd9BillingCodeId = icd9BillingCodeElement.Element("Icd9BillingId").Value;
                YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = null;
                if (accessionOrder.ICD9BillingCodeCollection.Exists(icd9BillingCodeId) == true)
                {
                    icd9BillingCode = accessionOrder.ICD9BillingCodeCollection.Get(icd9BillingCodeId);
                }
                else
                {
                    icd9BillingCode = new Billing.Model.ICD9BillingCode();
                    accessionOrder.ICD9BillingCodeCollection.Add(icd9BillingCode);
                }
				
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(icd9BillingCodeElement, icd9BillingCode);
				xmlPropertyWriter.Write();
			}
		}

		private void BuildSurgicalSpecific(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
		{
			YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();
			if (panelSetOrder.PanelSetId == panelSetSurgical.PanelSetId)
			{
				YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)panelSetOrder;
				List<XElement> surgicalSpecimenElements = (from item in panelSetOrderElement.Elements("SurgicalSpecimenCollection")
																	 select item).ToList<XElement>();

                panelSetOrderSurgical.SurgicalSpecimenCollection.RemoveDeleted(surgicalSpecimenElements.Elements("SurgicalSpecimen"));
				foreach (XElement surgicalSpecimenElement in surgicalSpecimenElements.Elements("SurgicalSpecimen"))
				{
                    string surgicalSpecimenId = surgicalSpecimenElement.Element("SurgicalSpecimenId").Value;
                    YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = null;
                    if (panelSetOrderSurgical.SurgicalSpecimenCollection.Exists(surgicalSpecimenId) == true)
                    {
                        surgicalSpecimen = panelSetOrderSurgical.SurgicalSpecimenCollection.Get(surgicalSpecimenId);
                    }
                    else
                    {
                        surgicalSpecimen = new YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen();
                        panelSetOrderSurgical.SurgicalSpecimenCollection.Add(surgicalSpecimen);
                    }

					YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(surgicalSpecimenElement, surgicalSpecimen);
					xmlPropertyWriter.Write();
					BuildICD9Code(surgicalSpecimen, surgicalSpecimenElement);
					BuildIntraoperativeConsultationResult(surgicalSpecimen, surgicalSpecimenElement);
					BuildStainResult(surgicalSpecimen, surgicalSpecimenElement);
                    BuildTypingStainCollection(panelSetOrderSurgical);
				}

				BuildSurgicalAudit(panelSetOrderSurgical, panelSetOrderElement);
				SetSurgicalAuditAmendment(panelSetOrderSurgical);
				SetSurgicalSpecimenSpecimenOrder(accessionOrder, panelSetOrderSurgical);
				SetSurgicalSpecimenAuditSpecimenOrder(accessionOrder, panelSetOrderSurgical);
				SetSurgicalSpecimenOrderItemCollection(accessionOrder, panelSetOrderSurgical);
			}

		}

		private void BuildSurgicalAudit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrder, XElement panelSetOrderElement)
		{
			List<XElement> collectionElements = (from item in panelSetOrderElement.Elements("SurgicalAuditCollection")
												 select item).ToList<XElement>();

            panelSetOrder.SurgicalAuditCollection.RemoveDeleted(collectionElements.Elements("SurgicalAudit"));
			foreach (XElement surgicalAuditElement in collectionElements.Elements("SurgicalAudit"))
			{
                string surgicalAuditId = surgicalAuditElement.Element("SurgicalAuditId").Value;
                YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit = null;

                if(panelSetOrder.SurgicalAuditCollection.Exists(surgicalAuditId) == true)
                {
                    surgicalAudit = panelSetOrder.SurgicalAuditCollection.Get(surgicalAuditId);
                }
                else
                {
                    surgicalAudit = new Test.Surgical.SurgicalAudit();
                    panelSetOrder.SurgicalAuditCollection.Add(surgicalAudit);
                }

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(surgicalAuditElement, surgicalAudit);
				xmlPropertyWriter.Write();
				BuildSurgicalSpecimenAuditResult(surgicalAudit, surgicalAuditElement);
			}
		}

		private void BuildSurgicalSpecimenAuditResult(YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit, XElement surgicalAuditElement)
		{
			List<XElement> collectionElements = (from item in surgicalAuditElement.Elements("SurgicalSpecimenAuditCollection")
												 select item).ToList<XElement>();

            surgicalAudit.SurgicalSpecimenAuditCollection.RemoveDeleted(collectionElements.Elements("SurgicalSpecimenAudit"));
			foreach (XElement surgicalSpecimenResultAuditElement in collectionElements.Elements("SurgicalSpecimenAudit"))
			{
                string surgicalSpecimenAuditId = surgicalSpecimenResultAuditElement.Element("SurgicalSpecimenAuditId").Value;
                YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit surgicalSpecimenAudit = null;
                if (surgicalAudit.SurgicalSpecimenAuditCollection.Exists(surgicalSpecimenAuditId) == true)
                {
                    surgicalSpecimenAudit = surgicalAudit.SurgicalSpecimenAuditCollection.Get(surgicalSpecimenAuditId);
                }
                else
                {
                    surgicalSpecimenAudit = new YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit();
                    surgicalAudit.SurgicalSpecimenAuditCollection.Add(surgicalSpecimenAudit);
                }

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(surgicalSpecimenResultAuditElement, surgicalSpecimenAudit);
				xmlPropertyWriter.Write();				
			}
		}

		private void BuildICD9Code(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, XElement surgicalSpecimenElement)
		{
			List<XElement> collectionElements = (from item in surgicalSpecimenElement.Elements("ICD9BillingCodeCollection")
												 select item).ToList<XElement>();

            surgicalSpecimen.ICD9BillingCodeCollection.RemoveDeleted(collectionElements.Elements("ICD9BillingCode"));
			foreach (XElement icd9BillingElement in collectionElements.Elements("ICD9BillingCode"))
			{
                string icd9BillingId = icd9BillingElement.Element("Icd9BillingId").Value;
                YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9Billing = null;
                if (surgicalSpecimen.ICD9BillingCodeCollection.Exists(icd9BillingId) == true)
                {
                    surgicalSpecimen.ICD9BillingCodeCollection.Get(icd9BillingId);
                }
                else
                {
                    icd9Billing = new YellowstonePathology.Business.Billing.Model.ICD9BillingCode();
                    surgicalSpecimen.ICD9BillingCodeCollection.Add(icd9Billing);
                }

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(icd9BillingElement, icd9Billing);
				xmlPropertyWriter.Write();
			}
		}

		private void BuildIntraoperativeConsultationResult(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, XElement surgicalSpecimenElement)
		{
			List<XElement> collectionElements = (from item in surgicalSpecimenElement.Elements("IntraoperativeConsultationResultCollection")
												 select item).ToList<XElement>();

            surgicalSpecimen.IntraoperativeConsultationResultCollection.RemoveDeleted(collectionElements.Elements("IntraoperativeConsultationResult"));
			foreach (XElement intraoperativeConsultationResultElement in collectionElements.Elements("IntraoperativeConsultationResult"))
			{
                string icResultId = intraoperativeConsultationResultElement.Element("IntraoperativeConsultationResultId").Value;
                YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult = null;
                if (surgicalSpecimen.IntraoperativeConsultationResultCollection.Exists(icResultId) == true)
                {
                    intraoperativeConsultationResult = surgicalSpecimen.IntraoperativeConsultationResultCollection.Get(icResultId);
                }
                else
                {
                    intraoperativeConsultationResult = new YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult();
                    surgicalSpecimen.IntraoperativeConsultationResultCollection.Add(intraoperativeConsultationResult);
                }

				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(intraoperativeConsultationResultElement, intraoperativeConsultationResult);
				xmlPropertyWriter.Write();
			}
		}

		private void BuildStainResult(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, XElement surgicalSpecimenElement)
		{
			List<XElement> collectionElements = (from item in surgicalSpecimenElement.Elements("StainResultItemCollection")
												 select item).ToList<XElement>();

            surgicalSpecimen.StainResultItemCollection.RemoveDeleted(collectionElements.Elements("StainResultItem"));
			foreach (XElement stainResultElement in collectionElements.Elements("StainResultItem"))
			{
                string stainResultId = stainResultElement.Element("StainResultId").Value;
                YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem = null;
                if (surgicalSpecimen.StainResultItemCollection.Exists(stainResultId) == true)
                {
                    stainResultItem = surgicalSpecimen.StainResultItemCollection.Get(stainResultId);
                }
                else
                {
                    stainResultItem = new SpecialStain.StainResultItem();
                    surgicalSpecimen.StainResultItemCollection.Add(stainResultItem);
                }
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(stainResultElement, stainResultItem);
				xmlPropertyWriter.Write();
			}
		}
        
        private void BuildTypingStainCollection(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            List<string> stainResultIdList = new List<string>();
            foreach (Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in surgicalSpecimen.StainResultItemCollection)
                {
                    stainResultIdList.Add(stainResultItem.StainResultId);
                    if(surgicalTestOrder.TypingStainCollection.Exists(stainResultItem.StainResultId) == false)
                    {
                        surgicalTestOrder.TypingStainCollection.Add(stainResultItem);
                    }                    
                }
            }
            surgicalTestOrder.TypingStainCollection.RemoveDeleted(stainResultIdList);
        }		
	}
}

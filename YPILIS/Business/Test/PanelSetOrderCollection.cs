﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;

namespace YellowstonePathology.Business.Test
{
	public class PanelSetOrderCollection : ObservableCollection<PanelSetOrder>
	{
		private PathologistTestOrderItemList m_PathologistTestOrderItemList;        

		public PanelSetOrderCollection()
		{
			m_PathologistTestOrderItemList = new PathologistTestOrderItemList();
		}                

        public string GetAdditionalTestingString(string currentReportNo)
        {
            StringBuilder result = new StringBuilder();
            Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();

            foreach(PanelSetOrder pso in this)
            {
                if(pso.ReportNo != currentReportNo)
                {
                    Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(pso.PanelSetId);
                    if (panelSet.ReportAsAdditionalTesting == true)
                    {
                        result.AppendLine(pso.ReportNo + " " + pso.PanelSetName);
                    }
                }                
            }

            if (result.Length == 0) result.AppendLine("No additional testing has been ordered at this time.");
            return result.ToString();
        }

        public string GetAdditionalTestsDisplayString()
        {
            List<string> testNameList = new List<string>();
            foreach(PanelSetOrder pso in this)
            {
                if(pso.TechnicalComponentFacilityId != "YPIBLGS")
                {
                    testNameList.Add(pso.PanelSetName);
                }
            }

            if (testNameList.Count != 0)
            {
                String displayName = string.Empty;
                foreach (string name in testNameList)
                {
                    displayName = displayName + name + ", ";
                }
                char[] trimChars = new char[] { ',',' ' };
                displayName = displayName.TrimEnd(trimChars);
                if(testNameList.Count >= 3)
                {
                    int lastCommaIndex = displayName.LastIndexOf(",");
                    displayName = displayName.Remove(lastCommaIndex, 1);
                    displayName = displayName.Insert(lastCommaIndex, " and");
                }
                return displayName;    
            }
            else
            {
                return "None";
            }
        }
        
        public bool HasUnfinaledTests(List<int> panelSetIdList)
        {
            bool result = false;
            foreach(int panelSetId in panelSetIdList)
            {
                if(this.Exists(panelSetId) == true)
                {
                    PanelSetOrder panelSetOrder = this.GetItem(panelSetId);
                    if(panelSetOrder.Final == false)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }       

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string reportNo = element.Element("ReportNo").Value;
                    if (this[i].ReportNo == reportNo)
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

        public void RemoveDeleted(IEnumerable<string> reportNoList)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (string reportNo in reportNoList)
                {                    
                    if (this[i].ReportNo == reportNo)
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

        public string FromClientOrder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, 
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,                         
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = null;
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            switch (clientOrder.UniversalServiceId)
            {
                case "TRCHMNAA":
					YellowstonePathology.Business.Specimen.Model.SpecimenOrder trichSpecimen = accessionOrder.SpecimenOrderCollection.GetThinPrep();
					YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == false)
                    {                        
                        YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetTrichomonas, trichSpecimen, true);
                        YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                        accessionOrder.TakeATrip(orderTestOrderVisitor);    
                    }
                    break;
                case "THINPREP":
					YellowstonePathology.Business.Specimen.Model.SpecimenOrder papSpecimen = accessionOrder.SpecimenOrderCollection.GetThinPrep();
					YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrepPap.PanelSetId) == false)
                    {
                        YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetThinPrepPap, papSpecimen, true);                        
                        YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                        accessionOrder.TakeATrip(orderTestOrderVisitor);                        
                    }
                    break;
                case "CTGC":
					YellowstonePathology.Business.Specimen.Model.SpecimenOrder ngctSpecimen = accessionOrder.SpecimenOrderCollection.GetThinPrep();
                    YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == false)
                    {
                        YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetNGCT, ngctSpecimen, true);                        
                        YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                        accessionOrder.TakeATrip(orderTestOrderVisitor);  
                    }
                    break;                
                case "CFYPI":
					YellowstonePathology.Business.Specimen.Model.SpecimenOrder cfSpecimen = accessionOrder.SpecimenOrderCollection[0];
					YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTest panelSetCF = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetCF.PanelSetId) == false)
                    {
                        YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetCF, cfSpecimen, true);                        
                        YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                        accessionOrder.TakeATrip(orderTestOrderVisitor); 
                    }
                    break;
            }
            return result;
        }        

        public void HandleReflexTestingFromClientOrder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            if (string.IsNullOrEmpty(accessionOrder.SpecialInstructions) == false)
            {
                string asccpManagement = "Test->ThinPrep Pap test screen (acceptable for ages 21 and above) with management of abnormal screening results per ASCCP preferred guidelines";
                string asccpManagementWithCotest = "Test->ThinPrep Pap with High Risk HPV screen (Co-test, preferred use in ages 30 and above) with management of abnormal screening results per ASCCP preferred guidelines";

                if(accessionOrder.SpecialInstructions.Contains(asccpManagement) == true)
                {
                    YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = null;
                    if (this.HasWomensHealthProfileOrder() == false)
                    {
                        YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                        string reportNo = accessionOrder.GetNextReportNo(womensHealthProfileTest);
                        string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        womensHealthProfileTestOrder = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder(accessionOrder.MasterAccessionNo, reportNo, objectId, womensHealthProfileTest, accessionOrder.SpecimenOrderCollection[0], false);
                        accessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);
                    }
                    else
                    {
                        womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(116);
                    }
                    womensHealthProfileTestOrder.ManagePerASCCP = true;
                }

                if (accessionOrder.SpecialInstructions.Contains(asccpManagementWithCotest) == true)
                {
                    YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = null;
                    if (this.HasWomensHealthProfileOrder() == false)
                    {
                        YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                        string reportNo = accessionOrder.GetNextReportNo(womensHealthProfileTest);
                        string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        womensHealthProfileTestOrder = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder(accessionOrder.MasterAccessionNo, reportNo, objectId, womensHealthProfileTest, accessionOrder.SpecimenOrderCollection[0], false);
                        accessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);
                    }
                    else
                    {
                        womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(116);
                    }
                    womensHealthProfileTestOrder.ManagePerASCCPWithCotest = true;
                }

                string reflexInstruction11A = "Test->Pap Test with High Risk HPV with reflex to HPV 16/18 Genotyping (only if PAP neg/HPV Pos)";
                string reflexInstruction11B = "Test->HPV Screen with Reflex to 16/18 if HPV positive";
                string reflexInstruction12 = "Test->Pap Test with High Risk HPV with reflex to HPV Genotyping (only if PAP neg/HPV Pos)";
                if (accessionOrder.SpecialInstructions.Contains(reflexInstruction11A) == true ||
                    accessionOrder.SpecialInstructions.Contains(reflexInstruction11B) == true ||
                    accessionOrder.SpecialInstructions.Contains(reflexInstruction12) == true)
                {
                    YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = null;
                    if (this.HasWomensHealthProfileOrder() == false)
                    {
                        YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                        string reportNo = accessionOrder.GetNextReportNo(womensHealthProfileTest);
						string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        womensHealthProfileTestOrder = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder(accessionOrder.MasterAccessionNo, reportNo, objectId, womensHealthProfileTest, accessionOrder.SpecimenOrderCollection[0], false);                        
                        accessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);
                    }
                    else
                    {
                        womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(116);
                    }

                    womensHealthProfileTestOrder.AssignedToId = 5051;

                    YellowstonePathology.Business.Client.Model.HPV1618ReflexOrderPAPNormalHPVPositive hpv1618ReflexOrderPAPNormalHPVPositive = new YellowstonePathology.Business.Client.Model.HPV1618ReflexOrderPAPNormalHPVPositive();
                    womensHealthProfileTestOrder.HPV1618ReflexOrderCode = hpv1618ReflexOrderPAPNormalHPVPositive.ReflexOrderCode;
                    womensHealthProfileTestOrder.OrderHPV = true;                    

					YellowstonePathology.Business.Specimen.Model.SpecimenOrder hpvSpecimen = accessionOrder.SpecimenOrderCollection.GetThinPrep();
					YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();

                    if (Exists(panelSetHPV.PanelSetId) == false)
                    {
                        YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetHPV, hpvSpecimen, true);
                        YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                        accessionOrder.TakeATrip(orderTestOrderVisitor);
                    }

                    if (string.IsNullOrEmpty(accessionOrder.SpecialInstructions) == false)
                    {
                        if (accessionOrder.SpecialInstructions.Contains(reflexInstruction11A) == true || 
                            accessionOrder.SpecialInstructions.Contains(reflexInstruction11B) == true)
                        {
                            womensHealthProfileTestOrder.HPV1618ReflexOrderCode = "RFLXHPV1618HPVPOS";
                        }
                    }
                }                

                string reflexInstruction21 = "Test->Pap Test with High Risk HPV DNA reflex testing if diagnosis is ASCUS";
                string reflexInstruction22 = "Test->Pap Test with High Risk HPV  reflex testing if diagnosis is ASCUS";                
                if (accessionOrder.SpecialInstructions.Contains(reflexInstruction21) == true || accessionOrder.SpecialInstructions.Contains(reflexInstruction22) == true)
                {
                    YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = null;
                    if (this.HasWomensHealthProfileOrder() == false)
                    {
                        YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                        string reportNo = accessionOrder.GetNextReportNo(womensHealthProfileTest);
						string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        womensHealthProfileTestOrder = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder(accessionOrder.MasterAccessionNo, reportNo, objectId, womensHealthProfileTest, accessionOrder.SpecimenOrderCollection[0], false);                        
                        accessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);
                    }
                    else
                    {
                        womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(116);
                    }

                    womensHealthProfileTestOrder.AssignedToId = 5051;
                    YellowstonePathology.Business.Client.Model.HPVReflexOrderRule18 hpvReflexOrderRule18 = new YellowstonePathology.Business.Client.Model.HPVReflexOrderRule18();
                    womensHealthProfileTestOrder.HPVReflexOrderCode = hpvReflexOrderRule18.ReflexOrderCode;                    
                }

                string reflexInstruction31 = "Test->Pap Test with High Risk HPV DNA testing regardless of diagnosis";
                string reflexInstruction32 = "Test->Pap Test with High Risk HPV testing regardless of diagnosis";
                if (accessionOrder.SpecialInstructions.Contains(reflexInstruction31) == true || 
                    accessionOrder.SpecialInstructions.Contains(reflexInstruction32) == true)
                {
                    YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = null;
                    if (this.HasWomensHealthProfileOrder() == false)
                    {
                        YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                        string whpReportNo = accessionOrder.GetNextReportNo(womensHealthProfileTest);
                        string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        womensHealthProfileTestOrder = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder(accessionOrder.MasterAccessionNo, whpReportNo, objectId, womensHealthProfileTest, accessionOrder.SpecimenOrderCollection[0], false);                        
                        accessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);
                    }
                    else
                    {
                        womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(116);
                    }

                    womensHealthProfileTestOrder.OrderHPV = true;
                    womensHealthProfileTestOrder.AssignedToId = 5051;

                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder hpvSpecimen = accessionOrder.SpecimenOrderCollection.GetThinPrep();
					YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();

                    if (Exists(panelSetHPV.PanelSetId) == false)
                    {
                        YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetHPV, hpvSpecimen, true);
                        YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                        accessionOrder.TakeATrip(orderTestOrderVisitor);
                    }
                }
            }
        }
        
        public YellowstonePathology.Business.Test.Model.TestOrderCollection GetTestOrderCollection(string reportNo, YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection)        
        {
            YellowstonePathology.Business.Test.Model.TestOrderCollection result = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.GetPanelSetOrder(reportNo);

            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
            {
                foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                {                    
                    if (aliquotOrderCollection.Exists(testOrder.AliquotOrderId) == true)
                    {
                        result.Add(testOrder);
                    }                    
                }
            }
            return result;
        }

        public void LoadPanelOrderInCorrectPanelSetOrder(YellowstonePathology.Business.Test.PanelOrder panelOrder)
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.ReportNo == panelOrder.ReportNo)
                {
                    panelSetOrder.PanelOrderCollection.Add(panelOrder);
                }
            }
        }      		        

        public bool IsSomeoneAssigned()
        {
            bool result = false;
            foreach (PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.AssignedToId != 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public int GetFirstAssignedToId()
        {
            int result = 0;
            foreach (PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.AssignedToId != 0)
                {
                    result = panelSetOrder.AssignedToId;
                    break;
                }
            }
            return result;
        }

        public bool HasSurgical()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == 13)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool HasPap()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == 15)
                {
                    result = true;
                }
            }
            return result;
        }

        /*public bool HasOpenAmendment
        {
            get
            {
                bool hasOpenAmendment = false;
                foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
                {
                    if (panelSetOrder.AmendmentCollection.HasOpenAmendment() == true)
                    {
                        hasOpenAmendment = true;
                        break;
                    }
                }
                return hasOpenAmendment;
            }
        }*/

        public bool HasPathologistReviewFor(int pathologistId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == 197)
                {
                    if (panelSetOrder.AssignedToId == pathologistId)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool HasUnfinaledProspectiveReview()
        {
            bool result = false;            
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == 197)
                {
                    if (panelSetOrder.Final == false)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

		public bool HasWomensHealthProfileOrder()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == 116)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasThinPrepPapOrder()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == 15) result = true; //PAP
                if (panelSetOrder.PanelSetId == 14) result = true; //High risk
                if (panelSetOrder.PanelSetId == 62) result = true; //16 18
                if (panelSetOrder.PanelSetId == 3) result = true; //NG/CT
                if (panelSetOrder.PanelSetId == 61) result = true; //Trich
            }
            return result;
        }

        public bool HasGrossBeenOrdered()
        {
            return this.HasTestBeenOrdered("48");
        }

        public bool HasUnassignedPanelSetOrder(int panelSetId)
        {
            bool result = false;
            foreach (PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == panelSetId)
                {
                    if (panelSetOrder.AssignedToId == 0)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool HasTestBeenOrdered(string testId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
					foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        if (testOrder.TestId == testId)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelOrder GetPanelOrderByTestOrderId(string testOrderId)
        {
            YellowstonePathology.Business.Test.PanelOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        if (testOrder.TestOrderId == testOrderId)
                        {
                            result = panelOrder;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetThinPrepPap()
        {
            YellowstonePathology.Business.Test.PanelSetOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == 15)
                {
                    result = panelSetOrder;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetPanelSetOrderByOrderedOnId(string orderedOnId)
        {
            YellowstonePathology.Business.Test.PanelSetOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.OrderedOnId == orderedOnId)
                {
                    result = panelSetOrder;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetPanelSetOrderByTestId(string testId)
        {
            YellowstonePathology.Business.Test.PanelSetOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        if (testOrder.TestId == testId)
                        {
                            result = panelSetOrder;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetFirstByPanelSetId(int panelSetId)
        {
            YellowstonePathology.Business.Test.PanelSetOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if(panelSetOrder.PanelSetId == panelSetId)
                {
                    result = panelSetOrder;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetPanelSetOrderByTestOrderId(string testOrderId)
        {
            YellowstonePathology.Business.Test.PanelSetOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        if (testOrder.TestOrderId == testOrderId)
                        {
                            result = panelSetOrder;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.Model.TestOrder GetTestOrderByTestId(string testId)
        {
            YellowstonePathology.Business.Test.Model.TestOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        if (testOrder.TestId == testId)
                        {
                            result = testOrder;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.Model.TestOrder GetTestOrderByTestOrderId(string testOrderId)
        {
            YellowstonePathology.Business.Test.Model.TestOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        if (testOrder.TestOrderId == testOrderId)
                        {
                            result = testOrder;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public bool HasPanelSetBeenOrdered(int panelSetId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {                
                if (panelSetOrder.PanelSetId == panelSetId)
                {
                    result = true;
                    break;
                }                
            }
            return result;
        }

        public bool HasPanelSetBeenOrdered(List<int> panelSetIdList)
        {
            bool result = false;
            foreach(int id in panelSetIdList)
            {
                if(HasPanelSetBeenOrdered(id) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetItem(int panelSetId)
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = null;
            if (this.Count != 0)
            {
                foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
                {
                    if (item.PanelSetId == panelSetId)
                    {
                        panelSetOrder = item;
                    }
                }
            }
            return panelSetOrder;
        }

		public PathologistTestOrderItemList PathologistTestOrderItemList
		{
			get { return this.m_PathologistTestOrderItemList; }
            set { this.m_PathologistTestOrderItemList = value; }
		}

        public YellowstonePathology.Business.Test.PanelOrder GetPanelOrder(string panelOrderId)
        {
            YellowstonePathology.Business.Test.PanelOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    if (panelOrder.PanelOrderId == panelOrderId)
                    {
                        result = panelOrder;
                        break;
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelOrder GetPanelOrderFromTestId(string testId)
        {
            YellowstonePathology.Business.Test.PanelOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach(Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        if(testOrder.TestId == testId)
                        {
                            result = panelOrder;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetPanelSetOrder(string reportNo)
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
				if (item.ReportNo.ToUpper() == reportNo.ToUpper())
                {
                    return item;
                }
            }
            return null;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetPanelSetOrder(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {
            YellowstonePathology.Business.Test.PanelSetOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrders)
                {
                    foreach (YellowstonePathology.Business.Test.Model.TestOrder to in panelOrder.TestOrderCollection)
                    {
                        if (to.TestOrderId == testOrder.TestOrderId)
                        {
                            result = panelSetOrder;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetPanelSetOrder(int panelSetId)
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == panelSetId)
                {
                    return item;
                }
            }
            return null;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetFirstCOVIDTest()
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == 400 || item.PanelSetId == 415)
                {
                    return item;
                }
            }
            return null;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetPanelSetOrder(int panelSetId, string orderedOnId, bool restrictToOrderedOn)
        {
            PanelSetOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == panelSetId && panelSetOrder.OrderedOnId == orderedOnId)
                {
                    result = panelSetOrder;
                    break;
                }
            }

            if (result == null && restrictToOrderedOn == false)
            {
                foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
                {
                    if (panelSetOrder.PanelSetId == panelSetId)
                    {
                        result = panelSetOrder;
                        break;
                    }
                }
            }
            return result;
        }

        public bool DoesPanelSetExist(int panelSetId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == panelSetId)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool DoesStainOrderExist(string testId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder pso in this)
            {
                foreach(Business.Test.PanelOrder panelOrder in pso.PanelOrderCollection)
                {
                    foreach(Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        if(testOrder.TestId == testId)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public ObservableCollection<YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder> GetProspectiveReviewCollection()
        {
            ObservableCollection<YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder> result = new ObservableCollection<YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder>();
            YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTest peerReviewTest = new YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTest();
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == peerReviewTest.PanelSetId)
                {
                    result.Add(panelSetOrder as YellowstonePathology.Business.Test.ProspectiveReview.ProspectiveReviewTestOrder);
                }
            }
            return result;
        }
                
        public YellowstonePathology.Business.Test.PanelSetOrder GetBrafPanelSetOrder()
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = null;

            YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            YellowstonePathology.Business.PanelSet.Model.PanelSetArupBraf panelSetArupBraf = new YellowstonePathology.Business.PanelSet.Model.PanelSetArupBraf();

            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == krasStandardReflexTest.PanelSetId || item.PanelSetId == brafV600EKTest.PanelSetId || item.PanelSetId == panelSetArupBraf.PanelSetId)
                {
                    panelSetOrder = item;
                }
            }
            return panelSetOrder;
        }

		public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder GetSurgical()
        {
			YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == 13)
                {
					result = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)item;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder GetWomensHealthProfile()
        {
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder result = null;
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == womensHealthProfileTest.PanelSetId)
                {
                    result = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)item;
                    break;
                }
            }
            return result;
        }

        public bool WomensHealthProfileExists()
        {
            bool result = false;
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == womensHealthProfileTest.PanelSetId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }        

        public YellowstonePathology.Business.Test.PanelSetOrder GetPAP()
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == 15)
                {
                    return item;
                }
            }
            return null;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder GetFirstNonSurgical()
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId != 13 || item.PanelSetId == 128)
                {
                    return item;
                }
            }
            return null;
        }		

		public bool Exists(int panelSetId)
		{
			bool result = false;
				foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
				{
					if (item.PanelSetId == panelSetId)
					{
						result = true;
						break;
					}
				}
			return result;
		}

        public bool Exists(string reportNo)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.ReportNo == reportNo)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(int panelSetId, string orderedOnId, bool restrictToOrderedOn)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
            {
                if (item.PanelSetId == panelSetId && item.OrderedOnId == orderedOnId)
                {
                    result = true;
                    break;
                }
            }

            if (result == false && restrictToOrderedOn == false)
            {
                foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
                {
                    if (item.PanelSetId == panelSetId)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }
        
		public bool UnAcknowledgedPanelExists()
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Test.PanelSetOrder item in this)
			{
				foreach(PanelOrder panelOrder in item.PanelOrderCollection)
				{
					if (panelOrder.Acknowledged == false)
					{
						result = true;
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

        public List<string> GetUnAcknowledgedPanelOrderIdList()
        {
            List<string> resultList = new List<string>();
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    if (panelOrder.Acknowledged == false)
                    {
                        resultList.Add(panelOrder.PanelOrderId);
                    }
                }
            }
            return resultList;
        }

        public ObservableCollection<YellowstonePathology.Business.Test.PanelOrder> GetUnAcknowledgedPanelOrders()
        {
            ObservableCollection<YellowstonePathology.Business.Test.PanelOrder> resultList = new ObservableCollection<YellowstonePathology.Business.Test.PanelOrder>();
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    if (panelOrder.Acknowledged == false)
                    {
                        resultList.Add(panelOrder);
                    }
                }
            }
            return resultList;
        }

        public bool HasReflexTestingPlan()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder is YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan GetReflexTestingPlan()
        {
            YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan result = null;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder is YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan == true)
                {
                    result = panelSetOrder as YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan;
                    break;
                }
            }
            return result;
        }

        public void UpdateTumorNucleiPercentage(YellowstonePathology.Business.Interface.ISolidTumorTesting solidTumorTestingToUpdateFrom)
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrderToUpdateFrom = (YellowstonePathology.Business.Test.PanelSetOrder)solidTumorTestingToUpdateFrom;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder is YellowstonePathology.Business.Interface.ISolidTumorTesting)
                {
                    
                    if (panelSetOrder.ReportNo != panelSetOrderToUpdateFrom.ReportNo)
                    {
                        YellowstonePathology.Business.Interface.ISolidTumorTesting solidTumorTesting = (YellowstonePathology.Business.Interface.ISolidTumorTesting)panelSetOrder;
                        solidTumorTesting.TumorNucleiPercentage = solidTumorTestingToUpdateFrom.TumorNucleiPercentage;
                    }
                }
            }
        }

        public virtual string GetLocationPerformedSummary(List<int> panelSetIDList)
        {
            string result = null;
            for(int i=0; i<this.Count; i++)
            {
                if(panelSetIDList.Contains(this[i].PanelSetId) == true)
                {
                    result += this[i].PanelSetName + ": " + this[i].GetLocationPerformedComment();
                    if (i != this.Count - 1) result += " ";
                }                
            }
            return result;
        }

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }

        public void Sync(DataTable dataTable)
        {                        
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string reportNo = dataTableReader["ReportNo"].ToString();

                PanelSetOrder panelSetOrder = null;

                if (this.Exists(reportNo) == true)
                {
                    panelSetOrder = this.GetPanelSetOrder(reportNo);
                }
                else
                {
                    int panelSetId = (int)dataTableReader["PanelSetId"];
                    PanelSet.Model.PanelSet panelSet = PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetId);
                    panelSetOrder = Test.PanelSetOrderFactory.CreatePanelSetOrder(panelSet);
                    this.Add(panelSetOrder);
                }

                YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(panelSetOrder, dataTableReader);
                sqlDataTableReaderPropertyWriter.WriteProperties();
            }
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string reportNo = dataTable.Rows[idx]["ReportNo"].ToString();
                    if (this[i].ReportNo == reportNo)
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

        public static PanelSetOrderCollection Sort(PanelSetOrderCollection panelSetOrderCollection)
        {
            PanelSetOrderCollection result = new PanelSetOrderCollection();
            IOrderedEnumerable<PanelSetOrder> orderedResult = panelSetOrderCollection.OrderBy(i => i.ReportNo);
            foreach (PanelSetOrder panelSetOrder in orderedResult)
            {
                result.Add(panelSetOrder);
            }
            return result;
        }

        public List<Business.Test.PanelSetOrder> GetBoneMarrowAccessionSummaryList(string summaryReportNo, bool includeOtherReports)
        {
            List<Business.Test.PanelSetOrder> result = new List<PanelSetOrder>();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSets = Business.PanelSet.Model.PanelSetCollection.GetAll();

            Business.Test.PanelSetOrderCollection flow = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection cyto = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection fish = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection molecular = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection other = new PanelSetOrderCollection();

            List<int> exclusionList = this.GetBoneMarrowSummaryExclusionList();

            foreach (Business.Test.PanelSetOrder pso in this)
            {
                if (exclusionList.IndexOf(pso.PanelSetId) == -1)
                {
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSets.GetPanelSet(pso.PanelSetId);
                    if (panelSet.CaseType == Business.CaseType.FlowCytometry) flow.Insert(0, pso);
                    else if (panelSet.CaseType == Business.CaseType.Cytogenetics) cyto.Insert(0, pso);
                    else if (panelSet.CaseType == Business.CaseType.FISH) fish.Insert(0, pso);
                    else if (panelSet.CaseType == Business.CaseType.Molecular) molecular.Insert(0, pso);
                    else other.Insert(0, pso);
                }
            }

            if (includeOtherReports == true)
            {
                BoneMarrowSummary.OtherReportViewCollection otherReports = Gateway.AccessionOrderGateway.GetOtherReportViewsForSummary(summaryReportNo);
                foreach (BoneMarrowSummary.OtherReportView otherReportView in otherReports)
                {
                    AccessionOrder ao = Persistence.DocumentGateway.Instance.PullAccessionOrder(otherReportView.MasterAccessionNo, this);
                    Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(otherReportView.ReportNo);
                    other.Insert(0, pso);
                }
            }

            result.AddRange(other);
            result.AddRange(molecular);
            result.AddRange(fish);
            result.AddRange(cyto);
            result.AddRange(flow);

            return result;
        }

        public List<int> GetBoneMarrowSummaryExclusionList()
        {
            List<int> result = new List<int>();            
            result.Add(31);   // Technical Only
            result.Add(66);   // Test Cancelled
            result.Add(197);
            result.Add(244);  // Ship Material
            result.Add(262);
            result.Add(268);  // Bone Marrow Summary
            result.Add(211);
            result.Add(189);
            result.Add(190);
            result.Add(212);

            result.Add(136);  // MPN Standard Reflex            
            result.Add(300);  //Extract and Hold for Preauthorization    
            result.Add(359);
            result.Add(360);
            result.Add(361);
            result.Add(363); //Authorization For Verbal Test Request

            return result;
        }

        public bool DoesSpecimenHavCPTCodes(string specimenOrderId)
        {
            bool result = false;
            foreach(PanelSetOrder panelSetOrder in this)
            {
                foreach(PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetOrder.PanelSetOrderCPTCodeCollection)
                {
                    if(panelSetOrderCPTCode.SpecimenOrderId == specimenOrderId)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool IsLastReportInSummaryToFinal(List<int> exclusionList, int panelSetId)
        {
            bool result = true;
            if (exclusionList.IndexOf(panelSetId) == -1)
            {
                foreach (Test.PanelSetOrder panelSetOrder in this)
                {
                    if (exclusionList.IndexOf(panelSetOrder.PanelSetId) == -1)
                    {
                        if (panelSetOrder.Final == false)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void UpdateWHPExpectedFinalTimeOnOrder(PanelSetOrder panelSetOrder)
        {
            if(this.HasWomensHealthProfileOrder() == true)
            {
                WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = this.GetWomensHealthProfile();
                if (womensHealthProfileTestOrder.Final == false)
                {
                    DateTime expectedFinalTime = DateTime.Now;
                    if (womensHealthProfileTestOrder.ExpectedFinalTime.HasValue) expectedFinalTime = womensHealthProfileTestOrder.ExpectedFinalTime.Value;

                    YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest thinPrepPapTest = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
                    YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
                    YellowstonePathology.Business.Test.HPV1618.HPV1618Test hpv1618Test = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
                    YellowstonePathology.Business.Test.NGCT.NGCTTest ngctTest = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
                    YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest trichomonasTest = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

                    if (panelSetOrder.PanelSetId == thinPrepPapTest.PanelSetId ||
                        panelSetOrder.PanelSetId == panelSetHPV.PanelSetId ||
                        panelSetOrder.PanelSetId == hpv1618Test.PanelSetId ||
                        panelSetOrder.PanelSetId == ngctTest.PanelSetId ||
                        panelSetOrder.PanelSetId == trichomonasTest.PanelSetId)
                    {
                        DateTime? orderExpectedFinalTime = panelSetOrder.ExpectedFinalTime;
                        if (orderExpectedFinalTime.HasValue && orderExpectedFinalTime.Value > expectedFinalTime)
                        {
                            womensHealthProfileTestOrder.ExpectedFinalTime = orderExpectedFinalTime;
                        }
                    }
                }
            }
        }

        public YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection GetUniqueDistributions()
        {
            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection result = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection();
            throw new Exception("This needs help");
            /*
            foreach (PanelSetOrder panelSetOrder in this)
            {
                foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in panelSetOrder.TestOrderReportDistributionCollection)
                {
                    if (result.IsDuplicate(testOrderReportDistribution) == false)
                    {
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
            */
        }
    }
}

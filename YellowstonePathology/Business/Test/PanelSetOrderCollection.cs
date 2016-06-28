using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test
{
	public class PanelSetOrderCollection : ObservableCollection<PanelSetOrder>
	{
		private PathologistTestOrderItemList m_PathologistTestOrderItemList;
        private PanelSetOrder m_CurrentPanelSetOrder;

		public PanelSetOrderCollection()
		{
			m_PathologistTestOrderItemList = new PathologistTestOrderItemList();
		}

        public PanelSetOrder CurrentPanelSetOrder
        {
            get { return this.m_CurrentPanelSetOrder; }
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
                string reflexInstruction1 = "Test->Pap Test with High Risk HPV with reflex to HPV 16/18 Genotyping (only if PAP neg/HPV Pos)";                
                if (accessionOrder.SpecialInstructions.Contains(reflexInstruction1) == true)
                {
                    if (this.HasWomensHealthProfileOrder() == false)
                    {
                        YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                        string reportNo = accessionOrder.GetNextReportNo(womensHealthProfileTest);
						string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
						YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder(accessionOrder.MasterAccessionNo, reportNo, objectId, womensHealthProfileTest, accessionOrder.SpecimenOrderCollection[0], false);
                        womensHealthProfileTestOrder.AssignedToId = 5051;

                        YellowstonePathology.Business.Client.Model.HPV1618ReflexOrderPAPNormalHPVPositive hpv1618ReflexOrderPAPNormalHPVPositive = new YellowstonePathology.Business.Client.Model.HPV1618ReflexOrderPAPNormalHPVPositive();
                        womensHealthProfileTestOrder.HPV1618ReflexOrderCode = hpv1618ReflexOrderPAPNormalHPVPositive.ReflexOrderCode;
                        womensHealthProfileTestOrder.OrderHPV = true;
                        accessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);

						YellowstonePathology.Business.Specimen.Model.SpecimenOrder hpvSpecimen = accessionOrder.SpecimenOrderCollection.GetThinPrep();
						YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
                        YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(panelSetHPV, hpvSpecimen, true);
                        YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                        accessionOrder.TakeATrip(orderTestOrderVisitor); 
                    }
                }

                string reflexInstruction2 = "Test->Pap Test with High Risk HPV DNA reflex testing if diagnosis is ASCUS";
                if (accessionOrder.SpecialInstructions.Contains(reflexInstruction2) == true)
                {
                    if (this.HasWomensHealthProfileOrder() == false)
                    {
                        YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                        string reportNo = accessionOrder.GetNextReportNo(womensHealthProfileTest);
						string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
						YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder(accessionOrder.MasterAccessionNo, reportNo, objectId, womensHealthProfileTest, accessionOrder.SpecimenOrderCollection[0], false);
                        womensHealthProfileTestOrder.AssignedToId = 5051;

                        YellowstonePathology.Business.Client.Model.HPVReflexOrderRule2 hpvReflexOrderRule2 = new YellowstonePathology.Business.Client.Model.HPVReflexOrderRule2();
                        womensHealthProfileTestOrder.HPVReflexOrderCode = hpvReflexOrderRule2.ReflexOrderCode;
                        accessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);
                    }                    
                }

                string reflexInstruction3 = "Test->Pap Test with High Risk HPV DNA testing regardless of diagnosis";
                if (accessionOrder.SpecialInstructions.Contains(reflexInstruction3) == true)
                {
                    if (this.HasWomensHealthProfileOrder() == false)
                    {
                        YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                        string whpReportNo = accessionOrder.GetNextReportNo(womensHealthProfileTest);
						string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
						YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder(accessionOrder.MasterAccessionNo, whpReportNo, objectId, womensHealthProfileTest, accessionOrder.SpecimenOrderCollection[0], false);
                        womensHealthProfileTestOrder.AssignedToId = 5051;
                        womensHealthProfileTestOrder.OrderHPV = true;
                        accessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);

						YellowstonePathology.Business.Specimen.Model.SpecimenOrder hpvSpecimen = accessionOrder.SpecimenOrderCollection.GetThinPrep();
						YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
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

        public bool HasOpenAmendment
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
        }

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

        public bool HasUnfinaledPeerReview()
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
            return this.HasTestBeenOrdered(48);
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

        public bool HasTestBeenOrdered(int testId)
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

        public YellowstonePathology.Business.Test.PanelSetOrder GetPanelSetOrderByTestId(int testId)
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

        public YellowstonePathology.Business.Test.Model.TestOrder GetTestOrderByTestId(int testId)
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

        public ObservableCollection<YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder> GetPeerReviewCollection()
        {
            ObservableCollection<YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder> result = new ObservableCollection<YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder>();
            YellowstonePathology.Business.Test.PeerReview.PeerReviewTest peerReviewTest = new YellowstonePathology.Business.Test.PeerReview.PeerReviewTest();
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this)
            {
                if (panelSetOrder.PanelSetId == peerReviewTest.PanelSetId)
                {
                    result.Add(panelSetOrder as YellowstonePathology.Business.Test.PeerReview.PeerReviewTestOrder);
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
	}
}

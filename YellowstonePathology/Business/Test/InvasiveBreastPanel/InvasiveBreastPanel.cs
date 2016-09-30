using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.InvasiveBreastPanel
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class InvasiveBreastPanel : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan
	{
		public InvasiveBreastPanel() 
        {
            
        }

		public InvasiveBreastPanel(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            
		}

        public override void OrderInitialTests(AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(her2AmplificationByISHTest, orderTarget, true);            
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
            accessionOrder.TakeATrip(orderTestOrderVisitor);            

            bool hasSurgical = accessionOrder.PanelSetOrderCollection.Exists(13);
            if (hasSurgical == true)
            {
                string surgicalReportNo = accessionOrder.PanelSetOrderCollection.GetSurgical().ReportNo;
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(surgicalReportNo);

                if (panelSetOrder.AssignedToId != 5132)
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)orderTarget;

                    YellowstonePathology.Business.Test.Model.EstrogenReceptorSemiquant er = new YellowstonePathology.Business.Test.Model.EstrogenReceptorSemiquant();
                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderERTestVisitor = new Visitor.OrderTestVisitor(surgicalReportNo, er, er.OrderComment, null, false, aliquotOrder, false, false, accessionOrder.TaskOrderCollection);
                    accessionOrder.TakeATrip(orderERTestVisitor);

                    YellowstonePathology.Business.Test.Model.ProgesteroneReceptorSemiquant pr = new YellowstonePathology.Business.Test.Model.ProgesteroneReceptorSemiquant();
                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderPRTestVisitor = new Visitor.OrderTestVisitor(surgicalReportNo, pr, pr.OrderComment, null, false, aliquotOrder, false, false, accessionOrder.TaskOrderCollection);
                    accessionOrder.TakeATrip(orderPRTestVisitor);
                }
                else
                {
                    //If Dr Shannon order separte ERPR
                    YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest erPrSemiQuantitativeTest = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest();
                    YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoERPR = new TestOrderInfo(erPrSemiQuantitativeTest, orderTarget, true);
                    YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorERPR = new Visitor.OrderTestOrderVisitor(testOrderInfoERPR);
                    accessionOrder.TakeATrip(orderTestOrderVisitorERPR);
                }
            }
            else
            {
				YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest erPrSemiQuantitativeTest = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest();
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfoERPR = new TestOrderInfo(erPrSemiQuantitativeTest, orderTarget, true);                
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitorERPR = new Visitor.OrderTestOrderVisitor(testOrderInfoERPR);
                accessionOrder.TakeATrip(orderTestOrderVisitorERPR);     
            }
        }

        public override void SetStatus(PanelSetOrderCollection panelSetOrderCollection)
        {
            this.m_ReflexTestingPlanStepCollection.Clear();

            InvasiveBreastPanelStep1 step1 = new InvasiveBreastPanelStep1();
            step1.Walk(panelSetOrderCollection, this);

            InvasiveBreastPanelStep2 step2 = new InvasiveBreastPanelStep2();
            step2.Walk(panelSetOrderCollection, this);

            InvasiveBreastPanelStep3 step3 = new InvasiveBreastPanelStep3();
            step3.Walk(panelSetOrderCollection, this);
        }

		public override string ToResultString(AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("Her2 By Ish");
			YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(her2AmplificationByISHTest.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder panelSetOrderHer2ByIsh = (YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(her2AmplificationByISHTest.PanelSetId);
				result.AppendLine(panelSetOrderHer2ByIsh.ToResultString(accessionOrder));
			}
			result.AppendLine();

			YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();
			if(accessionOrder.PanelSetOrderCollection.Exists(panelSetSurgical.PanelSetId) == true)
			{
				result.Append("Estrogen/Progesterone Receptor, Semi-Quantitative - Estrogen Receptor : ");
				YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetSurgical.PanelSetId);
                YellowstonePathology.Business.Test.Model.EstrogenReceptorSemiquant er = new YellowstonePathology.Business.Test.Model.EstrogenReceptorSemiquant();
                YellowstonePathology.Business.Test.Model.ProgesteroneReceptorSemiquant pr = new YellowstonePathology.Business.Test.Model.ProgesteroneReceptorSemiquant();
				YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = panelSetOrderSurgical.GetTestOrders();
				if(testOrders.Exists(er.TestId) == true)
				{
					YellowstonePathology.Business.Test.Model.TestOrder testOrder = testOrders.GetTestOrder(er.TestId);
					YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem = panelSetOrderSurgical.GetStainResult(testOrder.TestOrderId);
					result.AppendLine(stainResultItem.Result);
				}
				if(testOrders.Exists(pr.TestId) == true)
				{
					YellowstonePathology.Business.Test.Model.TestOrder testOrder = testOrders.GetTestOrder(pr.TestId);
					YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem = panelSetOrderSurgical.GetStainResult(testOrder.TestOrderId);
					result.AppendLine("Progesterone Receptor : " + stainResultItem.Result);
				}
			}
			else
			{
				YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest erPrSemiQuantitativeTest = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest();
				if (accessionOrder.PanelSetOrderCollection.Exists(erPrSemiQuantitativeTest.PanelSetId) == true)
				{
					YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder panelSetOrderErPrSemiQuantitative = (YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(erPrSemiQuantitativeTest.PanelSetId);
					result.AppendLine(panelSetOrderErPrSemiQuantitative.ToResultString(accessionOrder));
				}
			}
			result.AppendLine();
			return result.ToString();
		}
	}
}

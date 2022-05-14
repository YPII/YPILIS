using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.GastricAdenocarcinoma
{
    public class GastricAdenocarcinomaTestOrder : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan
    {

        public GastricAdenocarcinomaTestOrder()
        {

        }

        public GastricAdenocarcinomaTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{ }

        public override void OrderInitialTests(AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(her2AmplificationByISHTest.PanelSetId, this.m_OrderedOnId, true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(her2AmplificationByISHTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
            }

            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new LynchSyndrome.LynchSyndromeEvaluationTest();
            if(accessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId, this.m_OrderedOnId, true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(lynchSyndromeEvaluationTest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
            }
           
            YellowstonePathology.Business.Test.PDL122C3forGastricGEA.PDL122C3forGastricGEATest pdl122C3forGastricGEATest = new PDL122C3forGastricGEA.PDL122C3forGastricGEATest();
            if (accessionOrder.PanelSetOrderCollection.Exists(pdl122C3forGastricGEATest.PanelSetId, this.m_OrderedOnId, true) == false)
            {
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(pdl122C3forGastricGEATest, orderTarget, false);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);
            }           
        }
    }
}

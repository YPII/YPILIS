using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.NeoTYPELungTumorProfile
{
    [PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
    public class NeoTYPELungTumorProfileTestOrder : ReflexTesting.ReflexTestingPlan
    {
        public NeoTYPELungTumorProfileTestOrder() { }

        public NeoTYPELungTumorProfileTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

        public override void OrderInitialTests(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysisPreliminary.EGFRMutationAnalysisPreliminaryTest egfrMutationAnalysisPreliminaryTest = new EGFRMutationAnalysisPreliminary.EGFRMutationAnalysisPreliminaryTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(egfrMutationAnalysisPreliminaryTest, orderTarget, true);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
            accessionOrder.TakeATrip(orderTestOrderVisitor);
        }
    }
}

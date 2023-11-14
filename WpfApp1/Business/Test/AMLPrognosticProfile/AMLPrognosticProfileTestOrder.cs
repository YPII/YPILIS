using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.AMLPrognosticProfile
{
    [PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
    public class AMLPrognosticProfileTestOrder : ReflexTesting.ReflexTestingPlan
    {
        public AMLPrognosticProfileTestOrder() { }

        public AMLPrognosticProfileTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,

            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

        public override void OrderInitialTests(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Test.FLT3Preliminary.FLT3PreliminaryTest flt3PreliminaryTest = new FLT3Preliminary.FLT3PreliminaryTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new TestOrderInfo(flt3PreliminaryTest, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Visitor.OrderTestOrderVisitor(testOrderInfo);
            accessionOrder.TakeATrip(orderTestOrderVisitor);
        }
    }
}

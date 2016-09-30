using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    public class TestOrderRemover
    {
        public TestOrderRemover()
        { }

        public TestOrderRemover(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            YellowstonePathology.Business.Test.PanelSetOrder testOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            accessionOrder.PanelSetOrderCollection.Remove(testOrder);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }
    }
}

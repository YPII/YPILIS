using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReflexTesting
{
    public class ReflexTestingPlanStepTest : ReflexTestingPlanStep
    {        
        protected YellowstonePathology.Business.Test.Model.Test m_Test;

        public ReflexTestingPlanStepTest(string stepId, string stepDescription, YellowstonePathology.Business.Test.Model.Test test) 
            : base(stepId, stepDescription)
        {
            this.m_Test = test;
        }        

        public YellowstonePathology.Business.Test.Model.Test Test
        {
            get { return this.m_Test; }
        }
        
        public override void SetStatus(PanelSetOrderCollection panelSetOrderCollection)
        {
            if (panelSetOrderCollection.HasTestBeenOrdered(this.m_Test.TestId) == false)
            {
                this.m_Ordered = false;
                this.m_StatusMessage = "Not Ordered";
            }
            else
            {
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = panelSetOrderCollection.GetTestOrderByTestId(this.m_Test.TestId);
                YellowstonePathology.Business.Test.PanelOrder panelOrder = panelSetOrderCollection.GetPanelOrderByTestOrderId(testOrder.TestOrderId);
                this.m_Ordered = true;
                this.m_OrderDate = panelOrder.OrderTime;
                this.m_StatusMessage = "Test has been ordered";
            } 
        }        
    }
}

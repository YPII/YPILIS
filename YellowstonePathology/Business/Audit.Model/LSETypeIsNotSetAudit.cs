using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class LSETypeIsNotSetAudit : AccessionOrderAudit
    {
        public LSETypeIsNotSetAudit(Test.AccessionOrder accessionOrder)
            :base(accessionOrder)
        {

        }

        public override void Run()
        {
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest panelSetLSE = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetLSE.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLSE = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLSE.PanelSetId);
                if (panelSetOrderLSE.LynchSyndromeEvaluationType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.NOTSET)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.AppendLine("The Lynch Syndrome Evaluation Type is not set.");
                }
            }            
        }
    }
}

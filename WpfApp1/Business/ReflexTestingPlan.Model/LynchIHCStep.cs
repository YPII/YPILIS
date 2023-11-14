using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReflexTestingPlan.Model
{
    public class LynchIHCStep : ReflexTestingStep
    {        
        private YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC m_PanelSetOrderLynchSyndromeIHC;

        public LynchIHCStep()
        {
            this.m_StepName = "Lynch IHC Panel";
			this.m_PanelSet = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();            
        }

        public override void SetState(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            base.SetState(accessionOrder);
            
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSet.PanelSetId) == true)
            {
                this.m_HasPanelSetBeenOrdered = true;
                this.m_PanelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSet.PanelSetId);                                
                if (this.m_PanelSetOrderLynchSyndromeIHC.Final == true)
                {
                    while (true)
                    {
                        if (this.HandlePPPPResults() == true) break;
                        if (this.HandleNPPNResults() == true) break;
                        break;
                    }
                }                
            }            
        }

        private bool HandlePPPPResults()
        {
            bool result = false;
            YellowstonePathology.Business.Test.LynchSyndrome.IHCResultNoLossOfNuclearExpression ihcResult = new Test.LynchSyndrome.IHCResultNoLossOfNuclearExpression();
            if (this.m_PanelSetOrderLynchSyndromeIHC.ResultCode == ihcResult.ResultCode)
            {
                result = true;
                this.m_IsStepComplete = true;

                throw new Exception("Needs Work");
                /*
                if (this.m_PanelSetOrderLynchSyndromeIHC.OrderMSI == true)
                {                    
                    this.m_NextStep = new LynchMSIStep();                
                }
                else
                {                    
                    this.m_Stop = true;
                }
                 */
            }
            return result;
        }

        private bool HandleNPPNResults()
        {
            bool result = false;
            YellowstonePathology.Business.Test.LynchSyndrome.IHCResult ihcResult = new YellowstonePathology.Business.Test.LynchSyndrome.IHCResultLossOfNuclearExpressionMLH1PMS2();
            if (this.m_PanelSetOrderLynchSyndromeIHC.ResultCode == ihcResult.ResultCode)
            {
                result = true;
                this.m_IsStepComplete = true;
                this.m_NextStep = new LynchBRAFStep();
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReflexTestingPlan.Model
{
    public class LynchMLH1ByPCRStep : ReflexTestingStep
    {
        public LynchMLH1ByPCRStep()            
        {
            this.m_StepName = "MLH1 By PCR";
			this.m_PanelSet = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
        }
    }
}

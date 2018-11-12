﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGroup5Breast : HER2AmplificationResultBreast
    {

        public HER2AmplificationResultGroup5Breast(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        {
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble.HasValue &&
                this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble < 2.0)
            {
                result = true;
                this.m_Result = HER2AmplificationResultEnum.Negative;
            }
            return result;
        }
    }
}

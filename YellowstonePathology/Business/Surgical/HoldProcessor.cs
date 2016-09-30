using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class HoldProcessor : Processor
    {
        public HoldProcessor()
        {
            this.m_Name = "Hold Specimen";
            this.m_ProcessorRunCollection.Add(new ProcessorRun("HOLD", "Hold Specimen, Overnight", new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), "Hold Specimen", ProcessorRunDayEnum.Tomorrow));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class Chong : Processor
    {
        public Chong()
        {
            this.m_Name = "Chong";
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHNGTO", "Chong, Today, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(2, 30, 0), "2hrs 30min", ProcessorRunDayEnum.Today));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHNGTMLM", "Chong, Tomorrow, Long Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 60, 0), "60min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHNGTMSM", "Chong, Tomorrow, Short Mini", new TimeSpan(5, 0, 0), new TimeSpan(0, 30, 0), "30min", ProcessorRunDayEnum.Tomorrow));
            this.m_ProcessorRunCollection.Add(new ProcessorRun("CHNGTMO", "Chong, Tomorrow, Overnight", new TimeSpan(17, 0, 0), new TimeSpan(2, 30, 0), "2hrs 30min", ProcessorRunDayEnum.Tomorrow));            
        }
    }
}
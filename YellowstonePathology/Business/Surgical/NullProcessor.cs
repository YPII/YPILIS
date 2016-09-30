using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class NullProcessor : Processor
    {
        public NullProcessor()
        {
            this.m_Name = null;
            this.m_ProcessorRunCollection.Add(new ProcessorRun(null, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), null, ProcessorRunDayEnum.Null));            
        }
    }
}
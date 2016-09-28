using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class Cheech : Processor
    {
        public Cheech()
        {
            this.m_Name = "Cheech";            
            this.m_ProcessorRunCollection.Add(new CheechTodayOvernight());
            this.m_ProcessorRunCollection.Add(new CheechTodayShortMini());
            this.m_ProcessorRunCollection.Add(new CheechTomorrowLongMini());
            this.m_ProcessorRunCollection.Add(new CheechTomorrowShortMini());
            this.m_ProcessorRunCollection.Add(new CheechTomorrowOvernight());
            this.m_ProcessorRunCollection.Add(new CheechSunday());
        }
    }
}
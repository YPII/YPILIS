using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class Processor
    {
        protected string m_Name;
        protected ProcessorRunCollection m_ProcessorRunCollection;

        public Processor()
        {
            this.m_ProcessorRunCollection = new ProcessorRunCollection();
        }

        public ProcessorRunCollection ProcessorRunCollection
        {
            get { return this.m_ProcessorRunCollection; }
        }

        public string Name
        {
            get { return this.m_Name; }
        }
    }
}

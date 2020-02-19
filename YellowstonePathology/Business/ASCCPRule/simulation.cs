using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class Simulation
    {
        protected BaseRule m_Rule;
        protected WomanCollection m_WomanCollection;

        public Simulation(BaseRule rule)
        {
            this.m_Rule = rule;
            this.m_WomanCollection = new WomanCollection();
        }

        public virtual void Run()
        {
            throw new Exception("Not implemented here.");
        }

        public WomanCollection WomanCollection
        {
            get { return this.m_WomanCollection; }
        }

        public BaseRule Rule
        {
            get { return this.m_Rule; }
        }
    }
}

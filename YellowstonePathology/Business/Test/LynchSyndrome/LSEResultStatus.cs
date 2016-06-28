using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEResultStatus 
    {
        protected List<LSEResult> m_LSEResultList;
        protected string m_Status;
        protected bool m_IsMatch;
        protected bool m_IsOrdered;
        protected string m_OrderedOnId;

        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected YellowstonePathology.Business.Test.LynchSyndrome.LSEResult m_LSEResult;

        public LSEResultStatus(LSEResult lseResult, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string orderedOnId)
        {
            this.m_LSEResult = lseResult;
            this.m_AccessionOrder = accessionOrder;
            this.m_OrderedOnId = orderedOnId;
            this.m_LSEResultList = new List<LSEResult>();
        }

        public bool IsOrdered
        {
            get { return this.m_IsOrdered; }
        }

        public virtual bool IsMatch()
        {
            throw new Exception("Not implemented in the base.");
        }

        public string Status
        {
            get { return this.m_Status; }
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class ResultCollection
    {
        private List<string> m_DetectedMutations = new List<string>();
        private List<string> m_NotDetectedMutations = new List<string>();

        public ResultCollection(RASRAFPanelTestOrder testOrder)
        {
            if (testOrder.BRAFResult == RASRAFPanelResult.DetectedResult) m_DetectedMutations.Add(RASRAFPanelResult.BRAFAbbreviation);
            else m_NotDetectedMutations.Add(RASRAFPanelResult.BRAFAbbreviation);

            if (testOrder.KRASResult == RASRAFPanelResult.DetectedResult) m_DetectedMutations.Add(RASRAFPanelResult.KRASAbbreviation);
            else m_NotDetectedMutations.Add(RASRAFPanelResult.KRASAbbreviation);

            if (testOrder.NRASResult == RASRAFPanelResult.DetectedResult) m_DetectedMutations.Add(RASRAFPanelResult.NRASAbbreviation);
            else m_NotDetectedMutations.Add(RASRAFPanelResult.NRASAbbreviation);

            if (testOrder.HRASResult == RASRAFPanelResult.DetectedResult) m_DetectedMutations.Add(RASRAFPanelResult.HRASAbbreviation);
            else m_NotDetectedMutations.Add(RASRAFPanelResult.HRASAbbreviation);
        }

        public List<string> DetectedMutations
        {
            get { return this.m_DetectedMutations; }
        }

        public List<string> NotDetectedMutations
        {
            get { return this.m_NotDetectedMutations; }
        }

        public RASRAFPanelResult GetResult()
        {
            RASRAFPanelResult result = null;
            if(this.m_DetectedMutations.Count!= 0)
            {
                result = new DetectedResult();
            }
            else
            {
                result = new NotDetectedResult();
            }
            return result;
        }

        public string GetDetectedListString()
        {
            StringBuilder result = new StringBuilder();
            for (int idx = 0; idx < this.m_DetectedMutations.Count; idx++)
            {
                result.Append(this.m_DetectedMutations[idx]);
                if (idx == this.m_DetectedMutations.Count - 2)
                {
                    result.Append(" and ");
                }
                else if (idx < this.m_DetectedMutations.Count - 2)
                {
                    result.Append(", ");
                }
            }
            return result.ToString();
        }

        public string GetNotDetectedListString()
        {
            StringBuilder result = new StringBuilder();
            for (int idx = 0; idx < this.m_NotDetectedMutations.Count; idx++)
            {
                result.Append(this.m_NotDetectedMutations[idx]);
                if (idx == this.m_NotDetectedMutations.Count - 2)
                {
                    result.Append(" or ");
                }
                else if (idx < this.m_NotDetectedMutations.Count - 2)
                {
                    result.Append(", ");
                }
            }
            return result.ToString();
        }
    }
}

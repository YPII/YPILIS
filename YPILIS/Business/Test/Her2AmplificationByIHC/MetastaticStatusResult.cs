using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
    public class MetastaticStatusResult
    {
        private string m_ISHResult;
        private string m_IHCResult;
        private string m_IHCScore;
        private string m_Her2Status;

        public MetastaticStatusResult(string ishResult, string ihcResult, string ihcScore, string her2Status)
        {
            m_ISHResult = ishResult;
            m_IHCResult = ihcResult;
            m_IHCScore = ihcScore;
            m_Her2Status = her2Status;
        }

        public string ISHResult
        {
            get { return this.m_ISHResult; }
            set { this.m_ISHResult = value; }
        }

        public string IHCResult
        {
            get { return this.m_IHCResult; }
            set { this.m_IHCResult = value; }
        }

        public string Her2Status
        {
            get { return this.m_Her2Status; }
            set { this.m_Her2Status = value; }
        }

        public string IHCScore
        {
            get { return this.m_IHCScore;}
            set { this.m_IHCScore = value; }
        }
    }
}

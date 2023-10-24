using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
    public class Her2IHCResult
    {
        private string m_Indication;
        private string m_Result;
        private string m_Score;
        private string m_Interpretation;
        private string m_Method;

        public Her2IHCResult(string indication, string result, string score, string interpretation, string method)
        {
            this.m_Indication = indication;
            this.m_Result = result;
            this.m_Score = score;
            this.m_Interpretation = interpretation;
            this.m_Method = method;
        }

        public string Indication
        {
            get { return m_Indication; }
            set { this.m_Indication = value; }
        }

        public string Result
        {
            get { return m_Result; }
            set { m_Result = value; }
        }

        public string Score
        {
            get { return this.m_Score; }
            set { this.m_Score = value; }
        }

        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set { this.m_Interpretation = value; }
        }

        public string Method
        {
            get { return this.m_Method; }
            set { this.m_Method = value; }
        }
    }
}

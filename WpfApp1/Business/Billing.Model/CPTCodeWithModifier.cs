using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeWithModifier
    {
        private string m_Code;
        private string m_Modifier;

        public CPTCodeWithModifier()
        { }

        public CPTCodeWithModifier(string code, string modifier)
        {
            this.m_Code = code;
            this.m_Modifier = modifier;
        }

        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        public string Modifier
        {
            get { return this.m_Modifier; }
            set { this.m_Modifier = value; }
        }
    }
}

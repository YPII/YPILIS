using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeModifier
    {
        public const string TwentySix = "26";
        public const string TechnicalComponent = "TC";

        private string m_Modifier;
        private string m_Description;

        public CptCodeModifier() { }

        public string Modifier
        {
            get { return this.m_Modifier; }
            set { this.m_Modifier = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}

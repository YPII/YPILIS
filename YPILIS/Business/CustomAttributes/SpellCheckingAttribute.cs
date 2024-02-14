using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.CustomAttributes
{
    public class SpellCheckingAttribute : Attribute
    {
        bool m_CheckSpelling;
        bool m_IsChecked;

        public SpellCheckingAttribute(bool checkSpelling)
        {
            this.m_CheckSpelling = checkSpelling;
        }

        public bool CheckSpelling
        {
            get { return this.m_CheckSpelling; }
            set { this.m_CheckSpelling = value; }
        }

        public bool IsChecked
        {
            get { return this.m_IsChecked; }
            set { this.m_IsChecked = value; }
        }
    }
}

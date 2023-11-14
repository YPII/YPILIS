using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Gross
{
    public class TemplateWord
    {
        private string m_Word;
        private string m_PlaceHolder;

        public TemplateWord(string word, string placeHolder)
        {
            this.m_Word = word;
            this.m_PlaceHolder = placeHolder;
        }

        public string Word
        {
            get { return this.m_Word; }
            set { this.m_Word = value; }
        }

        public string PlaceHolder
        {
            get { return this.m_PlaceHolder; }
            set { this.m_PlaceHolder = value; }
        }
    }
}

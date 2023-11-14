using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Typing
{
    public class TemplateWord
    {
        private string m_Description;
        private string m_PlaceHolder;

        public TemplateWord(string placeHolder, string description)
        {
            this.m_Description = description;
            this.m_PlaceHolder = placeHolder;
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public string PlaceHolder
        {
            get { return this.m_PlaceHolder; }
            set { this.m_PlaceHolder = value; }
        }
    }
}

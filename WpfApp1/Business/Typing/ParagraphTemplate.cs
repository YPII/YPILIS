using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Typing
{
    public class ParagraphTemplate
    {
        string m_Description;
        string m_Text;
        Collection<TemplateWord> m_WordCollection;

        public ParagraphTemplate()
        {
            this.m_WordCollection = new Collection<TemplateWord>();
        }

        public Collection<TemplateWord> WordCollection
        {
            get { return this.m_WordCollection; }
            set { this.m_WordCollection = value; }
        }


        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public string Text
        {
            get { return this.m_Text; }
            set { this.m_Text = value; }
        }

        public virtual string GenerateParagraph(string wordList)
        {           
            string[] words = wordList.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string result = this.m_Text;
            if (words.Length == this.m_WordCollection.Count)
            {                
                for (int i = 0; i < this.m_WordCollection.Count; i++)
                {
                    result = result.Replace("*" + this.m_WordCollection[i].PlaceHolder.Trim() + "*", words[i]);
                }
            }
            return result;
        }
    }
}

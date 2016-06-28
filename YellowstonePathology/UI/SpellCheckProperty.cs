using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;
using NHunspell;

namespace YellowstonePathology.UI
{
    public class SpellCheckProperty
    {
        private string m_Description;
        private PropertyInfo m_Property;
        private object m_O;
        private MatchCollection m_Matches;
        private Regex m_Regex;
        private int m_CurrentMatchIndex;
        private int m_ErrorCount;

        public SpellCheckProperty(PropertyInfo property, object o, string description)
        {
            this.m_Regex = new System.Text.RegularExpressions.Regex(@"\b\w+\b");
            this.m_CurrentMatchIndex = -1;
            this.m_Property = property;
            this.m_O = o;
            this.m_Description = description;
            string text = string.Empty;

            if (property.GetValue(this.m_O) != null)
            {
                text = (string)property.GetValue(this.m_O);
            }
            this.m_Matches = this.m_Regex.Matches(text);
        }

        public PropertyInfo Property
        {
            get { return this.m_Property; }
        }

        public object O
        {
            get { return m_O; }
        }

        public string Description
        {
            get { return this.m_Description; }
        }

        public int ErrorCount
        {
            get { return this.m_ErrorCount; }
        }

        public MatchCollection Matches
        {
            get { return this.m_Matches; }
        }

        public string GetText()
        {
            return (string)this.m_Property.GetValue(this.m_O);
        }

        public bool HasNextMatch()
        {
            bool result = false;
            if (this.m_CurrentMatchIndex < this.m_Matches.Count - 1)
            {
                result = true;
            }
            return result;
        }

        public System.Text.RegularExpressions.Match GetNextMatch()
        {
            this.m_CurrentMatchIndex += 1;
            return this.m_Matches[this.m_CurrentMatchIndex];
        }

        public void Reset(string text)
        {
            this.m_Property.SetValue(this.m_O, text);
            this.m_Matches = this.m_Regex.Matches(this.m_Property.GetValue(this.m_O).ToString());
        }

        public void SetErrorCount(Hunspell hunspell)
        {
            int errorCount = 0;
            foreach(Match match in this.Matches)
            {
                if(hunspell.Spell(match.Value) == false)
                {
                    errorCount += 1;
                }
            }
            this.m_ErrorCount = errorCount;
        }
    }
}

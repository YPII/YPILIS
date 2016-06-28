using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YellowstonePathology.UI.ICD10
{
    public class Tokenizer
    {
        private string m_Text;

        public Tokenizer(string text)
        {
            this.m_Text = text;
        }

        public string[] GetWords()
        {            
            var result = Regex.Matches(this.m_Text, @"([a-z]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();
            return result;
        }
    }
}

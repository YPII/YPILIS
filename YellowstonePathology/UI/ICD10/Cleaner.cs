using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.ICD10
{
    public class Cleaner
    {
        private StopWords m_StopWords;

        public Cleaner()
        {
            this.m_StopWords = new StopWords();
        }

        public List<string> Clean(string[] words)
        {
            List<string> result = new List<string>();
            foreach (string word in words)
            {
                if (this.m_StopWords.IsStopWord(word) == false)
                {
                    result.Add(word);
                }
            }
            return result;
        }
    }
}

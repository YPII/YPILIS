using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Windows;

namespace YellowstonePathology.Business.Common
{
	public class SpellChecker
	{
        Microsoft.Office.Interop.Word.Application m_WordApp;        

        object oMissing = System.Reflection.Missing.Value;
        Object oTrue = true;
        Object oFalse = false;

        Object MainDictionary = Type.Missing;
        Object CustomDictionary = Type.Missing;
        Object IgnoreUppercase = true;
        Object SuggestionMode = Type.Missing;

        Object CustomDictionary2 = Type.Missing;
        Object CustomDictionary3 = Type.Missing;
        Object CustomDictionary4 = Type.Missing;
        Object CustomDictionary5 = Type.Missing;
        Object CustomDictionary6 = Type.Missing;
        Object CustomDictionary7 = Type.Missing;
        Object CustomDictionary8 = Type.Missing;
        Object CustomDictionary9 = Type.Missing;
        Object CustomDictionary10 = Type.Missing;

        List<SpellCheckListItem> m_SpellCheckList;
        int m_CurrentPropertyIndex = -1;

		public SpellChecker()
		{
		}

		public void CheckSpelling(List<SpellCheckListItem> spellCheckList)
        {
            this.m_SpellCheckList = spellCheckList;
            this.m_WordApp = new Microsoft.Office.Interop.Word.Application();
            this.m_WordApp.Visible = false;
            this.m_WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oTrue);
            this.m_CurrentPropertyIndex = -1;                        
            this.FindNextSpellingError();            
            this.m_WordApp.Quit(ref oFalse, ref oMissing, ref oMissing);            
        }        

        public void FindNextSpellingError()
        {
            this.m_CurrentPropertyIndex += 1;
            if (this.m_CurrentPropertyIndex == this.m_SpellCheckList.Count)
            {
                return;
            }
            else
            {
				object value = this.m_SpellCheckList[this.m_CurrentPropertyIndex].Property.GetValue(this.m_SpellCheckList[this.m_CurrentPropertyIndex].PropertyObject, null);
				if(value != null)
				{
				    Microsoft.Office.Interop.Word._Document document = this.m_WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oFalse);
                    document.Words.First.InsertBefore(value.ToString());

                    Microsoft.Office.Interop.Word.ProofreadingErrors spellingErrors = document.SpellingErrors;

                    if (spellingErrors.Count > 0)
                    {
                        document.CheckSpelling(ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing, ref oMissing,
                            ref oMissing, ref oMissing);

                        object oFirst = 0;
                        object oLast = document.Characters.Count - 1;

                        string text = document.Range(ref oFirst, ref oLast).Text;
                        text = FixString(text);
					    this.m_SpellCheckList[this.m_CurrentPropertyIndex].Property.SetValue(this.m_SpellCheckList[this.m_CurrentPropertyIndex].PropertyObject, text, null);
                    }
                }
			}
            this.FindNextSpellingError();
        }

        public static string FixString(string searchString, string patternString, string replaceString)
        {
            string fixedString = string.Empty;
            Regex regex = new Regex(patternString);
            MatchCollection matchCollection = regex.Matches(searchString);
            if (matchCollection.Count > 0)
            {
                fixedString = regex.Replace(searchString, replaceString);
            }
            return fixedString;
        }

        public static string FixString(string fixString)
        {
			if (fixString == null) return string.Empty;

            Regex regexNN = new Regex(@"\n\n");
            Regex regexR = new Regex(@"\r[^\n]");
            Regex regexN = new Regex(@"[^\r]\n");            

            MatchCollection matchCollectionNN = regexNN.Matches(fixString);
            if (matchCollectionNN.Count > 0)
            {
                fixString = regexNN.Replace(fixString, new MatchEvaluator(MatchEvalNN));                
            }

            MatchCollection matchCollectionR = regexR.Matches(fixString);
            if (matchCollectionR.Count > 0)
            {
                fixString = regexR.Replace(fixString, new MatchEvaluator(MatchEvalR));
            }            

            MatchCollection matchCollectionN = regexN.Matches(fixString);
            if (matchCollectionN.Count > 0)
            {
                fixString = regexN.Replace(fixString, new MatchEvaluator(MatchEvalN));
            }            

            return fixString;
        }

        public static string MatchEvalNN(Match m)
        {
            string text = m.ToString();
            text = text.Replace("\n\n", "\r\n\r\n");
            return text;
        }

        public static string MatchEvalR(Match m)
        {
            string text = m.ToString();
            text = text.Replace("\r", "\r\n");
            return text;
        }        

        public static string MatchEvalN(Match m)
        {
            string text = m.ToString();
            text = text.Replace("\n", "\r\n");
            return text;
        }        

        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs args)
        {
            this.m_WordApp.Quit(ref oFalse, ref oMissing, ref oMissing);
        }		
    }        
}

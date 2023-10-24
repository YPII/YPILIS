using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YellowstonePathology.Business
{
    public class WordApplication : INotifyPropertyChanged
    {
        private static YellowstonePathology.Business.WordApplication m_Instance;

        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        Microsoft.Office.Interop.Word.Application m_WordApp;
        Microsoft.Office.Interop.Word.Documents m_Documents;
        Microsoft.Office.Interop.Word._Document m_Document;        
        Microsoft.Office.Interop.Word.Range m_Range;

        Microsoft.Office.Interop.Word.SpellingSuggestions m_SpellingSuggestionList;
        List<string> m_WordSuggestionList;

        object oMissing = System.Reflection.Missing.Value;
        Object oTrue = true;
        Object oFalse = false;

        Object MainDictionary = Type.Missing;
        Object CustomDictionary = Type.Missing;
        Object IgnoreUppercase = Type.Missing;
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

        int m_CurrentSpellingErrorIndex = 0;

        private WordApplication()
        {
            this.m_WordApp = new Microsoft.Office.Interop.Word.Application();
            this.m_WordApp.Visible = false;
            this.m_Documents = this.m_WordApp.Documents;
            this.m_Document = this.m_Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oTrue);
            this.m_WordSuggestionList = new List<string>();
        }        

        public static YellowstonePathology.Business.WordApplication Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new YellowstonePathology.Business.WordApplication();
                }
                return m_Instance;
            }
        }

        public void ClearCurrentDocument()
        {
            this.m_WordApp.Documents.Close(ref oFalse, ref oMissing, ref oMissing);
            this.m_Document = this.m_Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oTrue);                        
        }

        public void SetRange(string text)
        {
            Object oStart = 0;
            this.m_Range = this.m_WordApp.ActiveDocument.Range(ref oStart, ref  oStart);
            this.m_Range.InsertAfter(text);   
        }

        public int CurrentSpellingErrorIndex
        {
            get { return this.m_CurrentSpellingErrorIndex; }
            set 
            {
                this.m_CurrentSpellingErrorIndex = value;
                this.NotifyPropertyChanged("WordSuggestionList");
            }
        }       

        public string CurrentSpellingError
        {
            get { return this.m_Range.SpellingErrors[this.m_CurrentSpellingErrorIndex].Text; }            
        }

        public int SpellingErrorCount
        {
            get { return this.m_Range.SpellingErrors.Count; }
        }        

        public bool IsLastSpellingError
        {
            get
            {
                if (this.m_CurrentSpellingErrorIndex == this.SpellingErrorCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int CurrentSpellingErrorStart
        {
            get { return this.m_Range.SpellingErrors[this.m_CurrentSpellingErrorIndex].Start; }
        }

        public List<string> WordSuggestionList
        {
            get 
            {
                this.m_SpellingSuggestionList = this.m_WordApp.GetSpellingSuggestions(this.CurrentSpellingError, ref CustomDictionary,
                ref IgnoreUppercase, ref MainDictionary, ref SuggestionMode, ref CustomDictionary2,
                ref CustomDictionary3, ref CustomDictionary4,
                ref CustomDictionary5, ref CustomDictionary6,
                ref CustomDictionary7, ref CustomDictionary8,
                ref CustomDictionary9, ref CustomDictionary10);

                List<string> suggestions = new List<string>();
                for (int i = 1; i < this.m_SpellingSuggestionList.Count; i++)
                {
                    suggestions.Add(this.m_SpellingSuggestionList[i].Name);
                }
                return suggestions;
            }   
        }

        public void Close()
        {
            try
            {
                this.m_WordApp.Quit(ref oFalse, ref oMissing, ref oMissing);
            }
            catch(Exception)
            {

            }
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}

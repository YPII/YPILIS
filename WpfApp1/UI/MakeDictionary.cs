using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    public class MakeDictionary
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

        Microsoft.Office.Interop.Word._Document m_Document;

        public MakeDictionary()
        {
            this.m_WordApp = new Microsoft.Office.Interop.Word.Application();
            this.m_WordApp.Visible = true;
            this.m_WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oTrue);
            this.m_Document = this.m_WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oTrue);
        }

        public void DoIt()
        {
            string word = null;
            using (System.IO.StreamWriter writeGoodWords = new System.IO.StreamWriter(@"C:\Program Files\Yellowstone Pathology Institute\goodwords.txt"))
            {
                using (System.IO.StreamWriter writeBadWords = new System.IO.StreamWriter(@"C:\Program Files\Yellowstone Pathology Institute\badwords.txt"))
                {
                    System.IO.StreamReader readFile = new System.IO.StreamReader(@"C:\Program Files\Yellowstone Pathology Institute\ypi-custom.dic");
                    while ((word = readFile.ReadLine()) != null)
                    {
                        this.m_Document.Words.First.Delete();
                        this.m_Document.Words.First.InsertBefore(word);

                        Microsoft.Office.Interop.Word.ProofreadingErrors spellingErrors = this.m_Document.SpellingErrors;
                        
                        if (spellingErrors.Count > 0)
                        {
                            writeBadWords.WriteLine(word);
                        }
                        else
                        {
                            writeGoodWords.WriteLine(word);
                        }                        
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.ICD10
{
    public class StopWords : List<string>
    {
        public StopWords()
        {
            this.Add("a");
            this.Add("b");
            this.Add("c");
            this.Add("d");
            this.Add("and");
            this.Add("or");
            this.Add("for");
            this.Add("but");
            this.Add("an");
            this.Add("the");
            this.Add("a");
            this.Add("of");
            this.Add("is");
            this.Add("with");
            this.Add("as");
            this.Add("up");
            this.Add("to");
            this.Add("that");
            this.Add("has");
            this.Add("been");
            this.Add("it");
            this.Add("by");
            this.Add("are");
            this.Add("all");
            this.Add("near");
            this.Add("was");
            this.Add("after");
            this.Add("at");
            this.Add("beyond");
            this.Add("between");
            this.Add("behind");
            this.Add("over");
            this.Add("about");
            this.Add("along");
            this.Add("around");
            this.Add("be");
            this.Add("near");
            this.Add("beside");
            this.Add("it");
            this.Add("on");
            this.Add("because");
            this.Add("in");
            this.Add("inside");
            this.Add("on");
            this.Add("under");
            this.Add("above");
            this.Add("with");
            this.Add("without");
            this.Add("until");
            this.Add("among");
            this.Add("by");
            this.Add("beside");
            this.Add("except");
            this.Add("from");
            this.Add("near");
            this.Add("off");
            this.Add("into");
            this.Add("has");
            this.Add("was");
            this.Add("across");
            this.Add("against");
            this.Add("below");
            this.Add("above");
        }        

        public bool IsStopWord(string word)
        {
            bool result = false;
            foreach (string w in this)
            {
                if (w == word)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class EmbeddingInstructionList : List<string>
    {        
        public EmbeddingInstructionList()
        {
            this.Add("Alopecia");
            this.Add("Vas-def.");
            this.Add("Tubes");
            this.Add("Bisect");
            this.Add("Scant");
            this.Add("Tiny");
            this.Add("On Edge");
            this.Add("En Face");
            this.Add("Punch");
            this.Add("Bone");
            this.Add("?");
            this.Add("Temp. Art.");
            this.Add("S.N.");
        }
    }
}

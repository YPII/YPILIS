using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Typing
{
    public class GallbladderTemplate : ParagraphTemplate
    {
        public GallbladderTemplate()
        {
            this.Description = "Gallbladder Template";
            this.Text = "Specimen *SPECIMENNO* is received in *RECEIVEDIN* in a container labeled \"*CONTAINERLABELING*\" and consists of *HOWRECEIVED* gallbladder " +
                "measuring *MEASUREMENT1* in diameter.  The serosal surface is *SEROSALSURFACE*.  The adventitial surface is *ADVENTITIALSURFACE*.  A *MEASURMENT2* defect is present within the " +
                "*PERTINENTABNORMALITIES* of the gallbladder.  The gallbladder is opened revealing *GALLBLADDEROPENEDREVEALING* *BILE* bile and *STONES* stones measuring *MEASURMENT3* " +
                "in aggregate.  There are *LOOSESTONES* loose stones found in the specimen container.  The mucosa is *MUCOSA*.  The gallbladder wall measures *WALLTHICKNESS* in thickness." +
                "Representative sections of the specimen are submitted in *COLOR*  cassette \" *NUMBER*\" ";

            this.WordCollection.Add(new TemplateWord("SPECIMENNO", "1, 2, 3, etc."));
            this.WordCollection.Add(new TemplateWord("RECEIVEDIN", "formalin, fresh with formalin added at YPI Lab"));
            this.WordCollection.Add(new TemplateWord("CONTAINERLABELING", "Last Name, First Name, Middle Initial - additional designations"));
            this.WordCollection.Add(new TemplateWord("HOWRECEIVED", "intact / previously opened"));
            this.WordCollection.Add(new TemplateWord("MEASUREMENT1", "___ cm in length X ___ to ___ cm diameter"));
            this.WordCollection.Add(new TemplateWord("SEROSALSURFACE", "color, texture, covered with prominent vessels/exudate, etc."));
            this.WordCollection.Add(new TemplateWord("ADVENTITIALSURFACE", "color, texture, exudate, holes, etc."));
            this.WordCollection.Add(new TemplateWord("MEASUREMENT2 (if applicable)", " ___ X ___ cm defect "));
            this.WordCollection.Add(new TemplateWord("PERTINENTABNORMALITIES (if applicable)", "neck, body, fundus"));
            this.WordCollection.Add(new TemplateWord("GALLBLADDEROPENEDREVEALING", "trace, a moderate amount, abundant"));
            this.WordCollection.Add(new TemplateWord("BILE", "color/consistency"));
            this.WordCollection.Add(new TemplateWord("STONES", "present/absent, (number, color, character, if applicable"));
            this.WordCollection.Add(new TemplateWord("MEASUREMENT3", "___ cm"));
            this.WordCollection.Add(new TemplateWord("LOOSESTONES", "yes/no"));
            this.WordCollection.Add(new TemplateWord("MUCOSA", "color, texture, yellow streaks, ulcers, nodules, etc."));
            this.WordCollection.Add(new TemplateWord("WALLTHICKNESS", "___ cm to ___ cm"));
            this.WordCollection.Add(new TemplateWord("COLOR", "color"));
            this.WordCollection.Add(new TemplateWord("NUMBER", "number"));            
        }
    }
}

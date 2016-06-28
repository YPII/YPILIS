using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Typing
{
    public class ParagraphTemplateCollection : Collection<ParagraphTemplate>
    {
        public ParagraphTemplateCollection()
        {
            SimpleSkinTemplate simpleSkinTemplate = new SimpleSkinTemplate();
            this.Add(simpleSkinTemplate);

            ComplexSkinTemplate complexSkinTemplate = new ComplexSkinTemplate();
            this.Add(complexSkinTemplate);

            GallbladderTemplate gallbladderTemplate = new GallbladderTemplate();
            this.Add(gallbladderTemplate);

			TechnicalGrossTemplate technicalGrossTemplate = new TechnicalGrossTemplate();
			this.Add(technicalGrossTemplate);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplateCollection : ObservableCollection<DictationTemplate>
    {
        public DictationTemplateCollection()
        {

        }        

        public DictationTemplate GetTemplate(string specimenId)
        {
            DictationTemplate result = new TemplateNotFound();
            if (string.IsNullOrEmpty(specimenId) == false)
            {
                foreach (DictationTemplate dictationTemplate in this)
                {
                    if (dictationTemplate.SpecimenCollection.Exists(specimenId) == true)
                    {
                        result = dictationTemplate;
                        break;
                    }
                }
            }
            return result;
        }

        public static DictationTemplateCollection GetAll()
        {
            DictationTemplateCollection result = new DictationTemplateCollection();
            result.Add(new AppendixExcisionTemplate());
            result.Add(new ProstateNeedleCoreTemplate());
            result.Add(new ProstateTURTemplate());
            result.Add(new GITemplate());
            result.Add(new SkinShavePunchMiscTemplate());
            result.Add(new SkinExcisionTemplate());
            result.Add(new GallbladderExcisionTemplate());
            result.Add(new TonsilExcision());
            result.Add(new POC());
            result.Add(new BreastReduction());
            result.Add(new ECC());
            result.Add(new EMB());
            result.Add(new CervicalBiopsy());
            result.Add(new LEEPExcision());
            result.Add(new Placenta());
            result.Add(new Unterus());
            result.Add(new NeedleCoreBiopsy());
            return result;
        }               
    }
}

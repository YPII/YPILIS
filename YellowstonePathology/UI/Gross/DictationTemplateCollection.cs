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
                        result = (DictationTemplate)Activator.CreateInstance(dictationTemplate.GetType());                        
                        break;
                    }
                }
            }
            return result;
        }

        public static DictationTemplateCollection GetAll()
        {
            DictationTemplateCollection result = new DictationTemplateCollection();
            result.Add(new AdenoidExcisionTemplate());
            result.Add(new AorticValveTemplate());
            result.Add(new AppendixExcisionTemplate());
            result.Add(new BreastReductionTemplate());
            result.Add(new CervicalBiopsyTemplate());
            result.Add(new EMBTemplate());
            result.Add(new ECCTemplate());
            result.Add(new FemoralHeadTemplate());
            result.Add(new FallopianTubeTemplate());
            result.Add(new PeripheralBloodTemplate());
            result.Add(new GallbladderExcisionTemplate());
            result.Add(new BXTemplate());
            result.Add(new GITemplate());
            result.Add(new ConsultTemplate());
            result.Add(new KneeTissueTemplate());
            result.Add(new LEEPTemplate());
            result.Add(new CervicalConeTemplate());
            result.Add(new LEEPPiecesTemplate());
            result.Add(new MitralValveTemplate());
            result.Add(new NeedleCoreBiopsyTemplate());
            result.Add(new POCTemplate());
            result.Add(new ProstateNeedleCoreTemplate());
            result.Add(new ProstateTURTemplate());
            result.Add(new SinusContentTemplate());
            result.Add(new SinglePlacentaTemplate());
            result.Add(new Twin1PlacentaTemplate());
            result.Add(new Twin2PlacentaTemplate());
            result.Add(new SkinExcisionOrientedTemplate());
            result.Add(new SkinExcisionUnorientedTemplate());
            result.Add(new SkinExcisionOrientedwithCurettingsTemplate());
            result.Add(new SkinExcisionUnorientedwithCurettingsTemplate());
            result.Add(new SkinShavewithCurettingsTemplate());
            result.Add(new SkinShavePunchMiscTemplate());
            result.Add(new TonsilAdenoidExcisionTemplate());
            result.Add(new TonsilExcisionTemplate());
            result.Add(new UterusAdnexaTemplate());
            result.Add(new UterusTemplate());
            result.Add(new FluidTemplate());
            result.Add(new InitialReadingTemplate());
            return result;
        }               
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplateCollection : ObservableCollection<DictationTemplate>
    {
        private static DictationTemplateCollection instance;

        public DictationTemplateCollection() { }

        public static DictationTemplateCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = BuildFromRedis();
                }
                return instance;
            }
        }

        /*public DictationTemplate GetTemplate(string specimenId)
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
        }*/

        public DictationTemplate GetClone(string specimenId)
        {
            DictationTemplate notFound = DictationTemplateCollection.Instance.FirstOrDefault(t => t.TemplateName == "Template Not Found.");
            YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new YellowstonePathology.Business.Persistence.ObjectCloner();
            DictationTemplate result = (DictationTemplate)objectCloner.Clone(notFound);

            foreach (DictationTemplate dictationTemplate in this)
            {
                if(dictationTemplate.SpecimenCollection.Exists(specimenId) == true)
                {
                    result = (DictationTemplate)objectCloner.Clone(dictationTemplate);
                    break;
                }
            }
            return result;
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        public static DictationTemplateCollection BuildFromRedis()
        {
            DictationTemplateCollection result = new DictationTemplateCollection();
            Store.RedisDB dictationTemplateDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.DictationTemplate);
            foreach (string jString in dictationTemplateDb.GetAllJSONKeys())
            {
                DictationTemplate dictationTemplate = JsonConvert.DeserializeObject<DictationTemplate>(jString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });

                JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                foreach(string id in jObject["specimenIds"])
                {
                    Business.Specimen.Model.Specimen specimen = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen(id);
                    dictationTemplate.SpecimenCollection.Add(specimen);
                }

                result.Add(dictationTemplate);
            }
            return result;
        }

        /*public static DictationTemplateCollection GetAll()
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
            result.Add(new FallopianTubeAndOvariesTemplate());
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
            result.Add(new BladderTURTemplate());
            result.Add(new SinusContentTemplate());
            result.Add(new SinglePlacentaTemplate());
            result.Add(new TwinPlacentaTemplate());
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

            //Added for creation of redis WHC result.Add(new TemplateNotFound());

            return result;
        }*/               
    }
}

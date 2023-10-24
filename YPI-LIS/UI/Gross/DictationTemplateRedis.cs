using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplateRedis
    {
        private string m_TemplateId;
        private string m_TemplateName;
        private string m_Text;
        private bool m_UseAppendInitials;
        private List<string> m_SpecimenIds;

        public DictationTemplateRedis(DictationTemplate dictationTemplate)
        {
            this.m_TemplateId = dictationTemplate.TemplateId;
            this.m_TemplateName = dictationTemplate.TemplateName;
            this.m_Text = dictationTemplate.Text;
            this.m_UseAppendInitials = dictationTemplate.UseAppendInitials;
            this.m_SpecimenIds = new List<string>();
            foreach(YellowstonePathology.Business.Specimen.Model.Specimen specimen in dictationTemplate.SpecimenCollection)
            {
                this.m_SpecimenIds.Add(specimen.SpecimenId);
            }
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        public string TemplateId
        {
            get { return this.m_TemplateId; }
        }

        public string TemplateName
        {
            get { return this.m_TemplateName; }
        }

        public string Text
        {
            get { return this.m_Text; }
        }

        public bool UseAppendInitials
        {
            get { return this.m_UseAppendInitials; }
        }

        public List<string> SpecimenIds
        {
            get { return this.m_SpecimenIds; }
        }
    }
}

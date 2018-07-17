using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Specimen : YellowstonePathology.Business.Interface.IOrderTargetType
    {
        protected string m_SpecimenId;
        protected string m_SpecimenName;
        protected string m_Description;

        protected YellowstonePathology.Business.Billing.Model.CptCode m_CPTCode;
        protected int m_CPTCodeQuantity;

        protected string m_ClientFixation;
        protected string m_LabFixation;        
        protected string m_ProcessorRunId;
        protected bool m_RequiresGrossExamination;
        

        public Specimen()
        {
            this.m_RequiresGrossExamination = true;
            this.m_CPTCodeQuantity = 1;
        }

        public string TypeId
        {
            get { return this.m_SpecimenId; }
        }

        [PersistentPrimaryKeyProperty(false)]
        public string SpecimenId
        {
            get { return this.m_SpecimenId; }
            set { this.m_SpecimenId = value; }
        }

        [PersistentProperty()]
        public string SpecimenName
        {
            get { return this.m_SpecimenName; }
            set { this.m_SpecimenName = value; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        [PersistentProperty()]
        public YellowstonePathology.Business.Billing.Model.CptCode CPTCode
        {
            get { return this.m_CPTCode; }
            set { this.m_CPTCode = value; }
        }

        [PersistentProperty()]
        public int CPTCodeQuantity
        {
            get { return this.m_CPTCodeQuantity; }
            set { this.m_CPTCodeQuantity = value; }
        }

        [PersistentProperty()]
        public string ClientFixation
        {
            get { return this.m_ClientFixation; }
            set { this.m_ClientFixation = value; }
        }

        [PersistentProperty()]
        public string LabFixation
        {
            get { return this.m_LabFixation; }
            set { this.m_LabFixation = value; }
        }

        [PersistentProperty()]
        public bool RequiresGrossExamination
        {
            get { return this.m_RequiresGrossExamination; }
            set { this.m_RequiresGrossExamination = value; }
        }

        [PersistentProperty()]
        public string ProcessorRunId
        {
            get { return this.m_ProcessorRunId; }
            set { this.m_ProcessorRunId = value; }
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }
    }
}

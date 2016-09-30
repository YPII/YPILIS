using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Specimen : YellowstonePathology.Business.Interface.IOrderTargetType
    {
        protected string m_SpecimenId;
        protected string m_SpecimenName;
        protected string m_Description;
        protected YellowstonePathology.Business.Billing.Model.CptCode m_CPTCode;
        protected string m_ClientFixation;
        protected string m_LabFixation;        
        protected string m_ProcessorRunId;
        protected bool m_RequiresGrossExamination;

        public Specimen()
        {
            this.m_RequiresGrossExamination = true;
        }

        public string TypeId
        {
            get { return this.m_SpecimenId; }
        }

        public string SpecimenId
        {
            get { return this.m_SpecimenId; }
            set { this.m_SpecimenId = value; }
        }

        public string SpecimenName
        {
            get { return this.m_SpecimenName; }
            set { this.m_SpecimenName = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public YellowstonePathology.Business.Billing.Model.CptCode CPTCode
        {
            get { return this.m_CPTCode; }
            set { this.m_CPTCode = value; }
        }

        public string ClientFixation
        {
            get { return this.m_ClientFixation; }
            set { this.m_ClientFixation = value; }
        }

        public string LabFixation
        {
            get { return this.m_LabFixation; }
            set { this.m_LabFixation = value; }
        }

        public bool RequiresGrossExamination
        {
            get { return this.m_RequiresGrossExamination; }
            set { this.m_RequiresGrossExamination = value; }
        }

        public string ProcessorRunId
        {
            get { return this.m_ProcessorRunId; }
            set { this.m_ProcessorRunId = value; }
        }
    }
}

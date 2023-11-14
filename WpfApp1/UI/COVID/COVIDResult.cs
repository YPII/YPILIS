using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI.COVID
{
    public class COVIDResult
    {
        private string m_Color;
        private string m_Position;
        private string m_SampleName;
        private string m_GeneName;
        private string m_GeneType;
        private string m_Cq;
        private string m_Call;
        private string m_CombinedResult;
        private string m_Failure;
        private string m_Excluded;
        private string m_SampleType;
        private string m_ReplicateGroup;
        private string m_Dye;
        private string m_EditedCall;
        private string m_Slope;
        private string m_EPF;
        private string m_Notes;
        private string m_SamplePrepNotes;
        private string m_Number;

        public COVIDResult()
        {

        }

        [MappingAttribute("Color")]
        public string Color
        {
            get { return this.m_Color; }
            set { this.m_Color = value; }
        }

        [MappingAttribute("Position")]
        public string Position
        {
            get { return this.m_Position; }
            set { this.m_Position = value; }
        }

        [MappingAttribute("Sample Name")]
        public string SampleName
        {
            get { return this.m_SampleName; }
            set { this.m_SampleName = value; }
        }

        [MappingAttribute("Gene Name")]
        public string GeneName
        {
            get { return this.m_GeneName; }
            set { this.m_GeneName = value; }
        }

        [MappingAttribute("Gene Type")]
        public string GeneType
        {
            get { return this.m_GeneType; }
            set { this.m_GeneType = value; }
        }

        [MappingAttribute("Cq")]
        public string Cq
        {
            get { return this.m_Cq; }
            set { this.m_Cq = value; }
        }

        [MappingAttribute("Call")]
        public string Call
        {
            get { return this.m_Call; }
            set { this.m_Call = value; }
        }

        [MappingAttribute("Combined Result")]
        public string CombinedResult
        {
            get { return this.m_CombinedResult; }
            set { this.m_CombinedResult = value; }
        }

        [MappingAttribute("Failure")]
        public string Failure
        {
            get { return this.m_Failure; }
            set { this.m_Failure  = value; }
        }

        [MappingAttribute("Excluded")]
        public string Excluded
        {
            get { return this.m_Excluded; }
            set { this.m_Excluded = value; }
        }

        [MappingAttribute("Sample Type")]
        public string SampleType
        {
            get { return this.m_SampleType; }
            set { this.m_SampleType = value; }
        }

        [MappingAttribute("Replicate Group")]
        public string ReplicateGroup
        {
            get { return this.m_ReplicateGroup; }
            set { this.m_ReplicateGroup = value; }
        }

        [MappingAttribute("Dye")]
        public string Dye
        {
            get { return this.m_Dye; }
            set { this.m_Dye = value; }
        }

        [MappingAttribute("Edited Call")]
        public string EditedCall
        {
            get { return this.m_EditedCall; }
            set { this.m_EditedCall = value; }
        }

        [MappingAttribute("Slope")]
        public string Slope
        {
            get { return this.m_Slope; }
            set { this.m_Slope = value; }
        }

        [MappingAttribute("EPF")]
        public string EPF
        {
            get { return this.m_EPF; }
            set { this.m_EPF = value; }
        }

        [MappingAttribute("Notes")]
        public string Notes
        {
            get { return this.m_Notes; }
            set { this.m_Notes = value; }
        }

        [MappingAttribute("Sample Prep Notes")]
        public string SamplePrepNotes
        {
            get { return this.m_SamplePrepNotes; }
            set { this.m_SamplePrepNotes = value; }
        }

        [MappingAttribute("Number")]
        public string Number
        {
            get { return this.m_Number; }
            set { this.m_Number = value; }
        }
    }
}

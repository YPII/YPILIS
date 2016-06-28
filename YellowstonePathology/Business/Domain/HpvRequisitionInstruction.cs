using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{    
    public class HpvRequisitionInstruction
    {
        private int m_InstructionId;
        private string m_InstructionDescription;

        public HpvRequisitionInstruction()
        {
        }

        public HpvRequisitionInstruction(int instructionId, string instructionDescription)
        {
            this.m_InstructionId = instructionId;
            this.m_InstructionDescription = instructionDescription;
        }

        public int InstructionId
        {
            get { return this.m_InstructionId; }
            set
            {
                if (this.m_InstructionId != value)
                {
                    this.m_InstructionId = value;                    
                }
            }
        }

        public string InstructionDescription
        {
            get { return this.m_InstructionDescription; }
            set
            {
                if (this.m_InstructionDescription != value)
                {
                    this.m_InstructionDescription = value;                    
                }
            }
        }

        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
            this.m_InstructionId = propertyWriter.WriteInt("InstructionId");
            this.m_InstructionDescription = propertyWriter.WriteString("InstructionDescription");
        }
    }
}

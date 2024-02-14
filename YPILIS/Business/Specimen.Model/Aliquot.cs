using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Aliquot
    {
        private string m_Name;
        private string m_IdentificationType;
        private string m_AliquotType;

        public Aliquot()
        {

        }

        public Aliquot(string name, string aliquotType, string identificationType)
        {
            this.m_Name = name;
            this.m_AliquotType = aliquotType;
            this.m_IdentificationType = identificationType;            
        }

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value;}
        }

        public string AliquotType
        {
            get { return this.m_AliquotType; }
            set { this.m_AliquotType = value; }
        }

        public string IdentificationType
        {
            get { return this.m_IdentificationType; }
            set { this.m_IdentificationType = value; }
        }        
    }
}

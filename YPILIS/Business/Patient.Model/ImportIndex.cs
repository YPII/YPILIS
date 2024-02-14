using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Patient.Model
{
    public class ImportIndex : System.Attribute
    {                
        private int m_Index;

        public ImportIndex(int index)
        {
            this.m_Index = index;
        }		

        public int Index
        {
            get { return this.m_Index; }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
    public class Icd9CodeView 
    {
        private string m_Icd9Code;
        private string m_Category;        
        
        public Icd9CodeView(string icd9Code, string category)
        {
            this.m_Icd9Code = icd9Code;
            this.m_Category = category;
        }

        public string Icd9Code
        {
            get { return this.m_Icd9Code; }
            set { this.m_Icd9Code = value; }
        }

        public string Category
        {
            get { return this.m_Category; }
            set { this.m_Category = value; }
        }
    }
}

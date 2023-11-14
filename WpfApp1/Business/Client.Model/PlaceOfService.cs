using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class PlaceOfService
    {
        private string m_Code;
        private string m_Description;

        public PlaceOfService(string code, string description)
        {
            this.m_Code = code;
            this.m_Description = description;
        }

        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}

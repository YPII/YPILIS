using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class GeneralDataCassetteProstate : Cassette
    {
        private string m_Delimeter = "|";

        public GeneralDataCassetteProstate() 
            : base()
        {

        }

        public GeneralDataCassetteProstate(string color, string prostateDescription) 
            : base(color, prostateDescription)
        {
            
        }

        public override string GetFileExtension()
        {
            return ".gdc";
        }

        public override string GetLine(int printerColorCode)
        {            
            StringBuilder line = new StringBuilder(this.m_Delimeter);
            line.Append(printerColorCode.ToString() + this.m_Delimeter);
            line.Append(this.m_Delimeter);
            line.Append(this.m_Delimeter);
            line.Append(this.m_Delimeter);            
            line.Append(this.m_Delimeter);            
            line.Append(this.m_Delimeter);
            line.Append(this.m_Delimeter);
            line.Append(this.m_Delimeter);
            line.Append(this.m_ProstateDescription);
            return line.ToString();
        }
    }
}

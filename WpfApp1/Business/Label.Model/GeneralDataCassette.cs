using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class GeneralDataCassette : Cassette
    {
        private string m_Delimeter = "|";

        public GeneralDataCassette() 
            : base()
        {

        }

        public GeneralDataCassette(string color, string prostateDescription) 
            : base(color, prostateDescription)
        {
            
        }

        public override string GetFileExtension()
        {
            return ".gdc";
        }

        public override string GetLine(int printerColorCode)
        {
            //C:\Program Files\General Data Company\Cassette Printing\Normal.itl|102|15-28044|1A|JA|YPII|ALQ15-28044.1A|15|28044
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);
            StringBuilder line = new StringBuilder(this.m_Delimeter);

            line.Append(printerColorCode.ToString() + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNo + this.m_Delimeter);
            line.Append(this.BlockTitle + this.m_Delimeter);
            line.Append(this.PatientInitials + this.m_Delimeter);

            if (this.m_ClientAccessioned == true)
            {
                line.Append(this.m_ClientAccessionNo + this.m_Delimeter);
            }
            else
            {
                if (string.IsNullOrEmpty(this.m_EmbeddingInstructions) == false)
                {
                    line.Append(this.m_CompanyId + ":" + this.m_EmbeddingInstructions + this.m_Delimeter);
                }
                else
                {
                    line.Append(this.m_CompanyId + this.m_Delimeter);
                }
            }

            line.Append(this.ScanningId + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString() + this.m_Delimeter);
            line.Append(this.m_Delimeter);
            return line.ToString();
        }
    }
}

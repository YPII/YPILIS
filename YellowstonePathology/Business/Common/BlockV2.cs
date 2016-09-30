using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Common
{
	public class BlockV2 : Block
	{
        //C:\GDHC\cassette\formats\Normal.itl#102#$#H2#15-23277#FS1A#JP#YPII#ALQ15-23277.1A#15#23277

        private const string TemplateFileName = @"C:\Program Files\General Data Company\Cassette Printing\Normal.itl";
        private const string SomeNumber = "102";

        public BlockV2()
		{

		}

        public override string ToLaserString()
        {
            //C:\Program Files\General Data Company\Cassette Printing\Normal.itl|102|15-28044|1A|JA|YPII|ALQ15-28044.1A|15|28044
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);
            StringBuilder result = new StringBuilder(TemplateFileName + this.m_LaserDelimeter);            

            int cassetteCollumn = 103;
            switch (CassetteColumn)
            {
                case 6:
                    cassetteCollumn = 105;
                    break;
                case 5:
                    cassetteCollumn = 111;
                    break;
                case 4:
                    cassetteCollumn = 104;
                    break;
                case 3:
                    cassetteCollumn = 111;
                    break;
                default:
                    cassetteCollumn = 105;
                    break;
            }

            result.Append(cassetteCollumn.ToString() + this.m_LaserDelimeter);
            result.Append(orderIdParser.MasterAccessionNo + this.m_LaserDelimeter);
            result.Append(this.BlockTitle + this.m_LaserDelimeter);
            result.Append(this.PatientInitials + this.m_LaserDelimeter);
            result.Append(this.CompanyId + this.m_LaserDelimeter);
            result.Append(this.ScanningId + this.m_LaserDelimeter);
            result.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_LaserDelimeter);
            result.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());
            return result.ToString();
        }

        public override string ToString()
		{
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);

            StringBuilder result = new StringBuilder(this.m_Prefix + this.m_Delimeter);
            result.Append(this.FormattedCassetteColumn + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNo + this.m_Delimeter);
            result.Append(this.BlockTitle + this.m_Delimeter);
            result.Append(this.PatientInitials + this.m_Delimeter);
            result.Append(this.CompanyId + this.m_Delimeter);
            result.Append(this.ScanningId + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());
            return result.ToString();
        }
	}
}

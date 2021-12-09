using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class DistributionTypeList : List<string>
    {
        public DistributionTypeList()
        {
            this.Add("Fax");
            this.Add("Mail");
            this.Add("Print");
            this.Add("Web Service");
            this.Add("Web Service and Fax");
            this.Add("EPIC");
            this.Add("EPIC and Fax");
            this.Add("Eclinical Works");
            this.Add("MTDOH");
            this.Add("EYDOH");
            this.Add("Athena Health");
            this.Add("Meditech");
            this.Add("Do Not Distribute");
            this.Add("Text");
            this.Add("Email");
            this.Add("ECW Riverstone");
            this.Add("Meditech NMH");
        }
    }
}

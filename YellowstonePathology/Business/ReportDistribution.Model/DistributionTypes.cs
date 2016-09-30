using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class DistributionType
    {        
        public const string FAX = "Fax";        
        public const string MAIL = "Mail";
        public const string PRINT = "Print";
        public const string WEBSERVICE = "Web Service";
        public const string WEBSERVICEANDFAX = "Web Service and Fax";
        public const string EPIC = "EPIC";
        public const string EPICANDFAX = "EPIC and Fax";
        public const string ECW = "Eclinical Works";
        public const string MTDOH = "MTDOH";
        public const string WYDOH = "WYDOH";
        public const string ATHENA = "Athena Health";
        public const string MEDITECH = "Meditech";        
        public const string DONOTDISTRIBUTE = "Do Not Distribute";        
    }
}

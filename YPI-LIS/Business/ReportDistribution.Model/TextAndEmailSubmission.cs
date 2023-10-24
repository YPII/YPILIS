using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class TextAndEmailSubmission
    {        
        public static DistributionResult Submit(string phoneNumber, string emailAddress, string reportNo, string firstName)
        {
            DistributionResult result = new DistributionResult();
            TextSubmission.Submit(phoneNumber, reportNo, firstName);
            EmailSubmission.Submit(emailAddress, reportNo, firstName);
            result.IsComplete = true;            
            return result;
        }        
    }
}

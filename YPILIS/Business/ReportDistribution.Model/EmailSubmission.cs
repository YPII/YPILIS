using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class EmailSubmission
    {
        public static DistributionResult Submit(string emailAddress, string reportNo, string firstName)
        {
            DistributionResult result = new DistributionResult();
            if (string.IsNullOrEmpty(emailAddress) == false)
            {
                JObject jsonRequest = Business.APIRequestHelper.GetSendCovidResultEmailMessage(emailAddress, reportNo, firstName);
                Business.APIResult apiResult = Business.APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);
                result.IsComplete = true;
            }
            else
            {
                result.IsComplete = false;
            }
            return result;
        }
    }
}

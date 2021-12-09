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
    public class TextSubmission
    {        
        public static DistributionResult Submit(string phoneNumber, string reportNo, string firstName)
        {
            DistributionResult result = new DistributionResult();
            if (string.IsNullOrEmpty(phoneNumber) == false)
            {
                JObject jsonRequest = Business.APIRequestHelper.GetSendCovidResultTextMessage(phoneNumber, reportNo, firstName);
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

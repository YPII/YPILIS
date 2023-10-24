using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
    public class FedexAccountTesting : FedexAccount
    {
        public FedexAccountTesting()             
        {            
            this.m_URL = "https://wsbeta.fedex.com:443/web-services";
            this.m_Key = "qbOVOeskQnCDoevZ";
            this.m_Password = "vY9MrbdlhRrBdKCSKDzvsm4ll";
            this.m_MeterNo = "118737613";
            this.m_AccountNo = "510087640";
        }
    }
}

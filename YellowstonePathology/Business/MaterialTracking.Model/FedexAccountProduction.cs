using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
    public class FedexAccountProduction : FedexAccount
    {        
        public FedexAccountProduction() 
        {
            this.m_URL = "https://ws.fedex.com:443/web-services";
            this.m_Key = "9AKQLFGu15YA91pZ";
            this.m_Password = "sDY7lb5HCpaNvU2B98PZ6JoUY";
            this.m_MeterNo = "109758384";
            this.m_AccountNo = "235542196";
        }
    }
}

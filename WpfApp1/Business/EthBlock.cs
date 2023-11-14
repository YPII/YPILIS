using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business
{
    public class EthBlock
    {        
        private string m_Result;
        private int m_Number;
        private DateTime m_TimeStamp;
        private List<string> m_TransactionHashes;

        public EthBlock(string contents)
        {
            this.m_TransactionHashes = new List<string>();

            JObject jObject = JObject.Parse(contents);            
            this.m_Result = jObject["result"].ToString();
            JObject jResult = JObject.Parse(this.m_Result);

            this.m_Number = Convert.ToInt32(jResult["number"].ToString(), 16);
            JArray transactions = (JArray)jResult["transactions"];
            if(transactions.Count > 0)
            {
                JObject tran = (JObject)transactions[0];
                this.m_TransactionHashes.Add(tran["hash"].ToString());
            }

            int seconds = Convert.ToInt32(jResult["timestamp"].ToString(), 16);
            DateTime beginningDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            this.m_TimeStamp = beginningDate.AddSeconds(seconds).ToLocalTime();
        }

        public List<string> TransactionHashes
        {
            get { return this.m_TransactionHashes; }            
        }

        public int Number
        {
            get { return this.m_Number; }
            set { this.m_Number = value; }
        }

        public string Result
        {
            get { return this.m_Result; }
            set { this.m_Result = value; }
        } 
        
        public DateTime TimeStamp
        {
            get { return this.m_TimeStamp; }
            set { this.m_TimeStamp = value; }
        }                
    }
}

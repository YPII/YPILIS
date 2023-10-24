using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace YellowstonePathology.Business
{
    public class APIResult
    {
        private JObject m_JSONRequest;
        private JObject m_JSONResult;

        private string m_Status;
        private string m_Message;
        private object m_Result;

        public APIResult(JObject jsonRequest, JObject jsonResult)
        {
            this.m_JSONRequest = jsonRequest;
            this.m_JSONResult = jsonResult;

            this.m_Status = jsonResult["result"]["status"].ToString();
            if (jsonResult["result"]["message"] != null) this.m_Message = jsonResult["result"]["message"].ToString();
            this.m_Result = jsonResult["result"].ToString();
        }

        public string Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

        public object Result
        {
            get { return this.m_Result; }
            set { this.m_Result = value; }
        }

        public JObject JSONResult
        {
            get { return this.m_JSONResult; }
        }

        public JObject JSONRequest
        {
            get { return this.m_JSONRequest; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace YellowstonePathology.Business
{
    public class GethAPI
    {
        string m_BaseJSON = "{\"jsonrpc\":\"2.0\",\"method\":\"method_name\",\"params\":[],\"id\":1}";
        //CMMC Shipping: 0xeaeea7452996740c862dee98fe929e1acd756d1c
        //Receiving: 0xd24801e7e96eb3cd238de31c2c337f2620148a7c        

        public GethAPI()
        {
            
        }

        public void GetNewPubPrivKeyPair()
        {
            RandomNumberGenerator generator = RNGCryptoServiceProvider.Create();
            Byte[] bytes = new Byte[32];
            generator.GetBytes(bytes);
            string privateKey = BitConverter.ToString(bytes).Replace("-", "");

        }

        public EthBlock GetLatestBlock()
        {
            JObject postObject = JObject.Parse(this.m_BaseJSON);
            postObject["method"] = "eth_getBlockByNumber";            
            JArray paramList = (JArray)postObject["params"];
            paramList.Add("latest");
            paramList.Add(true);

            string postResult = this.Post(postObject);
            return new EthBlock(postResult);
        }

        public int GetContainerCount(string contractAddress)
        {            
            JObject postObject = JObject.Parse(this.m_BaseJSON);
            postObject["method"] = "eth_call";
            JArray paramList = (JArray)postObject["params"];

            string functionHash = "0x5a5c9b13";
            string dataParameter = "00000000000000000000000000000000000000000000000000000000"; //56 digits
            string data = functionHash + dataParameter;

            JObject callParam = JObject.Parse("{'to':'" + contractAddress + "','data':'" + data + "'}");
            paramList.Add(callParam);
            paramList.Add("latest");

            JObject callResult = JObject.Parse(this.Post(postObject));
            return Convert.ToInt32(callResult["result"].ToString(), 16);
        }

        public string GetContainer(string contractAddress, int containerIndex)
        {
            JObject postObject = JObject.Parse(this.m_BaseJSON);
            postObject["method"] = "eth_call";
            JArray paramList = (JArray)postObject["params"];

            string functionHash = "0x2b3e8957";
            string dataParameter = containerIndex.ToString().PadLeft(64, '0');
            string data = functionHash + dataParameter;

            JObject callParam = JObject.Parse("{'to':'" + contractAddress + "','data':'" + data + "'}");
            paramList.Add(callParam);
            paramList.Add("latest");

            JObject callResult = JObject.Parse(this.Post(postObject));
            return callResult["result"].ToString();
        }

        public string GetContractAddress(string transactionHash)
        {
            JObject postObject = JObject.Parse(this.m_BaseJSON);
            postObject["method"] = "eth_getTransactionReceipt";            
            JArray paramList = (JArray)postObject["params"];
            paramList.Add(transactionHash);            

            JObject tranReceipt = JObject.Parse(this.Post(postObject));
            JObject tranReceiptResult = (JObject)tranReceipt["result"];
            return tranReceiptResult["contractAddress"].ToString();
        }

        public Business.EthBlock GetBlockByNumber(int blockNumber)
        {            
            JObject postObject = JObject.Parse(this.m_BaseJSON);
            postObject["method"] = "eth_getBlockByNumber";
            string hexValue = String.Format("0x{0:X}", blockNumber);
            JArray paramList = (JArray)postObject["params"];
            paramList.Add(hexValue);
            paramList.Add(true);

            string postResult = this.Post(postObject);
            Business.EthBlock block = new EthBlock(postResult);
            return block;
        }

        public int GetBlockTransactionCountByNumber(int blockNumber)
        {            
            JObject postObject = JObject.Parse(this.m_BaseJSON);
            string hexValue = String.Format("0x{0:X}", blockNumber);
            postObject["method"] = "eth_getBlockTransactionCountByNumber";
            JArray paramList = (JArray)postObject["params"];
            paramList.Add(hexValue);
            string postResult = this.Post(postObject);

            JObject resultObject = JObject.Parse(postResult);
            string hexTranCnt = resultObject["result"].ToString().Remove(0,2);
            int tranCnt = Int32.Parse(hexTranCnt, System.Globalization.NumberStyles.HexNumber);
            return tranCnt;
        }

        public string Post(JObject postObject)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.1.1.52:8545");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = postObject.ToString(Formatting.None);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
    }
}
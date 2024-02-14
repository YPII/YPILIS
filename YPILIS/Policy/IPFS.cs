using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;

namespace YellowstonePathology.Policy
{
    public class IPFS
    {
        private static string IPFSRootURL = "http://10.1.2.27:5001/api/v0";   
        
        public static async Task PubSubPub(string topic, string payload)
        {
            string url = IPFSRootURL + "/pubsub/pub?arg=" + topic + "&arg=" + payload;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    var response = await httpClient.SendAsync(request);                    
                }
            }
        }

        public static async Task PubSubSub(string topic)
        {
            string url = IPFSRootURL + "/pubsub/sub?arg=" + topic;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    var response = await httpClient.SendAsync(request);
                    Console.WriteLine("done");                    
                }
            }
        }

        public static async Task<JObject> AddAsync(string fileName)
        {            
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), IPFSRootURL + "/add"))
                {
                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(new ByteArrayContent(File.ReadAllBytes(fileName)), "file", Path.GetFileName(fileName));
                    request.Content = multipartContent;

                    var response = await httpClient.SendAsync(request);
                    using (HttpContent content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JObject>(json);
                    }                    
                }
            }
        }

        public static async Task<JObject> FilesLs(string path)
        {
            string url = IPFSRootURL + "/files/ls?arg=" + path;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {                    
                    var response = await httpClient.SendAsync(request);
                    using (HttpContent content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<JObject>(json);
                    }
                }
            }
        }

        public static async Task FilesMkdir(string path)
        {
            string url = IPFSRootURL + "/files/mkdir?arg=" + path;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    var response = await httpClient.SendAsync(request);                    
                }
            }
        }                        
    }    
}

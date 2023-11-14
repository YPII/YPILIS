using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using MongoDB.Bson;

namespace YellowstonePathology.Business
{
    public class APIRequestHelper
    {
        public static JObject CreateSubmitSQLCommandMessage(string commandText)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "databaseOperation"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("databaseOperation", new JObject(
                            new JProperty("method", "executeSqlCommand"),
                            new JProperty("commandText", commandText)
                            ))
                    )
                ))
            );
        }

        public static JObject GetTokenMessage(string userName, string password, string authenticatorToken)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "authenticationOperation"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("authenticationOperation", new JObject(
                            new JProperty("method", "getToken"),
                            new JProperty("userName", userName),
                            new JProperty("password", password),
                            new JProperty("authenticatorToken", authenticatorToken)
                            ))
                    )
                ))
            );
        }

        public static JObject GetCaseDocumentMessage(string fileName)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "caseDocumentOperation"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("caseDocumentOperation", new JObject(
                            new JProperty("method", "getCaseDocument"),
                            new JProperty("fileName", fileName)
                            ))
                    )
                ))
            );
        }

        public static JObject GetQRCodeMessage(string userName, string password)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "authenticationOperation"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("authenticationOperation", new JObject(
                            new JProperty("target", "authenticationRequest"),
                            new JProperty("method", "getQRCode"),
                            new JProperty("userName", userName),
                            new JProperty("password", password)
                            ))
                    )
                ))
            );
        }

        public static JObject GetSendTextMessage(string phoneNumber, string message)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "sendTextMessage"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("phoneNumber", phoneNumber),
                        new JProperty("message", message)
                    )
                ))
            );
        }

        public static JObject GetSendCovidResultTextMessage(string phoneNumber, string reportNo, string firstName)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "sendCovidResultText"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("phoneNumber", phoneNumber),
                        new JProperty("reportNo", reportNo),
                        new JProperty("firstName", firstName)
                    )
                ))
            );
        }

        public static JObject GetSendCovidResultEmailMessage(string emailAddress, string reportNo, string firstName)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "sendCovidResultEmail"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("emailAddress", emailAddress),
                        new JProperty("reportNo", reportNo),
                        new JProperty("firstName", firstName)
                    )
                ))
            );
        }

        public static JObject GetCreateResultURLMessage(string reportNo)
        {
            return new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "createResultUrl"),
                new JProperty("id", ObjectId.GenerateNewId().ToString()),
                new JProperty("params", new JArray(
                    new JObject(
                        new JProperty("reportNo", reportNo)
                    )
                ))
            );
        }

        public static APIResult SubmitAPIRequestMessage(JObject requestMessage)
        {            
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.1.2.90:50000");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string msg = requestMessage.ToString();
                streamWriter.Write(requestMessage.ToString());
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                JObject responseMessage = JObject.Parse(streamReader.ReadToEnd());
                APIResult apiResult = new APIResult(requestMessage, responseMessage);
                return apiResult;
            }
        }
    }
}

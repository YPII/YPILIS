using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Helper
{
    public class JSONHelper
    {
        public static string HandleValue(string [] splitArray, int index)
        {
            if(splitArray.Length >= index + 1)
            {
                return splitArray[index];
            }
            else
            {
                return null;
            }
        }

        public static string HandleValue(string value)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public static YellowstonePathology.Business.Rules.MethodResult IsValidJSONString(string stringToValidate)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            result.Success = true;

            string jString = stringToValidate.Trim();
            if ((jString.StartsWith("{") && jString.EndsWith("}")) || //For object
                (jString.StartsWith("[") && jString.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(jString);
                }
                catch (JsonReaderException jex)
                {
                    result.Message = jex.Message;
                    result.Success = false;
                }
                catch (Exception ex)
                {
                    result.Message = ex.ToString();
                    result.Success = false;
                }
            }
            else
            {
                result.Message = "The JSON string does not start and end wiith '[' and']' or '{' and'}'.";
                result.Success = false;
            }

            return result;
        }
    }
}

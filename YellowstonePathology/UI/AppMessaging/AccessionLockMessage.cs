using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YellowstonePathology.UI.AppMessaging
{
    public class AccessionLockMessage
    {        
        private string m_MasterAccessionNo;
        private string m_From;
        private string m_To;
        private AccessionLockMessageIdEnum m_MessageId;        

        public AccessionLockMessage(string masterAccessionNo, string from, string to, AccessionLockMessageIdEnum messageId)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_From = from;
            this.m_To = to;
            this.m_MessageId = messageId;            
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
        }

        public string From
        {
            get { return this.m_From; }
        }

        public string To
        {
            get { return this.m_To; }
        }

        public AccessionLockMessageIdEnum MessageId
        {
            get { return this.m_MessageId; }
        } 
        
        public string ToMachineName
        {
            get
            {
                string[] splitString = this.m_To.Split(new char[] { '\\' });
                return splitString[0];
            }
        }           

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }               
        
        public static string GetMyAddress()
        {
            return System.Environment.MachineName + "\\" + Business.User.SystemIdentity.Instance.User.UserName;
        }                
    }
}

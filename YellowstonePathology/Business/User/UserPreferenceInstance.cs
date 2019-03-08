using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.User
{
	public class UserPreferenceInstance
	{
		private static UserPreferenceInstance instance;
		private UserPreference m_UserPreference;        

		private UserPreferenceInstance()
		{
            this.m_UserPreference = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullUserPreference(System.Environment.MachineName, this);
        }

        public static UserPreferenceInstance Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new UserPreferenceInstance();
				}
				return instance;
			}
		}

		public UserPreference UserPreference
		{
			get { return m_UserPreference; }
		}

        public void Save()
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }

        public void Refresh()
        {
            instance = null;
        }

        public static void SetUserPreferenceHostNameByLocation()
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ypilis.json";
            string jString = System.IO.File.ReadAllText(path);
            JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
            string location = jObject["location"].ToString();
            YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetUserPreferenceHostNameByLocation(location);
            UserPreferenceInstance.Instance.Refresh();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Flush();
        }


        public static void SetDefaultUserPreference()
        {
            YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetDefaultUserPreference();
            UserPreferenceInstance.Instance.Refresh();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Flush();
        }
    }
}

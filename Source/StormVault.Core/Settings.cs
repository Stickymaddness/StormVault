
using System;
using System.Collections.Generic;
using System.IO;
namespace StormVault.Core
{
    public class Settings
    {
        private const string fileName = "settings.json";

        private Dictionary<string, string> userSettings;

        public string PlayerName
        {
            get { return userSettings["PlayerName"]; }
            set { userSettings["PlayerName"] = value; }
        }

        public string ReplayPath
        {
            get { return userSettings["ReplayPath"]; }
            set { userSettings["ReplayPath"] = value; }
        }

        public bool MonitoringEnabled
        {
            get 
            {
                if (!userSettings.ContainsKey("MonitoringEnabled"))
                    return true;
                
                return Convert.ToBoolean(userSettings["MonitoringEnabled"]); 
            }
            set { userSettings["MonitoringEnabled"] = value.ToString(); }
        }

        public bool UserSettingsEmpty
        {
            get { return string.IsNullOrEmpty(PlayerName) || string.IsNullOrEmpty(ReplayPath); }
        }

        public Settings()
        {
            userSettings = DataStore.Fetch<Dictionary<string, string>>(fileName);

            if (userSettings != null)
                return;

            userSettings = new Dictionary<string, string>();
            userSettings["PlayerName"] = string.Empty;
            userSettings["ReplayPath"] = string.Empty;
        }

        public void Save()
        {
            DataStore.Store(userSettings, fileName);
        }
    }
}

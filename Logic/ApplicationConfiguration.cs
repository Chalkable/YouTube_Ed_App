using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Youtube.Logic
{
    public class ApplicationConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("appSecret")]
        public string AppSecret
        {
            get { return (string)this["appSecret"]; }
        }

        [ConfigurationProperty("authUri")]
        public string AuthUri
        {
            get { return (string)this["authUri"]; }
        }

        [ConfigurationProperty("acsUri")]
        public string AcsUri
        {
            get { return (string)this["acsUri"]; }
        }

        [ConfigurationProperty("redirectUri")]
        public string RedirectUri
        {
            get { return (string)this["redirectUri"]; }
        }

        [ConfigurationProperty("applicationName")]
        public string ApplicationName
        {
            get { return (string)this["applicationName"]; }
        }

        [ConfigurationProperty("chalkableRoot")]
        public string ChalkableRoot
        {
            get { return (string)this["chalkableRoot"]; }
        }
    }
}
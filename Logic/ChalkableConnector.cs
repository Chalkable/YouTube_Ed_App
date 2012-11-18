using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using WindowsAzure.Acs.Oauth2.Client;
using Youtube.Logic.Dto;

namespace Youtube.Logic
{
    public class ChalkableConnector
    {
        public T Call<T>(string url)
        {
            Debug.WriteLine("Request on: " + url);
            var x = typeof(T);
            var ser = new DataContractJsonSerializer(x);
            if (oauthClient != null)
            {
                //var url = Settings.Configuration.ChalkableRoot + "/Student/GetStudentsStartWith.json";
                Debug.WriteLine("Request on: " + url);
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                oauthClient.AppendAccessTokenTo(webRequest);
                WebResponse response = webRequest.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    return (T)ser.ReadObject(stream);
                }
            }
            throw new Exception("oauth client isn't initialized");
        }

        private SimpleOAuth2Client oauthClient;
        public ChalkableConnector(SimpleOAuth2Client oauthClient)
        {
            this.oauthClient = oauthClient;
        }

        public AnnouncementApplicationDto GetAnnouncementApplicationById(int announcementApplicationId)
        {
            var url = Settings.Configuration.ChalkableRoot + "/App/GetAnnouncementApplication.json";
            url = string.Format("{0}?{1}={2}", url, "announcementApplicationId", announcementApplicationId);
            return Call<AnnouncementApplicationDto>(url);
        }

        public AnnouncementDto GetAnnouncemnetById(int announcementId)
        {
            var url = Settings.Configuration.ChalkableRoot + "/announcement/read.json";
            url = string.Format("{0}?{1}={2}", url, "id", announcementId);
            return Call<AnnouncementDto>(url);
        }

    }
}
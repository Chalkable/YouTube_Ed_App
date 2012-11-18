using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;

namespace Youtube.Logic
{
    public class YoutubeConnector
    {
        public IEnumerable<Video> Search(string videoQuery, int start = 0, int count = 20)
        {
            var author = "";
            var orderby = "";
            var time = "All Time";
            var category = "";
            var query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
            if (!string.IsNullOrEmpty(videoQuery))
            {
                query.Query = videoQuery;
            }
            if (!string.IsNullOrEmpty(author))
            {
                query.Author = author;
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                query.OrderBy = orderby;
            }
            query.SafeSearch = YouTubeQuery.SafeSearchValues.None;
            if (String.IsNullOrEmpty(time) != true)
            {
                if (time == "All Time")
                    query.Time = YouTubeQuery.UploadTime.AllTime;
                else if (time == "Today")
                    query.Time = YouTubeQuery.UploadTime.Today;
                else if (time == "This Week")
                    query.Time = YouTubeQuery.UploadTime.ThisWeek;
                else if (time == "This Month")
                    query.Time = YouTubeQuery.UploadTime.ThisMonth;
            }
            if (String.IsNullOrEmpty(category) != true)
            {
                QueryCategory q = new QueryCategory(new AtomCategory(category));
                query.Categories.Add(q);
            }
            var res = GetVideos(query);
            return res.Skip(start).Take(count);
        }

        public static YouTubeRequest GetRequest()
        {
            YouTubeRequestSettings settings = new YouTubeRequestSettings("Chalkable Youtube app",
                                                "AI39si6y_3ZKWG2A4_-v5ogSal_5Y41jmsiQ3aYD0AUVHBTT7mNjOAhh1r24xJWUkki67hLg0l4EXZHS-d4h-kysPd9yGAV0Wg");
            settings.AutoPaging = true;
            YouTubeRequest request = new YouTubeRequest(settings);
            return request;
        }

        private static IEnumerable<Video> GetVideos(YouTubeQuery q)
        {
            YouTubeRequest request = GetRequest();
            Feed<Video> feed = null;
            try
            {
                feed = request.Get<Video>(q);
            }
            catch (GDataRequestException gdre)
            {
                var response = (HttpWebResponse)gdre.Response;
            }
            return feed != null ? feed.Entries : null;
        }

        public Video GetById(string id)
        {
            var query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri + "/" + id);
            var res = GetVideos(query);
            var v = res.FirstOrDefault();
            return v;
        }
    }
}
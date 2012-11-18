using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.YouTube;

namespace Youtube.Models
{
    public class SearchModel
    {
        public string Query { get; set; }
        public IEnumerable<VideoModel> Videos { get; set; }
        public int AnnouncementApplicationId { get; set; }
    }

    public class VideoModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AnnouncementApplicationId { get; set; }
        public const int MAX_DESCRIPTION = 800;
        public string Duration { get; set; }
        public string UploadedBy { get; set; }
        public int Views { get; set; }

        public static VideoModel Create(Video video)
        {
            var desc = video.Description ?? "";
            if (desc.Length > MAX_DESCRIPTION)
                desc = desc.Substring(0, MAX_DESCRIPTION) + "....";
            var res = new VideoModel
                       {
                           Id = video.VideoId,
                           Title = video.Title,
                           Views = video.ViewCount,
                           UploadedBy = video.Uploader,
                           Description = desc
                       };
            int dur;
            if (int.TryParse(video.Media.Duration.Seconds, out dur))
            {
                res.Duration = TimeSpan.FromSeconds(dur).ToString();
            }
            return res;
        }
    }
}
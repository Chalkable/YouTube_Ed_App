using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Youtube.Logic;
using Youtube.Models;

namespace Youtube.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            if (!string.IsNullOrEmpty(Mode))
            {
                if (Mode == Settings.EDIT_MODE)
                    return RedirectToAction("Edit", new RouteValueDictionary { { "announcementApplicationId", AnnouncementApplicationId } });
                if (Mode == Settings.VIEW_MODE || Mode == Settings.EMBEDDED_MODE)
                    return RedirectToAction("Video", new RouteValueDictionary { { "announcementApplicationId", AnnouncementApplicationId } });
            }

            return View();
        }

        public ActionResult Edit(string query, int announcementApplicationId)
        {
            var model = new SearchModel();
            model.Query = query;
            if (string.IsNullOrEmpty(query) )
            {
                var chalkableConnector = new ChalkableConnector(OauthClient);
                var anap = chalkableConnector.GetAnnouncementApplicationById(announcementApplicationId);
                var ann = chalkableConnector.GetAnnouncemnetById(anap.data.announcementid);
                query = ann.data.@class.name;
            }
            var connector = new YoutubeConnector();
            var videos = connector.Search(query);
            model.Videos = videos.Select(VideoModel.Create);
            model.AnnouncementApplicationId = announcementApplicationId;
            
            return View("Edit", model);
        }

        public ActionResult Preview(string id, int announcementApplicationId)
        {
            var storage = new Storage();
            storage.Set(announcementApplicationId.ToString(), id);
            var connector = new YoutubeConnector();
            var video = connector.GetById(id);
            var model = VideoModel.Create(video);
            model.AnnouncementApplicationId = announcementApplicationId;
            return View("Preview", model);
        }

        public ActionResult Video(int announcementApplicationId)
        {
            Storage storage = new Storage();
            var videoId = storage.Get(announcementApplicationId.ToString());
            var video = (new YoutubeConnector()).GetById(videoId);
            return View("Video", VideoModel.Create(video));
        }
    }
}

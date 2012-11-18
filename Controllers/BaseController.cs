using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WindowsAzure.Acs.Oauth2.Client;
using Youtube.Logic;

namespace Youtube.Controllers
{
    public class BaseController : Controller
    {

        private const string OAUTH_CLIENT = "OAUTH_CLIENT";
        protected SimpleOAuth2Client OauthClient;
        

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            AcceptAllCerts();//TODO: just for testing
            base.Initialize(requestContext);
        }

        private void AcceptAllCerts()
        {
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuthenticateByChalkable();
            base.OnActionExecuting(filterContext);
        }

        protected string Mode { private set; get; }
        protected int AnnouncementApplicationId { private set; get; }

        private void AuthenticateByChalkable()
        {
            RetrieveParams();

            var authUri = new Uri(Settings.Configuration.AuthUri);
            var acsUri = new Uri(Settings.Configuration.AcsUri);
            var redirectUri = new Uri(Settings.Configuration.RedirectUri);
            var appSecret = Settings.Configuration.AppSecret;
            var appName = Settings.Configuration.ApplicationName;
            var chlkRoot = Settings.Configuration.ChalkableRoot;
            

            if (Session[OAUTH_CLIENT] == null)
            {
                OauthClient = new SimpleOAuth2Client(authUri, acsUri, appName, appSecret, chlkRoot, redirectUri);
                string code = Request.Params["code"];
                string error = Request.Params["error"];
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(string.Format("OAuth error {0}. {1}", error, Request.Params["error_description"]));
                }
                if (!string.IsNullOrEmpty(code))
                {
                    OauthClient.Authorize(code);
                    Session[OAUTH_CLIENT] = OauthClient;
                }
                else
                {
                    var uri = OauthClient.BuildAuthorizationUri();
                    var callId = SaveParamsToCache();
                    Response.Redirect(uri + "&" + Settings.CALL_ID_PARAM + "=" + callId, true);
                }
            }
            else
                OauthClient = Session[OAUTH_CLIENT] as SimpleOAuth2Client;
        }

        private void RetrieveParams()
        {
            string callId = Request.Params[Settings.CALL_ID_PARAM];
            if (!string.IsNullOrEmpty(callId))
            {
                Mode = HttpContext.Cache[callId + "-mode"].ToString();
                AnnouncementApplicationId = int.Parse(HttpContext.Cache[callId + "-aaid"].ToString());    
            }
            else
            {
                Mode = Request[Settings.PAGE_MODE_PARAM] ?? Settings.MY_VIEW_MODE;
                if (!string.IsNullOrEmpty(Request[Settings.ANNOUNCEMENT_APPLICATION_ID]))
                    AnnouncementApplicationId = int.Parse(Request[Settings.ANNOUNCEMENT_APPLICATION_ID]);
                else
                    AnnouncementApplicationId = -1;
            }
        }

        private string SaveParamsToCache()
        {
            var callId = Guid.NewGuid().ToString().Replace("-", "");
            HttpContext.Cache[callId + "-mode"] = Mode;
            HttpContext.Cache[callId + "-aaid"] = AnnouncementApplicationId;
            return callId;
        }

    }

}

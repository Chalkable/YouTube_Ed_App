<h1>Chalkable YouTube App</h1>
<p>This is an example application that is easily integrated into the Chalkable App Store</p>
<p>It uses the data model: <blockquote> var storage = new Storage();
            storage.Set(announcementApplicationId.ToString(), id);<br>
            var connector = new YoutubeConnector();<br>
            var video = connector.GetById(id);<br>
            var model = VideoModel.Create(video);<br>
            model.AnnouncementApplicationId = announcementApplicationId;<br>
            return View("Preview", model);</blockquote><br>

 to determine the user ID, and assign an initial video search based on the user's credentials. For example, if a teacher is a U.S. History teacher, the app will show relavent U.S. history videos</p>
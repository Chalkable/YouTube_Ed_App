<h1>Chalkable YouTube App</h1>
<p>This is an example application that is easily integrated into the Chalkable App Store</p>
<p>It uses the data model: <blockquote> 
var url = Settings.Configuration.ChalkableRoot + "/announcement/read.json";<br>
            url = string.Format("{0}?{1}={2}", url, "id", announcementId);<br>
            return Call<AnnouncementDto>(url);</blockquote><br>

 ...to determine the user ID, and assign an initial video search based on the user's credentials. For example, if a teacher is a U.S. History teacher, the app will show relavent U.S. history videos</p>
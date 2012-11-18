using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Youtube.Logic.Dto
{
    public class AnnouncementApplicationData
    {
        public int id { get; set; }
        public int applicationid { get; set; }
        public int announcementid { get; set; }
        public int? schoolpersonid { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
    }

    public class AnnouncementApplicationDto
    {
        public AnnouncementApplicationData data { get; set; }
    }
}
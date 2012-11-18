using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Youtube.Logic.Dto
{
    public class ClassData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string cursetitle { get; set; }
    }

    public class AnnouncementData
    {
        public int id { get; set; }
        public string announcementtypename { get; set; }
        public string schoolpersonname { get; set; }
        public ClassData @class { get; set; }
    }

    public class AnnouncementDto
    {
        public AnnouncementData data { get; set; }
    }
}
/*
 * created: "11/7/2012 04:11:25 PM",

announcementtypeid: 4,
expiresdate: "2012-11-08",
isowner: true,
gradable: true,
starred: true,
order: 5,
state: 1,
statetyped: 1,
qnacount: 0,
attachmentscount: 0,
title: "English 10",
recipientid: 133,
content: "This is just a quiz 2222",
shortcontent: "This is just a quiz 2222",
subject: null,
schoolpersonref: 49,

schoolpersongender: "f",
announcementtyperef: 4,
systemtype: 4,
grade: null,
dropped: null,
studentscount: 39,
studentscountwithattachments: 0,
studentscountwithoutattachments: 39,
gradingstudentscount: 0,
nongradingstudentscount: 39,
comment: null,

gradesummary: "0/39",
attachmentsummary: "0/39",
avg: null,
avgnumeric: null,
gradingstyle: 0,
applicationscount: 1,
wasannouncementtypegraded: true,
showgradingicon: false
*/
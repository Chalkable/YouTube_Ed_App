using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Youtube.Logic
{
    public class Settings
    {
        public const string APPLICATION_CONFIG = "applicationconfig";

        public const string SCHOOL_PERSON_ID_PARM = "schoolpersonid";
        public const string USER_TOKEN = "usertoken";
        public const string ANNOUNCEMENT_APPLICATION_ID = "announcementapplicationid";
        public const string ANNOUNCEMENT_OWNER_ID_PARAM = "announcementownerid";
        public const string APPLICATION_INSTALL_ID = "applicationinstallid";
        public const string REMOTE_HOST_NAME = "remotehostname";

        public const string USER_ROLE_PARAM = "userroleparam";
        public const string TEACHER_ROLE_NAME = "teacher";
        public const string STUDENT_ROLE_NAME = "student";

        public const string PAGE_MODE_PARAM = "mode";
        public const string EDIT_MODE = "edit";
        public const string EMBEDDED_MODE = "embedded";
        public const string VIEW_MODE = "view";
        public const string MY_VIEW_MODE = "myview";
        public const string SUMMARY_VIEW_MODE = "summaryview";
        public const string GRADING_VIEW_MODE = "gradingview";

        public const string CALL_ID_PARAM = "callid";

        static Settings()
        {
            configuration = ConfigurationManager.GetSection(APPLICATION_CONFIG) as ApplicationConfiguration;
        }

        private static ApplicationConfiguration configuration;
        public static ApplicationConfiguration Configuration
        {
            get { return configuration; }
        }
    
    }
}
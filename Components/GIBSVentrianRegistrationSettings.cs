using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;

namespace GIBS.GIBSVentrianRegistration.Components
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class GIBSVentrianRegistrationSettings : ModuleSettingsBase
    {
       

        #region public properties

        /// <summary>
        /// get/set template used to render the module content
        /// to the user
        /// </summary>


        public string ReturnUrlPath
        {
            get
            {
                if (Settings.Contains("returnUrlPath"))
                    return Settings["returnUrlPath"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "returnUrlPath", value.ToString());
            }
        }

        public string VentrianModuleID
        {
            get
            {
                if (Settings.Contains("ventrianModuleID"))
                    return Settings["ventrianModuleID"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "ventrianModuleID", value.ToString());
            }
        }

        public string EmailFrom
        {
            get
            {
                if (Settings.Contains("emailFrom"))
                    return Settings["emailFrom"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "emailFrom", value.ToString());
            }
        }


        public string EmailNotify
        {
            get
            {
                if (Settings.Contains("emailNotify"))
                    return Settings["emailNotify"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "emailNotify", value.ToString());
            }
        }


        public string EmailSubject
        {
            get
            {
                if (Settings.Contains("emailSubject"))
                    return Settings["emailSubject"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "emailSubject", value.ToString());
            }
        }


        #endregion
    }
}

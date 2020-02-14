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
    public class GIBSVentrianRegistrationSettings
    {
        ModuleController controller;
        int tabModuleId;

        public GIBSVentrianRegistrationSettings(int tabModuleId)
        {
            controller = new ModuleController();
            this.tabModuleId = tabModuleId;
        }

        protected T ReadSetting<T>(string settingName, T defaultValue)
        {
            Hashtable settings = controller.GetTabModuleSettings(this.tabModuleId);

            T ret = default(T);

            if (settings.ContainsKey(settingName))
            {
                System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                try
                {
                    ret = (T)tc.ConvertFrom(settings[settingName]);
                }
                catch
                {
                    ret = defaultValue;
                }
            }
            else
                ret = defaultValue;

            return ret;
        }

        protected void WriteSetting(string settingName, string value)
        {
            controller.UpdateTabModuleSetting(this.tabModuleId, settingName, value);
        }

        #region public properties

        /// <summary>
        /// get/set template used to render the module content
        /// to the user
        /// </summary>
        public string VentrianModuleID
        {
            get { return ReadSetting<string>("ventrianModuleID", null); }
            set { WriteSetting("ventrianModuleID", value); }
        }

        public string EmailFrom
        {
            get { return ReadSetting<string>("emailFrom", null); }
            set { WriteSetting("emailFrom", value); }
        }
        public string EmailNotify
        {
            get { return ReadSetting<string>("emailNotify", null); }
            set { WriteSetting("emailNotify", value); }
        }

        public string EmailSubject
        {
            get { return ReadSetting<string>("emailSubject", null); }
            set { WriteSetting("emailSubject", value); }
        }

        #endregion
    }
}

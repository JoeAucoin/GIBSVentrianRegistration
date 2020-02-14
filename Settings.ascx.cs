using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.GIBSVentrianRegistration.Components;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Tabs;
using System.Collections;
using System.Collections.Generic;
using DotNetNuke.Security;
using DotNetNuke.Common.Utilities;

namespace GIBS.Modules.GIBSVentrianRegistration
{
    public partial class Settings : ModuleSettingsBase
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {

                    BindModules();
                    GIBSVentrianRegistrationSettings settingsData = new GIBSVentrianRegistrationSettings(this.TabModuleId);

                    if (settingsData.VentrianModuleID != null)
                    {

                        drpModuleID.SelectedValue = settingsData.VentrianModuleID;
                    }
                    if (settingsData.EmailFrom != null)
                    {

                        txtEmailFrom.Text = settingsData.EmailFrom;
                    }

                    if (settingsData.EmailNotify != null)
                    {

                        txtEmailNotify.Text = settingsData.EmailNotify;
                    }

                    if (settingsData.EmailSubject != null)
                    {

                        txtEmailSubject.Text = settingsData.EmailSubject;
                    }

                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
                GIBSVentrianRegistrationSettings settingsData = new GIBSVentrianRegistrationSettings(this.TabModuleId);
                settingsData.VentrianModuleID = drpModuleID.SelectedValue;
                settingsData.EmailFrom = txtEmailFrom.Text;
                settingsData.EmailNotify = txtEmailNotify.Text;
                settingsData.EmailSubject = txtEmailSubject.Text;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        // GET THE DROPDOWN FOR PROPERTY AGENT MODULES

        private void BindModules()
        {

            DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
            ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, "Property Agent");

            foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)
            {
                if (!mi.IsDeleted)
                {
                    DotNetNuke.Entities.Tabs.TabController tabController = new DotNetNuke.Entities.Tabs.TabController();
                    DotNetNuke.Entities.Tabs.TabInfo tabInfo = tabController.GetTab(mi.TabID, this.PortalId);

                    string strPath = tabInfo.TabName.ToString();

                    ListItem objListItem = new ListItem();

                    objListItem.Value = mi.TabID.ToString() + "-" + mi.ModuleID.ToString();
                    objListItem.Text = strPath + " -> " + mi.ModuleTitle.ToString();

                    drpModuleID.Items.Add(objListItem);



                }
            }


            drpModuleID.Items.Insert(0, new ListItem(Localization.GetString("SelectModule", this.LocalResourceFile), "-1"));

        }



    }
}
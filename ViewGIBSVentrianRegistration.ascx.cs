using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using GIBS.GIBSVentrianRegistration.Components;
using DotNetNuke.Common.Lists;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using System.Text;


namespace GIBS.Modules.GIBSVentrianRegistration
{
    public partial class ViewGIBSVentrianRegistration : PortalModuleBase
    {

        public int intRecords = -1;
        static string PropertyID = "";
        static string PropertyAddress = "";
        static string AuctionDate = "";
        static string DepositAmount = "";
        static string terms = "";
        public string MyReturnURL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!IsPostBack)
                {

                    //lblPropertyName.Text = Request.QueryString["Foreclosure"].ToString() + " - " + UserInfo.Profile.Region.ToString();
                    if (Request.QueryString["Foreclosure"] != null)
                    {
                        

                        PropertyID = Request.QueryString["Foreclosure"].ToString();
                        GetPropertyInfo(Convert.ToInt32(PropertyID));

                        GetDropDownListStates();
                        
                        GetTermsAndConditions(); 
                        
                    }

                    if (Request.QueryString["LoginStatus"] != null)
                    {

                        if (Request.QueryString["LoginStatus"].ToString() == "Success")
                        {
                            pnlTerms.Visible = false;

                        }

                    }
                    //NewAccount Just Created
                    // NewAccount
                    if (Request.QueryString["NewAccount"] != null)
                    {

                        if (Request.QueryString["NewAccount"].ToString() == "Success")
                        {
                            pnlTerms.Visible = false;
                         //   lblErrorMessage.Text = "HERE I AM";
                            RegisterUser();
                            RegistrationCheck();
                          //  SetPanels("NewUserRegistered");

                        }

                    }

                }
                else
                {
                    PropertyID = Request.QueryString["Foreclosure"].ToString();
                    
                }

                
                


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void ReturnToProperty()
        {
            try
            {
               

                string _ReturnUrlPath = "";

                
                    _ReturnUrlPath = Settings["returnUrlPath"].ToString();
               

                if (_ReturnUrlPath != "")
                {
                    MyReturnURL = _ReturnUrlPath;
                    string newURL = MyReturnURL + PropertyID.ToString();

                 //   lblErrorMessage.Text = newURL;
                    
                    Response.Redirect(newURL, true);
                       
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }




        protected void RegistrationCheck()
        {
            try
            {
                
                int vPropID = Convert.ToInt32(PropertyID.ToString());
                
                List<GIBSVentrianRegistrationInfo> items;

                GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
                items = controller.Ventrian_PropertyAgent_CommentList(vPropID, UserInfo.UserID);

                if (items.Count > 0)
                {

                    GridRegistrations.DataSource = items;
                    GridRegistrations.DataBind();
                    GridRegistrations.Visible = true;
                    SetPanels("UserRegistered");
                }
                else
                {
                    GridRegistrations.Visible = false;
                    

                
                }




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void SetPanels(string SetPanels)
        {
            try
            {
                // Begin the switch.  
                switch (SetPanels)
                {
                    case "UserRegistered":
                        {
                            lblErrorMessage.Text = "You are registered for this auction!";
                            pnlEmailAddress.Visible = false;
                            PanelRegisterAuction.Visible = false;
                            pnlTerms.Visible = false;
                            break;
                        }
                    case "AuctionNotFound":
                        {
                            pnlEmailAddress.Visible = false;
                            PanelRegisterAuction.Visible = false;
                            pnlTerms.Visible = false;
                            break;
                        }

                    case "Unregister":
                        {
                            pnlEmailAddress.Visible = false;
                            PanelRegisterAuction.Visible = false;
                            pnlTerms.Visible = false;
                            break;
                        }

                    default:
                        // You can use the default case.
                        {
                            break;
                        }
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        protected void RegisterUser()
        {
            try
            {
                GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
                GIBSVentrianRegistrationInfo item = new GIBSVentrianRegistrationInfo();
                item.PropertyID = Convert.ToInt32(PropertyID.ToString());
                item.FullAddress = txtAddress.Text + ", " + txtCity.Text + ", " + ddlStates.SelectedValue + " " + txtZip.Text;
                item.UserID = this.UserId;
                item.CreateDate = DateTime.Now;
                item.FullName = UserInfo.DisplayName.ToString();
                item.Email = UserInfo.Email.ToString();
                item.Telephone = UserInfo.Profile.Telephone.ToString();

                controller.AddGIBSVentrianRegistration(item);

                lblErrorMessage.Text = "Registration Inserted";

                // ADD EMAIL NOTIFICATION

                StringBuilder EmailContent = new StringBuilder();
                EmailContent.Capacity = 5000;

                //EmailContent.Append("<h2>Property: " + PropertyAddress.ToString() + " on " + AuctionDate.ToString() + "</h2>");
                EmailContent.Append("<h2>You have succussfully been pre-registered for this auction!</h2>");
                EmailContent.Append("<p>" + item.FullName + "<br />");

                EmailContent.Append(item.FullAddress + "<br />");
                EmailContent.Append(item.Email + "</p>");
                EmailContent.Append(terms.ToString());

                // terms.ToString()
                string vEmailSubject = "Auction Pre-Registration - " + PropertyAddress.ToString() + " on " + AuctionDate.ToString();

                EmailNotificationHTML(EmailContent.ToString(), vEmailSubject.ToString(), item.Email);
          
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void EmailNotificationHTML(string content, string subject, string emailto)
        {

            try
            {
                //GIBSVentrianRegistrationSettings settingsData = new GIBSVentrianRegistrationSettings(this.TabModuleId);
                string EmailContent = content;

                //EMAIL THE PURCHASER
                string EmailFrom = "";


                if (Settings.Contains("emailFrom"))
                {
                    EmailFrom = Settings["emailFrom"].ToString();
                }
                else
                {
                    EmailFrom = PortalSettings.Email.ToString();
                }


                string _EmailNotify = "";
                if (Settings.Contains("emailNotify"))
                {
                    _EmailNotify = Settings["emailNotify"].ToString();
                }

                // DotNetNuke.Services.Mail.Mail.SendMail(EmailFrom.ToString(), emailto.ToString(), "", subject, EmailContent.ToString(), "", "HTML", "", "", "", "");

                string SMTPUserName = DotNetNuke.Entities.Controllers.HostController.Instance.GetString("SMTPUsername");

               // EmailFrom = SMTPUserName.ToString();

                string[] emptyStringArray = new string[0];

                DotNetNuke.Services.Mail.Mail.SendMail(SMTPUserName.Trim().ToString(), emailto.ToString(), "", "",
                    EmailFrom.ToString(), DotNetNuke.Services.Mail.MailPriority.Normal,
                    subject.ToString(), DotNetNuke.Services.Mail.MailFormat.Html,
                    System.Text.Encoding.ASCII, EmailContent.ToString(), emptyStringArray,
                    "", "", "", "", true);



                string AdminEmailContent = "";



                AdminEmailContent += "<h1>Administrator Copy</h1>" + content.ToString();

                if (_EmailNotify.Length > 0)
                {

                  //  string FromRegisteredPersonEmail = emailto;
                    string emailAddress = _EmailNotify.ToString().Replace(" ", "");
                    string[] valuePair = emailAddress.Split(new char[] { ';' });

                    for (int i = 0; i <= valuePair.Length - 1; i++)
                    {
                     //   DotNetNuke.Services.Mail.Mail.SendMail(SMTPUserName.Trim().ToString(), valuePair[i].ToString().Trim(), "", "Admin Copy - " + subject, AdminEmailContent.ToString(), "", "HTML", "", "", "", "");
                        DotNetNuke.Services.Mail.Mail.SendMail(SMTPUserName.Trim().ToString(), valuePair[i].ToString().Trim(), "", "",
                    EmailFrom.ToString(), DotNetNuke.Services.Mail.MailPriority.Normal,
                   "Admin Copy - " + subject.ToString(), DotNetNuke.Services.Mail.MailFormat.Html,
                    System.Text.Encoding.ASCII, AdminEmailContent.ToString(), emptyStringArray,
                    "", "", "", "", true);
                    }

                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetTermsAndConditions()
        {

            try
            {
                //load the item
                GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
                GIBSVentrianRegistrationInfo item = controller.Ventrian_Registration_Get_AuctionTerms(this.ModuleId, 1);

                terms = item.Content.ToString().Replace("[AUCTIONADDRESS]", PropertyAddress.ToString());
                terms = terms.ToString().Replace("[AUCTIONDATE]", AuctionDate.ToString());
                terms = terms.ToString().Replace("[DEPOSITAMOUNT]", DepositAmount.ToString());

                Literal Literal1 = new Literal();
                Literal1.Text = terms.ToString();

                pnlAuctionTerms.Controls.Add(Literal1);

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void GetUserInfo()
        {

            try
            {
                //this.UserId
                if (Request.IsAuthenticated)
                {
                    btnRegisterForAuction.Visible = true;
                    PanelRegisterAuction.Visible = true;
                    pnlEmailAddress.Visible = false;

                    txtFirstName.Text = UserInfo.FirstName.ToString();
                    txtLastName.Text = UserInfo.LastName.ToString();
                    txtPhoneNumber.Text = UserInfo.Profile.Telephone.ToString();
                    txtEmail.Text = UserInfo.Email.ToString();
                    txtAddress.Text = UserInfo.Profile.Street.ToString();
                    txtCity.Text = UserInfo.Profile.City.ToString();
                    txtZip.Text = UserInfo.Profile.PostalCode.ToString();

                    ListItem liToFind = ddlStates.Items.FindByValue(UserInfo.Profile.Region.ToString());
                    if (liToFind != null)
                    {
                        // value found
                        ddlStates.SelectedValue = UserInfo.Profile.Region.ToString();
                    }
                    else
                    {
                        //Value not found
                    }

                    

                    PanelPassword.Visible = false;
                    lblInstructions.Visible = false;

                    hiddenUserID.Value = UserInfo.UserID.ToString();

                    RegistrationCheck();
      
                }
                else
                {
                    btnRegisterForAuction.Visible = false;
                   // lblInstructions.Text = "Please provide the following information to pre-register for the auction. If you have a registered account with this site please login first.";
                   // PanelPassword.Visible = true;
                
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void GridRegistrations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int commentID = (int)GridRegistrations.DataKeys[e.RowIndex].Value;

            //APCController controller = new APCController();
            GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
            controller.Ventrian_PropertyAgent_CommentDelete(commentID);
            RegistrationCheck();
           // SetPanels("UserNotRegistered");
            lblErrorMessage.Text = "You have successfully unregistered from this auction!";

            SetPanels("Unregister");

            //controller.DeleteAPC(this.ModuleId, itemID);
            //FillGrid();

        }

        public void GetPropertyInfo(int vPropertyID)
        {

            if (!Null.IsNull(vPropertyID))
            {
                GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
                GIBSVentrianRegistrationInfo item = controller.Ventrian_PropertyAgent_GetPropertyAddress(vPropertyID);
                if (item != null)
                {
                    if (item.Address.ToString().Length > 0)
                    {
                        if (item.Unit.ToString().Length > 0)
                        {
                            PropertyAddress = item.Address + " (Unit# " + item.Unit + "), " + item.City + " " + item.State;
                        }
                        else
                        {
                            PropertyAddress = item.Address + ", " + item.City + " " + item.State;
                        }
                        AuctionDate = item.AuctionDate + " at " + item.AuctionTime;
                        decimal vDepositAmount = Convert.ToDecimal(item.DepositAmount);
                        DepositAmount = String.Format("{0:C0}", vDepositAmount);
                        //  DepositAmount = item.DepositAmount;
                        lblPropertyName.Text = PropertyAddress.ToString() + " on " + AuctionDate.ToString();
                        //  Response.Write(DepositAmount);
                        
                        GetUserInfo();
                    }

                    else

                    {

                        lblErrorMessage.Text = "UNABLE TO FIND THAT PAGE";
                        SetPanels("AuctionNotFound");
                    }

                }

            }

        
        }

        public void GetDropDownListStates()

                {

                    try
                    {
                        // Get State Dropdown from DNN Lists

                        var vStates = new ListController().GetListEntryInfoItems("Region", "Country.US", this.PortalId);
                
                        //  State
                        ddlStates.DataTextField = "Value";
                        ddlStates.DataValueField = "Text";
                        ddlStates.DataSource = vStates;
                        ddlStates.DataBind();
                        ddlStates.Items.Insert(0, new ListItem("--", ""));
                        ddlStates.SelectedValue = "Massachusetts";
                    //    ddlStates.SelectedValue = "MA";

                    }
                    catch (Exception ex)
                    {
                        Exceptions.ProcessModuleLoadException(this, ex);
                    }
                
                }

        //#region IActionable Members

        //public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        //{
        //    get
        //    {
        //        //create a new action to add an item, this will be added to the controls
        //        //dropdown menu
        //        ModuleActionCollection actions = new ModuleActionCollection();
        //        actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
        //            ModuleActionType.AddContent, "CommentId=1", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
        //             true, false);

        //        return actions;
        //    }
        //}

        //#endregion


        /// <summary>
        /// Handles the items being bound to the datalist control. In this method we merge the data with the
        /// template defined for this control to produce the result to display to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    
        // NEW REGISTRATION - Create New User, login User and register for auction
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (CreateNewUser(txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtEmail.Text.ToString(), txtPassword.Text.ToString(), "Registered Bidders") == true)

                {


                    GetUserInfo();
                    // CREATE NEW REGISTRATION
                    lblErrorMessage.Text = "SUCCESS - CreateNewUser, hiddenUserID: " + hiddenUserID.Value.ToString();

                }

                else

                {
                    lblErrorMessage.Text = "Problem on registration";
                
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
            
        }



        public bool CreateNewUser(string FirstName, string LastName, string Email, string Password, string AddUserRole)
        {

            try
            {

                UserInfo oUser = new UserInfo();
                oUser.PortalID = this.PortalId;
                oUser.IsSuperUser = false;
                oUser.FirstName = FirstName;
                oUser.LastName = LastName;
                oUser.Email = Email;
                oUser.Username = Email;
                oUser.DisplayName = FirstName + " " + LastName;

                //Fill MINIMUM Profile Items (KEY PIECE)
                oUser.Profile.PreferredLocale = PortalSettings.DefaultLanguage;
                oUser.Profile.PreferredTimeZone = PortalSettings.TimeZone;
                oUser.Profile.FirstName = oUser.FirstName;
                oUser.Profile.LastName = oUser.LastName;
                oUser.Profile.Country = "United States";
                oUser.Profile.Region = ddlStates.SelectedValue.ToString();
                oUser.Profile.Street = txtAddress.Text.ToString();
                oUser.Profile.City = txtCity.Text.ToString();
                oUser.Profile.Telephone = txtPhoneNumber.Text.ToString();
                oUser.Profile.PostalCode = txtZip.Text.ToString();

                //Set Membership
                UserMembership oNewMembership = new UserMembership(oUser);
                oNewMembership.Approved = true;
                oNewMembership.CreatedDate = System.DateTime.Now;

            //    oNewMembership.Email = oUser.Email;
                oNewMembership.IsOnLine = false;
           //     oNewMembership.Username = oUser.Username;
                oNewMembership.Password = Password;

                //Bind membership to user
                oUser.Membership = oNewMembership;
                

                //Add the user, ensure it was successful 
                if (DotNetNuke.Security.Membership.UserCreateStatus.Success == UserController.CreateUser(ref oUser))
                {
                    //Add Role if passed something from module settings

                    if (AddUserRole.Length > 0)
                    {
                        DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();
                        //retrieve role
                        int AuctionPortalID = this.PortalId;
                        string groupName = AddUserRole;

                        DotNetNuke.Security.Roles.RoleInfo ri = rc.GetRoleByName(AuctionPortalID, groupName);
                        rc.AddUserRole(AuctionPortalID, oUser.UserID, ri.RoleID, DotNetNuke.Security.Roles.RoleStatus.Approved, false, DateTime.Today, Null.NullDate);
                    }



                    // LOGIN THE NEWLY CREATED USER
                    DotNetNuke.Entities.Users.UserInfo myNewUser = new DotNetNuke.Entities.Users.UserInfo();
                    DotNetNuke.Security.Membership.UserLoginStatus userLoginStatus = new DotNetNuke.Security.Membership.UserLoginStatus();
                    myNewUser = DotNetNuke.Entities.Users.UserController.ValidateUser(this.PortalId, Email, Password, "", "", GetIPAddress(), ref userLoginStatus);

                    if (userLoginStatus == DotNetNuke.Security.Membership.UserLoginStatus.LOGIN_SUCCESS ||
                       userLoginStatus == DotNetNuke.Security.Membership.UserLoginStatus.LOGIN_SUPERUSER)
                    {
                        //login the user ...
                        DotNetNuke.Entities.Users.UserController.UserLogin(PortalId, myNewUser, "", GetIPAddress(), true);

                        string newURL = Globals.NavigateURL("", "", "NewAccount=Success", "Foreclosure=" + PropertyID.ToString());

                        // Response.Redirect(Request.RawUrl,true);
                        Response.Redirect(newURL, true);
                    }
                    else
                    {
                        lblErrorMessage.Text = "New User Login Failed";
                    }

                    return true;
                }
                else
                {

                    return false;
                }
               


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return false;
            }

        }




        //protected void txtEmail_Change(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        int intRecords = 0;

        //        ArrayList u = new ArrayList();

        //        u = UserController.GetUsersByEmail(this.PortalId, txtEmail.Text.ToString(), 0, 1, ref intRecords);
                

        //        if (u.Count > 0)
        //        {
        //            // Localization.GetString("AlreadyRegisteredAccountMessage", this.LocalResourceFile)
        //            // AlreadyRegisteredAccountMessage.Text
        //            // lblErrorMessage.Text = "Your e-mail address already has a site account. Please login first to proceed.";
        //            lblErrorMessage.Text = Localization.GetString("AlreadyRegisteredAccountMessage", this.LocalResourceFile);
        //            PanelLogin.Visible = true;
        //            //txtUserName.Text = u[7].ToString();
        //            GridView1.Visible = true; 
        //            GridView1.DataSource = u; 
        //            GridView1.DataBind();
        //            GridView1.Visible = true;


        //            txtUserName.Text = GridView1.Rows[0].Cells[11].Text.ToString();
        //            PanelPassword.Visible = false;




        //        }
        //        else
        //        {
                    
        // //           GridView1.Dispose();
        //   //         GridView1.Visible = false;
        //            PanelLogin.Visible = false;

        //            reqFirstName.ValidationGroup = "NewRegister";
        //            reqLastName.ValidationGroup = "NewRegister";
        //            reqAddress.ValidationGroup = "NewRegister";
        //            reqCity.ValidationGroup = "NewRegister";
        //            reqPhoneNumber.ValidationGroup = "NewRegister";

        //        }

        //        pnlTerms.Visible = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
               
        //    }



        //}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
                //validate username/password combination
                DotNetNuke.Entities.Users.UserInfo myUser = new DotNetNuke.Entities.Users.UserInfo();
                DotNetNuke.Security.Membership.UserLoginStatus userLoginStatus = new DotNetNuke.Security.Membership.UserLoginStatus();
                myUser = DotNetNuke.Entities.Users.UserController.ValidateUser(this.PortalId, txtUserName.Text.ToString(), txtLoginPassword.Text.ToString(), "", "", GetIPAddress(), ref userLoginStatus);

           //     Label1.Text += "userLoginStatus: " + userLoginStatus.ToString() + "";

                if (userLoginStatus == DotNetNuke.Security.Membership.UserLoginStatus.LOGIN_SUCCESS ||
                   userLoginStatus == DotNetNuke.Security.Membership.UserLoginStatus.LOGIN_SUPERUSER)
                {
                    //login the user ...
                    DotNetNuke.Entities.Users.UserController.UserLogin(PortalId, myUser, "", "", true);

                    string newURL = Globals.NavigateURL("", "", "LoginStatus=Success", "Foreclosure=" + PropertyID.ToString());
                    
                   // Response.Redirect(Request.RawUrl,true);
                    Response.Redirect(newURL, true);

                }
                else
                {
                    //loggedIn = false;
                    //loginFailed = true;
                    lblErrorMessage.Text = "Login Failed";
                }


        }



        private string GetIPAddress()
        {
            string sIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (sIPAddress == "")
            {
                sIPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return sIPAddress;
        }

        protected void btnCheckEmail_Click(object sender, EventArgs e)
        {
            try
            {
                int intRecords = 0;

                ArrayList u = new ArrayList();

                u = UserController.GetUsersByEmail(this.PortalId, txtEmail.Text.ToString(), 0, 1, ref intRecords);

                if (u.Count > 0)
                {
                  
                    // lblErrorMessage.Text = "That e-mail address is already registered. Please login first.";
                    lblErrorMessage.Text = Localization.GetString("AlreadyRegisteredAccountMessage", this.LocalResourceFile);
                    PanelLogin.Visible = true;

                    GridView1.Visible = true; 
                    GridView1.DataSource = u;
                    GridView1.DataBind();

                    txtUserName.Text = GridView1.Rows[0].Cells[1].Text.ToString();
                    PanelPassword.Visible = false;
                    btnCheckEmail.Visible = false;
                    PanelRegisterAuction.Visible = false;

                    hiddenUserID.Value = GridView1.Rows[0].Cells[0].Text.ToString();
                    lblInstructions.Visible = false;
                   
                    GridView1.Visible = false;

                }
                else
                {
                    
                    GridView1.Dispose();
                    GridView1.Visible = false;
                    PanelLogin.Visible = false;
                    PanelRegisterAuction.Visible = true;
                    btnRegister.Visible = true;
                    PanelPassword.Visible = true;
                    btnCheckEmail.Visible = false;
                    

                    reqFirstName.ValidationGroup = "NewRegister";
                    reqLastName.ValidationGroup = "NewRegister";
                    reqAddress.ValidationGroup = "NewRegister";
                    reqCity.ValidationGroup = "NewRegister";
                    reqPhoneNumber.ValidationGroup = "NewRegister";
                }

                pnlTerms.Visible = false;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);

            }
        }

        protected void lbPasswordReminder_Click(object sender, EventArgs e)
        {
            UserInfo p = new UserInfo();

            p = UserController.GetUserById(PortalId, Convert.ToInt32(hiddenUserID.Value.ToString()));
            DotNetNuke.Entities.Users.UserController.ResetPasswordToken(p);
            DotNetNuke.Services.Mail.Mail.SendMail(p, DotNetNuke.Services.Mail.MessageType.PasswordReminder, PortalSettings);

            lblErrorMessage.Text = "Your password reset link has been e-mailed to you.";

        }


        // ALREADY REGISTERED
        protected void btnRegisterForAuction_Click(object sender, EventArgs e)
        {
            UpdateUser(UserInfo.UserID);
            RegisterUser();
            RegistrationCheck();
            
          //  txtEmail.ValidationGroup = "PreRegister";
          //  GIBSVentrianRegistrationInfo item = new GIBSVentrianRegistrationInfo();
          //  item.UserID = Convert.ToInt32(hiddenUserID.Value.ToString());

        }


        public void UpdateUser(int UserID)
        {
            try
            {
                //  DotNetNuke.Entities.Users.UserInfo uUser = DotNetNuke.Entities.Users.UserController.GetUserById(PortalSettings.PortalId, UserID);

                UserController objUserController = new UserController();
                UserInfo uUser = objUserController.GetUser(this.PortalId, UserID);

                uUser.FirstName = txtFirstName.Text.ToString();
                uUser.LastName = txtLastName.Text.ToString();
                uUser.Email = txtEmail.Text.ToString();

                uUser.Profile.Street = txtAddress.Text.ToString();
                uUser.Profile.Telephone = txtPhoneNumber.Text.ToString();
            //    uUser.Profile.Cell = txtCellPhone.Text.ToString();
            //    uUser.Profile.Fax = txtFax.Text.ToString();
                uUser.Profile.PostalCode = txtZip.Text.ToString();
                uUser.Profile.Region = ddlStates.SelectedValue.ToString();
                uUser.Profile.Country = "United States";


                UserController.UpdateUser(PortalSettings.PortalId, uUser);

                //     lblErrorMessage.Text = "Record Successully Updated";

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            ReturnToProperty();
        }

    }
}
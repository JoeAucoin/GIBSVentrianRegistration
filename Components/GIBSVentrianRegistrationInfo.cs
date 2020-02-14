using System;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace GIBS.GIBSVentrianRegistration.Components
{
    public class GIBSVentrianRegistrationInfo
    {
        //private vars exposed thro the
        //properties
        private int moduleId;
        private int commentID;
        private int propertyID;
        private int userId;
        private int regUserId;
        private string fullname;
        private string fulladdress;
        private string email;
        private string telephone;
        private int createdByUser;
        private DateTime createDate;
        private string createdByUserName = null;
        private string address;
        private string city;
        private string state;
        private string auctiondate;
        private string auctiontime;
        private string content;
        private int itemId;
        private string depositAmount;
        private string unit;


        /// <summary>
        /// empty cstor
        /// </summary>
        public GIBSVentrianRegistrationInfo()
        {
        }


        #region properties

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public int CommentID
        {
            get { return commentID; }
            set { commentID = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }


        public string AuctionDate
        {
            get { return auctiondate; }
            set { auctiondate = value; }
        }



        public string AuctionTime
        {
            get { return auctiontime; }
            set { auctiontime = value; }
        }

        public int UserID
        {

            get { return userId; }
            set { userId = value; }
        }

        public int RegUserID
        {

            get { return regUserId; }
            set { regUserId = value; }
        }

        public string DepositAmount
        {
            get { return depositAmount; }
            set { depositAmount = value; }
        }

        public int PropertyID
        {
            get { return propertyID; }
            set { propertyID = value; }
        }

        public string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }
        public string FullAddress
        {
            get { return fulladdress; }
            set { fulladdress = value; }
        }
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        //public string CreatedByUserName
        //{
        //    get
        //    {
        //        if (createdByUserName == null)
        //        {
        //            int portalId = PortalController.GetCurrentPortalSettings().PortalId;
        //            UserInfo user = UserController.GetUser(portalId, createdByUser, false);
        //            createdByUserName = user.DisplayName;
        //        }

        //        return createdByUserName;
        //    }
        //}

        public string CreatedByUserName
        {
            get
            {
                if (createdByUserName == null)
                {
                    int portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
                    UserController controller = new UserController();
                    UserInfo user = controller.GetUser(portalId, createdByUser);
                    if (user != null)
                    {
                        createdByUserName = user.DisplayName;
                    }
                    else
                    {
                        createdByUserName = "Deleted User";
                    }

                }
                return createdByUserName;
            }
        }

        #endregion
    }
}

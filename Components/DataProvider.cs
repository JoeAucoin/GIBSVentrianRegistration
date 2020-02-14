using System;
using System.Data;
using DotNetNuke;
using DotNetNuke.Framework;

namespace GIBS.GIBSVentrianRegistration.Components
{
    public abstract class DataProvider
    {

        #region common methods

        /// <summary>
        /// var that is returned in the this singleton
        /// pattern
        /// </summary>
        private static DataProvider instance = null;

        /// <summary>
        /// private static cstor that is used to init an
        /// instance of this class as a singleton
        /// </summary>
        static DataProvider()
        {
            instance = (DataProvider)Reflection.CreateObject("data", "GIBS.GIBSVentrianRegistration.Components", "");
        }

        /// <summary>
        /// Exposes the singleton object used to access the database with
        /// the conrete dataprovider
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return instance;
        }

        #endregion


        #region Abstract methods

        /* implement the methods that the dataprovider should */

        //public abstract IDataReader GetGIBSVentrianRegistrations(int moduleId);
        //public abstract void UpdateGIBSVentrianRegistration(int moduleId, int itemId, string content, int userId);

        public abstract IDataReader Ventrian_PropertyAgent_CommentList(int propertyId, int regUserID);
        public abstract void Ventrian_PropertyAgent_CommentAdd(int propertyID, int userId, string fulladdress, DateTime createDate, string fullname, string email, string telephone);

        // (int propertyID, int userId, string fulladdress, DateTime createDate, string fullname, string email, string telephone)
        
        public abstract void Ventrian_PropertyAgent_CommentDelete(int commentID);

        public abstract IDataReader Ventrian_PropertyAgent_GetPropertyAddress(int propertyID);

        public abstract void Ventrian_Registration_Add_AuctionTerms(int moduleId, string content, int createdByUser);

        public abstract IDataReader Ventrian_Registration_Get_AuctionTerms(int moduleId, int itemId);

        public abstract void Ventrian_Registration_Update_AuctionTerms(int moduleId, int itemId, string content, int createdByUser);

        #endregion

    }



}

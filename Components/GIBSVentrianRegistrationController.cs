using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace GIBS.GIBSVentrianRegistration.Components
{
    public class GIBSVentrianRegistrationController  
    {

        #region public method

        

        public List<GIBSVentrianRegistrationInfo> Ventrian_PropertyAgent_CommentList(int propertyID, int regUserID)
        {
            return CBO.FillCollection<GIBSVentrianRegistrationInfo> (DataProvider.Instance().Ventrian_PropertyAgent_CommentList(propertyID, regUserID));
        }

        public GIBSVentrianRegistrationInfo Ventrian_PropertyAgent_GetPropertyAddress(int propertyID)
        {
            return (GIBSVentrianRegistrationInfo)CBO.FillObject(DataProvider.Instance().Ventrian_PropertyAgent_GetPropertyAddress(propertyID), typeof(GIBSVentrianRegistrationInfo));
        }


        public void AddGIBSVentrianRegistration(GIBSVentrianRegistrationInfo info)
        {
            //check we have some content to store
            if (info.FullAddress != string.Empty)
            {
                DataProvider.Instance().Ventrian_PropertyAgent_CommentAdd(info.PropertyID, info.UserID, info.FullAddress, info.CreateDate, info.FullName, info.Email, info.Telephone);
            }
        }


        public void Ventrian_Registration_Add_AuctionTerms(GIBSVentrianRegistrationInfo info)
        {
            //check we have some content to store
            if (info.Content != string.Empty)
            {
                DataProvider.Instance().Ventrian_Registration_Add_AuctionTerms(info.ModuleId, info.Content, info.CreatedByUser);
            }
        }

        public void Ventrian_Registration_Update_AuctionTerms(GIBSVentrianRegistrationInfo info)
        {
            //check we have some content to update
            if (info.Content.ToString() != string.Empty)
            {
                DataProvider.Instance().Ventrian_Registration_Update_AuctionTerms(info.ModuleId, info.ItemId, info.Content, info.CreatedByUser);
            }
        }

        public GIBSVentrianRegistrationInfo Ventrian_Registration_Get_AuctionTerms(int moduleId, int itemId)
        {
            return (GIBSVentrianRegistrationInfo)CBO.FillObject(DataProvider.Instance().Ventrian_Registration_Get_AuctionTerms(moduleId, itemId), typeof(GIBSVentrianRegistrationInfo));
        }


        public void Ventrian_PropertyAgent_CommentDelete(int commentID)
        {
            DataProvider.Instance().Ventrian_PropertyAgent_CommentDelete(commentID);
        }


        #endregion

        #region ISearchable Members

        /// <summary>
        /// Implements the search interface required to allow DNN to index/search the content of your
        /// module
        /// </summary>
        /// <param name="modInfo"></param>
        /// <returns></returns>
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(ModuleInfo modInfo)
        //{
        //    SearchItemInfoCollection searchItems = new SearchItemInfoCollection();

        //    List<GIBSVentrianRegistrationInfo> infos = GetGIBSVentrianRegistrations(modInfo.ModuleID);

        //    foreach (GIBSVentrianRegistrationInfo info in infos)
        //    {
        //        SearchItemInfo searchInfo = new SearchItemInfo(modInfo.ModuleTitle, info.FullAddress, info.CreatedByUser, info.CreateDate,
        //                                            modInfo.ModuleID, info.CommentID.ToString(), info.FullAddress, "Item=" + info.CommentID.ToString());
        //        searchItems.Add(searchInfo);
        //    }

        //    return searchItems;
        //}

        #endregion

        #region IPortable Members

        /// <summary>
        /// Exports a module to xml
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        //public string ExportModule(int moduleID)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    List<GIBSVentrianRegistrationInfo> infos = Ventrian_PropertyAgent_CommentList(1,1);

        //    if (infos.Count > 0)
        //    {
        //        sb.Append("<GIBSVentrianRegistrations>");
        //        foreach (GIBSVentrianRegistrationInfo info in infos)
        //        {
        //            sb.Append("<GIBSVentrianRegistration>");
        //            sb.Append("<content>");
        //            sb.Append(XmlUtils.XMLEncode(info.FullAddress));
        //            sb.Append("</content>");
        //            sb.Append("</GIBSVentrianRegistration>");
        //        }
        //        sb.Append("</GIBSVentrianRegistrations>");
        //    }

        //    return sb.ToString();
        //}

        /// <summary>
        /// imports a module from an xml file
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="Content"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //    XmlNode infos = DotNetNuke.Common.Globals.GetContent(Content, "GIBSVentrianRegistrations");

        //    foreach (XmlNode info in infos.SelectNodes("GIBSVentrianRegistration"))
        //    {
        //        GIBSVentrianRegistrationInfo GIBSVentrianRegistrationInfo = new GIBSVentrianRegistrationInfo();
        //        GIBSVentrianRegistrationInfo.ModuleId = ModuleID;
        //        GIBSVentrianRegistrationInfo.FullAddress = info.SelectSingleNode("content").InnerText;
        //        GIBSVentrianRegistrationInfo.CreatedByUser = UserID;

        //        AddGIBSVentrianRegistration(GIBSVentrianRegistrationInfo);
        //    }
        //}

        #endregion
    }
}

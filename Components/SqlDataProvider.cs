using System;
using System.Data;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace GIBS.GIBSVentrianRegistration.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "GIBS_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {
            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion

        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region override methods

        //public override IDataReader GetGIBSVentrianRegistrations(int moduleId)
        //{
        //    return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetGIBSVentrianRegistrations"), moduleId);
        //}

        public override IDataReader Ventrian_PropertyAgent_CommentList(int propertyID, int regUserID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Ventrian_PropertyAgent_CommentList"), propertyID, regUserID);
        }

        public override void Ventrian_PropertyAgent_CommentAdd(int propertyID, int userId, string fulladdress, DateTime createDate, string fullname, string email, string telephone)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Ventrian_PropertyAgent_CommentAdd"), propertyID, userId, fulladdress, createDate, fullname, email, telephone);
        }

        //public override void UpdateGIBSVentrianRegistration(int moduleId, int itemId, string content, int userId)
        //{
        //    SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("UpdateGIBSVentrianRegistration"), moduleId, itemId, content, userId);
        //}

        public override void Ventrian_PropertyAgent_CommentDelete(int commentID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Ventrian_PropertyAgent_CommentDelete"), commentID);
        }

        public override IDataReader Ventrian_PropertyAgent_GetPropertyAddress(int propertyID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Ventrian_PropertyAgent_GetPropertyAddress"), propertyID);
        }

        public override void Ventrian_Registration_Add_AuctionTerms(int moduleId, string content, int createdByUser)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Ventrian_Registration_Add_AuctionTerms"), moduleId, content, createdByUser);
        }

        public override IDataReader Ventrian_Registration_Get_AuctionTerms(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("Ventrian_Registration_Get_AuctionTerms"), moduleId, itemId);
        }

        public override void Ventrian_Registration_Update_AuctionTerms(int moduleId, int itemId, string content, int createdByUser)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("Ventrian_Registration_Update_AuctionTerms"), moduleId, itemId, content, createdByUser);
        }

        #endregion
    }
}

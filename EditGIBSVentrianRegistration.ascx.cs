using System;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using GIBS.GIBSVentrianRegistration.Components;

namespace GIBS.Modules.GIBSVentrianRegistration
{
    public partial class EditGIBSVentrianRegistration : PortalModuleBase
    {

       // int commentId = Null.NullInteger;
        int itemId = Null.NullInteger;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                
                
                
                //if (Request.QueryString["CommentId"] != null)
                //{
                //    commentId = Int32.Parse(Request.QueryString["CommentId"]);
                    
                //}

                if (!IsPostBack)
                {
                    //load the data into the control the first time
                    //we hit this page
                    itemId = 1;


                    cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");


                    //check we have an item to lookup
                    if (!Null.IsNull(itemId))
                    {
                        //load the item
                        GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
                        GIBSVentrianRegistrationInfo item = controller.Ventrian_Registration_Get_AuctionTerms(this.ModuleId, itemId);

                        if (item != null)
                        {
                            txtContent.Text = item.Content;
                            ctlAudit.CreatedByUser = item.CreatedByUserName;
                            ctlAudit.CreatedDate = item.CreateDate.ToLongDateString();
                        }
                        else
                        {
                            itemId = Null.NullInteger;
                            txtContent.Text = @"<h1 style=""text-align: center;"">Auction Terms &amp; Conditions</h1>
                                                <table border=""0"" cellspacing=""0"" cellpadding=""0"">
                                                    <tbody>
                                                        <tr>
                                                            <td valign=""top""><strong>Subject Property:&nbsp;
                                                            </strong></td>
                                                            <td>[AUCTIONADDRESS]</td>
                                                        </tr>
                                                        <tr>
                                                            <td valign=""top""><strong>Auction Date:&nbsp;
                                                            </strong></td>
                                                            <td>[AUCTIONDATE]</td>
                                                        </tr>
                                                        <tr>
                                                            <td valign=""top""><strong>Deposit Amount:&nbsp;
                                                            </strong></td>
                                                            <td>[DEPOSITAMOUNT]</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <p>The following terms are believed to be standard in the auction industry.
                                                Terms of sale for each individual auction will vary and the documents signed
                                                by the successful bidder at any auction shall supercede and take precedence
                                                over all other information including, but not limited to, the terms stated below.</p>
                                                <ol>
                                                    <li>All prospective Buyers are required to register
                                                    in order to Bid and to obtain a Bid Number.</li>
                                                    <li>The Property Auction sale is not contingent
                                                    on the Buyer obtaining financing, nor conditional
                                                    upon inspection, and will not be extended for any
                                                    purposes whatsoever.</li>
                                                    <li>If you are a successful bidder, a non-refundable
                                                    deposit of [DEPOSITAMOUNT] will be due and payable at the
                                                    conclusion of the Auction in the form of certified
                                                    check, money order, or bank draft. The money will
                                                    be held in a non-interest bearing account. At the
                                                    end of the Auction, the successful Bidder will immediately
                                                    enter into a Real Estate Purchase Contract, and
                                                    be bound to the Terms &amp; Conditions as set forth
                                                    in the Purchase Contract. Bidders who subsequently
                                                    fail to close the sale for any and all reasons will
                                                    be required to release their entire deposit to the
                                                    Seller as liquidated damages.</li>
                                                    <li>All Property(s) is being sold ""AS IS"" condition.
                                                    Bidders are encouraged to review all Property details,
                                                    zoning restrictions, compliance certificate, inspection
                                                    reports, and any other information prior to attending
                                                    the Auction.</li>
                                                    <li>All sales are final.</li>
                                                    <li>The Auction sale will begin promptly at the
                                                    Property location at the appointed date and time,
                                                    unless a condition prevents such a sale to take
                                                    place, in which case the Auctioneer will make a
                                                    decision on where and when.</li>
                                                    <li>Descriptions made by the Auction Company are
                                                    made as accurately as possible in its website and
                                                    advertisements, and are provided for information
                                                    purposes only and should be verified by the Bidder.
                                                    The Auction Company, the Designated Brokers, Seller
                                                    or their Agents cannot be held liable for any errors
                                                    or omissions whatsoever.</li>
                                                    <li>The Auction Company reserves the right to deny
                                                    any person admittance to, or expel any person from
                                                    the Auction premises, or to withdraw any or all
                                                    Property(s), or to change the Terms &amp; Conditions,
                                                    or to reopen bidding on the Property, or may designate
                                                    one of the Bidders as the successful Bidder, at
                                                    any time prior to or during the course of the Auction,
                                                    without just cause.</li>
                                                    <li>Bidders acknowledge that they are on the Sale
                                                    site at their own risk. No person shall have any
                                                    claims against the Auction Company, its staff and
                                                    representatives, including its Agents for any injuries
                                                    sustained or for any damages to or loss of property
                                                    which may occur.</li>
                                                    <li>In the case of dispute, the Auctioneer's decision
                                                    shall be final.</li>
                                                </ol>
                                                <div style=""margin: 10px 20px; padding: 0px 8px; border: 1px solid #000000; text-align: left; color: #000000; font-family: verdana; font-size: 12px; background-color: #feffb5;"">
                                                <blockquote>
                                                <p>By submitting this form, you as a successful Bidder agree
                                                to the Terms &amp; Conditions as stated by the Auction Company.
                                                Any other Terms &amp; Conditions announced on the Auction Day
                                                would take precedence over printed matter or statements
                                                made previously. I HAVE READ AND APPROVED OF THESE TERMS &amp; CONDITIONS, and ACKNOWLEDGE THAT THE INFORMATION IN MY REGISTRATION PROFILE IS ACCURATE AND TRUE</p>
                                                </blockquote>
                                                </div>";
                            
                            //item.Content = myString;
                            //item.ModuleId = this.ModuleId;
                            //item.CreatedByUser = this.UserId;
                            //controller.Ventrian_Registration_Add_AuctionTerms(item);
                            //txtContent.Text = item.Content;
                        
                        }
                        //    Response.Redirect(Globals.NavigateURL(), true);
                    }
                    else
                    {
                        cmdDelete.Visible = false;
                        ctlAudit.Visible = false;
                    }


                    ////check we have an item to lookup
                    //if (!Null.IsNull(commentId))
                    //{
                    //    //load the item
                    //    GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
                    //    GIBSVentrianRegistrationInfo item = controller.Ventrian_PropertyAgent_CommentList(this.ModuleId, commentId);

                    //    if (item != null)
                    //    {
                    //        txtContent.Text = item.FullAddress;
                    //        ctlAudit.CreatedByUser = item.CreatedByUserName;
                    //        ctlAudit.CreatedDate = item.CreateDate.ToLongDateString();
                    //    }
                    //    else
                    //        Response.Redirect(Globals.NavigateURL(), true);
                    //}
                    //else
                    //{
                    //    cmdDelete.Visible = false;
                    //    ctlAudit.Visible = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
                GIBSVentrianRegistrationInfo item = new GIBSVentrianRegistrationInfo();
                item.ItemId = itemId;
                item.Content = txtContent.Text;
                
                item.ModuleId = this.ModuleId;
                item.CreatedByUser = this.UserId;
                

                //determine if we are adding or updating
                if (Null.IsNull(item.ItemId))
                    controller.Ventrian_Registration_Add_AuctionTerms(item);
                else
                    controller.Ventrian_Registration_Update_AuctionTerms(item);

              //  Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!Null.IsNull(commentId))
                //{
                //    GIBSVentrianRegistrationController controller = new GIBSVentrianRegistrationController();
                //    controller.Ventrian_PropertyAgent_CommentDelete(commentId);
                //    Response.Redirect(Globals.NavigateURL(), true);
                //}
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}
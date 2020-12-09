<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.GIBSVentrianRegistration.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>

<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>

<div class="dnnForm" id="form-settings">

    <fieldset>

        <dnn:sectionhead id="sectGeneralSettings" cssclass="Head" runat="server" text="General Settings" section="GeneralSection"
            includerule="True" isexpanded="True">
        </dnn:sectionhead>

        <div id="GeneralSection" runat="server">

            <div class="dnnFormItem">
                <dnn:label id="lblReturnUrlPath" runat="server" controlname="txtReturnUrlPath" suffix=":" />
                <asp:TextBox ID="txtReturnUrlPath" runat="server" />
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblAuctionModuleID" runat="server" suffix=":" controlname="drpModuleID" />
                <asp:DropDownList ID="drpModuleID" runat="server" DataValueField="ModuleID" DataTextField="ModuleTitle"
                    CssClass="NormalTextBox" AutoPostBack="True">
                </asp:DropDownList>
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblEmailFrom" runat="server" controlname="txtEmailFrom" suffix=":" />
                <asp:TextBox ID="txtEmailFrom" CssClass="NormalTextBox" runat="server" />
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblEmailNotify" runat="server" controlname="txtEmailNotify" suffix=":" />
                <asp:TextBox ID="txtEmailNotify" CssClass="NormalTextBox" runat="server" />
            </div>

            <div class="dnnFormItem">
                <dnn:label id="lblEmailSubject" runat="server" controlname="txtEmailSubject" suffix=":" />
                <asp:TextBox ID="txtEmailSubject" CssClass="NormalTextBox" runat="server" />
            </div>


        </div>


    </fieldset>

</div>

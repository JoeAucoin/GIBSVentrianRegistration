<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.GIBSVentrianRegistration.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<table cellspacing="0" cellpadding="2" border="0" summary="ModuleName1 Settings Design Table">


	<tr>
		<td class="SubHead" width="150"><dnn:label id="lblAuctionModuleID" runat="server" suffix=":" controlname="drpModuleID"></dnn:label></td>
		<td valign="bottom">
			<asp:dropdownlist id="drpModuleID" Runat="server" Width="325" datavaluefield="ModuleID" datatextfield="ModuleTitle"
				CssClass="NormalTextBox" AutoPostBack="True"></asp:dropdownlist>
		</td>
	</tr>

	<tr>
        <td class="SubHead" valign="top"><dnn:label id="lblEmailFrom" runat="server" controlname="txtEmailFrom" suffix=":"></dnn:label></td>
        <td valign="top"><asp:TextBox ID="txtEmailFrom" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="SubHead" valign="top"><dnn:label id="lblEmailNotify" runat="server" controlname="txtEmailNotify" suffix=":"></dnn:label></td>
        <td valign="top"><asp:TextBox ID="txtEmailNotify" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox></td>
    </tr>

    <tr>
        <td class="SubHead" valign="top"><dnn:label id="lblEmailSubject" runat="server" controlname="txtEmailSubject" suffix=":"></dnn:label></td>
        <td valign="top"><asp:TextBox ID="txtEmailSubject" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox></td>
    </tr>


</table>
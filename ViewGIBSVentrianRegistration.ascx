<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewGIBSVentrianRegistration.ascx.cs"
    Inherits="GIBS.Modules.GIBSVentrianRegistration.ViewGIBSVentrianRegistration" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<style type="text/css">
    .style1
    {
        border-style: solid;
        border-width: 0px;
        
    }
</style>
<script type="text/javascript" language="javascript">
    function CallPrint(strid) {
        var prtContent = document.getElementById(strid);
        //var WinPrint = window.open('');
        var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
        WinPrint.document.write(prtContent.innerHTML);
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();
        // prtContent.innerHTML = strOldOne;
    }

</script>
<h1><asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label></h1>
<h2>Auction Pre-Registration: <asp:Label ID="lblPropertyName" runat="server" Text=""></asp:Label></h2>
<dnn:Label ID="lblInstructions" runat="server" CssClass="Normal"></dnn:Label>

<asp:Panel ID="pnlTerms" runat="server">
    <p style="text-align: right">
        <a href="javascript:CallPrint('<% = pnlAuctionTerms.ClientID %>')">Print Terms & Conditions</a></p>
    <asp:Panel ID="pnlAuctionTerms" runat="server" Height="200px" ScrollBars="Vertical"
        Width="100%">
    </asp:Panel>
</asp:Panel>

<asp:Panel ID="pnlEmailAddress" runat="server">
<br />
<div class="dnnForm" id="form-registration-email">

    <fieldset>

	<div class="dnnFormItem">					
                <dnn:Label ID="lblEmail" runat="server" ControlName="txtEmail" Suffix=":" CssClass="Normal">
                </dnn:Label>
            
			<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
			<asp:RequiredFieldValidator runat="server" id="reqEmail" resourcekey="reqEmail" controltovalidate="txtEmail" CssClass="dnnFormMessage dnnFormError" errormessage="E-Mail Address Required!" ValidationGroup="Email" Display="Dynamic" />
 <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" Display="Dynamic"
                    ErrorMessage="A valid e-mail address is required!" 
                    ControlToValidate="txtEmail" CssClass="dnnFormMessage dnnFormError"  
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ValidationGroup="Email"></asp:RegularExpressionValidator>
				
	</div>	
    </fieldset>

</div>
<div style="text-align:center;">
    <asp:Button ID="btnCheckEmail" runat="server" Text="Next" CssClass="dnnPrimaryAction" 
        OnClick="btnCheckEmail_Click" ValidationGroup="Email" />
</div>
</asp:Panel>
<asp:Panel ID="PanelRegisterAuction" runat="server" Visible="false">
<div class="dnnForm" id="form-registration-auction">

    <fieldset>
 
	<div class="dnnFormItem">					
	                <dnn:Label ID="lblFirstName" runat="server" ControlName="txtFirstName" Suffix=":"
                    CssClass="Normal"></dnn:Label>
            
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="reqFirstName" runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtFirstName" 
            ErrorMessage="First Name Required" ValidationGroup="PreRegister" Display="Dynamic" resourcekey="reqFirstName" />	
		
	</div>	



    <div class="dnnFormItem">					
<dnn:Label ID="lblLastName" runat="server" ControlName="txtLastName" Suffix=":" CssClass="Normal">
                </dnn:Label>
            
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="reqLastName" runat="server" CssClass="dnnFormMessage dnnFormError" resourcekey="reqLastName"
            ControlToValidate="txtLastName" ErrorMessage="Last Name Required" ValidationGroup="PreRegister" Display="Dynamic" />	
    </div>


	<div class="dnnFormItem">					
            <dnn:Label ID="lblAddress" runat="server" ControlName="txtAddress" Suffix=":" CssClass="Normal">
                </dnn:Label>
            
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="reqAddress" runat="server" CssClass="dnnFormMessage dnnFormError" 
                    ControlToValidate="txtAddress" ErrorMessage="Address Required" resourcekey="reqAddress" Display="Dynamic"
                    ValidationGroup="PreRegister" />	
    </div>

	<div class="dnnFormItem">					
              <dnn:Label ID="lblCityStateZip" runat="server" ControlName="txtCity" Suffix=":" CssClass="Normal">
                </dnn:Label>
            
            <asp:TextBox ID="txtCity" runat="server" Width="210px"></asp:TextBox>&nbsp;<asp:DropDownList
                    ID="ddlStates" runat="server" meta:resourcekey="ddlStates" Width="65px">
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="txtZip" runat="server" Width="56px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="reqZip" runat="server" CssClass="dnnFormMessage dnnFormError" resourcekey="reqZip" ControlToValidate="txtZip"
             ErrorMessage="Zip Code Required" ValidationGroup="NewRegister" Display="Dynamic" />
	       
            <asp:RequiredFieldValidator ID="reqCity" runat="server" CssClass="dnnFormMessage dnnFormError" resourcekey="reqCity" 
            ControlToValidate="txtCity" ErrorMessage="City, State & Zip Required" ValidationGroup="PreRegister" Display="Dynamic" />

        
    </div>		
	
	<div class="dnnFormItem">					
                <dnn:Label ID="lblPhoneNumber" runat="server" ControlName="txtPhoneNumber" Suffix=":"
                    CssClass="Normal"></dnn:Label>
            
            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="reqPhoneNumber" runat="server" CssClass="dnnFormMessage dnnFormError" 
                    ControlToValidate="txtPhoneNumber" ErrorMessage="Phone Number Required" resourcekey="reqPhoneNumber"
                    ValidationGroup="PreRegister" Display="Dynamic" />	
    </div>	


        			


    </fieldset>

</div>

<div style="text-align:center;">
    <asp:Button ID="btnRegisterForAuction" runat="server" Text="Register For Auction" CssClass="dnnPrimaryAction"
        OnClick="btnRegisterForAuction_Click" ValidationGroup="PreRegister" />
</div>
</asp:Panel>
<asp:Panel ID="PanelLogin" runat="server" Visible="false">
<div class="dnnForm" id="form-registration-login">

    <fieldset>
	
	<div class="dnnFormItem">					
                <dnn:Label ID="lblUserName" runat="server" ControlName="txtUserName" Suffix=":" CssClass="Normal">
                </dnn:Label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>	
	</div>	



    <div class="dnnFormItem">					
<dnn:Label ID="lblLoginPassword" runat="server" ControlName="txtLoginPassword" Suffix=":"
                    CssClass="Normal"></dnn:Label>
            
			<asp:TextBox ID="txtLoginPassword" TextMode="password" runat="server"></asp:TextBox><br />
                <div style="text-align: right;"><asp:LinkButton ID="lbPasswordReminder" runat="server" OnClick="lbPasswordReminder_Click">(E-Mail a Password Reset Link)</asp:LinkButton></div>
            
			<asp:RequiredFieldValidator ID="reqLoginPassword" runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtLoginPassword" ErrorMessage="Password Required" ValidationGroup="LoginUser" Display="Dynamic" />
    </div>

    </fieldset>

</div>
   
   <div style="text-align:center;">
    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" ValidationGroup="LoginUser" CssClass="dnnPrimaryAction" />
    </div>
</asp:Panel>
<asp:Panel ID="PanelPassword" runat="server" Visible="false">
<div class="dnnForm" id="form-registration-createpassword">

    <fieldset>
 
 <div>A site account will automatically be created for you. Please enter a password of your choice. </div>
 
    <div class="dnnFormItem">					
                <dnn:Label ID="lblPassword" runat="server" ControlName="txtPassword" Suffix=":" CssClass="Normal">
                </dnn:Label>

                <asp:TextBox ID="txtPassword" TextMode="password" runat="server"></asp:TextBox>
           <asp:RegularExpressionValidator ID="passwordLength" runat="server" CssClass="dnnFormMessage dnnFormError"  ControlToValidate="txtPassword" ErrorMessage="Minimum password length is 7 characters" ValidationExpression=".{7}.*" Display="Dynamic" /><asp:RequiredFieldValidator ID="req6" runat="server" ControlToValidate="txtPassword" CssClass="dnnFormMessage dnnFormError" ErrorMessage="Password Required" ValidationGroup="NewRegister" />

	
    </div>


	<div class="dnnFormItem">					
<dnn:Label ID="lblVerifyPassword" runat="server" ControlName="txtVerifyPassword"
                    Suffix=":" CssClass="Normal"></dnn:Label>
    
                <asp:TextBox ID="txtVerifyPassword" TextMode="password" runat="server" ValidationGroup="NewRegister"></asp:TextBox>
            
			<asp:CompareValidator runat="server" ID="Comp1" CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtPassword" ControlToCompare="txtVerifyPassword" Text="Verify Password must match!" Display="Dynamic" />
	
	
    </div>



    </fieldset>

</div>
    <div style="text-align:center;">
    <asp:Button ID="btnRegister" runat="server" Text="Pre-Register for Auction" OnClick="btnRegister_Click" ValidationGroup="NewRegister" CssClass="dnnPrimaryAction" />
    </div>
</asp:Panel>

<asp:GridView ID="GridRegistrations" runat="server" EnableModelValidation="True"
    DataKeyNames="CommentID" OnRowDeleting="GridRegistrations_RowDeleting" resourcekey="GridRegistrations"
    AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-list">
    
    <Columns>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("CommentID") %>' CommandName="Delete"
                    runat="server">
             Unregister</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Name" DataField="FullName"></asp:BoundField>
        <asp:BoundField HeaderText="E-Mail Address" DataField="Email"></asp:BoundField>
        <asp:BoundField HeaderText="Address" DataField="FullAddress"></asp:BoundField>
        <asp:BoundField HeaderText="Telephone" DataField="Telephone"></asp:BoundField>
        <asp:BoundField HeaderText="Date Registered" DataField="CreateDate" DataFormatString="{0:d}"></asp:BoundField>
    </Columns>
</asp:GridView>


<p style="text-align: center"><asp:LinkButton ID="LinkButtonCancel" runat="server" 
    onclick="LinkButtonCancel_Click">Return to Property</asp:LinkButton></p>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
<Columns>
<asp:BoundField HeaderText="UserID" DataField="UserID" />
<asp:BoundField HeaderText="UserName" DataField="UserName" />
<asp:BoundField HeaderText="DisplayName" DataField="DisplayName" />
<asp:BoundField HeaderText="Email" DataField="Email" />

</Columns>
</asp:GridView>

<asp:HiddenField ID="hiddenUserID" runat="server" />
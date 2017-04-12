<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <ajaxtoolkit:ToolkitScriptManager runat="server" />
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink>
        if you don't have an account.
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
        OnAuthenticate="LoginUser_Authenticate" OnLoginError="LoginUser_LoginError">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="LoginUserValidationGroup" />
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server" />
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    </p>
                    <p>
                    </p>
                    <p>
                        <asp:CustomValidator ID="LoginErrorValidator" runat="server" Visible="false"
                            ErrorMessage="Your login attempt was not successful. Please try again." 
                            ValidationGroup="LoginUserValidationGroup"
                            CssClass="failureNotification"  />
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" Text="Log In" ValidationGroup="LoginUserValidationGroup"
                        OnClick="LoginButton_Click" />
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <asp:Button ID="liPopup" Style="display: none" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="MPEOTP" runat="server" TargetControlID="liPopup"
        PopupControlID="OTPPanel" BackgroundCssClass="modalBackground">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="OTPPanel" runat="server" Style="display: none" Width="400px" CssClass="modalPopup">
        <div style="text-align: center">
            <p>
                Please enter the one-time passcode your device has generated:
            </p>
            <p>
                <asp:TextBox ID="OTPCode" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Button ID="CheckOTPCode" runat="server" Text="Submit" Width="100px" OnClick="CheckOTPCode_Click" />
            </p>
        </div>
    </asp:Panel>
</asp:Content>
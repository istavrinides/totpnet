<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditUserDetails.aspx.cs" Inherits="Account_EditUserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="accountInfo">
        <fieldset>
            <legend>Account Information</legend>
            <p class="login">
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" ReadOnly="true"></asp:TextBox>
            </p>
            <p >
                <asp:CheckBox ID="EnableOTP" style="display:inline" runat="server" Text="Enable One-Time-Password support" />
            </p>
            <p>
                <asp:Button ID="SaveButton" runat="server" Text="Save changes" 
                    onclick="SaveButton_Click" />
            </p>
        </fieldset>
    </div>
</asp:Content>


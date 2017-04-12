<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EnableUserOTP.aspx.cs" Inherits="Account_EnableUserOTP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="accountInfo">
        <fieldset class="login">
            <legend>One-Time Password Setup</legend>
            <p class="login">
                Please scan the following QR code with your phone
            </p>
            <p>
                <asp:Image ID="QRCode" runat="server" />
            </p>
            <p>
                or enter the following code if you are unable to scan the above QR code:
            </p>
            <p style="text-align:center">
                <asp:Label ID="TextCode" runat="server" />
            </p>
            <p>
                and enter the generated code here:
            </p>
            <p>
                <asp:TextBox ID="Code" runat="server" Width="150px" />
            </p>
            <p>
                <asp:Button ID="SubmitButton" runat="server" Text="Submit Code" 
                    onclick="SubmitButton_Click"  />
            </p>
        </fieldset>
    </div>
</asp:Content>


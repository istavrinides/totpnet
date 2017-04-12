<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OTPUnsuccesful.aspx.cs" Inherits="Account_OTPUnsuccesful" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3 style="color: Red">
    One-Time Password authentication was not enabled succesfully
    </h3>
    <p>
        <asp:LinkButton ID="LinkButton1" PostBackUrl="~/Account/EnableUserOTP.aspx" Text="Try Again" runat="server" />
    </p>
</asp:Content>


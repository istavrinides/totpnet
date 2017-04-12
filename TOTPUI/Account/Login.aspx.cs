using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TOTPBL;
using TOTPModel;
using System.Web.Security;
using System.Text;

public partial class Account_Login : System.Web.UI.Page
{
    private bool _authenticated = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
    }

    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {
        e.Authenticated = true;
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        _authenticated = Membership.ValidateUser(LoginUser.UserName, LoginUser.Password);

        if (_authenticated)
        {
            ProfileCommon _profile = (ProfileCommon)System.Web.Profile.ProfileBase.Create(LoginUser.UserName, true);
            Boolean otpNotEnabled = String.IsNullOrEmpty(_profile.OTPKey);

            if (!otpNotEnabled)
            {
                MPEOTP.Show();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(LoginUser.UserName, LoginUser.RememberMeSet);
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            CustomValidator cv = (CustomValidator)LoginUser.FindControl("LoginErrorValidator");
            cv.IsValid = false;
        }
    }

    protected void CheckOTPCode_Click(object sender, EventArgs e)
    {
        ProfileCommon _profile = (ProfileCommon)System.Web.Profile.ProfileBase.Create(LoginUser.UserName, true);
        Boolean otpNotEnabled = String.IsNullOrEmpty(_profile.OTPKey);

        if (!otpNotEnabled)
        {
            BL _bl = new BL();
            String _otp = _bl.calculateOneTimePassword(Convert.FromBase64String(_profile.OTPKey)).ToString().PadRight(6, '0');

            if (_otp != OTPCode.Text.Trim())
            {
                FormsAuthentication.SignOut();
                CustomValidator cv = (CustomValidator)LoginUser.FindControl("LoginErrorValidator");
                cv.IsValid = false;
            }
            else
            {
                FormsAuthentication.SetAuthCookie(LoginUser.UserName, LoginUser.RememberMeSet);
                Response.Redirect("~/Default.aspx");
            }
        }
    }

    protected void LoginUser_LoginError(object sender, EventArgs e)
    {

    }
}

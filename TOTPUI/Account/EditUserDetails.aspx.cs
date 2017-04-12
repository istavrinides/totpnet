using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_EditUserDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            EnableOTP.Checked = !String.IsNullOrEmpty(Profile.OTPKey);
            UserName.Text = User.Identity.Name;
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        Boolean otpNotEnabled = String.IsNullOrEmpty(Profile.OTPKey);

        if (otpNotEnabled)
            Response.Redirect("~/Account/EnableUserOTP.aspx");

        if (!otpNotEnabled != EnableOTP.Checked)
        {
            if (!otpNotEnabled && !EnableOTP.Checked)
            {
                Profile.OTPKey = null;
                Profile.Save();
            }
            else
            {
                Response.Redirect("~/Account/EnableUserOTP.aspx");
            }
        }
    }
}
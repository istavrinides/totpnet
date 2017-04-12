using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TOTPBL;
using TOTPModel;
using QR = MessagingToolkit.Barcode;
using System.Text;

public partial class Account_EnableUserOTP : System.Web.UI.Page
{
    private String fileName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BL _bl = new BL();

            fileName = "~/" + Guid.NewGuid().ToString() + ".png";

            QR.BarcodeEncoder encoder = new QR.BarcodeEncoder();
            QR.BarcodeFormat format = QR.BarcodeFormat.QRCode;

            Binary otpKey = null;

            Boolean otpNotEnabled = String.IsNullOrEmpty(Profile.OTPKey);

            if (otpNotEnabled)
            {
                otpKey = _bl.generateUserSecretKey();
                Profile.OTPKey = Convert.ToBase64String(otpKey.ToArray());
            }
            else
                otpKey = Convert.FromBase64String(Profile.OTPKey);

            String key = _bl.Base32(otpKey.ToArray());

            String totpUri = String.Format("otpauth://totp/TOTPWeb:{0}?secret={1}&issuer=TOTPWeb", User.Identity.Name, key);

            System.Drawing.Image img = encoder.Encode(format, totpUri);
            encoder.SaveImage(Server.MapPath(fileName), QR.SaveOptions.Png);
            QRCode.ImageUrl = fileName;

            TextCode.Text = key;
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        BL _bl = new BL();

        String _otp = _bl.calculateOneTimePassword(Convert.FromBase64String(Profile.OTPKey)).ToString().PadLeft(6, '0');

        if (_otp == Code.Text.Trim())
        {
            // Delete the image file
            if (fileName.Length > 0)
                if (System.IO.File.Exists(Server.MapPath(fileName)))
                    System.IO.File.Delete(Server.MapPath(fileName));

            Response.Redirect("~/Account/OTPEnabledSuccesfully.aspx");
        }
        else
        {
            Profile.OTPKey = null;

            // Delete the image file
            if (fileName.Length > 0)
                if (System.IO.File.Exists(Server.MapPath(fileName)))
                    System.IO.File.Delete(Server.MapPath(fileName));

            Response.Redirect("~/Account/OTPUnsuccesful.aspx");
        }
    }
}
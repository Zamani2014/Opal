using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Opal.Controls;

public partial class ChangePasswordN : PageBaseClass
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["firstuse"]) && bool.Parse(Request.QueryString["firstuse"]))
		{
			ChangePassword1.ChangePasswordTemplateContainer.FindControl("pnlFirstUse").Visible = true;
			(ChangePassword1.ChangePasswordTemplateContainer.FindControl("UserName") as TextBox).Text = "admin";
		}
	}
}

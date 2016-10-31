using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArvidfavaSMS;
using Opal.Controls;

public partial class unsuccess : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["error"] != string.Empty)
        {
            // this.ErrorCodeLabel.Text = Request.Params["error"];
            this.ErrorLabel.Text = ShortMessageService.getDescriptionForPaymentStatusMellat(int.Parse(Request.Params["error"]));
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}
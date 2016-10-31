using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;

public partial class success : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["TrackingNumber"] != string.Empty)
        {
            this.Label1.Text = Request.Params["TrackingNumber"];
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
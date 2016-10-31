using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;

public partial class Databases : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByEvenAndOdd.aspx");
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByTrafficProject.aspx");
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByTehranProvincePostalCode.aspx");
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByJobs.aspx");
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByTehranDistrict.aspx");
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByTehranCityRegion.aspx");
    }
    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByMetroStationsAndBRT.aspx");
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByIranCell.aspx");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendByGender.aspx");
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Databases/SendFromMap.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using System.Data;
using System.IO;

public partial class Administration_UsersList : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet DS = new DataSet();
        DS.ReadXml(Server.MapPath("~/App_Data/Users.config"));
        this.GridView1.DataSource = DS;
        this.GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //ExportToExcel.ExportToExcel ex = new ExportToExcel.ExportToExcel();
        //ex.ExportGridView(GridView1, "Clients.xls");
        //Export the GridView to Excel
        PrepareGridViewForExport(GridView1);
        ExportGridView();
    }

    private void ExportGridView()
    {
        string attachment = "attachment; filename=UsersList.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    private void PrepareGridViewForExport(Control gv)
    {
        LinkButton lb = new LinkButton();
        Literal l = new Literal();
        string name = String.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {
                l.Text = (gv.Controls[i] as LinkButton).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {
                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(CheckBox))
            {
                l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            if (gv.Controls[i].HasControls())
            {
                PrepareGridViewForExport(gv.Controls[i]);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;


namespace ExportToExcel
{

    /// <summary>
    /// Summary description for ExportToExcel
    /// </summary>
    public class ExportToExcel
    {
        public ExportToExcel()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void ExportGridView(GridView GridView1, String strFileName)
        {
            PrepareGridViewForExport(GridView1);
            //string attachment = "attachment; filename=Contacts.xls";
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Charset = "";
            //System.Web.UI.Page.EnableViewState = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
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

        /*Use this commented function in all the pages where the above export function is used
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}*/        
    }
}

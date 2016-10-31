using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using System.Data.SqlClient;
using Zamani;
using System.Data;
using System.Configuration;

public partial class Administration_BulletManagement_ProjectNews : PageBaseClass
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection CN = new SqlConnection(ConnectionString);

        String ID = TextBox3.Text;
        String News = TextBox2.Text;
        String Subject = TextBox1.Text;

        CN.Open();

        string strsql = "SELECT * FROM LatestAnnounces WHERE ID ='" + TextBox3.Text + "'";
        SqlCommand objcommand = new SqlCommand(strsql, CN);
        SqlDataReader dr = objcommand.ExecuteReader();
        if (dr.Read())
        {
            WebMsgBox.Show("شماره شناسائی وارد شده تکراری است.");
        }
        else
        {
            try
            {
                string MyStr = "INSERT INTO LatestAnnounces(ID, Subject, News, DateTime) values (@ID, @Subject, @News, @DateTime);";
                SqlCommand objcommand2 = new SqlCommand(MyStr, CN);

                objcommand2.Parameters.Add("@ID", SqlDbType.Int);
                objcommand2.Parameters.Add("@Subject", SqlDbType.NVarChar);
                objcommand2.Parameters.Add("@News", SqlDbType.NVarChar);
                objcommand2.Parameters.Add("@DateTime", SqlDbType.NVarChar);


                objcommand2.Parameters["@ID"].Value = ID;
                objcommand2.Parameters["@Subject"].Value = Subject;
                objcommand2.Parameters["@News"].Value = News;
                objcommand2.Parameters["@DateTime"].Value = GetDateTime.GenerateDateTime();

                dr.Close();
                objcommand2.ExecuteNonQuery();
                WebMsgBox.Show("اطلاعات خبر جدید با موفقیت اضافه شد .");

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                CN.Close();
                GridView1.DataBind();
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection CN = new SqlConnection(ConnectionString);

        String ID = TextBox4.Text;

        CN.Open();

        string strsql = "SELECT * FROM LatestAnnounces WHERE ID ='" + TextBox4.Text + "'";
        SqlCommand objcommand = new SqlCommand(strsql, CN);
        SqlDataReader dr = objcommand.ExecuteReader();
        if (!dr.Read())
        {
            WebMsgBox.Show("شماره شناسائی وارد شده وجود ندارد.");
        }
        else
        {
            try
            {
                dr.Close();
                string MyStr = "DELETE FROM LatestAnnounces WHERE ID='" + TextBox4.Text + "'";
                SqlCommand objcommand2 = new SqlCommand(MyStr, CN);
                objcommand2.ExecuteNonQuery();
                WebMsgBox.Show("رکورد مورد نظر با موفقیت حذف شد .");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                CN.Close();
                GridView1.DataBind();
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using System.Net;
using ArvidfavaSMS;
using Zamani;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Administration_ShortMessageService_Outbox : PageBaseClass
{
    string MyConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();
    MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();

    #region Variables
    private String username;
    private String password;
    private String domain;
    private static String senderNumber;
    private int N;
    private bool useProxy;
    private String proxyAddress;
    private String proxyUsername;
    private String proxyPassword;
    #endregion

    private static long[] messageId = new long[90];
    private static string MsgBody = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DataGrid1.DataSource = GetData();
        this.DataGrid1.DataBind();

        username = System.Configuration.ConfigurationManager.AppSettings["Username"];
        password = System.Configuration.ConfigurationManager.AppSettings["Password"];
        domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
        N = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Count"]);
        //senderNumber = System.Configuration.ConfigurationManager.AppSettings["SenderNumber"];
        useProxy = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseProxy"]);
        proxyAddress = System.Configuration.ConfigurationManager.AppSettings["ProxyAddress"];
        proxyUsername = System.Configuration.ConfigurationManager.AppSettings["ProxyUsername"];
        proxyPassword = System.Configuration.ConfigurationManager.AppSettings["ProxyPassword"];

        #region Get Credit & Sender Number
        string userName = User.Identity.Name;
        DataSet DS = new DataSet();
        DS.ReadXml(Server.MapPath("~/App_Data/Users.config"));
        DataTable users = DS.Tables[0];
        var query = from user in users.AsEnumerable()
                    where user.Field<string>("Username") == userName
                    select new
                    {
                        LineNo = user.Field<long>("LineNo"),
                        Credit = user.Field<long>("Credit")
                    };
        foreach (var lines in query)
        {
            senderNumber = lines.LineNo.ToString();
            //Credit = lines.Credit;
        }
        #endregion
    }
    private DataSet GetData()
    {
        SqlConnection cn = new SqlConnection(MyConnectionString);
        cn.Open();
        string sql = "SELECT * FROM OutBoxTbl WHERE msgsenderNumber='" + senderNumber + "'";
        SqlDataAdapter DA = new SqlDataAdapter(sql, cn);
        DataSet DS = new DataSet();
        DA.Fill(DS);
        //DS.Tables[0].TableName = "Inbox";
        return DS;
    }
    protected void DataGrid1_OnSelect(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
        messageId[0] = long.Parse(e.Item["ID"].ToString());
        MsgBody = e.Item["messageBody"].ToString();
    }
    protected void DataRefresh_Click(object sender, EventArgs e)
    {
        this.DataGrid1.DataSource = GetData();
        this.DataGrid1.DataBind();
    }
    protected void MsgOpen_Click(object sender, EventArgs e)
    {
        if (MsgBody == String.Empty)
        {
            WebMsgBox.Show("لطفا یک پیامک را از فهرست انتخاب نمائید .");
        }
        else
        {
            WebMsgBox.Show("متن پیامک :" + MsgBody);
        }
    }
    protected void DataDelete_Click(object sender, EventArgs e)
    {
        if (messageId[0] == 0)
        {
            WebMsgBox.Show("لطفا یک پیامک را از فهرست انتخاب نمائید .");
        }
        else
        {
            SqlConnection cn = new SqlConnection(MyConnectionString);
            cn.Open();
            string query = "DELETE FROM OutBoxTbl WHERE ID=" + messageId[0] + "";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.ExecuteNonQuery();
            WebMsgBox.Show("پیامک مورد نظر با موفقیت حذف شد");
            cn.Close();
            this.DataGrid1.DataSource = GetData();
            this.DataGrid1.DataBind();
        }
    }
    protected void ExcelExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Outbox.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        DataGrid1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void StatusCheck_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(MyConnectionString);

            sq.Credentials = new System.Net.NetworkCredential(username, password);
            sq.PreAuthenticate = true;

            if (messageId[0] == 0)
            {
                WebMsgBox.Show("لطفا از فهرست پیامک ها یک پیامک را برای بررسی وضعیت انتخاب نمائید !");
            }
            else
            {
                int[] statusResult = new int[90];
                statusResult = sq.getRealMessageStatuses(messageId);
                
                string status = string.Empty;

                #region DialogsBox
                if (statusResult[0] == 1)
                {
                    status = "رسیده به گوشی";
                    WebMsgBox.Show("وضعیت پیامک انتخابی" + " : " + status);
                }
                else if (statusResult[0] == 2)
                {
                    status = "نرسیده به گوشی";
                    WebMsgBox.Show("وضعیت پیامک انتخابی" + " : " + status);
                }
                else if (statusResult[0] == 8)
                {
                    status = "رسیده به مخابرات";
                    WebMsgBox.Show("وضعیت پیامک انتخابی" + " : " + status);
                }
                else if (statusResult[0] == 16)
                {
                    status = "نرسیده به مخابرات";
                    WebMsgBox.Show("وضعیت پیامک انتخابی" + " : " + status);
                }
                else if (statusResult[0] == 0)
                {
                    status = "در صف ارسال";
                    WebMsgBox.Show("وضعیت پیامک انتخابی" + " : " + status);
                }
                else if (statusResult[0] == -1)
                {
                    status = "خطا | ممکن است این پیامک برای بیش از یکروز پیش بوده باشد";
                    WebMsgBox.Show("وضعیت پیامک انتخابی" + " : " + status);
                }
                #endregion 

                string query = "UPDATE OutBoxTbl SET messageStatus='" + status + "' WHERE ID='" + messageId[0] + "'";
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                this.DataGrid1.DataSource = GetData();
                this.DataGrid1.DataBind();
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است :" + ex.Message);
        }
    }
    public static int Guid2Int(Guid value)
    {
        byte[] b = value.ToByteArray();
        int bint = BitConverter.ToInt32(b, 0);
        return bint;
    }
    protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        messageId[0] = long.Parse(DataGrid1.SelectedRow.Cells[1].Text);
        MsgBody = DataGrid1.SelectedRow.Cells[2].Text.ToString();
    }
}
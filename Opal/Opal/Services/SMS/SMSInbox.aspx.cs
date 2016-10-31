using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using System.Globalization;
using System.Net;
using System.Data.SqlClient;
using Zamani;
using ArvidfavaSMS;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Administration_ShortMessageService_Inbox : PageBaseClass
{
    #region Webservices
    MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
    string MyConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();
    //System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
    //string MyConnectionString = rootWebConfig.ConnectionStrings.ConnectionStrings["ArvidSMSConnectionString"];
    #endregion
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
    private static int InboxID;
    private static string MsgBody = string.Empty;
    #endregion
    #region DBVars
    int[] ID;
    string[] messageBody;
    string[] errorResult;
    string[] recipientNumber;
    string[] msgsenderNumber;
    string[] date;
    #endregion
    #region Methods
    public MagfaWebReference.DatedCustomerReturnIncomingFormat[] getMessages(Boolean useProxy, String proxyAddress, String proxyUsername, String proxyPassword, String username, String password, String domain, int numberOfMessages)
    {
        if (useProxy)
        {
            WebProxy proxy;
            proxy = new WebProxy(proxyAddress);
            proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
            sq.Proxy = proxy;
        }
        sq.Credentials = new System.Net.NetworkCredential(username, password);
        sq.PreAuthenticate = true;
        return (MagfaWebReference.DatedCustomerReturnIncomingFormat[])sq.getMessages(domain, numberOfMessages);
    }
    public MagfaWebReference.DatedCustomerReturnIncomingFormat[] getMessagesWithNumber(Boolean useProxy, String proxyAddress, String proxyUsername, String proxyPassword, String username, String password, String domain, int numberOfMessages)
    {
        if (useProxy)
        {
            WebProxy proxy;
            proxy = new WebProxy(proxyAddress);
            proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
            sq.Proxy = proxy;
        }
        sq.Credentials = new System.Net.NetworkCredential(username, password);
        sq.PreAuthenticate = true;
        return (MagfaWebReference.DatedCustomerReturnIncomingFormat[])sq.getMessagesWithNumber(domain, numberOfMessages, senderNumber);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
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

        InboxLoader();
    }
    private void InboxLoader()
    {
        try
        {
            #region Authentication
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(username, password);
            sq.PreAuthenticate = true;
            #endregion

            SqlConnection cn = new SqlConnection(MyConnectionString);
            cn.Open();

            MagfaWebReference.DatedCustomerReturnIncomingFormat[] returnValues = getMessages(useProxy, proxyAddress, proxyUsername, proxyPassword, username, password, domain, 2000000);
            //ReturnValues[] returnValues = getMessagesWithNumber(useProxy, proxyAddress, proxyUsername, proxyPassword, username, password, domain, 2000000);
            //MagfaWebReference.DatedCustomerReturnIncomingFormat[] returnValues = (MagfaWebReference.DatedCustomerReturnIncomingFormat[])sq.getMessagesWithNumber(domain, 2000000, senderNumber);

            int length = returnValues.Length;

            ID = new int[length];
            messageBody = new string[length];
            errorResult = new string[length];
            recipientNumber = new string[length];
            msgsenderNumber = new string[length];
            date = new string[length];

            for (int i = 0; i < length; i++)
            {
                ID[i] = GenerateIDColumn.GetNewID("IncomingTbl");
                messageBody[i] = returnValues[i].body;
                //if (returnValues[i].errorResult == string.Empty)
                //    errorResult[i] = null;
                //else
                errorResult[i] = returnValues[i].errorResult;
                recipientNumber[i] = returnValues[i].recipientNumber;
                msgsenderNumber[i] = returnValues[i].senderNumber;
                date[i] = returnValues[i].date;

                string mysql = "INSERT INTO IncomingTbl (ID, messageBody, errorResult, recipientNumber, msgsenderNumber, date, sysdate) values (@ID, @messageBody, @errorResult,  @recipientNumber, @msgsenderNumber, @date, @sysdate)";

                SqlCommand cmd = new SqlCommand(mysql, cn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                cmd.Parameters.Add("@messageBody", System.Data.SqlDbType.NText);
                cmd.Parameters.Add("@errorResult", System.Data.SqlDbType.NVarChar);
                cmd.Parameters.Add("@recipientNumber", System.Data.SqlDbType.NVarChar);
                cmd.Parameters.Add("@msgsenderNumber", System.Data.SqlDbType.NVarChar);
                cmd.Parameters.Add("@date", System.Data.SqlDbType.NVarChar);
                cmd.Parameters.Add("@sysdate", System.Data.SqlDbType.NVarChar);

                cmd.Parameters["@ID"].Value = ID[i];
                cmd.Parameters["@messageBody"].Value = messageBody[i];
                cmd.Parameters["@errorResult"].Value = GetDataValue(errorResult[i]);
                cmd.Parameters["@recipientNumber"].Value = recipientNumber[i];
                cmd.Parameters["@msgsenderNumber"].Value = msgsenderNumber[i];
                cmd.Parameters["@date"].Value = date[i];
                cmd.Parameters["@sysdate"].Value = GetDateTime.GenerateDateTime();

                cmd.ExecuteNonQuery();
            }
            this.DataGrid1.DataSource = GetData();
            this.DataGrid1.DataBind();
            cn.Close();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است :" + " : " + ex.Message);
            //Response.Write(ex.StackTrace);
        }
    }
    private DataSet GetData()
    {
        SqlConnection cn = new SqlConnection(MyConnectionString);
        cn.Open();
        string sql = "SELECT * FROM IncomingTbl WHERE recipientNumber='" + senderNumber + "'";
        SqlDataAdapter DA = new SqlDataAdapter(sql, cn);
        DataSet DS = new DataSet();
        DA.Fill(DS);
        //DS.Tables[0].TableName = "Inbox";
        return DS;
    }
    public static int Guid2Int(Guid value)
    {
        byte[] b = value.ToByteArray();
        int bint = BitConverter.ToInt32(b, 0);
        return bint;
    }
    public static object GetDataValue(object value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value;
    }
    /*protected void DataGrid1_OnSelect(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
        InboxID = int.Parse(e.Item["ID"].ToString());
        MsgBody = e.Item["messageBody"].ToString();
    }*/
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
        if (InboxID == 0)
        {
            WebMsgBox.Show("لطفا یک پیامک را از فهرست انتخاب نمائید .");
        }
        else
        {
            SqlConnection cn = new SqlConnection(MyConnectionString);
            cn.Open();
            string query = "DELETE FROM IncomingTbl WHERE ID=" + InboxID + "";
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
        string attachment = "attachment; filename=Inbox.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        DataGrid1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void SendToFolder_Click(object sender, EventArgs e)
    {
        // Soon !
    }
    protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        InboxID = int.Parse(DataGrid1.SelectedRow.Cells[1].Text);
        MsgBody = DataGrid1.SelectedRow.Cells[2].Text.ToString();
    }
}
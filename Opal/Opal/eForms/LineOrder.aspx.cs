using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using Zamani;

public partial class eForms_solutionsform : PageBaseClass
{
    private static int buyNumber = 0;
    private static int number10 = 1200000;
    private static int number12 = 1000000;
    private static int number14 = 300000;

    string ConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();

    public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
    public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
    public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
    public static readonly string UserName = ConfigurationManager.AppSettings["bpUserName"];
    public static readonly string UserPassword = ConfigurationManager.AppSettings["bpUserPassword"];
    public static readonly string additionalData = "فرم الکترونیکی خرید خط اختصاصی";
    long payerId = 0;
    public static string RefId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            buyNumber = number10;
            this.PriceLabel.Text = "1,200,000";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int ID = GenerateIDColumn.GetNewID("LineRequestTbl");
        long OrderID = GenerateOrderID.GetNewOrderID();
        string FirstAndLastName = this.FirstAndLastName.Text;
        string AccUserName = this.AccUserName.Text;
        string NumberType = this.DropDownList1.SelectedItem.Text;
        string BankName = this.DropDownList2.SelectedItem.Text;
        int BankSelected = int.Parse(this.DropDownList2.SelectedValue);
        string SysDateTime = GetDateTime.GenerateDateTime();
        string localDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
        string localTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        string PaymentStatus = String.Empty;
        try
        {
            string result;

            BypassCertificateError();

            ir.bankmellat.bpm.pgws.PaymentGatewayImplService bpService = new ir.bankmellat.bpm.pgws.PaymentGatewayImplService();

            result = bpService.bpPayRequest(Int64.Parse(TerminalId),
                UserName,
                UserPassword,
                OrderID,
                buyNumber,
                localDate,
                localTime,
                additionalData,
                CallBackUrl,
                payerId);

            String[] resultArray = result.Split(',');

            if (resultArray[0] == "0")
            {
                SqlConnection cn = new SqlConnection(ConnectionString);
                cn.Open();

                string query = "INSERT INTO LineRequestTbl (ID, OrderID, FirstAndLastName, AccUserName, buyNumber, NumberType, BankName, SysDateTime, LocalDate, LocalTime, PaymentStatus) values (@ID, @OrderID, @FirstAndLastName, @AccUserName, @buyNumber, @NumberType, @BankName, @SysDateTime, @LocalDate, @LocalTime, @PaymentStatus)";
                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters.Add("@OrderID", SqlDbType.Float);
                cmd.Parameters.Add("@FirstAndLastName", SqlDbType.NVarChar);
                cmd.Parameters.Add("@AccUserName", SqlDbType.NVarChar);
                cmd.Parameters.Add("@buyNumber", SqlDbType.NVarChar);
                cmd.Parameters.Add("@NumberType", SqlDbType.NVarChar);
                cmd.Parameters.Add("@BankName", SqlDbType.NVarChar);
                cmd.Parameters.Add("@SysDateTime", SqlDbType.NVarChar);
                cmd.Parameters.Add("@LocalDate", SqlDbType.NVarChar);
                cmd.Parameters.Add("@LocalTime", SqlDbType.NVarChar);
                cmd.Parameters.Add("@PaymentStatus", SqlDbType.NVarChar);

                cmd.Parameters["@ID"].Value = ID;
                cmd.Parameters["@OrderID"].Value = OrderID;
                cmd.Parameters["@FirstAndLastName"].Value = FirstAndLastName;
                cmd.Parameters["@AccUserName"].Value = AccUserName;
                cmd.Parameters["@buyNumber"].Value = buyNumber;
                cmd.Parameters["@NumberType"].Value = NumberType;
                cmd.Parameters["@BankName"].Value = BankName;
                cmd.Parameters["@SysDateTime"].Value = SysDateTime;
                cmd.Parameters["@LocalDate"].Value = localDate;
                cmd.Parameters["@LocalTime"].Value = localTime;
                cmd.Parameters["@PaymentStatus"].Value = PaymentStatus;

                cmd.ExecuteNonQuery();
                cn.Close();

                // Response.Redirect("https://pgw.bpm.bankmellat.ir/pgwchannel/startpay.mellat?RefId=" + resultArray[1] + "");
                string postRefIdScript =
                            @" <script language='javascript' type='text/javascript'>    
                                function postRefId (refIdValue) {
                                    var form = document.createElement('form');
                                    form.setAttribute('method', 'POST');
                                    form.setAttribute('action', '"
                            + PgwSite
                            + @"');         
                                    form.setAttribute('target', '_self');
                                    var hiddenField = document.createElement('input');              
                                    hiddenField.setAttribute('name', 'RefId');
                                    hiddenField.setAttribute('value', refIdValue);
                                    form.appendChild(hiddenField);
                                    document.body.appendChild(form);         
                                    form.submit();
                                    document.body.removeChild(form);
                                }
                            </script>";
                ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
            }
            else
            {
                WebMsgBox.Show("متاسفانه خطایی رخ داده است");
                WebMsgBox.Show(resultArray[0].ToString());
            }
        }
        catch (Exception exp)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است");
            Response.Write(exp.Message);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(this.DropDownList1.SelectedValue) == 300010)
        {
            this.PriceLabel.Text = "1,200,000";
            buyNumber = number10;
        }
        if (int.Parse(this.DropDownList1.SelectedValue) == 300012)
        {
            this.PriceLabel.Text = "1,000,000";
            buyNumber = number12;
        }
        if (int.Parse(this.DropDownList1.SelectedValue) == 300014)
        {
            this.PriceLabel.Text = "300,000";
            buyNumber = number14;
        }
    }
    void BypassCertificateError()
    {
        ServicePointManager.ServerCertificateValidationCallback +=
            delegate(
                Object sender1,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
    }

}
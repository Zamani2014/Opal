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

public partial class eForms_PanelOrder : PageBaseClass
{
    private static int buyNumber = 0;
    private static int Panel1 = 800000;
    private static int Panel2 = 1800000;
    private static int Panel3 = 3000000;
    private static int Panel4 = 5200000;
    private static int Panel5 = 6000000;

    string ConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();

    public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
    public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
    public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
    public static readonly string UserName = ConfigurationManager.AppSettings["bpUserName"];
    public static readonly string UserPassword = ConfigurationManager.AppSettings["bpUserPassword"];
    public static readonly string additionalData = "فرم الکترونیکی سفارش پنل های پیشنهادی";
    long payerId = 0;
    public static string RefId = "";

    protected void valAntiBotImage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (Session["antibotimage"] != null) && (txtAntiBotImage.Text.Trim().ToUpper() == (string)Session["antibotimage"]);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (int.Parse(Request.QueryString["PlanID"]) == 1)
            {
                this.DropDownList1.SelectedIndex = 0;
                this.PriceLabel.Text = "800,000";
                buyNumber = Panel1;
            }
            else if (int.Parse(Request.QueryString["PlanID"]) == 2)
            {
                this.DropDownList1.SelectedIndex = 1;
                this.PriceLabel.Text = "1,800,000";
                buyNumber = Panel2;
            }
            else if (int.Parse(Request.QueryString["PlanID"]) == 3)
            {
                this.DropDownList1.SelectedIndex = 2;
                this.PriceLabel.Text = "3,000,000";
                buyNumber = Panel3;
            }
            else if (int.Parse(Request.QueryString["PlanID"]) == 4)
            {
                this.DropDownList1.SelectedIndex = 3;
                this.PriceLabel.Text = "5,200,000";
                buyNumber = Panel4;
            }
            else if (int.Parse(Request.QueryString["PlanID"]) == 5)
            {
                this.DropDownList1.SelectedIndex = 4;
                this.PriceLabel.Text = "6,000,000";
                buyNumber = Panel5;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int ID = GenerateIDColumn.GetNewID("PanelRequestTbl");
        long OrderID = GenerateOrderID.GetNewOrderID();
        string Name = this.Name.Text;
        string NCode = this.NCode.Text;
        string PanelType = this.DropDownList1.SelectedItem.Text;
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

                string query = "INSERT INTO PanelRequestTbl (ID, OrderID, Name, NCode, buyNumber, PanelType, BankName, SysDateTime, LocalDate, LocalTime, PaymentStatus) values (@ID, @OrderID, @Name, @NCode, @buyNumber, @PanelType, @BankName, @SysDateTime, @LocalDate, @LocalTime, @PaymentStatus)";
                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters.Add("@OrderID", SqlDbType.Float);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters.Add("@NCode", SqlDbType.NVarChar);
                cmd.Parameters.Add("@buyNumber", SqlDbType.NVarChar);
                cmd.Parameters.Add("@PanelType", SqlDbType.NVarChar);
                cmd.Parameters.Add("@BankName", SqlDbType.NVarChar);
                cmd.Parameters.Add("@SysDateTime", SqlDbType.NVarChar);
                cmd.Parameters.Add("@LocalDate", SqlDbType.NVarChar);
                cmd.Parameters.Add("@LocalTime", SqlDbType.NVarChar);
                cmd.Parameters.Add("@PaymentStatus", SqlDbType.NVarChar);

                cmd.Parameters["@ID"].Value = ID;
                cmd.Parameters["@OrderID"].Value = OrderID;
                cmd.Parameters["@Name"].Value = Name;
                cmd.Parameters["@NCode"].Value = NCode;
                cmd.Parameters["@buyNumber"].Value = buyNumber;
                cmd.Parameters["@PanelType"].Value = PanelType;
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
        if (int.Parse(this.DropDownList1.SelectedValue) == 1)
        {
            this.PriceLabel.Text = "800,000";
            buyNumber = Panel1;
        }
        if (int.Parse(this.DropDownList1.SelectedValue) == 2)
        {
            this.PriceLabel.Text = "1,800,000";
            buyNumber = Panel2;
        }
        if (int.Parse(this.DropDownList1.SelectedValue) == 3)
        {
            this.PriceLabel.Text = "3,000,000";
            buyNumber = Panel3;
        }
        if (int.Parse(this.DropDownList1.SelectedValue) == 4)
        {
            this.PriceLabel.Text = "5,200,000";
            buyNumber = Panel4;
        }
        if (int.Parse(this.DropDownList1.SelectedValue) == 5)
        {
            this.PriceLabel.Text = "6,000,000";
            buyNumber = Panel5;
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
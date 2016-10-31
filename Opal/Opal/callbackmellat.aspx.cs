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

public partial class callback : PageBaseClass
{
    public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
    public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
    public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
    public static readonly string UserName = ConfigurationManager.AppSettings["bpUserName"];
    public static readonly string UserPassword = ConfigurationManager.AppSettings["bpUserPassword"];

    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();
        SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        sqlConnection.Open();

        if (Request.Form.Count != 0)
        {  
            string RefId = Request.Form["RefId"];
            string ResCode = Page.Request.Form["ResCode"];
            long saleOrderId = long.Parse(Page.Request.Form["saleOrderId"]);
            long SaleReferenceId = Request.Form["SaleReferenceId"] != string.Empty ? long.Parse(Page.Request.Form["SaleReferenceId"]) : 0;
            
            if (int.Parse(ResCode) == 0)
            {
                string MyQuery = "UPDATE OrderIDTbl SET RefId='" + RefId + "', ResCode='" + ResCode + "', SaleReferenceId='" + SaleReferenceId + "'  WHERE OrderID='" + saleOrderId + "'";
                SqlCommand cmd = new SqlCommand(MyQuery, sqlConnection);
                cmd.Parameters.Add("@RefId", SqlDbType.NVarChar);
                cmd.Parameters.Add("@ResCode", SqlDbType.NVarChar);
                cmd.Parameters.Add("@SaleReferenceId", SqlDbType.Float);

                cmd.Parameters["@RefId"].Value = RefId;
                cmd.Parameters["@ResCode"].Value = ResCode;
                cmd.Parameters["@SaleReferenceId"].Value = SaleReferenceId;

                cmd.ExecuteNonQuery();
                sqlConnection.Close();

                BypassCertificateError();
                ir.bankmellat.bpm.pgws.PaymentGatewayImplService bpService = new ir.bankmellat.bpm.pgws.PaymentGatewayImplService();

                string result = bpService.bpVerifyRequest(long.Parse(TerminalId), UserName, UserPassword, saleOrderId, saleOrderId, SaleReferenceId);
                string result2 = bpService.bpInquiryRequest(long.Parse(TerminalId), UserName, UserPassword, saleOrderId, saleOrderId, SaleReferenceId);
                string result3 = bpService.bpSettleRequest(long.Parse(TerminalId), UserName, UserPassword, saleOrderId, saleOrderId, SaleReferenceId);

                if (int.Parse(result) == 0 || int.Parse(result2) == 0)
                {
                    if (int.Parse(result3) == 0 || int.Parse(result3) == 45)
                    {
                        Response.Redirect("~/success.aspx?TrackingNumber=" + SaleReferenceId);
                    }
                }
                else
                {
                    string result4 = bpService.bpReversalRequest(long.Parse(TerminalId), UserName, UserPassword, saleOrderId, saleOrderId, SaleReferenceId);
                }
            }
            else
            {
                string MyQuery = "UPDATE OrderIDTbl SET RefId='" + RefId + "', ResCode='" + ResCode + "', SaleReferenceId='" + SaleReferenceId + "'  WHERE OrderID='" + saleOrderId + "'";
                SqlCommand cmd = new SqlCommand(MyQuery, sqlConnection);
                cmd.Parameters.Add("@RefId", SqlDbType.NVarChar);
                cmd.Parameters.Add("@ResCode", SqlDbType.NVarChar);
                cmd.Parameters.Add("@SaleReferenceId", SqlDbType.Float);

                cmd.Parameters["@RefId"].Value = RefId;
                cmd.Parameters["@ResCode"].Value = ResCode;
                cmd.Parameters["@SaleReferenceId"].Value = SaleReferenceId;

                cmd.ExecuteNonQuery();
                sqlConnection.Close();

                Response.Redirect("~/unsuccess.aspx?error=" + ResCode);
            }
          }
          else
          {
              Response.Redirect("~/Default.aspx");
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
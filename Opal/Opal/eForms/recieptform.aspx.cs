using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Zamani;

public partial class eForms_recieptform : PageBaseClass
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.DatePicker1.Date = DateTime.Now;
        }
    }
    protected void SendBtn_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            int ID = GenerateIDColumn.GetNewID("ReceiptTbl");
            string UserName = this.UserName.Text;
            string FirstName = this.FirstName.Text;
            string LastName = this.LastName.Text;
            string ReceiptNo = this.ReceiptNo.Text;
            string BankName = this.BankName.Text;
            string BankBranch = this.BankBranch.Text;
            string SettleDate = this.DatePicker1.DatePersian;
            string Comments = this.Comments.Text;
            string DateTime = GetDateTime.GenerateDateTime();

            string query = "INSERT INTO ReceiptTbl (ID, UserName, FirstName, LastName, ReceiptNo, BankName, BankBranch, SettleDate, Comments, DateTime) values (@ID, @UserName, @FirstName, @LastName, @ReceiptNo, @BankName, @BankBranch, @SettleDate, @Comments, @DateTime)";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BankName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BankBranch", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SettleDate", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);

            cmd.Parameters["@ID"].Value = ID;
            cmd.Parameters["@UserName"].Value = UserName;
            cmd.Parameters["@FirstName"].Value = FirstName;
            cmd.Parameters["@LastName"].Value = LastName;
            cmd.Parameters["@ReceiptNo"].Value = ReceiptNo;
            cmd.Parameters["@BankName"].Value = BankName;
            cmd.Parameters["@BankBranch"].Value = BankBranch;
            cmd.Parameters["@SettleDate"].Value = SettleDate;
            cmd.Parameters["@Comments"].Value = Comments;
            cmd.Parameters["@DateTime"].Value = DateTime;

            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            WebMsgBox.Show("اطلاعات فیش واریزی شما با موفقیت ثبت شد .");
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است :" + ex.Message);
        }
    }
    protected void valAntiBotImage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (Session["antibotimage"] != null) && (txtAntiBotImage.Text.Trim().ToUpper() == (string)Session["antibotimage"]);
    }

}
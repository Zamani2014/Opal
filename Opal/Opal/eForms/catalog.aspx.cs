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

public partial class eForms_catalog : PageBaseClass
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SendBtn_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            int ID = GenerateIDColumn.GetNewID("CatalogRequestTbl");
            string FirstName = this.FirstName.Text;
            string LastName = this.LastName.Text;
            string NationalCode = this.NationalCode.Text;
            string AcademicGrade = this.GradeLevelDrp.SelectedItem.Text;
            string CoPosition = this.CoPosition.Text;
            string CompanyName = this.CompanyName.Text;
            string WorkField = this.WorkField.Text;
            string SMSNeeds = this.SMSNeeds.Text;
            string Website = this.Website.Text;
            string EMail = this.EMail.Text;
            string Tel = this.Tel.Text;
            string Mobile = this.Mobile.Text;
            string Address = this.Address.Text;
            string Comments = this.Comments.Text;
            string DateTime = GetDateTime.GenerateDateTime();

            string query = "INSERT INTO CatalogRequestTbl (ID, FirstName, LastName, NationalCode, AcademicGrade, CoPosition, CompanyName, WorkField, SMSNeeds, Website, EMail, Tel, Mobile, Address, Comments, DateTime) values (@ID, @FirstName, @LastName, @NationalCode, @AcademicGrade, @CoPosition, @CompanyName, @WorkField, @SMSNeeds, @Website, @EMail, @Tel, @Mobile, @Address, @Comments, @DateTime)";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@NationalCode", SqlDbType.NVarChar);
            cmd.Parameters.Add("@AcademicGrade", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CoPosition", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@WorkField", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SMSNeeds", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Website", SqlDbType.NVarChar);
            cmd.Parameters.Add("@EMail", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Tel", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);

            cmd.Parameters["@ID"].Value = ID;
            cmd.Parameters["@FirstName"].Value = FirstName;
            cmd.Parameters["@LastName"].Value = LastName;
            cmd.Parameters["@NationalCode"].Value = NationalCode;
            cmd.Parameters["@AcademicGrade"].Value = AcademicGrade;
            cmd.Parameters["@CoPosition"].Value = CoPosition;
            cmd.Parameters["@CompanyName"].Value = CompanyName;
            cmd.Parameters["@WorkField"].Value = WorkField;
            cmd.Parameters["@SMSNeeds"].Value = SMSNeeds;
            cmd.Parameters["@Website"].Value = Website;
            cmd.Parameters["@EMail"].Value = EMail;
            cmd.Parameters["@Tel"].Value = Tel;
            cmd.Parameters["@Mobile"].Value = Mobile;
            cmd.Parameters["@Address"].Value = Address;
            cmd.Parameters["@Comments"].Value = Comments;
            cmd.Parameters["@DateTime"].Value = DateTime;

            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            WebMsgBox.Show("اطلاعات شما با موفقیت ثبت شد .");
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
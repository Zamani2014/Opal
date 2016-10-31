using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using Zamani;

public partial class eForms_agents : PageBaseClass
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void valAntiBotImage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (Session["antibotimage"] != null) && (txtAntiBotImage.Text.Trim().ToUpper() == (string)Session["antibotimage"]);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            cn.Open();

            int ID = GenerateIDColumn.GetNewID("AgentsTbl");
            string Name = this.Name.Text;
            string FatherName = this.FatherName.Text;
            string NationalCode = this.NationalCode.Text;
            string BirthDate = this.BirthDate.Text;
            string Job = this.Job.Text;
            string PreActivity = this.PreActivity.Text;
            string GradeLevel = this.GradeDrp.SelectedItem.Text;
            string Course = this.Course.Text;
            string HouseAddress = this.HouseAddress.Text;
            string CompanyName = this.CoName.Text;
            string RegisterNo = this.RegisterNo.Text;
            string CompanyType = this.CompanyType.SelectedItem.Text;
            string InvestBalance = this.InvestBalance.Text;
            string EmployeeCount = this.EmployeeNo.Text;
            string ActivityField = this.ActivityField.Text;
            string Website = this.Website.Text;
            string Email = this.EMail.Text;
            string CompanyOwner = this.CompanyOwnerType.SelectedItem.Text;
            string Licencer = this.Licencer.Text;
            string Tel = this.Tel.Text;
            string Mobile = this.Mobile.Text;
            string CoEmail = this.CoEmail.Text;
            string Fax = this.Fax.Text;
            string CoAddress = this.CoAddress.Text;
            string IntroWay = this.IntroWay.Text;
            string UserName = this.UserName.Text;
            string Budget = this.Budget.Text;
            string SMSfamiliar = this.SMSfamiliar.Text;
            string Comments = this.Comments.Text;
            string SysDateTime = GetDateTime.GenerateDateTime();

            string query = "INSERT INTO AgentsTbl (ID, Name, FatherName, NationalCode, BirthDate, Job, PreActivity, GradeLevel, Course, HouseAddress, CompanyName, RegisterNo, CompanyType, InvestBalance, EmployeeCount, ActivityField, Website, Email, CompanyOwner, Licencer, Tel, Mobile, CoEmail, Fax, CoAddress, IntroWay, UserName, Budget, SMSfamiliar, Comments, SysDateTime) values (@ID, @Name, @FatherName, @NationalCode, @BirthDate, @Job, @PreActivity, @GradeLevel, @Course, @HouseAddress, @CompanyName, @RegisterNo, @CompanyType, @InvestBalance, @EmployeeCount, @ActivityField, @Website, @Email, @CompanyOwner, @Licencer, @Tel, @Mobile, @CoEmail, @Fax, @CoAddress, @IntroWay, @UserName, @Budget, @SMSfamiliar, @Comments, @SysDateTime)";
            SqlCommand cmd = new SqlCommand(query, cn);

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters.Add("@FatherName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@NationalCode", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BirthDate", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Job", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PreActivity", SqlDbType.NVarChar);
            cmd.Parameters.Add("@GradeLevel", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Course", SqlDbType.NVarChar);
            cmd.Parameters.Add("@HouseAddress", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RegisterNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CompanyType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@InvestBalance", SqlDbType.NVarChar);
            cmd.Parameters.Add("@EmployeeCount", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ActivityField", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Website", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CompanyOwner", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Licencer", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Tel", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CoEmail", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Fax", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CoAddress", SqlDbType.NVarChar);
            cmd.Parameters.Add("@IntroWay", SqlDbType.NVarChar);
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Budget", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SMSfamiliar", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SysDateTime", SqlDbType.NVarChar);

            cmd.Parameters["@ID"].Value = ID;
            cmd.Parameters["@Name"].Value = Name;
            cmd.Parameters["@FatherName"].Value = FatherName;
            cmd.Parameters["@NationalCode"].Value = NationalCode;
            cmd.Parameters["@BirthDate"].Value = BirthDate;
            cmd.Parameters["@Job"].Value = Job;
            cmd.Parameters["@PreActivity"].Value = PreActivity;
            cmd.Parameters["@GradeLevel"].Value = GradeLevel;
            cmd.Parameters["@Course"].Value = Course;
            cmd.Parameters["@HouseAddress"].Value = HouseAddress;
            cmd.Parameters["@CompanyName"].Value = CompanyName;
            cmd.Parameters["@RegisterNo"].Value = RegisterNo;
            cmd.Parameters["@CompanyType"].Value = CompanyType;
            cmd.Parameters["@InvestBalance"].Value = InvestBalance;
            cmd.Parameters["@EmployeeCount"].Value = EmployeeCount;
            cmd.Parameters["@ActivityField"].Value = ActivityField;
            cmd.Parameters["@Website"].Value = Website;
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters["@CompanyOwner"].Value = CompanyOwner;
            cmd.Parameters["@Licencer"].Value = Licencer;
            cmd.Parameters["@Tel"].Value = Tel;
            cmd.Parameters["@Mobile"].Value = Mobile;
            cmd.Parameters["@CoEmail"].Value = CoEmail;
            cmd.Parameters["@Fax"].Value = Fax;
            cmd.Parameters["@CoAddress"].Value = CoAddress;
            cmd.Parameters["@IntroWay"].Value = IntroWay;
            cmd.Parameters["@UserName"].Value = UserName;
            cmd.Parameters["@Budget"].Value = Budget;
            cmd.Parameters["@SMSfamiliar"].Value = SMSfamiliar;
            cmd.Parameters["@Comments"].Value = Comments;
            cmd.Parameters["@SysDateTime"].Value = SysDateTime;

            cmd.ExecuteNonQuery();
            cn.Close();
            WebMsgBox.Show("اطلاعات شما با موفقیت ثبت شد، به زودی با شما تماس خواهیم گرفت .");
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است .");
            WebMsgBox.Show(ex.Message);
        }
    }
}
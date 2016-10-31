using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using System.Resources;
using Opal;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using Zamani;
using System.Data;
using Opal.Providers;

public partial class UserRegistration : PageBaseClass
{
	protected void Page_Init(object sender, EventArgs e)
	{
		if (!_website.AllowUserSelfRegistration)
			Response.Redirect("~/Login.aspx", true);

		createUserWizard.CreatedUser += new EventHandler(createUserWizard_CreatedUser);
	}
	void createUserWizard_CreatedUser(object sender, EventArgs e)
	{
        try
        {
            OurMembershipUser user = (OurMembershipUser)Membership.GetUser(createUserWizard.UserName);

            TextBox FirstName = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("FirstName");
            TextBox LastName = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("LastName");
            TextBox NationalCode = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("NationalCode");
            TextBox IntroduceWay = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("IntroduceWay");
            TextBox Concessionaire = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("Concessionaire");
            TextBox GradeLevel = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("GradeLevel");
            TextBox CompanyName = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("CompanyName");
            TextBox RegisterNo = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("RegisterNo");
            TextBox ActivityField = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("ActivityField");
            TextBox PostInCo = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("PostInCo");
            TextBox Website = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("Website");
            TextBox CompanyEmail = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("CompanyEmail");
            TextBox Tel = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("Tel");
            TextBox Mobile = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("Mobile");
            TextBox PostalCode = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("PostalCode");
            TextBox Address = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("Address");
            TextBox AgentName = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("AgentName");
            TextBox AgentTelNo = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("AgentTelNo");
            TextBox Comment = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("Comment");
            TextBox OldSMS = (TextBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("OldSMS");
            CheckBox Subscriber = (CheckBox)createUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("Subscriber");

            user.FirstName = FirstName.Text;
            user.LastName = LastName.Text;
            user.NationalCode = Int64.Parse(NationalCode.Text);
            user.IntroduceWay = IntroduceWay.Text;
            user.Concessionaire = Concessionaire.Text;
            user.GradeLevel = GradeLevel.Text;
            user.CompanyName = CompanyName.Text;
            user.RegisterNo = RegisterNo.Text == string.Empty ? 0 : int.Parse(RegisterNo.Text);
            user.ActivityField = ActivityField.Text;
            user.PostInCo = PostInCo.Text;
            user.Website = Website.Text;
            user.CompanyEmail = CompanyEmail.Text;
            user.Tel = Int64.Parse(Tel.Text);
            user.Mobile = Int64.Parse(Mobile.Text);
            user.PostalCode = Int64.Parse(PostalCode.Text);
            user.Address = Address.Text;
            user.AgentName = AgentName.Text == string.Empty ? string.Empty : AgentName.Text;
            user.AgentTelNo = AgentTelNo.Text == string.Empty ? 0 : Int64.Parse(AgentTelNo.Text);
            user.Comment = Comment.Text;
            user.OldSMS = OldSMS.Text;
            user.IsSubscriber = Subscriber.Checked;

            Membership.UpdateUser(user);

            //Send email to user for verifying account
            string url = string.Concat("http://", Request.Url.Authority, Response.ApplyAppPathModifier("~/Login.aspx?activate="));

            MailMessage mail = new MailMessage(_website.MailSenderAddress, user.Email);
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Subject = string.Format(Resources.StringsRes.pge_UserRegistration_ActivationEmailSubject, _website.WebSiteTitle);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = false;
            mail.Body = string.Format(Resources.StringsRes.pge_UserRegistration_ActivationEmailBody, _website.WebSiteTitle, url, user.ProviderUserKey.ToString());

            SmtpClient client = new SmtpClient(_website.SmtpServer);

            //when Smtp user/password/domain is given, SMTP-Authentication has to be used
            if (_website.SmtpUser != "" && _website.SmtpPassword != "" && _website.SmtpDomain != "")
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_website.SmtpUser, _website.SmtpPassword, _website.SmtpDomain);
            }

            client.Send(mail);
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است :" + ex.Message);
        }

	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			(createUserStep.ContentTemplateContainer.FindControl("EmailRegexValidator") as RegularExpressionValidator).ValidationExpression = Validation.EmailRegex;
		}
	}
    protected void valAntiBotImage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Control ctl = createUserStep.ContentTemplateContainer.FindControl("txtAntiBotImage");
        TextBox txtAntiBotImage = (TextBox)ctl;
        args.IsValid = (Session["antibotimage"] != null) && (txtAntiBotImage.Text.Trim().ToUpper() == (string)Session["antibotimage"]);
    }
}
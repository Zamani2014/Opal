using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal;
using Opal.Controls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;
using Opal.Providers;

public partial class NLogin : PageBaseClass
{
    private string _returnURL = string.Empty;
    private MD5 md5 = MD5.Create();

	protected void Page_Init(object sender, EventArgs e)
	{
		if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["activate"]))
		{
			Guid userId = new Guid(Request.QueryString["activate"]);
            //MembershipUser user = Membership.GetUser(userId);
            OurMembershipUser user = (OurMembershipUser)Membership.GetUser(userId);
            user.IsApproved = true;
			Membership.UpdateUser(user);

			Login1.UserName = user.UserName;

			Login1.FindControl("trActivationSuccess").Visible = true;
		}

		Login1.FindControl("trCreateUser").Visible = _website.AllowUserSelfRegistration;
	}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Control ctl = Login1.FindControl("UserName");
            if (ctl != null)
            {
                (ctl).Focus();
            }
            
            //hack for maintaining the virtual url
            if (Session["OriginalURL"] != null)
                ViewState["ReturnURL"] = Session["OriginalURL"].ToString();
        }
        else
        {
            //hack for maintaining the virtual url
            if(ViewState["ReturnURL"] != null)
                _returnURL = ViewState["ReturnURL"].ToString();
        }
    }

    protected void OnLoggingIn(object sender, EventArgs e)
    {
        //hack for upper and lower case: the asp login control is case sensitive: Admin <> admin
        //in order to make it not case sensitive, we rewrite the UserName (as it should be) to the login control
        TextBox txtUserName = (TextBox)Login1.FindControl("UserName");
        foreach (MembershipUser user in Membership.GetAllUsers())
        {
            if (string.Compare(user.UserName, txtUserName.Text, true) == 0)
            {
                Login1.UserName = user.UserName;
                return;
            }
        }
    }

    /// <summary>
    /// hack for maintaining the virtual url
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OnLoggedIn(object sender, EventArgs e)
    {
        //if(_returnURL != string.Empty)
            //Response.Redirect(_returnURL);
        Response.Redirect("~/services/default.aspx");
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        TextBox txtUserName = (TextBox)Login1.FindControl("UserName");
        TextBox txtPassword = (TextBox)Login1.FindControl("Password");

        try
        {
            if (!(Roles.RoleExists("Users")))
            {
                Roles.CreateRole("Users");
            }
            SqlConnection cn1 = new SqlConnection(@"Server=.\SQLEXPRESS; uid=zamani; pwd=adslpass; Database=ADSLDB");
            string strcn = "SELECT password FROM students WHERE studentID='" + txtUserName.Text + "'";
            SqlCommand cmd1 = new SqlCommand(strcn, cn1);
            cn1.Open();
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                string passwordInput;
                string mystr = dr["password"].ToString();
                bool pass;

                pass = mystr.Contains(":");
                if (pass)
                {
                    String salt = string.Empty;
                    int found = 0;
                    found = mystr.IndexOf(":");
                    salt = mystr.Substring(found + 1).Trim();

                    byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(txtPassword.Text + salt));
                    StringBuilder sBuilder = new StringBuilder();

                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    // Return the hexadecimal string.
                    passwordInput = sBuilder.ToString();

                    string mystr2 = dr["password"].ToString();
                    string passwordTable = mystr2.Substring(0, 32);
                    // string email = dr["email"].ToString();

                    if (passwordTable == passwordInput)
                    {
                        if (Roles.FindUsersInRole("Users", txtUserName.Text) == null)
                        {
                            Roles.AddUserToRole(txtUserName.Text, "Users");
                        }
                        FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                        AuthenticateEventArgs a = new AuthenticateEventArgs();
                        a.Authenticated = true;
                        Response.Redirect("~/Logged_on/AdslReg.aspx");

                    }
                    else
                    {
                        Zamani.WebMsgBox.Show("اسم رمز شما اشتباه است ، لطفا مجددا و با دقت سعی نمائید");
                    }

                }
                else
                {
                    byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(txtPassword.Text));
                    StringBuilder sBuilder = new StringBuilder();

                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    // Return the hexadecimal string.
                    passwordInput = sBuilder.ToString();

                    if (dr["password"].ToString() == passwordInput)
                    {
                        if (Roles.FindUsersInRole("Users", txtUserName.Text) == null)
                        {
                            Roles.AddUserToRole(txtUserName.Text, "Users");
                        }
                        FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                        AuthenticateEventArgs a = new AuthenticateEventArgs();
                        a.Authenticated = true;
                        Response.Redirect("~/Logged_on/AdslReg.aspx");

                    }
                    else
                    {
                        Zamani.WebMsgBox.Show("اسم رمز شما اشتباه است ، لطفا مجددا و با دقت سعی نمائید");
                    }
                }
            } // End If
            else
            {
                cn1.Close();
                Zamani.WebMsgBox.Show("نام کاربری/شماره دانشجوئی شما اشتباه است ، لطفا مجددا و با دقت سعی نمائید");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}

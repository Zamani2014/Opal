using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamani;
using System.Data;

public partial class EasyControls_TopMenu : System.Web.UI.UserControl
{
    #region Variables
    private String username;
    private String password;
    private String domain;
    private int N;
    private bool useProxy;
    private String proxyAddress;
    private String proxyUsername;
    private String proxyPassword;
    string[,] provinceNames;
    string[,] cityNames;
    private int SMSFee;
    private long SMSCount;
    private int messagePart;
    private static int BulkID = 0;
    private static string BulkMsg = String.Empty;
    private static int BulkCode = 0;
    private static long RecipientID = 0;
    #endregion
    #region Account Variables
    private long Credit;
    private string senderNumber;
    private string FirstName;
    private string LastName;
    private string Concessionaire;
    private string AgentName;
    private string Charge;
    private DateTime LastLoginDate;
    private DateTime CreationDate;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Get Credit & Sender Number
        string userName = Page.User.Identity.Name;
        DataSet DS = new DataSet();
        DS.ReadXml(Server.MapPath("~/App_Data/Users.config"));
        DataTable users = DS.Tables[0];
        var query = from user in users.AsEnumerable()
                    where user.Field<string>("Username") == userName
                    select new
                    {
                        LineNo = user.Field<long>("LineNo"),
                        Credit = user.Field<long>("Credit"),
                        FirstName = user.Field<string>("FirstName"),
                        LastName = user.Field<string>("LastName"),
                        Concessionaire = user.Field<string>("Concessionaire"),
                        AgentName = user.Field<string>("AgentName"),
                        Charge = user.Field<long>("Charge"),
                        LastLoginDate = user.Field<DateTime>("LastLoginDate"),
                        CreationDate = user.Field<DateTime>("CreationDate")
                    };
        foreach (var lines in query)
        {
            senderNumber = lines.LineNo.ToString();
            Credit = lines.Credit;
            FirstName = lines.FirstName.ToString();
            LastName = lines.LastName.ToString();
            Concessionaire = lines.Concessionaire.ToString();
            AgentName = lines.AgentName.ToString();
            Charge = lines.Charge.ToString();
            LastLoginDate = lines.LastLoginDate;
            CreationDate = lines.CreationDate;
        }
        #endregion
        #region Variables Initializations
        username = System.Configuration.ConfigurationManager.AppSettings["Username"];
        password = System.Configuration.ConfigurationManager.AppSettings["Password"];
        domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
        //senderNumber = System.Configuration.ConfigurationManager.AppSettings["SenderNumber"];
        useProxy = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseProxy"]);
        proxyAddress = System.Configuration.ConfigurationManager.AppSettings["ProxyAddress"];
        proxyUsername = System.Configuration.ConfigurationManager.AppSettings["ProxyUsername"];
        proxyPassword = System.Configuration.ConfigurationManager.AppSettings["ProxyPassword"];
        #endregion
        #region PanelLoader
        this.FirstAndLastName.Text = FirstName + " " + LastName;
        this.Concessionairetxbx.Text = Concessionaire;
        this.LineNotxbx.Text = senderNumber;
        this.AgentNametxbx.Text = AgentName;
        this.Credittxbx.Text = Credit.ToString();
        this.Chargetxbx.Text = Charge.ToString();
        this.LastLogintxbx.Text = LastLoginDate.ToString();
        this.CreationDatetxbx.Text = CreationDate.ToString();
        this.TodayDateLabel.Text = GetDateTime.GenerateDateTime();
        #endregion
    }
    private void Menu1_ItemSelected(object sender, ComponentArt.Web.UI.MenuItemEventArgs e)
    {
        if (e.Item.ID == "Services" || e.Item.ID == "MenuItem1" || e.Item.ID == "MenuItem2" || e.Item.ID == "MenuItem3" || e.Item.ID == "MenuItem4" || e.Item.ID == "MenuItem5" || e.Item.ID == "MenuItem6" || e.Item.ID == "MenuItem7" || e.Item.ID == "MenuItem8" || e.Item.ID == "MenuItem9")
        {
            WebMsgBox.Show("شما امکان دسترسی به ویژگی های پیشرفته این سامانه ندارید | برای کسب اطلاعات بیشتر با شماره 02133643817 تماس حاصل فرمائید");
        }
        else if (e.Item.ID == "Scheduling" || e.Item.ID == "MenuItem10" || e.Item.ID == "MenuItem11" || e.Item.ID == "MenuItem12" || e.Item.ID == "MenuItem13")
        {
            WebMsgBox.Show("شما امکان دسترسی به ویژگی های پیشرفته این سامانه ندارید | برای کسب اطلاعات بیشتر با شماره 02133643817 تماس حاصل فرمائید");
        }
        else if (e.Item.ID == "Reports" || e.Item.ID == "MenuItem14" || e.Item.ID == "MenuItem15" || e.Item.ID == "MenuItem16" || e.Item.ID == "MenuItem17" || e.Item.ID == "MenuItem18" || e.Item.ID == "MenuItem19" || e.Item.ID == "MenuItem20")
        {
            WebMsgBox.Show("شما امکان دسترسی به ویژگی های پیشرفته این سامانه ندارید | برای کسب اطلاعات بیشتر با شماره 02133643817 تماس حاصل فرمائید");
        }
        else if (e.Item.ID == "SendFastSMS")
        {
            Response.Redirect("~/Services/SMS/SendSMS.aspx");
        }
        else if (e.Item.ID == "SendSMSFromFile")
        {
            Response.Redirect("~/Services/SMS/SendSMSFromFile.aspx");
        }
        else if (e.Item.ID == "SendBulkSMS")
        {
            Response.Redirect("~/Services/SMS/SendBulkSMS.aspx");
        }
        else if (e.Item.ID == "SMSInbox")
        {
            Response.Redirect("~/Services/SMS/SMSInbox.aspx");
        }
        else if (e.Item.ID == "SMSOutbox")
        {
            Response.Redirect("~/Services/SMS/SMSOutbox.aspx");
        }
        else if (e.Item.ID == "SMSTemplates")
        {
            Response.Redirect("~/Services/SMS/SMSTemplates.aspx");
        }
        else if (e.Item.ID == "SMSFolders")
        {
            Response.Redirect("~/Services/SMS/SMSFolders.aspx");
        }

        else if (e.Item.ID == "Account")
        {
            Response.Redirect("~/account/default.aspx");
        }
        else if (e.Item.ID == "MenuItem21")
        {
            Response.Redirect("~/Databases/Default.aspx");
        }
        else if (e.Item.ID == "MenuItem22")
        {
            Response.Redirect("~/account/calendar.aspx");
        }
        else if (e.Item.ID == "MenuItem23")
        {
            Response.Redirect("~/account/contacts.aspx");
        }
        else if (e.Item.ID == "MenuItem24")
        {
            Response.Redirect("~/account/notes.aspx");
        }
        else if (e.Item.ID == "MenuItem25")
        {
            Response.Redirect("~/account/credit.aspx");
        }
        else if (e.Item.ID == "MenuItem26")
        {
            Response.Redirect("~/account/settings.aspx");
        }
    }
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //this.Menu1.ItemSelected += new ComponentArt.Web.UI.Menu.ItemSelectedEventHandler(this.Menu1_ItemSelected);
        this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion
    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
        if (e.Item.Value == "Services" || e.Item.Value == "MenuItem1" || e.Item.Value == "MenuItem2" || e.Item.Value == "MenuItem3" || e.Item.Value == "MenuItem4" || e.Item.Value == "MenuItem5" || e.Item.Value == "MenuItem6" || e.Item.Value == "MenuItem7" || e.Item.Value == "MenuItem8" || e.Item.Value == "MenuItem9")
        {
            WebMsgBox.Show("شما امکان دسترسی به ویژگی های پیشرفته این سامانه ندارید | برای کسب اطلاعات بیشتر با شماره 02133643817 تماس حاصل فرمائید");
        }
        else if (e.Item.Value == "Scheduling" || e.Item.Value == "MenuItem10" || e.Item.Value == "MenuItem11" || e.Item.Value == "MenuItem12" || e.Item.Value == "MenuItem13")
        {
            WebMsgBox.Show("شما امکان دسترسی به ویژگی های پیشرفته این سامانه ندارید | برای کسب اطلاعات بیشتر با شماره 02133643817 تماس حاصل فرمائید");
        }
        else if (e.Item.Value == "Reports" || e.Item.Value == "MenuItem14" || e.Item.Value == "MenuItem15" || e.Item.Value == "MenuItem16" || e.Item.Value == "MenuItem17" || e.Item.Value == "MenuItem18" || e.Item.Value == "MenuItem19" || e.Item.Value == "MenuItem20")
        {
            WebMsgBox.Show("شما امکان دسترسی به ویژگی های پیشرفته این سامانه ندارید | برای کسب اطلاعات بیشتر با شماره 02133643817 تماس حاصل فرمائید");
        }
        else if (e.Item.Value == "SendFastSMS")
        {
            Response.Redirect("~/Services/SMS/SendSMS.aspx");
        }
        else if (e.Item.Value == "SendSMSFromFile")
        {
            Response.Redirect("~/Services/SMS/SendSMSFromFile.aspx");
        }
        else if (e.Item.Value == "SendBulkSMS")
        {
            Response.Redirect("~/Services/SMS/SendBulkSMS.aspx");
        }
        else if (e.Item.Value == "SMSInbox")
        {
            Response.Redirect("~/Services/SMS/SMSInbox.aspx");
        }
        else if (e.Item.Value == "SMSOutbox")
        {
            Response.Redirect("~/Services/SMS/SMSOutbox.aspx");
        }
        else if (e.Item.Value == "SMSTemplates")
        {
            Response.Redirect("~/Services/SMS/SMSTemplates.aspx");
        }
        else if (e.Item.Value == "SMSFolders")
        {
            Response.Redirect("~/Services/SMS/SMSFolders.aspx");
        }
        else if (e.Item.Value == "Account")
        {
            Response.Redirect("~/account/default.aspx");
        }
        else if (e.Item.Value == "MenuItem21")
        {
            Response.Redirect("~/Databases/Default.aspx");
        }
        else if (e.Item.Value == "MenuItem22")
        {
            Response.Redirect("~/account/calendar.aspx");
        }
        else if (e.Item.Value == "MenuItem23")
        {
            Response.Redirect("~/account/contacts.aspx");
        }
        else if (e.Item.Value == "MenuItem24")
        {
            Response.Redirect("~/account/notes.aspx");
        }
        else if (e.Item.Value == "MenuItem25")
        {
            Response.Redirect("~/account/credit.aspx");
        }
        else if (e.Item.Value == "MenuItem26")
        {
            Response.Redirect("~/account/settings.aspx");
        }
    }
}
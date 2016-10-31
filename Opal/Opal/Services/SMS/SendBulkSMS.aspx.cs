using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using Zamani;
using ArvidfavaSMS;
using System.Drawing;
using System.Globalization;
using System.Net;
using Heidarpour.WebControlUI;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Administration_ShortMessageService_getProvinces : PageBaseClass
{
    #region Webservices
    string MyConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();
    MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
    com.magfa.sms.BulkSoapImplService bs = new com.magfa.sms.BulkSoapImplService();
    #endregion

    #region AccountVariables
    private String username;
    private String password;
    private String domain;
    private String senderNumber;
    private int N;
    private bool useProxy;
    private String proxyAddress;
    private String proxyUsername;
    private String proxyPassword;
    string[,] provinceNames;
    string[,] cityNames;
    #endregion
    #region Account Fee
    private static int SMSFee;
    private long Credit;
    private long SMSCount;
    private static int messagePart;
    #endregion
    #region IDs
    private static string BulkID = String.Empty;
    private static string BulkMsg = String.Empty;
    private static int BulkCode = 0;
    private static long RecipientID = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 
        if (!IsPostBack)
        {
            this.UpdatePanel1.Visible = true;
            this.UpdatePanel3.Visible = false;
            this.RadioButton2.Enabled = false;

            this.UpdatePanel4.Visible = false;
            //this.UpdatePanel5.Visible = false;

            this.RadioButton4.Checked = true;
            this.DatePicker1.Date = DateTime.Now;
            this.TimeSelector1.Date = DateTime.Now;
        }

        //if (!IsPostBack)
        //{
        //    Control ctrl = UpdatePanel2.FindControl("RadioButton4");
        //    RadioButton rbtn = (RadioButton)ctrl;
        //    rbtn.Checked = true;
        //}
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
            Credit = lines.Credit;
        }
        #endregion
    }

    #region Event Handlers
    protected void RadioBtn1_CheckedChanged(object sender, EventArgs e)
    {
        this.CountLabel.Text = "";
        try
        {
            if (this.RadioButton1.Checked == true)
            {
                bs.Credentials = new System.Net.NetworkCredential(username, password);
                bs.PreAuthenticate = true;

                string[] Provinces;

                Provinces = bs.getProvinces(domain);
                int arrayLength = Provinces.Length;

                provinceNames = new string[arrayLength, arrayLength];
                int found = 0;

                if (DropDownList1.Items.Count == 0)
                {
                    for (int i = 0; i < Provinces.Length; i++)
                    {
                        found = Provinces[i].IndexOf(":");
                        provinceNames[i, 0] = Provinces[i].Substring(found + 1).Trim(); // Province Name
                        provinceNames[0, i] = Provinces[i].Substring(0, found).Trim(); // Province Code
                        DropDownList1.Items.Add(new ListItem(provinceNames[i, 0], provinceNames[0, i]));
                    }
                }

                //DropDownList1.Items.Insert(0, new ListItem("لطفا استان را انتخاب کنید", "-1"));
                DropDownList1.DataBind();
                DropDownList3.DataSource = string.Empty;
                DropDownList3.DataBind();

                this.Label14.Text = "انتخاب استان و شهر :";
                this.DropDownList1.Visible = true;
                this.DropDownList3.Visible = true;
                this.fourNumbersTxbx.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("sms.magfa.com"))
            {
                WebMsgBox.Show("متاسفانه سرور اپراتور مخابرات پاسخی نمی دهد، لطفا چند دقیقه بعد تلاش کنید !");
            }
            else
            {
                WebMsgBox.Show("خطایی رخ داده است :" + ex.Message);
            }
        }
        finally
        {
        }
    }
    protected void RadioBtn2_CheckedChanged(object sender, EventArgs e)
    {
        if (this.RadioButton2.Checked == true)
        {
            this.Label14.Text = "چهار رقم کد پستی :";
            this.DropDownList1.Visible = false;
            this.DropDownList3.Visible = false;
            this.fourNumbersTxbx.Visible = true;
            this.CountLabel.Text = "";
        }
    }
    protected void RadioBtn3_CheckedChanged(object sender, EventArgs e)
    {
        if (this.RadioButton3.Checked == true)
        {
            this.Label14.Text = "چهار رقم پیش شماره :";
            this.DropDownList1.Visible = false;
            this.DropDownList3.Visible = false;
            this.fourNumbersTxbx.Visible = true;
            this.CountLabel.Text = "";
        }
    }
    protected void Drp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int SIMType = int.Parse(this.DropDownList6.SelectedValue);

            bs.Credentials = new System.Net.NetworkCredential(username, password);
            bs.PreAuthenticate = true;

            string provinceSelected = DropDownList1.SelectedValue;
            int found = 0;
            int provinceCode = int.Parse(provinceSelected);

            string[] cities = bs.getCitiesOfProvince(domain, provinceCode);
            int arrayLength = cities.Length;

            cityNames = new string[arrayLength, arrayLength];

            if (this.DropDownList3.Items.Count == 0)
            {
                for (int i = 0; i < arrayLength; i++)
                {
                    found = cities[i].IndexOf(":");
                    cityNames[i, 0] = cities[i].Substring(found + 1).Trim(); // City Names
                    cityNames[0, i] = cities[i].Substring(0, found).Trim(); // City Codes
                    DropDownList3.Items.Add(new ListItem(cityNames[i, 0], cityNames[0, i]));
                }
            }
            else
            {
                DropDownList3.Items.Clear();

                for (int i = 0; i < arrayLength; i++)
                {
                    found = cities[i].IndexOf(":");
                    cityNames[i, 0] = cities[i].Substring(found + 1).Trim(); // City Names
                    cityNames[0, i] = cities[i].Substring(0, found).Trim(); // City Codes
                    DropDownList3.Items.Add(new ListItem(cityNames[i, 0], cityNames[0, i]));
                }
            }

            DropDownList3.DataTextField = "انتخاب کنید";
            DropDownList3.Items.Insert(0, new ListItem("لطفا شهر را انتخاب کنید", "-1"));
            DropDownList3.DataBind();

            string citySelected = DropDownList3.SelectedValue;
            int cityCode = int.Parse(citySelected);

            this.CityProvinceCountLabel.Text = "تعداد گیرندگان فعال :";
            this.CountLabel.Text = bs.getCountByCityProvince(domain, provinceCode, cityCode, SIMType).ToString();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("خطایی رخ داده است :" + ex.Message + ex.StackTrace);
        }
    }
    protected void Drp3_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SIMType = int.Parse(this.DropDownList6.SelectedValue);

        bs.Credentials = new System.Net.NetworkCredential(username, password);
        bs.PreAuthenticate = true;

        string provinceSelected = DropDownList1.SelectedValue;
        int found = 0;
        //string provinceName = provinceName = provinceSelected.Substring(found + 1).Trim();

        //found = provinceSelected.IndexOf(":");
        //string provinceId = provinceSelected.Substring(0, found).Trim();
        int provinceCode = int.Parse(provinceSelected);

        //string[] cities;
        //cities = bs.getCitiesOfProvince(domain, provinceCode);
        //DropDownList3.DataSource = cities;
        //DropDownList3.DataBind();

        string citySelected = DropDownList3.SelectedValue;
        //found = citySelected.IndexOf(":");
        //string cityId = citySelected.Substring(0, found).Trim();
        int cityCode = int.Parse(citySelected);

        this.CityProvinceCountLabel.Text = "تعداد گیرندگان فعال :";
        this.CountLabel.Text = bs.getCountByCityProvince(domain, provinceCode, cityCode, SIMType).ToString();
    }
    protected void Drp6_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CityProvinceCountLabel.Text = "تعداد گیرندگان فعال :";
        if (this.RadioButton1.Checked == true)
        {
            // Province & State
            int SIMType = int.Parse(this.DropDownList6.SelectedValue);

            bs.Credentials = new System.Net.NetworkCredential(username, password);
            bs.PreAuthenticate = true;

            string provinceSelected = DropDownList1.SelectedValue;

            int provinceCode = GetNumber(provinceSelected);

            string citySelected = DropDownList3.SelectedValue;
            int cityCode = GetNumber(citySelected);

            this.CountLabel.Text = bs.getCountByCityProvince(domain, provinceCode, cityCode, SIMType).ToString();
        }
        else if (this.RadioButton2.Checked == true)
        {
            // Postal Code
            int SIMType = int.Parse(this.DropDownList6.SelectedValue);
            string postalCode = this.fourNumbersTxbx.Text;
            this.CountLabel.Text = bs.getCountByPostCode(domain, postalCode, SIMType).ToString();
        }
        else if (this.RadioButton3.Checked == true)
        {
            // Pre Tel Number
            int SIMType = int.Parse(this.DropDownList6.SelectedValue);
            string preNumber = this.fourNumbersTxbx.Text;
            this.CountLabel.Text = bs.getCountByPrefix(domain, preNumber, SIMType).ToString();
        }
    }
    protected void fourNumbersTxbx_TextChanged(object sender, EventArgs e)
    {
        bs.Credentials = new System.Net.NetworkCredential(username, password);
        bs.PreAuthenticate = true;
        this.CityProvinceCountLabel.Text = "تعداد گیرندگان فعال :";
        if (this.RadioButton2.Checked == true)
        {
            // Postal Code
            int SIMType = int.Parse(this.DropDownList6.SelectedValue);
            string postalCode = this.fourNumbersTxbx.Text;
            int numbersCount = bs.getCountByPostCode(domain, postalCode, SIMType);
            if (numbersCount < 0)
            {
                this.CountLabel.Text = "کد پستی وارد شده نامعتبر است .";
                this.CountLabel.ForeColor = Color.Red;
            }
            else
            {
                this.CountLabel.Text = numbersCount.ToString();
                this.CountLabel.ForeColor = Color.Black;
            }

        }
        else if (this.RadioButton3.Checked == true)
        {
            // Pre Tel Number
            int SIMType = int.Parse(this.DropDownList6.SelectedValue);
            string preNumber = this.fourNumbersTxbx.Text;
            int numbersCount = bs.getCountByPrefix(domain, preNumber, SIMType);
            if (numbersCount < 0)
            {
                this.CountLabel.Text = "پیش شماره وارد شده نامعتبر است .";
                this.CountLabel.ForeColor = Color.Red;
            }
            else
            {
                this.CountLabel.Text = numbersCount.ToString();
                this.CountLabel.ForeColor = Color.Black;
            }
        }
    }
    protected void radioBtn1(object sender, EventArgs e)
    {
        if (this.RadioButton4.Checked == true)
        {
            this.UpdatePanel1.Visible = true;
            this.UpdatePanel3.Visible = false;
            this.UpdatePanel4.Visible = false;
        }
    }
    protected void radioBtn2(object sender, EventArgs e)
    {
        if (this.RadioButton5.Checked == true)
        {
            this.UpdatePanel1.Visible = false;
            this.UpdatePanel3.Visible = true;

            this.DataGrid2.DataSource = GetData();
            this.DataGrid2.DataBind();
        }
    }
    protected void MessageTextBox_TextGhanged(object sender, EventArgs e)
    {
        int chars = this.MessageTextBox.Text.Length;
        Label7.Text = chars.ToString();

        DialogueMaster.Babel.BabelModel model = DialogueMaster.Babel.BabelModel._AllModel;
        string input = MessageTextBox.Text;
        DialogueMaster.Classification.ICategoryList result = model.ClassifyText(input, 1);
        DialogueMaster.Classification.ICategory category = result[0];
        if (category.Name == "fa" || category.Name == "ar")
        {
            SMSFee = GetFee.GetFarsiSMSFee(Credit);
            Label11.Text = "فارسی";
            if (chars <= 70)
            {
                this.Label9.Text = "1";
                messagePart = 1;
            }
            else
            {
                int d = chars / 67;
                this.Label9.Text = d.ToString();
                messagePart = d;
            }
        }
        else
        {
            SMSFee = GetFee.GetEnglishSMSFee(Credit);
            Label11.Text = "انگلیسی";
            if (chars <= 160)
            {
                this.Label9.Text = "1";
                messagePart = 1;
            }
            else
            {
                int d = chars / 153;
                this.Label9.Text = d.ToString();
                messagePart = d;
            }
        }
    }
    protected void DataGrid2_OnSelect(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
        BulkID = e.Item["BulkID"].ToString();
        BulkMsg = e.Item["Message"].ToString();
        BulkCode = int.Parse(e.Item["ID"].ToString());
        this.DataGrid1.DataSource = GetData2();
        this.DataGrid1.DataBind();

        int chars = BulkMsg.Length;

        DialogueMaster.Babel.BabelModel model = DialogueMaster.Babel.BabelModel._AllModel;
        string input = BulkMsg;
        DialogueMaster.Classification.ICategoryList result = model.ClassifyText(input, 1);
        DialogueMaster.Classification.ICategory category = result[0];
        if (category.Name == "fa" || category.Name == "ar")
        {
            SMSFee = GetFee.GetFarsiSMSFee(Credit);

            if (chars <= 70)
            {
                messagePart = 1;
            }
            else
            {
                int d = chars / 67;
                messagePart = d;
            }
        }
        else
        {
            SMSFee = GetFee.GetEnglishSMSFee(Credit);

            if (chars <= 160)
            {
                messagePart = 1;
            }
            else
            {
                int d = chars / 153;
                messagePart = d;
            }
        }
    }
    protected void DataGrid1_OnSelect(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
        RecipientID = long.Parse(e.Item["ID"].ToString());
    }
    protected void DataGrid2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BulkID = DataGrid2.SelectedRow.Cells[2].Text;
        BulkMsg = DataGrid2.SelectedRow.Cells[3].Text;
        BulkCode = int.Parse(DataGrid2.SelectedRow.Cells[1].Text);
        this.DataGrid1.DataSource = GetData2();
        this.DataGrid1.DataBind();

        int chars = BulkMsg.Length;

        DialogueMaster.Babel.BabelModel model = DialogueMaster.Babel.BabelModel._AllModel;
        string input = BulkMsg;
        DialogueMaster.Classification.ICategoryList result = model.ClassifyText(input, 1);
        DialogueMaster.Classification.ICategory category = result[0];
        if (category.Name == "fa" || category.Name == "ar")
        {
            SMSFee = GetFee.GetFarsiSMSFee(Credit);

            if (chars <= 70)
            {
                messagePart = 1;
            }
            else
            {
                int d = chars / 67;
                messagePart = d;
            }
        }
        else
        {
            SMSFee = GetFee.GetEnglishSMSFee(Credit);

            if (chars <= 160)
            {
                messagePart = 1;
            }
            else
            {
                int d = chars / 153;
                messagePart = d;
            }
        }
    }
    protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecipientID = long.Parse(DataGrid1.SelectedRow.Cells[1].Text);
    }
    #endregion

    #region Operational Buttons
    protected void DataRefresh_Click(object sender, EventArgs e)
    {// Bulk Table Reload
        this.DataGrid2.DataSource = GetData();
        this.DataGrid2.DataBind();
    }
    protected void DataDelete_Click(object sender, EventArgs e)
    {// Bulk Delete Button
        if (BulkCode == 0)
        {
            WebMsgBox.Show("لطفا یک پیامک را از فهرست انتخاب نمائید .");
        }
        else
        {
            SqlConnection cn = new SqlConnection(MyConnectionString);
            cn.Open();
            string query = "DELETE FROM SendBulkTbl WHERE ID=" + BulkCode + "";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.ExecuteNonQuery();
            WebMsgBox.Show("پیامک انبوه مورد نظر با موفقیت حذف شد");
            cn.Close();
            this.DataGrid2.DataSource = GetData();
            this.DataGrid2.DataBind();
        }
    }
    protected void StatusCheck_Click(object sender, EventArgs e)
    {// Bulk Status Check Button
        try
        {
            #region Authentications
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }

            sq.Credentials = new System.Net.NetworkCredential(username, password);
            sq.PreAuthenticate = true;

            bs.Credentials = new System.Net.NetworkCredential(username, password);
            bs.PreAuthenticate = true;
            #endregion
            if (BulkID == String.Empty)
            {
                WebMsgBox.Show("لطفا از فهرست پیامک های انبوه، پیامک مورد نظر خود را برای بررسی وضعیت انتخاب نمائید.");
            }
            else
            {
                string BulkStatus = bs.getBulkStatus(domain, BulkID);
                if (IsNumber(BulkStatus))
                {
                    WebMsgBox.Show("متاسفانه برای بررسی وضعیت پیامک مشکلی ایجاد شده است : " + ShortMessageService.getDescriptionForBulkCode(int.Parse(BulkStatus)));
                }
                else
                {
                    string statusResult = ShortMessageService.getDescriptionForBulkStatus(BulkStatus);
                    WebMsgBox.Show("وضعیت پیامک انتخابی برابر است با" + " : " + statusResult);
                    SqlConnection cn = new SqlConnection(MyConnectionString);
                    cn.Open();
                    string query = "UPDATE SendBulkTbl SET BulkStatus='" + statusResult + "' WHERE BulkID='" + BulkID + "'";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    this.DataGrid2.DataSource = GetData();
                    this.DataGrid2.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطائی رخ داده است : " + ex.Message);
            //Response.Write("پشته خطا : " + ex.StackTrace);
        }
    }
    protected void RecipientsAddBtn_Click(object sender, EventArgs e)
    {//Recipients Button
        this.UpdatePanel4.Visible = true;
        this.DataGrid1.DataSource = GetData2();
        this.DataGrid1.DataBind();

        this.DataGrid2.DataSource = GetData();
        this.DataGrid2.DataBind();
    }
    protected void BulkOpen_Click(object sender, EventArgs e)
    {//Open Bulk Button
        if (BulkMsg == String.Empty)
        {
            WebMsgBox.Show("لطفا یک پیامک را از فهرست انتخاب نمائید .");
        }
        else
        {
            WebMsgBox.Show("متن پیامک :" + BulkMsg);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {// Bulk Fee Button
        try
        {
            SqlConnection cn = new SqlConnection(MyConnectionString);

            #region Authentications
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }

            sq.Credentials = new System.Net.NetworkCredential(username, password);
            sq.PreAuthenticate = true;

            bs.Credentials = new System.Net.NetworkCredential(username, password);
            bs.PreAuthenticate = true;
            #endregion
            if (BulkID == String.Empty)
            {
                WebMsgBox.Show("لطفا از فهرست پیامک های انبوه پیامک مورد نظر خود را انتخاب نمائید .");
            }
            else
            {
                cn.Open();
                string query = "SELECT Count FROM RecipientsTbl WHERE BulkID='" + BulkID + "'";
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                int arrayLength = 0;

                while (dr.Read())
                {
                    arrayLength += int.Parse(dr[0].ToString());
                }
                //WebMsgBox.Show(arrayLength.ToString());
                int SendSMSCount = messagePart * arrayLength;
                long SMSBulkFee = SendSMSCount * SMSFee;
                WebMsgBox.Show("هزینه ارسال برابر است با :" + SMSBulkFee);
                //string feeBulkID = BulkID.ToString();
                //double fee = bs.getPrice(domain, BulkID);

                //if (fee < 0)
                //{
                //    WebMsgBox.Show("متاسفانه خطایی رخ داده است" + " : " + ShortMessageService.getDescriptionForBulkCode(int.Parse(fee.ToString())));
                //}
                //else
                //{
                //    WebMsgBox.Show(" : هزینه ارسال برابر است با" + fee.ToString());
                //}
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است" + " : " + ex.Message);
        }
    }
    protected void SendBtn_Click(object sender, EventArgs e)
    {// Send Bulk Button
        try
        {
            SqlConnection cn = new SqlConnection(MyConnectionString);
            
            if (Credit > 0)
            {
                #region Authentications
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

                if (BulkID == String.Empty)
                {
                    WebMsgBox.Show("لطفا از فهرست پیامک های انبوه پیامک مورد نظر خود را انتخاب نمائید . !");
                }
                else
                {
                    cn.Open();
                    string query = "SELECT Count FROM RecipientsTbl WHERE BulkID='" + BulkID + "'";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    int arrayLength = 0;

                    while (dr.Read())
                    {
                        arrayLength += int.Parse(dr[0].ToString());
                    }
                    //WebMsgBox.Show(arrayLength.ToString());
                    int SendSMSCount = messagePart * arrayLength;
                    SMSCount = Credit / SMSFee;

                    if (SMSCount >= SendSMSCount)
                    {
                        string sendBulkID = BulkID.ToString();
                        int sendBulkBtn =  bs.submitBulk(domain, sendBulkID);
                        if (sendBulkBtn == 0)
                        {
                            WebMsgBox.Show("پیامک انبوه شما با موفقیت ارسال شد");
                        }
                        else
                        {
                            WebMsgBox.Show("متاسفانه در ارسال پیامک انبوه شما خطایی رخ داده است" + " : " + ShortMessageService.getDescriptionForBulkCode(sendBulkBtn));
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("شما اعتبار کافی برای ارسال این تعداد پیامک را ندارید و یا متن پیامک طولانی است !");
                    }
                }
            }
            else
            {
                WebMsgBox.Show("شما هیچ اعتباری برای ارسال پیامک ندارید !");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است !" + ex.Message);
        }
    }
    protected void recipientAddBtn_Click(object sender, EventArgs e)
    {//Recipients Add to Table Button
        try
        {
            #region Authentications
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }

            sq.Credentials = new System.Net.NetworkCredential(username, password);
            sq.PreAuthenticate = true;

            bs.Credentials = new System.Net.NetworkCredential(username, password);
            bs.PreAuthenticate = true;

            #endregion
            if (BulkID == String.Empty)
            {
                WebMsgBox.Show("لطفا از فهرست پیامک های انبوه یکی را انتخاب نمائید .");
            }
            else
            {
                if (RadioButton1.Checked == true)
                {
                    //string sendBulkID = BulkID.ToString();
                    int provinceID = int.Parse(DropDownList1.SelectedValue);
                    int cityID = int.Parse(DropDownList3.SelectedValue);
                    int SIMType = int.Parse(this.DropDownList6.SelectedValue);
                    int startIndex = this.TextBox2.Text == string.Empty ? 0 : int.Parse(this.TextBox2.Text);
                    int smsCount = int.Parse(this.TextBox3.Text) == 0 ? int.Parse(CountLabel.Text) : int.Parse(this.TextBox3.Text);

                    if (int.Parse(this.TextBox3.Text) == 0)
                    {
                        startIndex = 0;
                    }

                    string addRecipientResult = bs.addRecipientsByCityProvince(domain, BulkID, provinceID, cityID, SIMType, startIndex, smsCount);

                    if (IsNumber(addRecipientResult))
                    {
                        WebMsgBox.Show("متاسفانه خطایی رخ داده است" + " : " + ShortMessageService.getDescriptionForBulkCode(int.Parse(addRecipientResult)));
                    }
                    else
                    {
                        //WebMsgBox.Show(addRecipientResult);
                        //int ID = GenerateIDColumn.GetNewID("RecipientsTbl");

                        SqlConnection cn = new SqlConnection(MyConnectionString);
                        cn.Open();
                        string query = "INSERT INTO RecipientsTbl (ID, SendBy, BulkID, Code, StartIndex, Count, SIMType, Comments, SysDateTime) values (@ID, @SendBy, @BulkID, @Code, @StartIndex, @Count, @SIMType, @Comments, @SysDateTime)";
                        SqlCommand cmd = new SqlCommand(query, cn);

                        cmd.Parameters.Add("@ID", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SendBy", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@BulkID", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Code", SqlDbType.NVarChar); ;
                        cmd.Parameters.Add("@StartIndex", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Count", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SIMType", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SysDateTime", SqlDbType.NVarChar);

                        cmd.Parameters["@ID"].Value = long.Parse(addRecipientResult);
                        cmd.Parameters["@SendBy"].Value = "استان و شهر";
                        cmd.Parameters["@BulkID"].Value = BulkID;
                        cmd.Parameters["@Code"].Value = DropDownList1.SelectedItem.Text + " - " + DropDownList3.SelectedItem.Text;
                        cmd.Parameters["@StartIndex"].Value = startIndex;
                        cmd.Parameters["@Count"].Value = smsCount;
                        cmd.Parameters["@SIMType"].Value = DropDownList6.SelectedItem.Text;
                        cmd.Parameters["@Comments"].Value = "";
                        cmd.Parameters["@SysDateTime"].Value = GetDateTime.GenerateDateTime();

                        cmd.ExecuteNonQuery();
                        WebMsgBox.Show("گیرنده مورد نظر با موفقیت افزوده شد");
                        cn.Close();
                        this.DataGrid1.DataSource = GetData2();
                        this.DataGrid1.DataBind();
                    }

                }
                else if (RadioButton2.Checked == true)
                {
                    //string sendBulkID = BulkID.ToString();
                    string postCode = fourNumbersTxbx.Text;
                    int SIMType = int.Parse(this.DropDownList6.SelectedValue);
                    int startIndex = this.TextBox2.Text == string.Empty ? 0 : int.Parse(this.TextBox2.Text);
                    int smsCount = int.Parse(this.TextBox3.Text) == 0 ? int.Parse(CountLabel.Text) : int.Parse(this.TextBox3.Text);

                    if (int.Parse(this.TextBox3.Text) == 0)
                    {
                        startIndex = 0;
                    }

                    string addRecipientResult = bs.addRecipientsByPostCode(domain, BulkID, postCode, SIMType, startIndex, smsCount);

                    if (IsNumber(addRecipientResult))
                    {
                        WebMsgBox.Show("متاسفانه خطایی رخ داده است" + " : " + ShortMessageService.getDescriptionForBulkCode(int.Parse(addRecipientResult)));
                    }
                    else
                    {
                        //int ID = GenerateIDColumn.GetNewID("RecipientsTbl");

                        SqlConnection cn = new SqlConnection(MyConnectionString);
                        cn.Open();
                        string query = "INSERT INTO RecipientsTbl (ID, SendBy, BulkID, Code, StartIndex, Count, SIMType, Comments, SysDateTime) values (@ID, @SendBy, @BulkID, @Code, @StartIndex, @Count, @SIMType, @Comments, @SysDateTime)";
                        SqlCommand cmd = new SqlCommand(query, cn);

                        cmd.Parameters.Add("@ID", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SendBy", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@BulkID", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Code", SqlDbType.NVarChar); ;
                        cmd.Parameters.Add("@StartIndex", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Count", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SIMType", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SysDateTime", SqlDbType.NVarChar);

                        cmd.Parameters["@ID"].Value = long.Parse(addRecipientResult);
                        cmd.Parameters["@SendBy"].Value = "کد پستی";
                        cmd.Parameters["@BulkID"].Value = BulkID;
                        cmd.Parameters["@Code"].Value = postCode;
                        cmd.Parameters["@StartIndex"].Value = startIndex;
                        cmd.Parameters["@Count"].Value = smsCount;
                        cmd.Parameters["@SIMType"].Value = DropDownList6.SelectedItem.Text;
                        cmd.Parameters["@Comments"].Value = "";
                        cmd.Parameters["@SysDateTime"].Value = GetDateTime.GenerateDateTime();

                        cmd.ExecuteNonQuery();
                        WebMsgBox.Show("گیرنده مورد نظر با موفقیت افزوده شد");
                        cn.Close();
                        this.DataGrid1.DataSource = GetData2();
                        this.DataGrid1.DataBind();
                    }
                }
                else if (RadioButton3.Checked == true)
                {
                    //string sendBulkID = BulkID.ToString();
                    string prefix = fourNumbersTxbx.Text;
                    int SIMType = int.Parse(this.DropDownList6.SelectedValue);
                    int startIndex = this.TextBox2.Text == string.Empty ? 0 : int.Parse(this.TextBox2.Text);
                    int smsCount = int.Parse(this.TextBox3.Text) == 0 ? int.Parse(CountLabel.Text) : int.Parse(this.TextBox3.Text);

                    if (int.Parse(this.TextBox3.Text) == 0)
                    {
                        startIndex = 0;
                    }

                    string addRecipientResult = bs.addRecipientsByPrefix(domain, BulkID, prefix, SIMType, startIndex, smsCount);

                    if (IsNumber(addRecipientResult))
                    {
                        WebMsgBox.Show("متاسفانه خطایی رخ داده است" + " : " + ShortMessageService.getDescriptionForBulkCode(int.Parse(addRecipientResult)));
                    }
                    else
                    {
                        //int ID = GenerateIDColumn.GetNewID("RecipientsTbl");

                        SqlConnection cn = new SqlConnection(MyConnectionString);
                        cn.Open();
                        string query = "INSERT INTO RecipientsTbl (ID, SendBy, BulkID, Code, StartIndex, Count, SIMType, Comments, SysDateTime) values (@ID, @SendBy, @BulkID, @Code, @StartIndex, @Count, @SIMType, @Comments, @SysDateTime)";
                        SqlCommand cmd = new SqlCommand(query, cn);

                        cmd.Parameters.Add("@ID", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SendBy", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@BulkID", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Code", SqlDbType.NVarChar); ;
                        cmd.Parameters.Add("@StartIndex", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Count", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SIMType", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@SysDateTime", SqlDbType.NVarChar);

                        cmd.Parameters["@ID"].Value = addRecipientResult;
                        cmd.Parameters["@SendBy"].Value = "پیش شماره";
                        cmd.Parameters["@BulkID"].Value = BulkID;
                        cmd.Parameters["@Code"].Value = prefix;
                        cmd.Parameters["@StartIndex"].Value = startIndex;
                        cmd.Parameters["@Count"].Value = smsCount;
                        cmd.Parameters["@SIMType"].Value = DropDownList6.SelectedItem.Text;
                        cmd.Parameters["@Comments"].Value = "";
                        cmd.Parameters["@SysDateTime"].Value = GetDateTime.GenerateDateTime();

                        cmd.ExecuteNonQuery();
                        WebMsgBox.Show("گیرنده مورد نظر با موفقیت افزوده شد");
                        cn.Close();
                        this.DataGrid1.DataSource = GetData2();
                        this.DataGrid1.DataBind();
                    }
                }
                else
                {
                    WebMsgBox.Show("لطفا مشخص کنید که بر چه اساس میخواهید پیامک انبوه ارسال کنید !");
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است !" + " : " + ex.Message + " - " + ex.StackTrace);
            Response.Write("متاسفانه خطایی رخ داده است !" + " : " + ex.Message + " - " + ex.StackTrace);
        }
    }
    protected void createBulk_Click(object sender, EventArgs e)
    {//Create Bulk Button
        try
        {
            #region Authentications
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }

            sq.Credentials = new System.Net.NetworkCredential(username, password);
            sq.PreAuthenticate = true;

            bs.Credentials = new System.Net.NetworkCredential(username, password);
            bs.PreAuthenticate = true;
            #endregion

            int ID = GenerateIDColumn.GetNewID("SendBulkTbl");
            string message = this.MessageTextBox.Text;
            int smsType = int.Parse(this.DropDownList4.SelectedValue);
            string sendDate = this.DatePicker1.DatePersian;
            int sendHour = this.TimeSelector1.Hour;
            int sendMinute = this.TimeSelector1.Minute;
            string bulkResult = bs.createBulk(domain, message, senderNumber, smsType, sendDate, sendHour, sendMinute);
            //WebMsgBox.Show(bulkResult);
            //int found = bulkResult.IndexOf("-", 2);
            //int bulkResultConverted = GetNumber(bulkResult.Substring(6));
            BulkID = bulkResult;
            string sysDateTime = GetDateTime.GenerateDateTime();

            if (IsNumber(bulkResult) && int.Parse(bulkResult) < 0)
            {
                WebMsgBox.Show("متاسفانه خطائی رخ داده است :" + ShortMessageService.getDescriptionForBulkCode(int.Parse(bulkResult)));
            }
            else
            {
                SqlConnection cn = new SqlConnection(MyConnectionString);
                cn.Open();
                string query = "INSERT INTO SendBulkTbl (ID, BulkID, Message, SenderNumber, SMSType, BulkDate, BulkHour, BulkMinute, SysDateTime) values (@ID, @BulkID, @Message, @SenderNumber, @SMSType, @BulkDate, @BulkHour, @BulkMinute, @SysDateTime)";
                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters.Add("@BulkID", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar);
                cmd.Parameters.Add("@SenderNumber", SqlDbType.BigInt); ;
                cmd.Parameters.Add("@SMSType", SqlDbType.NVarChar);
                cmd.Parameters.Add("@BulkDate", SqlDbType.NVarChar);
                cmd.Parameters.Add("@BulkHour", SqlDbType.NVarChar);
                cmd.Parameters.Add("@BulkMinute", SqlDbType.NVarChar);
                cmd.Parameters.Add("@SysDateTime", SqlDbType.NVarChar);

                cmd.Parameters["@ID"].Value = ID;
                cmd.Parameters["@BulkID"].Value = BulkID;
                cmd.Parameters["@Message"].Value = message;
                cmd.Parameters["@SenderNumber"].Value = Int64.Parse(senderNumber);
                cmd.Parameters["@SMSType"].Value = DropDownList4.SelectedItem.Text;
                cmd.Parameters["@BulkDate"].Value = sendDate;
                cmd.Parameters["@BulkHour"].Value = sendHour;
                cmd.Parameters["@BulkMinute"].Value = sendMinute;
                cmd.Parameters["@SysDateTime"].Value = sysDateTime; ;

                cmd.ExecuteNonQuery();
                WebMsgBox.Show("پیامک انبوه با موفقیت ذخیره شد");
                cn.Close();
            }

            //Response.Write(sendDate.ToString() + "" + sendHour.ToString() + "" + sendMinute.ToString());
            //WebMsgBox.Show(sendDate.ToString() + "  " + sendHour.ToString() + "  " + sendMinute.ToString());
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("خطائی رخ داده است :" + " " + ex.Message + " - " + ex.StackTrace);
            Response.Write("خطائی رخ داده است :" + " " + ex.Message + " - " + ex.StackTrace);
        }
    }
    protected void RecipientsOpBtn1_Click(object sender, EventArgs e)
    {//Recipients Table Refresh Button
        this.DataGrid1.DataSource = GetData2();
        this.DataGrid1.DataBind();

        this.DataGrid2.DataSource = GetData();
        this.DataGrid2.DataBind();
    }
    protected void RecipientsOpBtn2_Click(object sender, EventArgs e)
    {//Recipient Delete Button
        try
        {
            #region Authentications
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }

            sq.Credentials = new System.Net.NetworkCredential(username, password);
            sq.PreAuthenticate = true;

            bs.Credentials = new System.Net.NetworkCredential(username, password);
            bs.PreAuthenticate = true;
            #endregion
            if (BulkID == String.Empty)
            {
                WebMsgBox.Show("لطفا از فهرست پیامک های انبوه پیامک مورد نظر خود را انتخاب نمائید .");
            }
            else
            {
                if (RecipientID == 0)
                {
                    WebMsgBox.Show("لطفا از فهرست گیرندگان یکی را برای حذف انتخاب نمائید .");
                }
                else
                {
                    int deleteResult = bs.removeRecipients(domain, BulkID.ToString(), RecipientID.ToString());
                    if (deleteResult == 0)
                    {
                        SqlConnection cn = new SqlConnection(MyConnectionString);
                        cn.Open();
                        string query = "DELETE FROM RecipientsTbl WHERE ID=" + RecipientID + "";
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        WebMsgBox.Show("گیرنده مورد نظر با موفقیت حذف شد");
                    }
                    else
                    {
                        WebMsgBox.Show("متاسفانه در حذف گیرنده خطایی رخ داده است" + " : " + ShortMessageService.getDescriptionForBulkCode(deleteResult));
                        //WebMsgBox.Show(RecipientID.ToString());
                    }
                    this.DataGrid1.DataSource = GetData2();
                    this.DataGrid1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است : " + ex.Message);
        }
    }
    #endregion

    #region Utility Methods
    private DataSet GetData()
    {
        SqlConnection cn = new SqlConnection(MyConnectionString);
        cn.Open();
        string sql = "SELECT * FROM SendBulkTbl";
        SqlDataAdapter DA = new SqlDataAdapter(sql, cn);
        DataSet DS = new DataSet();
        DA.Fill(DS);
        //DS.Tables[0].TableName = "Inbox";
        return DS;
    }
    private DataSet GetData2()
    {
        SqlConnection cn = new SqlConnection(MyConnectionString);
        cn.Open();
        string sql = "SELECT * FROM RecipientsTbl WHERE BulkID='" + BulkID + "'";
        SqlDataAdapter DA = new SqlDataAdapter(sql, cn);
        DataSet DS = new DataSet();
        DA.Fill(DS);
        //DS.Tables[0].TableName = "Inbox";
        return DS;
    }
    public static object GetDataValue(object value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value;
    }
    private int GetNumber(string str)
    {
        Int64 length = str.Length;
        string output = String.Empty;
        Int64 test = 0;
        bool err = false;

        for (int i = 0; i <= length; i++)
        {
            try
            {
                test = Convert.ToInt64(str.Substring(i, 1));
            }
            catch
            {
                err = true;
            }

            if (!err)
                output += str.Substring(i, 1);
            else
                break;
        }
        return Convert.ToInt32(output);
    }
    public static int Guid2Int(Guid value)
    {
        byte[] b = value.ToByteArray();
        int bint = BitConverter.ToInt32(b, 0);
        return bint;
    }
    protected bool IsNumber(string s)
    {
        foreach (char c in s)
        {
            if (!Char.IsDigit(c))
                return false;
        }
        return (s.Length < 0);
    }
    #endregion
}
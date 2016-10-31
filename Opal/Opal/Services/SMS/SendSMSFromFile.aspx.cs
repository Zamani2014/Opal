using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Opal.Controls;
using ArvidfavaSMS;
using System.Net;
using Excel;
using System.Data;
using Zamani;
using System.Data.SqlClient;
using System.Configuration;
using Opal.Providers;
using System.Web.Security;

public partial class Administration_ShortMessageService_SendFromFile : PageBaseClass
{
    // Send SMS From Excel, CSV, TXT Files

    #region Web Services
    MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
    string MyConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();
    #endregion
    #region SMS Account Variables
    private String username;
    private String password;
    private String domain;
    private String senderNumber;
    private int Count;
    private bool useProxy;
    private String proxyAddress;
    private String proxyUsername;
    private String proxyPassword;
    #endregion
    #region Account Fee Variables
    private static int SMSFee;
    private long Credit;
    private long SMSCount;
    private static int messagePart;
    #endregion
    #region Numbers Variables
    private static string[] mobiles;
    private static string[] messages;
    private static int[] length;
    #endregion

    #region Event Handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        username = System.Configuration.ConfigurationManager.AppSettings["Username"];
        password = System.Configuration.ConfigurationManager.AppSettings["Password"];
        domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
        Count = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Count"]);
        //senderNumber = System.Configuration.ConfigurationManager.AppSettings["SenderNumber"];
        useProxy = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseProxy"]);
        proxyAddress = System.Configuration.ConfigurationManager.AppSettings["ProxyAddress"];
        proxyUsername = System.Configuration.ConfigurationManager.AppSettings["ProxyUsername"];
        proxyPassword = System.Configuration.ConfigurationManager.AppSettings["ProxyPassword"];
        ResultLabel.Text = "";

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

        //DataRow[] DR;
        //DR = DS.Tables[0].Select("WHERE Username =" + userName);
        //senderNumber = DR[0]["LineNo"].ToString();
        foreach (var lines in query)
        {
            senderNumber = lines.LineNo.ToString();
            Credit = lines.Credit;
        }
        //senderNumber = System.Configuration.ConfigurationManager.AppSettings["SenderNumber"];

    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (this.CheckBox3.Checked)
        {
            GatewayTextBox.Visible = true;
        }
        else
        {
            GatewayTextBox.Visible = false;
        }
    }
    protected void radioBtm1(object sender, EventArgs e)
    {
        this.UpdatePanel1.Visible = true;
        this.UpdatePanel3.Visible = false;
    }
    protected void radioBtn2(object sender, EventArgs e)
    {
        this.UpdatePanel1.Visible = false;
        this.UpdatePanel3.Visible = true;
    }
    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (this.CheckBox5.Checked)
        {
            GatewayTextBox2.Visible = true;
        }
        else
        {
            GatewayTextBox2.Visible = false;
        }
    }
    protected void DropDownList2_Changed(object sender, EventArgs e)
    {
        if (this.DropDownList2.SelectedValue == "3")
        {
            this.TextBox1.Visible = true;
        }
        else
        {
            this.TextBox1.Visible = false;
        }
    }
    protected void DropDownList4_Changed(object sender, EventArgs e)
    {
        if (this.DropDownList4.SelectedValue == "3")
        {
            this.TextBox2.Visible = true;
        }
        else
        {
            this.TextBox2.Visible = false;
        }
    }
    protected void MessageTextBox_TextChanged(object sender, EventArgs e)
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
    #endregion

    // Send From File 1 to N
    protected void Button1_Click(object sender, EventArgs e)
    {
        char[] delimiters = new char[] { ',', ';', '.' };
        char[] delimiters2 = new char[] { ';' };

        if (this.FileUpload1.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            if (fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".csv" || fileExt == ".txt")
            {
                try
                {
                    //string fileName = @"C:\\Upload\\" + this.FileUpload1.FileName;
                    string filePath = "C:\\Upload\\" + this.FileUpload1.FileName;

                    this.FileUpload1.SaveAs(filePath);
                    Label14.Text = "نام فایل : " + FileUpload1.PostedFile.FileName + "<br>" + "اندازه فایل : " + FileUpload1.PostedFile.ContentLength + " kb<br>" + "نوع محتوای فایل : " + FileUpload1.PostedFile.ContentType;

                    #region TXT Files
                    if (fileExt == ".txt")
                    {
                        using (StreamReader sr = new StreamReader(filePath))
                        {
                            String line = sr.ReadToEnd();
                            string[] splitedNumbers = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                            mobiles = new string[splitedNumbers.Length];
                            //for (int i = 0; i < splitedNumbers.Length; i++)
                            //{
                                mobiles = splitedNumbers;
                            //}
                        }
                        this.Label14.Text += "<br>" + "تعداد شماره ها :" + mobiles.Length.ToString();
                    }
                    #endregion
                    #region CSV Files
                    else if (fileExt == ".csv")
                    {
                        using (StreamReader sr = new StreamReader(filePath))
                        {
                            String line = sr.ReadToEnd();
                            string[] splitedNumbers = line.Split(delimiters2, StringSplitOptions.RemoveEmptyEntries);
                            mobiles = new string[splitedNumbers.Length];
                            mobiles = splitedNumbers;
                        }
                        this.Label14.Text += "<br>" + "تعداد شماره ها :" + mobiles.Length.ToString();
                    }
                    #endregion
                    #region XLS Files
                    else if (fileExt == ".xls")
                    {
                        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                        //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

                        //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                        //4. DataSet - Create column names from first row
                        excelReader.IsFirstRowAsColumnNames = this.CheckBox6.Checked;
                        DataSet result = excelReader.AsDataSet();
                        DataTable dt = result.Tables[0];

                        DataRow[] foundRows;
                        foundRows = dt.Select();

                        mobiles = new string[foundRows.Length];

                        for (int i = 0; i < foundRows.Length; i++)
                        {
                            mobiles[i] = foundRows[i][0].ToString();
                        }

                        excelReader.Close();

                        this.Label14.Text += "<br>" + "تعداد شماره ها :" + mobiles.Length.ToString();
                    }
                    #endregion
                    #region XLSX Files
                    else if (fileExt == ".xlsx")
                    {
                        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                        //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        excelReader.IsFirstRowAsColumnNames = this.CheckBox6.Checked;
                        DataSet result = excelReader.AsDataSet();
                        DataTable dt = result.Tables[0];

                        DataRow[] foundRows;
                        foundRows = dt.Select();

                        mobiles = new string[foundRows.Length];

                        for (int i = 0; i < foundRows.Length; i++)
                        {
                            mobiles[i] = foundRows[i][0].ToString();
                        }

                        excelReader.Close();

                        this.Label14.Text += "<br>" + "تعداد شماره ها :" + mobiles.Length.ToString();
                    }
                    #endregion

                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    Label14.Text = "ERROR: " + ex.Message.ToString() + ex.StackTrace;
                }
            }
            else
            {
                Label14.Text = "نوع فایل شما فقط میتواند یکی از انواع xls, xlsx, csv ویا txt باشد !";
            }
        }
        else
        {
            Label14.Text = "شما هیچ فایلی را برای بالاگذاری انتخاب نکرده اید !";
        }
    }
    protected void SendBtn_Click(object sender, EventArgs e)
    {
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
                #region Reading From SQL Database
                // Code for Load Mobile Numbers From Database
                //
                //SqlConnection cn = new SqlConnection(@"Data Source=(local);Initial Catalog=SMSMagfaDB;Integrated Security=True");
                //cn.Open();

                //string strsql = "SELECT * FROM Sheet1";
                //SqlCommand cmd = new SqlCommand(strsql, cn);

                //SqlDataReader dr = cmd.ExecuteReader();

                //long[] mobiles = new long[12500];

                //for (long i = 1; i <= 12500; i++)
                //{
                //        while (dr.Read())
                //        {
                //            mobiles[i] = GetNumber(dr["MNumber"].ToString());
                //            string MobileNumber = mobiles[i].ToString();

                //            long result = enqueue(useProxy, proxyAddress, proxyUsername, proxyPassword, username, password, domain, N, senderNumber, MobileNumber, MessageTextBox.Text)[0];
                //            if (result < ShortMessageService.MAX_VALUE)
                //                ResultLabel.Text = ShortMessageService.generateDateString() + "Error code: " + result + ", " + ShortMessageService.getDescriptionForCode((int)result);
                //            else
                //                ResultLabel.Text = ShortMessageService.generateDateString() + result;
                //        }
                //}
                #endregion
                #region Variables
                int arrayLength = mobiles.Length;

                SMSCount = Credit / SMSFee;
                int SendSMSCount = messagePart * arrayLength;

                int priority = GetNumber32(this.DropDownList3.SelectedValue.ToString());
                int messageClass = GetNumber32(this.DropDownList2.SelectedValue.ToString());

                //long[] results;

                long[] messageIds;
                int[] messageStatus;
                string[] messageStatusString;

                string[] messages;
                string[] origs;

                int[] encodings;
                string[] UDH;
                int[] mclass;
                int[] priorities;
                long[] checkingIds;

                //results = new long[count];

                messageIds = new long[arrayLength];
                messageStatus = new int[arrayLength];
                messageStatusString = new string[arrayLength];

                messages = new string[Count];
                origs = new string[Count];

                encodings = new int[Count];
                UDH = new string[Count];
                mclass = new int[Count];
                priorities = new int[Count];
                checkingIds = new long[Count];

                /*
                encodings = null;
                UDH = null;
                mclass = null;
                priorities = null;
                checkingIds = null;
                */
                #endregion

                for (int i = 0; i < Count; i++)
                {
                    messages[i] = MessageTextBox.Text;
                    origs[i] = senderNumber;

                    encodings[i] = -1;
                    UDH[i] = "";
                    mclass[i] = messageClass;
                    priorities[i] = priority;
                    checkingIds[i] = 200 + i;
                }

                if (SMSCount >= SendSMSCount)
                {
                    if (mobiles.Length <= 90)
                    {
                        #region SendWithGateway
                        if (this.CheckBox3.Checked)
                        {
                            if (this.GatewayTextBox.Text != String.Empty)
                            {
                                string Gateway = this.GatewayTextBox.Text;
                                messageIds = sq.enqueueWithGateway(domain, messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds, Gateway);
                                cn.Open();

                                for (int i = 0; i < mobiles.Length; i++)
                                {
                                    if (messageIds[i] < ShortMessageService.MAX_VALUE)
                                    {
                                        this.ListBox1.Visible = true;
                                        this.ListBox1.Items.Add("برای شماره :" + mobiles[i].ToString() + " : " + ShortMessageService.generateDateString() + "کد خطا :" + messageIds[i].ToString() + ", " + ShortMessageService.getDescriptionForCode((int)messageIds[i]));
                                    }
                                    else
                                    {
                                        //messageStatus[i] = sq.getRealMessageStatuses(messageIds)[i];
                                        messageStatusString[i] = ShortMessageService.getDescriptionForStatusCode(sq.getRealMessageStatuses(messageIds)[i]);
                                        string MyQuery = "INSERT INTO OutBoxTbl (ID, messageBody, recipientNumber, msgsenderNumber, DateTime, messageStatus, Comments) values (@ID, @messageBody, @recipientNumber, @msgsenderNumber, @DateTime, @messageStatus, @Comments)";
                                        SqlCommand cmd = new SqlCommand(MyQuery, cn);

                                        cmd.Parameters.Add("@ID", SqlDbType.BigInt);
                                        cmd.Parameters.Add("@messageBody", SqlDbType.NVarChar);
                                        cmd.Parameters.Add("@recipientNumber", SqlDbType.NVarChar);
                                        cmd.Parameters.Add("@msgsenderNumber", SqlDbType.NVarChar); ;
                                        cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);
                                        cmd.Parameters.Add("@messageStatus", SqlDbType.NVarChar);
                                        cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);

                                        cmd.Parameters["@ID"].Value = messageIds[i];
                                        cmd.Parameters["@messageBody"].Value = MessageTextBox.Text;
                                        cmd.Parameters["@recipientNumber"].Value = mobiles[i];
                                        cmd.Parameters["@msgsenderNumber"].Value = senderNumber;
                                        cmd.Parameters["@DateTime"].Value = GetDateTime.GenerateDateTime();
                                        cmd.Parameters["@messageStatus"].Value = messageStatusString[i];
                                        cmd.Parameters["@Comments"].Value = "ارسال از فایل بصورت یک به چند";

                                        cmd.ExecuteNonQuery();

                                        long SendSMSFee = SendSMSCount * SMSFee;
                                        long NewCredit = Credit - SendSMSFee;

                                        string userName = User.Identity.Name;
                                        OurMembershipUser user = (OurMembershipUser)Membership.GetUser(userName);
                                        user.Credit = NewCredit;
                                        user.SendFee += SendSMSFee;
                                        Membership.UpdateUser(user);
                                    }
                                }
                                WebMsgBox.Show("پیامک ارسال شد، جهت اطلاع از وضعیت پیامک یا پیامک های ارسال شده به صندوق خروجی بروید !");
                                cn.Close();
                            }
                            else if (this.GatewayTextBox.Text == String.Empty)
                            {
                                WebMsgBox.Show("لطفا نشانی آی پی مورد نظر خود را بعنوان دروازه وارد کنید !");
                            }
                        }
                        #endregion
                        #region SendNormal
                        else if (this.CheckBox3.Checked == false)
                        {
                            messageIds = sq.enqueue(domain, messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds);
                            cn.Open();

                            for (int i = 0; i < arrayLength; i++)
                            {
                                if (messageIds[i] < ShortMessageService.MAX_VALUE)
                                {
                                    this.ListBox1.Visible = true;
                                    this.ListBox1.Items.Add("برای شماره :" + mobiles[i].ToString() + " : " + ShortMessageService.generateDateString() + "کد خطا :" + messageIds[i].ToString() + ", " + ShortMessageService.getDescriptionForCode((int)messageIds[i]));
                                }
                                else
                                {
                                    //messageStatus[i] = sq.getRealMessageStatuses(messageIds)[i];
                                    messageStatusString[i] = ShortMessageService.getDescriptionForStatusCode(sq.getRealMessageStatuses(messageIds)[i]);
                                    string MyQuery = "INSERT INTO OutBoxTbl (ID, messageBody, recipientNumber, msgsenderNumber, DateTime, messageStatus, Comments) values (@ID, @messageBody, @recipientNumber, @msgsenderNumber, @DateTime, @messageStatus, @Comments)";
                                    SqlCommand cmd = new SqlCommand(MyQuery, cn);

                                    cmd.Parameters.Add("@ID", SqlDbType.BigInt);
                                    cmd.Parameters.Add("@messageBody", SqlDbType.NVarChar);
                                    cmd.Parameters.Add("@recipientNumber", SqlDbType.NVarChar);
                                    cmd.Parameters.Add("@msgsenderNumber", SqlDbType.NVarChar); ;
                                    cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);
                                    cmd.Parameters.Add("@messageStatus", SqlDbType.NVarChar);
                                    cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);

                                    cmd.Parameters["@ID"].Value = messageIds[i];
                                    cmd.Parameters["@messageBody"].Value = MessageTextBox.Text;
                                    cmd.Parameters["@recipientNumber"].Value = mobiles[i];
                                    cmd.Parameters["@msgsenderNumber"].Value = senderNumber;
                                    cmd.Parameters["@DateTime"].Value = GetDateTime.GenerateDateTime();
                                    cmd.Parameters["@messageStatus"].Value = messageStatusString[i];
                                    cmd.Parameters["@Comments"].Value = "ارسال از فایل بصورت یک به چند";

                                    cmd.ExecuteNonQuery();

                                    long SendSMSFee = SendSMSCount * SMSFee;
                                    long NewCredit = Credit - SendSMSFee;

                                    string userName = User.Identity.Name;
                                    OurMembershipUser user = (OurMembershipUser)Membership.GetUser(userName);
                                    user.Credit = NewCredit;
                                    user.SendFee += SendSMSFee;
                                    Membership.UpdateUser(user);
                                }
                            }
                            WebMsgBox.Show("پیامک ارسال شد، جهت اطلاع از وضعیت پیامک یا پیامک های ارسال شده به صندوق خروجی بروید !");
                            cn.Close();
                        }
                        #endregion
                    }
                    else
                    {
                        WebMsgBox.Show("شما در هر ارسال تنها 90 پیامک میتوانید ارسال کنید !" + " " + "لطفا تعداد شماره های همراه را کاهش دهید .");
                        WebMsgBox.Show("ممکن است متن پیام بیشتر از تعداد کاراکترهای مجاز باشد و پیامک شما چند قسمی شده باشد !");
                    }
                }
                else
                {
                    WebMsgBox.Show("شما اعتبار کافی برای ارسال این تعداد پیامک را ندارید و یا متن پیامک طولانی است !");
                }
            }
            else
            {
                WebMsgBox.Show("شما هیچ اعتباری برای ارسال پیامک ندارید !");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("خطایی رخ داده است :" + ex.Message + " پشته خطا : " + ex.StackTrace);
        }
    }

    // Send From File Peer 2 Peer
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (this.FileUpload2.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(FileUpload2.FileName);
            if (fileExt == ".xls" || fileExt == ".xlsx")
            {
                try
                {
                    //string fileName = @"C:\\Upload\\" + this.FileUpload1.FileName;
                    string filePath = "C:\\Upload\\" + this.FileUpload2.FileName;

                    this.FileUpload2.SaveAs(filePath);
                    Label18.Text = "نام فایل : " + FileUpload2.PostedFile.FileName + "<br>" + "اندازه فایل : " + FileUpload2.PostedFile.ContentLength + " kb<br>" + "نوع محتوای فایل : " + FileUpload2.PostedFile.ContentType;

                    #region XLS Files
                    if (fileExt == ".xls")
                    {
                        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                        //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

                        //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                        //4. DataSet - Create column names from first row
                        excelReader.IsFirstRowAsColumnNames = this.CheckBox7.Checked;
                        DataSet result = excelReader.AsDataSet();
                        DataTable dt = result.Tables[0];

                        DataRow[] foundRows;
                        foundRows = dt.Select();

                        mobiles = new string[foundRows.Length];
                        messages = new string[foundRows.Length];
                        length = new int[foundRows.Length];

                        for (int i = 0; i < foundRows.Length; i++)
                        {
                            mobiles[i] = foundRows[i][0].ToString();
                            messages[i] = foundRows[i][1].ToString();
                            length[i] = foundRows[i][1].ToString().Length;
                        }

                        excelReader.Close();

                        this.Label18.Text += "<br>" + "تعداد سطرها :" + mobiles.Length.ToString();
                    }
                    #endregion
                    #region XLSX Files
                    else if (fileExt == ".xlsx")
                    {
                        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                        //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        excelReader.IsFirstRowAsColumnNames = this.CheckBox7.Checked;
                        DataSet result = excelReader.AsDataSet();
                        DataTable dt = result.Tables[0];

                        DataRow[] foundRows;
                        foundRows = dt.Select();

                        mobiles = new string[foundRows.Length];
                        messages = new string[foundRows.Length];
                        length = new int[foundRows.Length];

                        for (int i = 0; i < foundRows.Length; i++)
                        {
                            mobiles[i] = foundRows[i][0].ToString();
                            messages[i] = foundRows[i][1].ToString();
                            length = new int[foundRows.Length];
                        }

                        excelReader.Close();

                        this.Label18.Text += "<br>" + "تعداد سطرها :" + mobiles.Length.ToString();
                    }
                    #endregion

                    //int chars = BulkMsg.Length;
                    //DialogueMaster.Babel.BabelModel model = DialogueMaster.Babel.BabelModel._AllModel;
                    //string input = BulkMsg;

                    //for (int i = 0; i < messages.Length; i++)
                    //{
                    //    DialogueMaster.Classification.ICategoryList result = model.ClassifyText(messages[i], 1);
                    //    DialogueMaster.Classification.ICategory category = result[0];
                    //    if (category.Name == "fa" || category.Name == "ar")
                    //    {
                    //        SMSFee[i] = GetFee.GetFarsiSMSFee(Credit);

                    //        if (length[i] <= 70)
                    //        {
                    //            messagePart[i] = 1;
                    //        }
                    //        else
                    //        {
                    //            int d = length[i] / 67;
                    //            messagePart[i] = d;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        SMSFee[i] = GetFee.GetEnglishSMSFee(Credit);

                    //        if (length[i] <= 160)
                    //        {
                    //            messagePart[i] = 1;
                    //        }
                    //        else
                    //        {
                    //            int d = length[i] / 153;
                    //            messagePart[i] = d;
                    //        }
                    //    }
                    //}

                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    Label18.Text = "ERROR: " + ex.Message.ToString() + ex.StackTrace;
                }
            }
            else
            {
                Label18.Text = "نوع فایل شما فقط میتواند یکی از انواع xls ویا xlsx باشد !";
            }
        }
        else
        {
            Label18.Text = "شما هیچ فایلی را برای بالاگذاری انتخاب نکرده اید !";
        }

    }
    protected void SendBtn0_Click(object sender, EventArgs e)
    {
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
                #region Variables
                SMSFee = 140;
                messagePart = 1;
                int arrayLength = mobiles.Length;

                SMSCount = Credit / SMSFee;
                int SendSMSCount = messagePart * arrayLength;

                int priority = GetNumber32(this.DropDownList3.SelectedValue.ToString());
                int messageClass = GetNumber32(this.DropDownList2.SelectedValue.ToString());

                //long[] results;

                long[] messageIds;
                int[] messageStatus;
                string[] messageStatusString;

                //string[] messages;
                string[] origs;

                int[] encodings;
                string[] UDH;
                int[] mclass;
                int[] priorities;
                long[] checkingIds;

                //results = new long[count];

                messageIds = new long[arrayLength];
                messageStatus = new int[arrayLength];
                messageStatusString = new string[arrayLength];

                //messages = new string[arrayLength];
                origs = new string[Count];

                encodings = new int[Count];
                UDH = new string[Count];
                mclass = new int[Count];
                priorities = new int[Count];
                checkingIds = new long[Count];

                /*
                encodings = null;
                UDH = null;
                mclass = null;
                priorities = null;
                checkingIds = null;
                */

                //for (int i = 0; i < arrayLength; i++)
                //{
                //    messages[i] = ;
                //}
                #endregion 

                for (int i = 0; i < Count; i++)
                {
                    origs[i] = senderNumber;

                    encodings[i] = -1;
                    UDH[i] = "";
                    mclass[i] = messageClass;
                    priorities[i] = priority;
                    checkingIds[i] = 200 + i;
                }
                if (SMSCount >= SendSMSCount)
                {
                    if (mobiles.Length <= 90)
                    {
                        #region SendWithGateway
                        if (this.CheckBox3.Checked)
                        {
                            if (this.GatewayTextBox.Text != String.Empty)
                            {
                                string Gateway = this.GatewayTextBox.Text;
                                messageIds = sq.enqueueWithGateway(domain, messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds, Gateway);
                                cn.Open();

                                for (int i = 0; i < mobiles.Length; i++)
                                {
                                    if (messageIds[i] < ShortMessageService.MAX_VALUE)
                                    {
                                        this.ListBox2.Visible = true;
                                        this.ListBox2.Items.Add("برای شماره :" + mobiles[i].ToString() + " : " + ShortMessageService.generateDateString() + "کد خطا :" + messageIds[i].ToString() + ", " + ShortMessageService.getDescriptionForCode((int)messageIds[i]));
                                    }
                                    else
                                    {
                                        //messageStatus[i] = sq.getRealMessageStatuses(messageIds)[i];
                                        messageStatusString[i] = ShortMessageService.getDescriptionForStatusCode(sq.getRealMessageStatuses(messageIds)[i]);
                                        string MyQuery = "INSERT INTO OutBoxTbl (ID, messageBody, recipientNumber, msgsenderNumber, DateTime, messageStatus, Comments) values (@ID, @messageBody, @recipientNumber, @msgsenderNumber, @DateTime, @messageStatus, @Comments)";
                                        SqlCommand cmd = new SqlCommand(MyQuery, cn);

                                        cmd.Parameters.Add("@ID", SqlDbType.BigInt);
                                        cmd.Parameters.Add("@messageBody", SqlDbType.NVarChar);
                                        cmd.Parameters.Add("@recipientNumber", SqlDbType.NVarChar);
                                        cmd.Parameters.Add("@msgsenderNumber", SqlDbType.NVarChar); ;
                                        cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);
                                        cmd.Parameters.Add("@messageStatus", SqlDbType.NVarChar);
                                        cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);

                                        cmd.Parameters["@ID"].Value = messageIds[i];
                                        cmd.Parameters["@messageBody"].Value = messages[i];
                                        cmd.Parameters["@recipientNumber"].Value = mobiles[i];
                                        cmd.Parameters["@msgsenderNumber"].Value = senderNumber;
                                        cmd.Parameters["@DateTime"].Value = GetDateTime.GenerateDateTime();
                                        cmd.Parameters["@messageStatus"].Value = messageStatusString[i];
                                        cmd.Parameters["@Comments"].Value = "ارسال از فایل بصورت نظیر به نظیر";

                                        cmd.ExecuteNonQuery();

                                        long SendSMSFee = SendSMSCount * SMSFee;
                                        long NewCredit = Credit - SendSMSFee;

                                        string userName = User.Identity.Name;
                                        OurMembershipUser user = (OurMembershipUser)Membership.GetUser(userName);
                                        user.Credit = NewCredit;
                                        user.SendFee += SendSMSFee;
                                        Membership.UpdateUser(user);
                                    }
                                }
                                WebMsgBox.Show("پیامک ارسال شد، جهت اطلاع از وضعیت پیامک یا پیامک های ارسال شده به صندوق خروجی بروید !");
                                cn.Close();
                            }
                            else if (this.GatewayTextBox.Text == String.Empty)
                            {
                                WebMsgBox.Show("لطفا نشانی آی پی مورد نظر خود را بعنوان دروازه وارد کنید !");
                            }
                        }
                        #endregion
                        #region SendNormal
                        else if (this.CheckBox3.Checked == false)
                        {
                            messageIds = sq.enqueue(domain, messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds);
                            cn.Open();

                            for (int i = 0; i < arrayLength; i++)
                            {
                                if (messageIds[i] < ShortMessageService.MAX_VALUE)
                                {
                                    this.ListBox2.Visible = true;
                                    this.ListBox2.Items.Add("برای شماره :" + mobiles[i].ToString() + " : " + ShortMessageService.generateDateString() + "کد خطا :" + messageIds[i].ToString() + ", " + ShortMessageService.getDescriptionForCode((int)messageIds[i]));
                                }
                                else
                                {
                                    //messageStatus[i] = sq.getRealMessageStatuses(messageIds)[i];
                                    messageStatusString[i] = ShortMessageService.getDescriptionForStatusCode(sq.getRealMessageStatuses(messageIds)[i]);
                                    string MyQuery = "INSERT INTO OutBoxTbl (ID, messageBody, recipientNumber, msgsenderNumber, DateTime, messageStatus, Comments) values (@ID, @messageBody, @recipientNumber, @msgsenderNumber, @DateTime, @messageStatus, @Comments)";
                                    SqlCommand cmd = new SqlCommand(MyQuery, cn);

                                    cmd.Parameters.Add("@ID", SqlDbType.BigInt);
                                    cmd.Parameters.Add("@messageBody", SqlDbType.NVarChar);
                                    cmd.Parameters.Add("@recipientNumber", SqlDbType.NVarChar);
                                    cmd.Parameters.Add("@msgsenderNumber", SqlDbType.NVarChar); ;
                                    cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar);
                                    cmd.Parameters.Add("@messageStatus", SqlDbType.NVarChar);
                                    cmd.Parameters.Add("@Comments", SqlDbType.NVarChar);

                                    cmd.Parameters["@ID"].Value = messageIds[i];
                                    cmd.Parameters["@messageBody"].Value = messages[i];
                                    cmd.Parameters["@recipientNumber"].Value = mobiles[i];
                                    cmd.Parameters["@msgsenderNumber"].Value = senderNumber;
                                    cmd.Parameters["@DateTime"].Value = GetDateTime.GenerateDateTime();
                                    cmd.Parameters["@messageStatus"].Value = messageStatusString[i];
                                    cmd.Parameters["@Comments"].Value = "ارسال از فایل بصورت نظیر به نظیر";

                                    cmd.ExecuteNonQuery();

                                    long SendSMSFee = SendSMSCount * SMSFee;
                                    long NewCredit = Credit - SendSMSFee;

                                    string userName = User.Identity.Name;
                                    OurMembershipUser user = (OurMembershipUser)Membership.GetUser(userName);
                                    user.Credit = NewCredit;
                                    user.SendFee += SendSMSFee;
                                    Membership.UpdateUser(user);
                                }
                            }
                            WebMsgBox.Show("پیامک ارسال شد، جهت اطلاع از وضعیت پیامک یا پیامک های ارسال شده به صندوق خروجی بروید !");
                            cn.Close();
                        }
                        #endregion
                    }
                    else
                    {
                        WebMsgBox.Show("شما در هر ارسال تنها 90 پیامک میتوانید ارسال کنید !" + " " + "لطفا تعداد شماره های همراه را کاهش دهید .");
                        WebMsgBox.Show("ممکن است متن پیام بیشتر از تعداد کاراکترهای مجاز باشد و پیامک شما چند قسمی شده باشد !");
                    }
                }
                else
                {
                    WebMsgBox.Show("شما اعتبار کافی برای ارسال این تعداد پیامک را ندارید و یا متن پیامک طولانی است !");
                }
            }
            else
            {
                WebMsgBox.Show("شما هیچ اعتباری برای ارسال پیامک ندارید !");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("خطایی رخ داده است :" + ex.Message + " پشته خطا : " + ex.StackTrace);
        }
    }

    #region Utility Methods
    private Int64 GetNumber64(string str)
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
        return Convert.ToInt64(output);
    }
    private Int32 GetNumber32(string str)
    {
        Int32 length = str.Length;
        string output = String.Empty;
        Int32 test = 0;
        bool err = false;

        for (int i = 0; i <= length; i++)
        {
            try
            {
                test = Convert.ToInt32(str.Substring(i, 1));
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
    #endregion
}
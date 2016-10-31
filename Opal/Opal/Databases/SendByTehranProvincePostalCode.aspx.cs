using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArvidfavaSMS;
using Opal.Controls;
using Opal.Providers;
using Zamani;

public partial class Databases_SendByPostalCode : PageBaseClass
{
    string MyConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();
    MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();

    #region AccountVariables
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
    #region Account Fee
    private static int SMSFee;
    private long Credit;
    private long SMSCount;
    private static int messagePart;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.CheckBox1.Checked)
            {
                this.Image5.Visible = true;
                this.Image6.Visible = true;
            }
            else
            {
                this.Image6.Visible = false;
                this.Image5.Visible = false;
            }
        }

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

        username = System.Configuration.ConfigurationManager.AppSettings["Username"];
        password = System.Configuration.ConfigurationManager.AppSettings["Password"];
        domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
        Count = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Count"]);
        useProxy = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseProxy"]);
        proxyAddress = System.Configuration.ConfigurationManager.AppSettings["ProxyAddress"];
        proxyUsername = System.Configuration.ConfigurationManager.AppSettings["ProxyUsername"];
        proxyPassword = System.Configuration.ConfigurationManager.AppSettings["ProxyPassword"];
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
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (this.CheckBox1.Checked)
        {
            this.Image5.Visible = true;
            this.Image6.Visible = true;
        }
        else
        {
            this.Image6.Visible = false;
            this.Image5.Visible = false;
        }
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
    protected void SendBtn_Click(object sender, EventArgs e)
    {
        SqlConnection cn = new SqlConnection(MyConnectionString);
        cn.Open();
        try
        {
            if (Credit > 0)
            {
                SMSCount = Credit / SMSFee;

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

                int PostalCodeNumbers = int.Parse(this.MobileTextBox.Text);

                string myQuery = "SELECT MobileNumber FROM PCodeTbl WHERE PostalCode=" + PostalCodeNumbers + "";
                SqlCommand cmd1 = new SqlCommand(myQuery, cn);

                SqlDataReader dr = cmd1.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    count++;
                }
                string[] mobileNo = new string[count];
                dr.Close();

                SqlDataReader dr2 = cmd1.ExecuteReader();
                if (dr2.Read())
                {
                    dr2.Close();

                    SqlDataReader dr3 = cmd1.ExecuteReader();
                    int h = 0;
                    while (dr3.Read())
                    {
                        mobileNo[h] = dr3[h].ToString();
                        //WebMsgBox.Show(mobileNo[h].ToString());
                        h++;
                    } 
                    dr3.Close();
                    int arrayLength = mobileNo.Length;

                    //char[] delimiters = new char[] { ',', ';', '.' };
                    //string[] splitedNumbers = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    int priority = GetNumber32(this.DropDownList3.SelectedValue.ToString());
                    int messageClass = GetNumber32(this.DropDownList2.SelectedValue.ToString());

                    int SendSMSCount = messagePart * arrayLength;

                    long[] messageIds;
                    int[] messageStatus;
                    string[] messageStatusString;

                    string[] messages;
                    string[] mobiles;
                    string[] origs;

                    int[] encodings;
                    string[] UDH;
                    int[] mclass;
                    int[] priorities;
                    long[] checkingIds;

                    messageIds = new long[arrayLength];
                    messageStatus = new int[arrayLength];
                    messageStatusString = new string[arrayLength];

                    messages = new string[Count];
                    mobiles = new string[arrayLength];
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

                    for (int j = 0; j < Count; j++)
                    {
                        messages[j] = MessageTextBox.Text;
                        origs[j] = senderNumber;

                        encodings[j] = -1;
                        UDH[j] = "";
                        mclass[j] = messageClass;
                        priorities[j] = priority;
                        checkingIds[j] = 200 + j;
                    }

                    //if (DropDownList2.SelectedValue == "3")
                    //{
                    //    for (int i = 0; i < Count; i++)
                    //    {
                    //        UDH[i] = TextBox1.Text;
                    //    }
                    //}

                    mobiles = mobileNo;
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
                                            cmd.Parameters["@messageBody"].Value = messages[i];
                                            cmd.Parameters["@recipientNumber"].Value = mobiles[i];
                                            cmd.Parameters["@msgsenderNumber"].Value = origs[i];
                                            cmd.Parameters["@DateTime"].Value = GetDateTime.GenerateDateTime();
                                            cmd.Parameters["@messageStatus"].Value = messageStatusString[i];
                                            cmd.Parameters["@Comments"].Value = "ارسال بر اساس کد پستی استان تهران";

                                            cmd.ExecuteNonQuery();

                                        }
                                    }
                                    long SendSMSFee = SendSMSCount * SMSFee;
                                    long NewCredit = Credit - SendSMSFee;

                                    string userName = User.Identity.Name;
                                    OurMembershipUser user = (OurMembershipUser)Membership.GetUser(userName);
                                    user.Credit = NewCredit;
                                    user.SendFee += SendSMSFee;
                                    Membership.UpdateUser(user);

                                    WebMsgBox.Show("پیامک ارسال شد، جهت اطلاع از وضعیت پیامک یا پیامک های ارسال شده به صندوق خروجی بروید !");
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
                                        cmd.Parameters["@messageBody"].Value = messages[i];
                                        cmd.Parameters["@recipientNumber"].Value = mobiles[i];
                                        cmd.Parameters["@msgsenderNumber"].Value = origs[i];
                                        cmd.Parameters["@DateTime"].Value = GetDateTime.GenerateDateTime();
                                        cmd.Parameters["@messageStatus"].Value = messageStatusString[i];
                                        cmd.Parameters["@Comments"].Value = "ارسال بر اساس کد پستی استان تهران";

                                        cmd.ExecuteNonQuery();

                                    }
                                }
                                long SendSMSFee = SendSMSCount * SMSFee;
                                long NewCredit = Credit - SendSMSFee;

                                string userName = User.Identity.Name;
                                OurMembershipUser user = (OurMembershipUser)Membership.GetUser(userName);
                                user.Credit = NewCredit;
                                user.SendFee += SendSMSFee;
                                Membership.UpdateUser(user);

                                WebMsgBox.Show("پیامک ارسال شد، جهت اطلاع از وضعیت پیامک یا پیامک های ارسال شده به صندوق خروجی بروید !");
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
                    cn.Close();
                }
                else
                {
                    WebMsgBox.Show("متاسفانه خطایی رخ داده است ! و یا کد پستی مورد نظر شما در بانک موجود نیست، لطفا کد پستی پنج رقمی را وارد نمائید");
                }
            }
            else
            {
                WebMsgBox.Show("شما هیچ اعتباری برای ارسال پیامک ندارید !");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("متاسفانه خطایی رخ داده است، لطفا صفحه را مجددا بارگداری نمائید !");
            Response.Write(ex.Message + ex.StackTrace);
        }
    }
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
    public static int Guid2Int(Guid value)
    {
        byte[] b = value.ToByteArray();
        int bint = BitConverter.ToInt32(b, 0);
        return bint;
    }
    public static object GetDataValue(object value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value;
    }
}
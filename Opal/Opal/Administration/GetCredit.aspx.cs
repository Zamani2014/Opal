using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Opal.Controls;
using ArvidfavaSMS;
using System.Net;
using Zamani;

public partial class Administration_ShortMessageService_GetCredit : PageBaseClass
{
    MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();

    private String username;
    private String password;
    private String domain;
    private String senderNumber;
    private int N;
    private bool useProxy;
    private String proxyAddress;
    private String proxyUsername;
    private String proxyPassword;

    public System.Single getCredit(Boolean useProxy, String proxyAddress, String proxyUsername, String proxyPassword, String username, String password, String domain)
    {
        if (useProxy)
        {
            WebProxy proxy;
            proxy = new WebProxy(proxyAddress);
            proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
            sq.Proxy = proxy;
        }
        sq.Credentials = new System.Net.NetworkCredential(username, password);
        sq.PreAuthenticate = true;
        return sq.getCredit(domain);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        username = System.Configuration.ConfigurationManager.AppSettings["Username"];
        password = System.Configuration.ConfigurationManager.AppSettings["Password"];
        domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
        N = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Count"]);
        senderNumber = System.Configuration.ConfigurationManager.AppSettings["SenderNumber"];
        useProxy = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseProxy"]);
        proxyAddress = System.Configuration.ConfigurationManager.AppSettings["ProxyAddress"];
        proxyUsername = System.Configuration.ConfigurationManager.AppSettings["ProxyUsername"];
        proxyPassword = System.Configuration.ConfigurationManager.AppSettings["ProxyPassword"];
        ResultLabel.Text = "";
    }
    protected void GetCreditBtn_Click(object sender, EventArgs e)
    {
        try
        {
            float result = getCredit(useProxy, proxyAddress, proxyUsername, proxyPassword, username, password, domain);
            if (result < ShortMessageService.MAX_VALUE)
                ResultLabel.Text = ShortMessageService.generateDateString() + "خطایی رخ داده است :" + result + ", " + ShortMessageService.getDescriptionForCode((int)result);
            else
                ResultLabel.Text = result + " [ریال] ";
        }
        catch (Exception exception)
        {
           WebMsgBox.Show(ShortMessageService.generateDateString() + "خطایی رخ داده است :" + (exception));
        }
    }
}
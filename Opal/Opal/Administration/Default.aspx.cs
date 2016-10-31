using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Opal.Controls;
using System.Net;

public partial class Administration_Default : PageBaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string localVersion = ConfigurationManager.AppSettings["Version"].Trim().ToLower();
		
		if (_website.EnableVersionChecking)
		{
			try
			{
				using (WebClient wc = new WebClient())
				{
					string newestVersion = wc.DownloadString("http://www.ArvidSMS.ir/Opal/version.txt").Trim().ToLower();

					if (!localVersion.Equals(newestVersion, StringComparison.InvariantCultureIgnoreCase))
					{
						litVersion.Text = string.Format(
							GetGlobalResourceObject("StringsRes", "adm_Default_VersionOutdated") as string,
							localVersion,
							newestVersion
						);
					}
					else
					{
						litVersion.Text = string.Format(GetGlobalResourceObject("StringsRes", "adm_Default_VersionUpToDate") as string, localVersion);
					}
				}
			}
			catch { }
		}
		else
		{
			litVersion.Text = GetGlobalResourceObject("StringsRes", "adm_Default_Version").ToString() + localVersion;
		}
    }
}

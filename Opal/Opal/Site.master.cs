using Opal;
using System.Web.UI.HtmlControls;
using System;

public partial class Site : System.Web.UI.MasterPage
{
	protected override void OnLoad(System.EventArgs e)
	{
		// This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
		// All webpages displaying an ASP.NET menu control must inherit this class.
		if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
			Page.ClientTarget = "uplevel";

		base.OnLoad(e);
	}

	protected override void OnPreRender(System.EventArgs e)
	{
		//if configured, render out the metatag that sets IE8 to IE7 compatibility mode
		//this meta-tag MUST BE THE FIRST tag inside the head-element of the page, otherwise it won't have any effect
		if (WebSite.GetInstance().EnableIE8CompatibilityMetatag)
		{
			HtmlMeta ie8compatMeta = new HtmlMeta();
			ie8compatMeta.HttpEquiv = "X-UA-Compatible";
			ie8compatMeta.Content = "IE=7";
			Page.Header.Controls.AddAt(0, ie8compatMeta); 
		}

		base.OnPreRender(e);
	}
}

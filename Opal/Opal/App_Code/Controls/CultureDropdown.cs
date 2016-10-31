using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Resources;
using System.Web.Configuration;

namespace Opal.Controls
{

    /// <summary>
    /// Summary description for CultureDropdown
    /// </summary>
    public class CultureDropdown : DropDownList
    {
        public CultureDropdown() : base()
        {
            CssClass = "sel";
        }

        public string SelectedCultureID
        {
            get { return this.SelectedValue; }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SortedList lst = getAvailableCultures();
                DataSource = getAvailableCultures();
                DataValueField = "Value";
                DataTextField = "Key";
                DataBind();

                if (WebSite.GetInstance().LocaleID == string.Empty)
                {
                    SelectedValue = "en-GB";
                }
                else
                {
                    SelectedValue = WebSite.GetInstance().LocaleID;
                }
            }
        }

        private SortedList getAvailableCultures()
        {
			SortedList results = new SortedList();

			CultureInfo info = new CultureInfo(WebSite.GetInstance().LocaleID);
			bool rtl = info.TextInfo.IsRightToLeft;

			string[] languages = ((string)((IDictionary)WebConfigurationManager.GetSection("supportedLanguages"))["languages"]).Split(',');
			foreach (string language in languages)
			{
				foreach (CultureInfo specific in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				{
					if (specific.TwoLetterISOLanguageName == language)
					{
						if (rtl)
						{
							string text = specific.DisplayName.Replace("(", "/ ");
							text = text.Replace(")", "");
							results.Add(text, specific.Name);
						}
						else
						{
							results.Add(specific.DisplayName, specific.Name);
						}

					}
				}
			}

			return results;
        }
    }
}
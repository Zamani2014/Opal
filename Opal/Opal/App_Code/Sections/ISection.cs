using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections;

namespace Opal
{
    public interface ISection
    {
        string UserControl
        {
            get;
        }

        string SectionId
        {
            get;
        }

        bool Delete();

        void SaveData();

        List<SearchResult> Search(string searchString, WebPage page);

		string GetLocalizedSectionName();
    }
}

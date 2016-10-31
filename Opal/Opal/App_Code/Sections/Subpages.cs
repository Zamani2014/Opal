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

namespace Opal
{
	public class Subpages : Section<Subpages.SubpagesData>
	{
		public Subpages() : base() { }
		public Subpages(string id) : base(id) { }


		public struct SubpagesData
		{
		}

		public override List<SearchResult> Search(string searchString, WebPage page)
		{
			return new List<SearchResult>();
		}
	}


}
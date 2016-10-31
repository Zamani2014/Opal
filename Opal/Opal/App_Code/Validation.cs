using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Opal
{
    /// <summary>
    /// Utility class for validation (mostly regular expressions used for validation). Contains only static fields, so it cannot be instanciated.
    /// </summary>
    public class Validation
    {
        private Validation(){}

        /// <summary>
        /// Regular expression to validate e-mail addresses
        /// </summary>
        public static string EmailRegex = @"^(\w[-._\w]*@\w[-._\w]*\w\.\w{2,6})$";

        /// <summary>
        /// Regular expression to validate urls
        /// </summary>
        public static string UrlRegex = @"^((((http|https):\/\/)|^)[\w-_]+(\.[\w-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)$";
    }
}

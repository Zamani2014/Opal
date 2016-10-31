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
    public enum RoleNames
    {
        Administrators,
        PowerUsers, // extended role for (PowerUser Management)
        Users,
        Customers,
        Agents,
        Operators
    }

    public enum ViewMode
    {
        Readonly,
        Edit
    }
}

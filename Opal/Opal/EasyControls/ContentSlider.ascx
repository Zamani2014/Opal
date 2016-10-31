<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentSlider.ascx.cs" Inherits="EasyControls_WebUserControl2" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
  <link href="../App_Themes/ElasticOrange/Outlook2007/theme.css" type="text/css" rel="stylesheet" />
  <link href="../App_Themes/ElasticOrange/Outlook2007/icons.css" type="text/css" rel="stylesheet" />

<ComponentArt:TabStrip ID="TabStrip1" runat="server"  Orientation="HorizontalTopToBottom"
    AutoThemingCssClassPrefix="cart-" style="direction: rtl">
    <Tabs>
        <ComponentArt:TabStripTab runat="server" Text="New Root">
            <ComponentArt:TabStripTab runat="server" Text="New Item">
            </ComponentArt:TabStripTab>
        </ComponentArt:TabStripTab>
        <ComponentArt:TabStripTab runat="server" Text="New Root">
            <ComponentArt:TabStripTab runat="server" Text="New Item">
            </ComponentArt:TabStripTab>
        </ComponentArt:TabStripTab>
        <ComponentArt:TabStripTab runat="server" Text="New Root">
        </ComponentArt:TabStripTab>
        <ComponentArt:TabStripTab runat="server" Text="New Root">
            <ComponentArt:TabStripTab runat="server" Text="New Item">
            </ComponentArt:TabStripTab>
        </ComponentArt:TabStripTab>
        <ComponentArt:TabStripTab runat="server" Text="New Root">
        </ComponentArt:TabStripTab>
    </Tabs>
</ComponentArt:TabStrip>

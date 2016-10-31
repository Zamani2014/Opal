<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administration_BulletManagement_Default" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<fieldset dir="rtl">
                            <legend style="direction: rtl"><strong>مدیریت خبرها</strong></legend>
    <asp:BulletedList ID="BulletedList1" runat="server" style="direction: rtl" DisplayMode="HyperLink">
                <asp:ListItem Text="آخرین خبر های سایت" Value="~/administration/NewsManagement/LatestNews.aspx"></asp:ListItem>
                <asp:ListItem Text="آخرین اطلاعیه های ویژه کاربران" Value="~/administration/NewsManagement/LatestAccounces.aspx"></asp:ListItem>
    </asp:BulletedList>
     </fieldset>
</asp:Content>

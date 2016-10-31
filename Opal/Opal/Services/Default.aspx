<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administration_ShortMessageService_Default" MasterPageFile="~/Site.master" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="~/EasyControls/TopMenu.ascx" TagPrefix="TopMenu" TagName="TopMenu" %>
<%@ Register Src="~/EasyControls/LatestAnnouncement.ascx" TagPrefix="LatestAnnouncement" TagName="LatestAnnouncement" %>
<asp:Content ID="profileMainContent" ContentPlaceHolderID="mainContent" Runat="Server">
<div class="profileMainContent">
<TopMenu:TopMenu ID="TopMenu1" runat="server" />
<br />
    <div id="profileShortcuts">
    <table id="profileShortcutsTable">
    <tr>
    <td><div class="profileBtn" onclick="window.location='SMS/SendSMS.aspx'"><div><img alt="ارسال سریع پیامک" class="imgBtn" src="../App_Themes/ElasticOrange/_images/SendSMS.gif" /></div>ارسال سریع پیامک</div></td>
    <td><div class="profileBtn" onclick="window.location='SMS/SendSMSFromFile.aspx'"><div><img alt="ارسال پیامک از فایل" class="imgBtn" src="../App_Themes/ElasticOrange/_images/SendFromFile.png" /></div>ارسال پیامک از فایل</div></td>
    <td><div class="profileBtn" onclick="window.location='SMS/SendBulkSMS.aspx'"><div><img alt="ارسال پیامک انبوه" class="imgBtn" src="../App_Themes/ElasticOrange/_images/SendBulk.png" /></div>ارسال پیامک انبوه</div></td>
    </tr>
    <tr>
    <td><div class="profileBtn" onclick="window.location='SMS/SMSInbox.aspx'"><div><img alt="صندوق ورودی" class="imgBtn" src="../App_Themes/ElasticOrange/_images/Inbox6.png" /></div>صندوق ورودی</div></td>
    <td><div class="profileBtn" onclick="window.location='SMS/SMSOutbox.aspx'"><div><img alt="صندوق خروجی" class="imgBtn" src="../App_Themes/ElasticOrange/_images/Outbox-512.png" /></div>صندوق خروجی</div></td>
    <td><div class="profileBtn" onclick="window.location='SMS/SMSServices.aspx'"><div><img alt="خدمات" class="imgBtn" src="../App_Themes/ElasticOrange/_images/Services1.png" /></div>خدمات</div></td>
    </tr>
    <tr>
    <td><div class="profileBtn" onclick="window.location='../Account/Contacts.aspx'"><div><img alt="تماس ها" class="imgBtn" src="../App_Themes/ElasticOrange/_images/Contacts.png" /></div>تماس ها</div></td>
    <td><div class="profileBtn" onclick="window.location='../Account/Credit.aspx'"><div><img alt="اعتبار و شارژ" class="imgBtn" src="../App_Themes/ElasticOrange/_images/Credit.png" /></div>اعتبار و شارژ</div></td>
    <td><div class="profileBtn" onclick="window.location='../Account/Default.aspx'"><div><img alt="حساب کاربری" class="imgBtn" src="../App_Themes/ElasticOrange/_images/Account.png" /></div>حساب کاربری</div></td>
    </tr>
    </table>
    </div>
    <div class="latestNews">
    <fieldset style="height:320px">
    <legend> آخرین اطلاعیه های ویژه کاربران</legend>
    <LatestAnnouncement:LatestAnnouncement ID="LatestAnnouncement2" runat="server" />
    </fieldset>
    </div>
    </div>
</asp:Content>
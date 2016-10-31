<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopMenu.ascx.cs" Inherits="EasyControls_TopMenu" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<fieldset>
<legend >به حساب کاربری خود خیلی خوش آمدید !</legend>
<table width="100%">
<tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="نام و نام خانوادگی :"></asp:Label>
</td>
<td>
    <asp:Label ID="FirstAndLastName" runat="server" Text="Label" Font-Bold="true"></asp:Label>
</td>
<td>
    <asp:Label ID="Label3" runat="server" Text="شماره خط :"></asp:Label>
</td>
<td>
    <asp:Label ID="LineNotxbx" runat="server" Text="Label" Font-Bold="true"></asp:Label>
</td>
<td>
    <asp:Label ID="Label2" runat="server" Text="صاحب امتیاز :"></asp:Label>
</td>
<td>
    <asp:Label ID="Concessionairetxbx" runat="server" Text="Label" Font-Bold="true"></asp:Label>
</td>
<td>
    <asp:Label ID="Label4" runat="server" Text="نام نماینده :"></asp:Label>
</td>
<td>
    <asp:Label ID="AgentNametxbx" runat="server" Text="Label" Font-Bold="true"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label5" runat="server" Text="اعتبار باقیمانده به ریال :"></asp:Label>
</td>
<td>
    <asp:Label ID="Credittxbx" runat="server" Text="Label" Font-Bold="true"></asp:Label>
</td>
<td>
    <asp:Label ID="Label7" runat="server" Text="میزان کل شارژ به ریال :"></asp:Label>
</td>
<td>
    <asp:Label ID="Chargetxbx" runat="server" Text="Label" Font-Bold="true"></asp:Label>
</td>
<td>
    <asp:Label ID="Label9" runat="server" Text="آخرین ورود :"></asp:Label>
</td>
<td>
    <asp:Label ID="LastLogintxbx" runat="server" Text="Label" Font-Bold="true"></asp:Label>
</td>
<td>
    <asp:Label ID="Label11" runat="server" Text="تاریخ ایجاد :"></asp:Label>
</td>
<td>
    <asp:Label ID="CreationDatetxbx" runat="server" Text="Label" Font-Bold="true"></asp:Label>
</td>
</tr>
</tr>
<tr>
<td style="text-align: center">
    <asp:LinkButton ID="LinkButton1" runat="server" style="text-align: right" PostBackUrl="~/services/default.aspx">صفحه اصلی کنترل پنل</asp:LinkButton>
</td>
<td>
    &nbsp;</td>
<td style="text-align: center">
    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/eforms/epayment.aspx" style="text-align: right">شارژ آنلاین حساب کاربری</asp:LinkButton>
    </td>
<td>
    &nbsp;</td>
<td>
    <asp:Label ID="Label12" runat="server" Text="تاریخ امروز :"></asp:Label>
    </td>
<td>
    <asp:Label ID="TodayDateLabel" runat="server" Text="Label" style="font-weight: 700"></asp:Label>
    </td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
</table>
</fieldset>
<asp:Menu ID="Menu2" runat="server" BackColor="#AED0FD" Font-Names="B Traffic" Font-Size="12px" ForeColor="#FFDA70" StaticSubMenuIndent="10px" Orientation="Horizontal" BorderColor="#FFDA70" Style="direction: rtl; text-align: center;" StaticMenuItemStyle-Width="100px" StaticMenuItemStyle-ItemSpacing="2px" StaticMenuItemStyle-VerticalPadding="2px" StaticMenuItemStyle-HorizontalPadding="2px" OnMenuItemClick="Menu2_MenuItemClick">
    <DynamicHoverStyle BackColor="#FFD800" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#7FC9FF" />
    <DynamicSelectedStyle BackColor="#FFD800" />
    <Items>
        <asp:MenuItem Text="ارسال پیامک" Value="ارسال پیامک">
            <asp:MenuItem Text="ارسال سریع پیامک" Value="SendFastSMS"></asp:MenuItem>
            <asp:MenuItem Text="ارسال پیامک از فایل" Value="SendSMSFromFile"></asp:MenuItem>
            <asp:MenuItem Text="پیامک های انبوه" Value="SendBulkSMS"></asp:MenuItem>
            <asp:MenuItem Text="بانک های اطلاعاتی" Value="MenuItem21"></asp:MenuItem>
            <asp:MenuItem Text="صندوق ورودی" Value="SMSInbox"></asp:MenuItem>
            <asp:MenuItem Text="صندوق خروجی" Value="SMSOutbox"></asp:MenuItem>
            <asp:MenuItem Text="الگوهای پیامک" Value="SMSTemplates"></asp:MenuItem>
            <asp:MenuItem Text="پوشه ها" Value="SMSFolders"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="خدمات" Value="Services">
            <asp:MenuItem Text="انتقال پیام" Value="MenuItem1"></asp:MenuItem>
            <asp:MenuItem Text="پاسخ خودکار" Value="MenuItem2"></asp:MenuItem>
            <asp:MenuItem Text="منشی" Value="MenuItem3"></asp:MenuItem>
            <asp:MenuItem Text="مسابقه و نظرسنجی" Value="MenuItem4"></asp:MenuItem>
            <asp:MenuItem Text="دریافت از وب" Value="MenuItem5"></asp:MenuItem>
            <asp:MenuItem Text="ارسال به ایمیل" Value="MenuItem6"></asp:MenuItem>
            <asp:MenuItem Text="پاسخ هوشمند به مخاطبین" Value="MenuItem7"></asp:MenuItem>
            <asp:MenuItem Text="دریافت پاسخ هوشمند از فایل" Value="MenuItem8"></asp:MenuItem>
            <asp:MenuItem Text="دریافت سرویس منشی از فایل" Value="MenuItem9"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="زمانبندی" Value="Scheduling">
            <asp:MenuItem Text="زمانبندی یک مخاطب" Value="MenuItem10"></asp:MenuItem>
            <asp:MenuItem Text="زمانبندی گروه | تاریخ مبنا" Value="MenuItem11"></asp:MenuItem>
            <asp:MenuItem Text="زمانبندی گروه | تقویمی" Value="MenuItem12"></asp:MenuItem>
            <asp:MenuItem Text="دربافت تاریخ مبنا" Value="MenuItem13"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="گزارش ها و تنظیمات" Value="Reports">
            <asp:MenuItem Text="گزارش ارسال" Value="MenuItem14"></asp:MenuItem>
            <asp:MenuItem Text="گزارش دریافت" Value="MenuItem15"></asp:MenuItem>
            <asp:MenuItem Text="گزارش شارژ ها" Value="MenuItem16"></asp:MenuItem>
            <asp:MenuItem Text="آمار ارسال و دریافت" Value="MenuItem17"></asp:MenuItem>
            <asp:MenuItem Text="فیلتر" Value="MenuItem18"></asp:MenuItem>
            <asp:MenuItem Text="فهرست سرویس ها" Value="MenuItem19"></asp:MenuItem>
            <asp:MenuItem Text="ماژول ها" Value="MenuItem20"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="حساب کاربری" Value="Account">
            <asp:MenuItem Text="تقویم" Value="MenuItem22"></asp:MenuItem>
            <asp:MenuItem Text="تماس ها" Value="MenuItem23"></asp:MenuItem>
            <asp:MenuItem Text="یادداشت ها" Value="MenuItem24"></asp:MenuItem>
            <asp:MenuItem Text="اعتبار و شارژ" Value="MenuItem25"></asp:MenuItem>
            <asp:MenuItem Text="تنظیمات" Value="MenuItem26"></asp:MenuItem>
        </asp:MenuItem>
    </Items>
    <StaticHoverStyle BackColor="#FFD800" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#507CD1" />
</asp:Menu>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LineOrder.aspx.cs" Inherits="eForms_solutionsform" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<script language='javascript' type='text/javascript'>
    function postRefId(refIdValue) {
        var form = document.createElement('form');
        form.setAttribute('method', 'POST');
        form.setAttribute('action', 'https://pgw.bpm.bankmellat.ir/pgwchannel/startpay.mellat');
        form.setAttribute('target', '_self');
        var hiddenField = document.createElement('input');
        hiddenField.setAttribute('name', 'RefId');
        hiddenField.setAttribute('value', refIdValue);
        form.appendChild(hiddenField);
        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    }
</script>
<fieldset>
<legend>فرم سفارش خط اختصاصی</legend>
<h2>پیش از تکمیل فرم حتما موارد زیر را مطالعه بفرمائید :</h2>
<ul>
<li>این فرم برای سفارش و پرداخت الکترونیکی <strong>خط اختصاصی
    </strong>میباشد .</li>
<li>با هر یک از کارتهای بانکی که عضو شبکه شتاب باشند و در صورتیکه پرداخت الکترونیکی کارت شما فعال باشد و دارای رمز دوم باشید میتوانید از این فرم استفاده کنید .</li>
<li>برای اطلاعات ناقص و نامفهوم ترتیب اثر داده نخواهد شد .</li>
<li>پیش از تکمیل این فرم شما باید از طریق <strong>
    <a href="http://www.ArvidSMS.ir/userregistration.aspx">"فرم عمومی ثبت نام در سامانه"</a></strong> در سامانه ثبت نام کرده باشید و نام کاربری داشته باشید .</li>
<li>کلیه مواردی که با ستاره قرمز رنگ مشخص شده اند باید تکمیل گردند .</li>
<li>در صورتیکه تراکنش پرداخت الکترونیکی شما با موفقیت انجام شود به شما پیغام داده میشود .</li>
<li>توصیه میشود پس از انجام موفقیت آمیز پرداخت الکترونیکی کد پیگیری خود را یادداشت نمائید .</li>
<li>پس از تکمیل فرم با کلیک بر روی پرداخت الکترونیکی شما وارد صفحه پرداخت الکترونیکی بانک انتخابی میشوید .</li>
<li>کلیه تراکنش ها در سامانه بانک انجام میشود و هیچکدام از اطلاعات حساب بانکی شما و یا اطلاعات کارت شما در سامانه ما ذخیره نمیشوند .</li>
<li>حتما در پرداخت الکترونیکی و ورود به صفحه پرداخت الکترونیکی بانک ملاحظات امنیتی را رعایت نمائید .</li>
<li>در بخش فایل ها و مستندات اطلاعاتی در مورد افزایش امنیت در پرداخت های الکترونیکی وجود دارد که میتوانید به آنها رجوع کنید .</li>
<li>در صورت هر گونه سوال میتوانید با شماره های 021-33643817 و یا 09128584771 تماس حاصل فرمائید .</li>
    <li style="font-weight: 700">برای سفارش خطوط رند باید با بخش فروش هماهنگ نمائید، این فرم صرفا برای شماره های معمولی میباشد .</li>
</ul>
<br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  DisplayMode="BulletList" ValidationGroup="1" HeaderText="لطفا به پیغام های خطای زیر توجه فرمائید و مجددا تلاش کنید :" />
<br />
    <table style="width:67%;">
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label1" runat="server" Text="نام و نام خانوادگی ثبت کننده :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="FirstAndLastName" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td>
               <span style="color: #FF0000">*</span> <asp:Label ID="Label2" runat="server" Text="نام کاربری در سیستم :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="AccUserName" runat="server" Width="150px" style="direction: ltr"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label3" runat="server" Text="نوع شماره :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" CssClass="aspcontrols" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Value="300010">3000 ده رقمی</asp:ListItem>
                    <asp:ListItem Value="300012">3000 دوازده رقمی</asp:ListItem>
                    <asp:ListItem Value="300014">3000 چهارده رقمی</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label6" runat="server" Text="بانک عامل :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="aspcontrols" Width="150px">
                    <asp:ListItem Value="2">بانک ملت</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="مبلغ قابل پرداخت بریال :"></asp:Label>
            </td>
            <td>
                <asp:Label ID="PriceLabel" runat="server" Text="0"></asp:Label>
            </td>
            <td>
               <span style="color: #FF0000">*</span> <asp:Label ID="Label4" runat="server" Text="تصویر ضد بات :"></asp:Label>
            </td>
            <td>
            <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /><br />
            <asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox" 
                    Width="75px" style="direction: ltr" ValidationGroup="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="تائید و پرداخت الکترونیکی" ValidationGroup="1" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</fieldset>
</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
    </Triggers>
</asp:UpdatePanel>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="وارد کردن نام و نام خانوادگی ضروری است ." ValidationGroup="1" Display="None" ControlToValidate="FirstAndLastName"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="وارد کردن نام کاربری ضروری است ." ValidationGroup="1" Display="None" ControlToValidate="AccUserName"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="وارد کردن تصویر ضد بات ضروری است ." ValidationGroup="1" Display="None" ControlToValidate="txtAntiBotImage"></asp:RequiredFieldValidator>
</asp:Content>
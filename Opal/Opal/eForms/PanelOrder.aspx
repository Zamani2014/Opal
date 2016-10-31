<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PanelOrder.aspx.cs" Inherits="eForms_PanelOrder"  MasterPageFile="~/Site.master"%>

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
        <legend>فرم سفارش پنل های پیشنهادی</legend>
<h2>پیش از تکمیل فرم حتما موارد زیر را مطالعه بفرمائید :</h2>
<ul>
<li>این فرم برای سفارش و پرداخت الکترونیکی <strong>پنل های پیشنهادی
    </strong>میباشد .</li>
<li>علاوه بر این فرم شما می توانید از طریق <strong>
    <a href="http://www.ArvidSMS.ir/userregistration.aspx">"فرم عمومی ثبت نام در سامانه"</a></strong>
در سامانه ثبت نام کنید و نام کاربری دریافت کنید سپس از طریق <br />
    <a href="http://www.ArvidSMS.ir/eforms/LineOrder.aspx">"فرم سفارش خط اختصاصی"</a></strong>
    خط اختصاصی سفارش دهید و یا از طریق
    <a href="http://www.ArvidSMS.ir/eforms/epayment.aspx">"فرم پرداخت الکترونیکی حساب"</a></strong>
   . حساب خود را به اندازه مورد نیاز خود شارژ کنید</li>
<li>با هر یک از کارتهای بانکی که عضو شبکه شتاب باشند و در صورتیکه پرداخت الکترونیکی کارت شما فعال باشد و دارای رمز دوم باشید میتوانید از این فرم استفاده کنید .</li>
<li>در صورتیکه رمز دوم ندارید میتوانید با مراجعه به خودپرداز بانک صادرکننده کارت خود رمز مورد نظر خود را تخصیص و یا تغییر دهید .</li>
<li>برای اطلاعات ناقص و نامفهوم ترتیب اثر داده نخواهد شد .</li>
<li>کلیه مواردی که با ستاره قرمز رنگ مشخص شده اند باید تکمیل گردند .</li>
<li>در صورتیکه تراکنش پرداخت الکترونیکی شما با موفقیت انجام شود به شما پیغام داده میشود .</li>
<li>توصیه میشود پس از انجام موفقیت آمیز پرداخت الکترونیکی کد پیگیری خود را یادداشت نمائید .</li>
<li>پس از تکمیل فرم با کلیک بر روی پرداخت الکترونیکی شما وارد صفحه پرداخت الکترونیکی بانک انتخابی میشوید .</li>
<li>کلیه تراکنش ها در سامانه بانک انجام میشود و هیچکدام از اطلاعات حساب بانکی شما و یا اطلاعات کارت شما در سامانه ما ذخیره نمیشوند .</li>
<li>حتما در پرداخت الکترونیکی و ورود به صفحه پرداخت الکترونیکی بانک ملاحظات امنیتی را رعایت نمائید .</li>
<li>در بخش فایل ها و مستندات اطلاعاتی در مورد افزایش امنیت در پرداخت های الکترونیکی وجود دارد که میتوانید به آنها رجوع کنید .</li>
<li>در صورت هر گونه سوال میتوانید با شماره های 021-33643817 و یا 09128584771 تماس حاصل فرمائید .</li>
<li>پس از تکمیل این فرم شما باید از طریق<strong>
    <a href="http://www.ArvidSMS.ir/userregistration.aspx">"فرم عمومی ثبت نام در سامانه"</a></strong>در سامانه ثبت نام کنید .</li>
</ul>
<br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  DisplayMode="BulletList" ValidationGroup="1" HeaderText="لطفا به پیغام های خطای زیر توجه فرمائید و مجددا تلاش کنید :" />
        <table style="width:59%;">
            <tr>
                <td>
                    <span style="color: #FF0000">*</span><asp:Label ID="Label1" runat="server" Text="نام و نام خانوادگی :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Name" runat="server" Width="150px" ValidationGroup="1" MaxLength="30"></asp:TextBox>
                </td>
                <td>
                    <span style="color: #FF0000">*</span><asp:Label ID="Label2" runat="server" Text="کد ملی :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="NCode" runat="server" Width="150px" style="direction: ltr" ValidationGroup="1" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: #FF0000">*</span><asp:Label ID="Label3" runat="server" Text="پنل پیشنهادی انتخابی :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="aspcontrols" Width="150px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="1">مبتدی</asp:ListItem>
                        <asp:ListItem Value="2">پیشنهاد ویژه</asp:ListItem>
                        <asp:ListItem Value="3">مصرف متوسط</asp:ListItem>
                        <asp:ListItem Value="4">پرمصرف</asp:ListItem>
                        <asp:ListItem Value="5">حرفه ای</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span style="color: #FF0000">*</span><asp:Label ID="Label4" runat="server" Text="بانک عامل :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server"  CssClass="aspcontrols" Width="150px">
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
                    <span style="color: #FF0000">*</span><asp:Label ID="Label7" runat="server" Text="تصویر ضد بات :"></asp:Label>
                </td>
                <td>
            <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /><br />
            <asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox" 
                    Width="75px" style="direction: ltr" ValidationGroup="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="تائید و پرداخت الکترونیکی" OnClick="Button1_Click" ValidationGroup="1"/>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
<br />

    </fieldset>
</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
    </Triggers>
</asp:UpdatePanel>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name" Display="None" ValidationGroup="1" ErrorMessage="وارد کردن نام و نام خانوادگی ضروری است ."></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NCode" Display="None" ValidationGroup="1" ErrorMessage="وارد کردن کد ملی معتبر ضروری است ."></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAntiBotImage" Display="None" ValidationGroup="1" ErrorMessage="وارد کردن تصویر ضد بات ضروری است ."></asp:RequiredFieldValidator>
</asp:Content>
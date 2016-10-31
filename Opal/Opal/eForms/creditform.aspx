<%@ Page Language="C#" AutoEventWireup="true" CodeFile="creditform.aspx.cs" Inherits="eForms_creditform"  MasterPageFile="~/Site.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<script language='javascript' type='text/javascript'>    
    function postRefId (refIdValue) {
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
<legend>فرم الکترونیکی واریز اعتبار</legend>
<h2>خواهشمند است پیش از تکمیل فرم موارد زیر را مطالعه فرمائید :</h2>
<ul>
<li>این فرم برای واریز مبالغی با مقادیر مختلف و خارج از تعرفه ها و پنل های اینترنتی است .</li>
<li>به فرم هایی که حاوی اطلاعات جعلی و آزمایشی باشند ترتیب اثر داده نخواهد شد .</li>
<li>برای استفاده از این فرم حتما میبایست قبلا با شماره 09128584771 و یا شماره 021-33643817 تماس بگیرید و مبلغ خود را هماهنگ نمائید .</li>
<li>کلیه مواردی که با ستاره قرمز رنگ مشخص شده اند باید تکمیل گردند .</li>
<li>اطلاعات شما نزد سامانه محفوظ خواهد بود و به هیچ عنوان بدون کسب اجازه از شما در اختیار افراد بیگانه قرار نخواهد گرفت .</li>
<li>در صورت هر گونه سوال میتوانید با شماره های 021-33643817 و یا 09128584771 تماس حاصل فرمائید .</li>
<li>در صورتیکه تراکنش پرداخت الکترونیکی شما با موفقیت انجام شود به شما پیغام داده میشود .</li>
<li>پس از تکمیل فرم با کلیک بر روی پرداخت الکترونیکی شما وارد صفحه پرداخت الکترونیکی بانک انتخابی میشوید .</li>
<li>کلیه تراکنش ها در سامانه بانک انجام میشود و هیچکدام از اطلاعات حساب بانکی شما و یا اطلاعات کارت شما در سامانه ما ذخیره نمیشوند .</li>
<li>حتما در پرداخت الکترونیکی و ورود به صفحه پرداخت الکترونیکی بانک ملاحظات امنیتی را رعایت نمائید .</li>
<li>در بخش فایل ها و مستندات اطلاعاتی در مورد افزایش امنیت در پرداخت های الکترونیکی وجود دارد که میتوانید به آنها رجوع کنید .</li>
<li><strong>توصیه میشود شماره تماس خود را در بخش توضیحات وارد نمائید</strong> .</li>
</ul>
<br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  DisplayMode="BulletList" ValidationGroup="1" HeaderText="لطفا به پیغام های خطای زیر توجه فرمائید و مجددا تلاش کنید :" />
<br />
    <table style="width: 61%;">
        <tr>
            <td>
                <span style="color: #FF0066">*</span><asp:Label ID="Label1" runat="server" Text="نام و نام خانوادگی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Name" runat="server" Width="150px" style="direction: rtl" MaxLength="20" ValidationGroup="1"></asp:TextBox>
            </td>
            <td>
                <span style="color: #FF0066">*</span><asp:Label ID="Label2" runat="server" Text="مبلغ مورد نظر بریال :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Price" runat="server" style="direction: ltr" Width="150px" MaxLength="50" ValidationGroup="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0066">*</span><asp:Label ID="Label3" runat="server" Text="واریز بابت :" ValidationGroup="1"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Goal" runat="server" Width="150px" MaxLength="100"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="توضیحات :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Comments" runat="server" Width="150px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0066">*</span><asp:Label ID="Label5" runat="server" Text="تصویر ضد بات :" ></asp:Label>
            </td>
            <td>
            <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /><br />
            <asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox" 
                    Width="75px" style="direction: ltr" ValidationGroup="1"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="انتخاب بانک :"></asp:Label>
            </td>
            <td>

                <asp:DropDownList ID="DropDownList1" runat="server" style="direction: rtl" Width="150px" CssClass="aspcontrols">
                    <asp:ListItem Value="2">بانک ملت</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
    </table>
                <asp:Button ID="Button1" runat="server" Text="پرداخت الکترونیکی" ValidationGroup="1" OnClick="Button1_Click"/>
</fieldset>
<asp:RequiredFieldValidator runat="server" ID="valAntiBotImageRequired" ControlToValidate="txtAntiBotImage" Display="None" ErrorMessage="لطفا کاراکترهای نمایش داده شده در تصویر ضد بات را وارد کنید ." SetFocusOnError="true"  ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="Name" Display="None" ErrorMessage="لطفا نام و نام خانوادگی خود را وارد کنید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="Price" Display="None" ErrorMessage="لطفا مبلغ مورد نظر را وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="Goal" Display="None" ErrorMessage="لطفا اینکه بابت چه چیزی این هزینه را واریز میکنید را در کادر مربوطه تایپ نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>

</asp:Content>
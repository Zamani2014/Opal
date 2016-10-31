<%@ Page Language="C#" AutoEventWireup="true" CodeFile="catalog.aspx.cs" Inherits="eForms_catalog"  MasterPageFile="~/Site.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<fieldset>
<legend>فرم ثبت سفارش دریافت رایگان کاتالوگ و مستندات محصولات</legend>
<h2>لطفا پیش از تکمیل فرم موارد زیر را مطالعه فرمائید :</h2>
<ul>
<li>این فرم برای سفارش رایگان مستندات و کاتالوگ محصولات مورد استفاده قرار می گیرد .</li>
<li>به فرم هایی که حاوی اطلاعات جعلی و آزمایشی باشند ترتیب اثر داده نخواهد شد .</li>
<li>تکمیل کردن فیلد هایی که با ستاره قرمز رنگ مشخص شده اند ضروری است .</li>
<li>در بخش نیازها میتوانید نیازهای فناوری اطلاعات خود را در حوزه خدمات ارزش افزوده تلفن همراه بنویسید .</li>
<li>نشانی کامل پستی همراه با نام استان و شهر و کد پستی درج گردد .</li>
<li>تکمیل این فرم هیچگونه مسئولیتی را برای شرکت ایجاد نمیگند تنها در صورت موافقت کارشناسان درخواست شما ارسال خواهد شد .</li>
<li>اطلاعات شما نزد سامانه محفوظ خواهد بود و به هیچ عنوان بدون کسب اجازه از شما در اختیار افراد بیگانه قرار نخواهد گرفت .</li>
<li>در صورت هر گونه سوال میتوانید با شماره های 021-33643817 و یا 09128584771 تماس حاصل فرمائید .</li>
</ul>
<br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  DisplayMode="BulletList" ValidationGroup="1" HeaderText="لطفا به پیغام های خطای زیر توجه فرمائید و مجددا تلاش کنید :" />
<br />

    <table style="width: 87%;">
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label1" runat="server" Text="نام :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="FirstName" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
</td>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label2" runat="server" Text="نام خانوادگی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="LastName" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="کد ملی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NationalCode" runat="server" Width="150px" 
                    style="direction: ltr" MaxLength="10"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="میزان تحصیلات :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="GradeLevelDrp" runat="server" CssClass="aspcontrols">
                    <asp:ListItem>زیر دیپلم</asp:ListItem>
                    <asp:ListItem>دیپلم</asp:ListItem>
                    <asp:ListItem>کاردانی</asp:ListItem>
                    <asp:ListItem>کارشناسی</asp:ListItem>
                    <asp:ListItem>کارشناسی ارشد</asp:ListItem>
                    <asp:ListItem>دکتری</asp:ListItem>
                    <asp:ListItem>سایر ...</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label5" runat="server" Text="سمت | شغل :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CoPosition" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="نام شرکت | موسسه :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CompanyName" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label7" runat="server" Text="زمینه کاری :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="WorkField" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label8" runat="server" Text="نیازها :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="SMSNeeds" runat="server" MaxLength="100" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="نشانی وبسایت :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Website" runat="server" Width="150px" style="direction: ltr" 
                    MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="نشانی پست الکترونیکی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="EMail" runat="server" Width="150px" style="direction: ltr" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label11" runat="server" Text="تلفن ثابت :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Tel" runat="server" Width="150px" style="direction: ltr" 
                    MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label12" runat="server" Text="تلفن همراه :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Mobile" runat="server" Width="150px" style="direction: ltr" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label13" runat="server" Text="نشانی کامل :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Address" runat="server" MaxLength="100"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label14" runat="server" Text="توضیحات :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Comments" runat="server" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label15" runat="server" Text="تصویر ضد بات :"></asp:Label>
            </td>
            <td>
            <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /><br />
            <asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox" 
                    Width="75px" style="direction: ltr"></asp:TextBox>
               </td>
            <td>
               </td>
            <td>
                <asp:Button ID="SendBtn" runat="server" Text="ارسال اطلاعات"  ValidationGroup="1"
                    onclick="SendBtn_Click" />
            </td>
        </tr>
    </table>
</fieldset>
<asp:RequiredFieldValidator runat="server" ID="valAntiBotImageRequired" ControlToValidate="txtAntiBotImage" Display="None" ErrorMessage="لطفا کاراکترهای نمایش داده شده در تصویر ضد بات را وارد کنید ." SetFocusOnError="true"  ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="CoPosition" Display="None" ErrorMessage="لطفا سمت خود در شرکت و یا شغل خود را وارد کنید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="FirstName" Display="None" ErrorMessage="لطفا نام کوجک واریز کننده به حساب بانکی را در بخش مشخص شده وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="LastName" Display="None" ErrorMessage="لطفا نام خانوادگی واریز کننده به حساب بانکی را در بخش مشخص شده وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="WorkField" Display="None" ErrorMessage="لطفا زمینه کاری خود را وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="SMSNeeds" Display="None" ErrorMessage="لطفا نیازهای خود به راهکارهای ارزش افزوده تلفن همراه را وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="Tel" Display="None" ErrorMessage="لطفا تلفن ثابت خود را وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="Mobile" Display="None" ErrorMessage="لطفا تلفن همراه خود را وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="Address" Display="None" ErrorMessage="لطفا نشانی کامل پستی خود را وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>

<asp:CustomValidator runat="server" ID="valAntiBotImage" OnServerValidate="valAntiBotImage_ServerValidate" Text="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibotInvalid %>" Display="dynamic" ValidationGroup="1" ></asp:CustomValidator>

</asp:Content>
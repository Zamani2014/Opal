<%@ Page Language="C#" AutoEventWireup="true" CodeFile="recieptform.aspx.cs" Inherits="eForms_recieptform"  MasterPageFile="~/Site.master"%>
<%@ Register Assembly="Heidarpour.WebControlUI" Namespace="Heidarpour.WebControlUI" TagPrefix="Heidarpour" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script type="text/javascript">

        function onSelect(calendar, date) {
            // Beware that this function is called even if the end-user only
            // changed the month/year. In order to determine if a date was
            // clicked you can use the dateClicked property of the calendar:
            if (calendar.dateClicked) {
                var msg =
                        "<br/>Persian: Year: " + calendar.date.getJalaliFullYear() +
                        ", Month: " + (calendar.date.getJalaliMonth() + 1) +
                        ", Day: " + calendar.date.getJalaliDate() +
                        "<br/>Gregorian: Year: " + calendar.date.getFullYear() +
                        ", Month: " + calendar.date.getMonth() +
                        ", Day: " + calendar.date.getDate();

                $("#<%= DatePicker1.ClientID %>").val(date);
                logEvent("onSelect Event: <br> Selected Date: " + date + msg);
                calendar.hide();
                //calendar.callCloseHandler(); // this calls "onClose"
            }
        };

        function onUpdate(calendar) {
            var msg =
                    "<br/>Persian: Year: " + calendar.date.getJalaliFullYear() +
                    ", Month: " + (calendar.date.getJalaliMonth() + 1) +
                    ", Day: " + calendar.date.getJalaliDate() +
                    "<br/>Gregorian: Year: " + calendar.date.getFullYear() +
                    ", Month: " + calendar.date.getMonth() +
                    ", Day: " + calendar.date.getDate();

            logEvent("onUpdate Event: <br> Selected Date: " + calendar.date.print('%Y/%m/%d', 'jalali') + msg);
        };

        function onClose(calendar) {
            logEvent("onClose Event");
            calendar.hide();
        };

        function logEvent(str) {
            $("#log").append("<li>" + str + "</li>");
        }
    </script>

<fieldset>
<legend>فرم ثبت اطلاعات فیش بانکی</legend>
<h2>پیش از تکمیل فرم حتما موارد زیر را مطالعه بفرمائید :</h2>
<ul>
<li>این فرم برای ثبت فیش واریزی در پرداخت های حضوری به بانک در نظر گرفته شده است . <br />در صورتیکه کارتهای الکترونیکی عضو شبکه شتاب دارید میتوانید از طریق یکی از فرم های مربوطه اقدام به واریز الکترونیکی کنید .</li>
<li>برای اطلاعات ناقص و نامفهوم ترتیب اثر داده نخواهد شد .</li>
<li>پیش از تکمیل این فرم شما باید از طریق <strong>
    <a href="http://www.ArvidSMS.ir/userregistration.aspx">"فرم عمومی ثبت نام در سامانه"</a></strong> در سامانه ثبت نام کرده باشید و نام کاربری داشته باشید .</li>
<li>کلیه مواردی که با ستاره قرمز رنگ مشخص شده اند باید تکمیل گردند .</li>
<li>حداکثر 24 ساعت پس از ثبت اطلاعات حساب کاربری شما شارژ میشود در این مدت از تماس با شرکت خودداری نمائید .</li>
<li>در صورتیکه کنترل تاریخ باز نمیشود لطفا صفحه را ریفرش نمائید .</li>
<li>اطلاعات شما نزد سامانه محفوظ خواهد بود و به هیچ عنوان بدون کسب اجازه از شما در اختیار افراد بیگانه قرار نخواهد گرفت .</li>
<li>در قسمت مشخص شده باید نام و نام خانوادگی واریز کننده به حساب را وارد نمائید .</li>
<li>در صورت هر گونه سوال میتوانید با شماره های 021-33643817 و یا 09128584771 تماس حاصل فرمائید .</li>
</ul>
<br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  DisplayMode="BulletList" ValidationGroup="1" HeaderText="لطفا به پیغام های خطای زیر توجه فرمائید و مجددا تلاش کنید :" />
<br />
    <table style="width: 63%;">
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label1" runat="server" Text="نام کاربری :"></asp:Label>
            </td>
            <td>
                
                <asp:TextBox ID="UserName" runat="server" style="direction: ltr" Width="150px" 
                    MaxLength="50"></asp:TextBox>
            </td>
            <td>
                
                <span style="color: #FF0000">*</span><asp:Label ID="Label2" runat="server" Text="شماره فیش واریزی :"></asp:Label>
            </td>
            <td>
                
                <asp:TextBox ID="ReceiptNo" runat="server" style="direction: ltr" Width="150px" 
                    MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label3" runat="server" Text="نام :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="FirstName" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label4" runat="server" Text="نام خانوادگی :"></asp:Label>
            </td>
            <td>
                               <asp:TextBox ID="LastName" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label5" runat="server" Text="نام بانک :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="BankName" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label6" runat="server" Text="نام شعبه :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="BankBranch" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label7" runat="server" Text="تاریخ واریز :"></asp:Label>
            </td>
            <td>
                            <Heidarpour:DatePicker ID="DatePicker1" runat="server" DatePersian="1392/07/11" CalendarType="Persian"
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" ShowWeekNumbers="True" AutoPostBack="true"
        ReadOnly="True" FirstDayOfWeek="Saturday" ShowOthers="True" Height="25px" style="text-align:center;"
        onclose="onClose" OnUpdate="onUpdate"></Heidarpour:DatePicker>
                </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="توضیحات :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Comments" runat="server" Width="150px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span style="color: #FF0000">*</span><asp:Label ID="Label9" runat="server" Text="تصویر ضد بات :"></asp:Label>
            </td>
            <td>
            <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /><br />
            <asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox" 
                    Width="75px" style="direction: ltr"></asp:TextBox>
                </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Button ID="SendBtn" runat="server" Text="ارسال اطلاعات"  ValidationGroup="1" 
                    onclick="SendBtn_Click" />
            </td>
        </tr>
    </table>
</fieldset>
<asp:RequiredFieldValidator runat="server" ID="valAntiBotImageRequired" ControlToValidate="txtAntiBotImage" Display="None" ErrorMessage="لطفا کاراکترهای نمایش داده شده در تصویر ضد بات را وارد کنید ." SetFocusOnError="true"  ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="UserName" Display="None" ErrorMessage="لطفا نام کاربری را وارد کنید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="FirstName" Display="None" ErrorMessage="لطفا نام کوجک واریز کننده به حساب بانکی را در بخش مشخص شده وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="LastName" Display="None" ErrorMessage="لطفا نام خانوادگی واریز کننده به حساب بانکی را در بخش مشخص شده وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ReceiptNo" Display="None" ErrorMessage="لطفا شماره فیش بانکی مورد نظر خود را در بخش مربوطه وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="BankName" Display="None" ErrorMessage="لطفا نام بانکی که مبلغ مورد نظر خود را واریز نموده اید وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="BankBranch" Display="None" ErrorMessage="لطفا نام شعبه ای که واریز نموده اید وارد نمائید ." SetFocusOnError="true" ValidationGroup="1" ></asp:RequiredFieldValidator>

<asp:CustomValidator runat="server" ID="valAntiBotImage" OnServerValidate="valAntiBotImage_ServerValidate" Text="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibotInvalid %>" Display="dynamic" ValidationGroup="1" ></asp:CustomValidator>

</asp:Content>
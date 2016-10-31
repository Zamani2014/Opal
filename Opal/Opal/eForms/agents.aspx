<%@ Page Language="C#" AutoEventWireup="true" CodeFile="agents.aspx.cs" Inherits="eForms_agents"  MasterPageFile="~/Site.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<fieldset>
<legend>فرم درخواست نمایندگی فروش</legend>
<h2>خواهشمند است پیش از تکمیل فرم موارد زیر را مطالعه فرمائید :</h2>
<ul>
<li>این فرم برای ارزیابی اولیه نمایندگان میباشد .</li>
<li>به فرم هایی که حاوی اطلاعات جعلی و آزمایشی باشند ترتیب اثر داده نخواهد شد .</li>
<li>کلیه مواردی که با ستاره قرمز رنگ مشخص شده اند باید تکمیل گردند .</li>
<li>اطلاعات شما نزد سامانه محفوظ خواهد بود و به هیچ عنوان بدون کسب اجازه از شما در اختیار افراد بیگانه قرار نخواهد گرفت .</li>
<li>در صورت هر گونه سوال میتوانید با شماره های 021-33643817 و یا 09128584771 تماس حاصل فرمائید .</li>
<li>تکمیل این فرم هیچ مسئولیتی برای شرکت آروید فاوا ایجاد نخواهد کرد تنها در صورت تائید هیات مدیره نمایندگی برای شما صادر میشود .</li>
<li>پس از تائید نمایندگی شما میبایست ظرف مدت 24 ساعت هزینه بیعانه درخواست نمایندگی را از طریق فرم الکترونیکی واریز اعتبار واریز نمائید .</li>
    <li>در صورتیکه شما شرکت ندارید از تکمیل بخش اطلاعات سازمانی خودداری فرمائید .</li>
</ul>
<br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  DisplayMode="BulletList" ValidationGroup="1" HeaderText="لطفا به پیغام های خطای زیر توجه فرمائید و مجددا تلاش کنید :" />
<br />
    <fieldset>
        <legend><span style="color: #FF0000">*</span>اطلاعات شخصی</legend>
    <table style="width: 69%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="نام و نام خانوادگی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Name" runat="server" Width="150px" MaxLength="20" ValidationGroup="1"></asp:TextBox>
                </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="نام پدر :"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="FatherName" runat="server" Width="150px" MaxLength="20" ValidationGroup="1"></asp:TextBox>
</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="کد ملی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NationalCode" runat="server" Width="150px" style="direction: ltr" MaxLength="10" ValidationGroup="1"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="تاریخ تولد :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="BirthDate" runat="server" Width="150px" style="direction: ltr" MaxLength="15" ValidationGroup="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="شغل | حرفه :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Job" runat="server" Width="150px" MaxLength="20" ValidationGroup="1"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="سابقه فعالیت :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PreActivity" runat="server" Width="150px" MaxLength="20" ValidationGroup="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="میزان تحصیلات :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="GradeDrp" runat="server" style="direction: rtl" CssClass="aspcontrols" Width="150">
                    <asp:ListItem>متوسطه</asp:ListItem>
                    <asp:ListItem>کاردانی</asp:ListItem>
                    <asp:ListItem>کارشناسی</asp:ListItem>
                    <asp:ListItem>کارشناسی ارشد</asp:ListItem>
                    <asp:ListItem>دکتری</asp:ListItem>
                    <asp:ListItem>سایر</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="رشته تحصیلی :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Course" runat="server" Width="150px" MaxLength="20" ValidationGroup="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="محل سکونت :"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="HouseAddress" runat="server" MaxLength="100" ValidationGroup="1"></asp:TextBox>
            </td>
        </tr>
    </table>
</fieldset><br />
    <fieldset>
    <legend>اطلاعات سازمانی</legend>
        <table style="width: 732px">
            <tr>
                <td style="width: 125px">
                    <asp:Label ID="Label11" runat="server" Text="نام شرکت | موسسه :"></asp:Label>
                </td>
                <td style="width: 190px">
                    <asp:TextBox ID="CoName" runat="server" Width="150" MaxLength="20"></asp:TextBox>
                </td>
                <td style="width: 104px">
                    <asp:Label ID="Label12" runat="server" Text="شماره ثبت :"></asp:Label>
                </td>
                <td style="width: 191px">
                    <asp:TextBox ID="RegisterNo" runat="server" Width="150" style="direction: ltr" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <asp:Label ID="Label13" runat="server" Text="نوع شرکت | موسسه"></asp:Label>
                </td>
                <td style="width: 190px">
                    <asp:DropDownList ID="CompanyType" runat="server" CssClass="aspcontrols" Width="150">
                        <asp:ListItem>شرکت با مسئولیت محدود</asp:ListItem>
                        <asp:ListItem>شرکت سهامی خاص</asp:ListItem>
                        <asp:ListItem>شرکت سهامی عام</asp:ListItem>
                        <asp:ListItem>سازمان مردم نهاد | سمن</asp:ListItem>
                        <asp:ListItem>موسسه غیرانتفاعی | غیر تجاری</asp:ListItem>
                        <asp:ListItem>شرکت تضامنی</asp:ListItem>
                        <asp:ListItem>شرکت نسبی</asp:ListItem>
                        <asp:ListItem>سایر ...</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 104px">
                    <asp:Label ID="Label14" runat="server" Text="میزان سرمایه :"></asp:Label>
                </td>
                <td style="width: 191px">
                    <asp:TextBox ID="InvestBalance" runat="server" Width="150" style="direction: ltr" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <asp:Label ID="Label15" runat="server" Text="تعداد کارمندان :"></asp:Label>
                </td>
                <td style="width: 190px">
                    <asp:TextBox ID="EmployeeNo" runat="server" Width="150" style="direction: ltr" MaxLength="20"></asp:TextBox>
                </td>
                <td style="width: 104px">
                    <asp:Label ID="Label16" runat="server" Text="زمینه فعالیت :"></asp:Label>
                </td>
                <td style="width: 191px">
                    <asp:TextBox ID="ActivityField" runat="server" Width="150" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <asp:Label ID="Label17" runat="server" Text="وبسایت :"></asp:Label>
                </td>
                <td style="width: 190px">
                    <asp:TextBox ID="Website" runat="server" style="direction: ltr" Width="150" MaxLength="20"></asp:TextBox>
                </td>
                <td style="width: 104px">
                    <asp:Label ID="Label18" runat="server" Text="ایمیل :"></asp:Label>
                </td>
                <td style="width: 191px">
                    <asp:TextBox ID="EMail" runat="server" style="direction: ltr" Width="150" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 125px">
                    <asp:Label ID="Label19" runat="server" Text="نوع مالکیت شرکت :"></asp:Label>
                </td>
                <td style="width: 190px">
                    <asp:DropDownList ID="CompanyOwnerType" runat="server" CssClass="aspcontrols" Width="150">
                        <asp:ListItem>خصوصی</asp:ListItem>
                        <asp:ListItem>دولتی</asp:ListItem>
                        <asp:ListItem>نیمه دولتی</asp:ListItem>
                        <asp:ListItem>سایر ...</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 104px">
                    <asp:Label ID="Label20" runat="server" Text="مرجع صدور مجوز :"></asp:Label>
                </td>
                <td style="width: 191px">
                    <asp:TextBox ID="Licencer" runat="server" Width="150" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
        </table>
</fieldset>
    <br />
    <fieldset>
        <legend><span style="color: #FF0000">*</span>اطلاعات تماس</legend>

        <table style="width:71%;">
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="تلفن ثابت :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Tel" runat="server" style="direction: ltr" Width="150" MaxLength="20" ValidationGroup="1"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="تلفن همراه :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Mobile" runat="server" style="direction: ltr" Width="150" MaxLength="20" ValidationGroup="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="ایمیل :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="CoEmail" runat="server" style="direction: ltr" Width="150" MaxLength="20" ValidationGroup="1"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="نمابر :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Fax" runat="server" style="direction: ltr" Width="150" MaxLength="20" ValidationGroup="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="نشانی شرکت :"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="CoAddress" runat="server" TextMode="MultiLine" MaxLength="200" ValidationGroup="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="تصویر ضد بات :"></asp:Label>
                </td>
                <td>
            <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /><br />
            <asp:TextBox runat="server" ID="txtAntiBotImage" MaxLength="4" CssClass="textbox" 
                    Width="75px" style="direction: ltr" ValidationGroup="1"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td style="text-align:center">
                    &nbsp;</td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend>سایر اطلاعات</legend>

        <table style="width:65%;">
            <tr>
                <td>

                    <asp:Label ID="Label27" runat="server" Text="نحوه آشنائی با ما :"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="IntroWay" runat="server" Width="150" MaxLength="50"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label28" runat="server" Text="نام کاربری در سامانه :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="UserName" runat="server" style="direction: ltr" Width="150" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="میزان بودجه :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Budget" runat="server" style="direction: ltr" Width="150" MaxLength="20"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label30" runat="server" Text="میزان آشنائی با خدمات پیامک :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="SMSfamiliar" runat="server" Width="150" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label31" runat="server" Text="توضیحات :"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="Comments" runat="server" TextMode="MultiLine" MaxLength="100" ></asp:TextBox>
                </td>
            </tr>
        </table>
                    <asp:Button ID="Button1" runat="server" Text="تائید و ارسال" OnClick="Button1_Click" ValidationGroup="1"/>
                    </fieldset>
</fieldset>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name" ErrorMessage="وارد کردن نام و نام خانوادگی ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FatherName" ErrorMessage="وارد کردن نام پدر ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="NationalCode" ErrorMessage="وارد کردن کد ملی ده رقمی الزاامی است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="BirthDate" ErrorMessage="وارد کردن تاریخ تولد ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Job" ErrorMessage="وارد کردن شغل و حرفه ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="PreActivity" ErrorMessage="وارد کردن سابفه فعالیت ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Course" ErrorMessage="وارد کردن رشته تحصیلی ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="HouseAddress" ErrorMessage="وارد کردن نشانی محل سکونت ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="Tel" ErrorMessage="وارد کردن شماره تلفن ثابت ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="Mobile" ErrorMessage="وارد کردن شماره تلفن همراه ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="Fax" ErrorMessage="وارد کردن شماره فکس ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="CoEmail" ErrorMessage="وارد کردن پست الکترونیکی شرکت ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="CoAddress" ErrorMessage="وارد کردن نشانی شرکت ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtAntiBotImage" ErrorMessage="وارد کردن تصویر ضد بات ضروری است ." ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
</asp:Content>
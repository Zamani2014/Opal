<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendSMSFromFile.aspx.cs" Inherits="Administration_ShortMessageService_SendFromFile" MasterPageFile="~/Site.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<%@ Register Src="~/EasyControls/TopMenu.ascx" TagPrefix="TopMenu" TagName="TopMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<TopMenu:TopMenu ID="TopMenu1" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
    <ContentTemplate>
<fieldset>
<legend>ارسال پیامک از فایل</legend>
    <asp:RadioButton ID="RadioButton1" runat="server"  GroupName="SMSSelection" 
        Text="ارسال یک پیام ثابت برای گیرندگان مختلف" AutoPostBack="True" 
        oncheckedchanged="radioBtm1" Checked="true" Font-Bold="true"/><br />
    <asp:RadioButton ID="RadioButton2" runat="server"  GroupName="SMSSelection" 
        Text="ارسال پیام های مختلف برای گیرندگان مختلف (نظیر به نظیر)" 
        oncheckedchanged="radioBtn2" AutoPostBack="True" Font-Bold="true"/>
    <br />    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:Panel ID="Panel2" runat="server" CssClass="Panel">   
    طول استاندارد یک پیامک برابر با 140 بایت است، طول استاندارد یک پیامک فارسی 70 کاراکتر و طول استاندارد یک پیامک لاتین 160 کاراکتر میباشد.
    نکته اینکه حتی وجود یک کارکتر فارسی در پیامک باعث تغییر رمز نگاری آن پیامک شده و در نتیجه پیامک بطور کلی فارسی محاسبه میشود و طول آن بر اساس طول پیامک فارسی در نظر گرفته خواهد شد . <br />
    برای ارسال پیامک هایی که طول آنها از طول استاندارد بیشتر است ، سیستم بصورت خودکار پیامک را به تعداد بخش های لازم تقسیم کرده و ارسال می کند، میزان اعتبار کسر شده از کاربر بر اساس تعداد بخش های متن پیام میباشد . <br />
    برای تغییر جهت تایپ و تایپ انگلیسی از کلید های ترکیبی Ctrl + Shift چپ استفاده کنید.<br />
    برای اطلاع از تعداد بخش های پیامک خود و تعداد کاراکترها به توضیحات پائین جعبه ی متنی توجه نمائید.
    </asp:Panel>
    <asp:BalloonPopupExtender ID="BalloonPopupExtender2" runat="server" TargetControlID="Image3" BalloonStyle="Rectangle" BalloonPopupControlID="panel2" BalloonSize="Large">
    </asp:BalloonPopupExtender>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="MessageTextBox" WatermarkText="متن پیامک خود را اینجا تایپ کنید ..." WatermarkCssClass="watermark">
    </asp:TextBoxWatermarkExtender>
<fieldset>
<legend>ارسال یک پیام ثابت برای گیرندگان مختلف</legend>
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="انتخاب فایل شماره ها :"></asp:Label><br />
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/help.png" />
                <asp:Panel ID="Panel3" runat="server">
        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:stringsRes,sendSMSFromFileNotices %>">"></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender3" runat="server" BalloonPopupControlID="Panel3" BalloonSize="Large" BalloonStyle="Rectangle" TargetControlID="Image2" >
                </asp:BalloonPopupExtender>
            </td>
            <td style="width: 329px">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:RegularExpressionValidator ID="uplValidator" runat="server" ControlToValidate="FileUpload1"
                 ErrorMessage="نوع فایل شما فقط میتواند یکی از انواع xls, xlsx, csv ویا txt باشد !" 
                 ValidationExpression="(.+\.([Xx][Ll][Ss][Xx])|.+\.([Xx][Ll][Ss])|.+\.([Cc][Ss][Vv])|.+\.([Tt][Xx][Tt]))"  ValidationGroup="1"></asp:RegularExpressionValidator>  <br />
                <asp:CheckBox ID="CheckBox6" runat="server" Text="برای فایل های اکسل سطر اول بعنوان نام ستون" /><br />
                <asp:CheckBox ID="CheckBox9" runat="server" Text="افزودن شماره ها به تماس ها" /><br />
                <asp:Button ID="Button1" runat="server" Text="بالاگذاری" onclick="Button1_Click" />
            </td>
            <td>
                <asp:Label ID="Label14" runat="server" Text="" style="direction: ltr"></asp:Label>
            </td>
        </tr>
                <tr>
            <td style="width: 133px">
                <asp:Label ID="Label2" runat="server" Text="متن پیام :"></asp:Label><br />
                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/help.png" />
            </td>
            <td style="width: 329px">
                <asp:TextBox ID="MessageTextBox" runat="server" TextMode="MultiLine" ValidationGroup="1" OnTextChanged="MessageTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                <br />
                [<asp:Label ID="Label6" runat="server" Text="تعداد کاراکتر:"></asp:Label><asp:Label
                    ID="Label7" runat="server" Text="0"></asp:Label>]&nbsp;[<asp:Label ID="Label8"
                        runat="server" Text="تعداد باقیمانده:"></asp:Label><asp:Label ID="Label9" runat="server"
                            Text="70"></asp:Label>&nbsp;<asp:Label ID="Label12" runat="server" Text="(1)"></asp:Label>]&nbsp;[<asp:Label ID="Label10" runat="server" Text="زبان :"></asp:Label><asp:Label
                                ID="Label11" runat="server" Text="فارسی"></asp:Label>]
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="وارد کردن متن پیام ضروری است ." ControlToValidate="MessageTextBox" ValidationGroup="1"></asp:RequiredFieldValidator>
                <br />
                <asp:LinkButton ID="LinkButton1" runat="server">ذخیره بعنوان الگو</asp:LinkButton>
            </td>
            <td>
                <asp:Button ID="Button4" runat="server" Text="انتخاب الگوی متن" /><br />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="پیامک ادغامی" /><br />
                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/help.png" />
            </td>
        </tr>

        <tr>
            <td style="width: 133px">
                <asp:Label ID="Label4" runat="server" Text="نحوه نمایش پیامک :"></asp:Label>&nbsp;<asp:Image ID="Image5" runat="server" ImageUrl="~/Images/help.png" />
                <asp:Panel ID="Panel4" runat="server">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:stringsRes, sendSMS_DeliveryOption%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender4" runat="server" BalloonPopupControlID="Panel4" BalloonSize="Large" TargetControlID="Image5">
                </asp:BalloonPopupExtender>
            </td>
            <td style="width: 329px">
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="aspcontrols" 
                    style="direction: rtl" AutoPostBack="True" 
                    onselectedindexchanged="DropDownList2_Changed">
                    <asp:ListItem Value="1">پیامک عادی</asp:ListItem>
                    <asp:ListItem Value="0">پیامک خبری</asp:ListItem>
                    <asp:ListItem Value="2">ذخیره در حافظه سیم کارت</asp:ListItem>
                    <asp:ListItem Value="3">ذخیره در نرم افزاری خاص</asp:ListItem>
                </asp:DropDownList><asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" Visible="false" Height="22px" 
                        style="direction: ltr; margin-left: 0px;" Width="154px"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TextBox1" WatermarkText="شماره پورت نرم افزار مورد نظر">
                </asp:TextBoxWatermarkExtender>
            </td>
        </tr>
            <tr>
                <td style="width: 133px">
                    <asp:Label ID="Label5" runat="server" Text="اولویت ارسال :"></asp:Label>
                </td>
                <td style="width: 329px">
                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="aspcontrols" 
                        style="direction: rtl">
                        <asp:ListItem Value="1">اول</asp:ListItem>
                        <asp:ListItem Value="2">دوم</asp:ListItem>
                        <asp:ListItem Value="3">سوم</asp:ListItem>
                        <asp:ListItem Value="4">چهارم</asp:ListItem>
                        <asp:ListItem Value="5">پنجم</asp:ListItem>
                        <asp:ListItem Value="6">ششم</asp:ListItem>
                        <asp:ListItem Value="7">هفتم</asp:ListItem>
                        <asp:ListItem Value="8">هشتم</asp:ListItem>
                        <asp:ListItem Value="9">نهم</asp:ListItem>
                        <asp:ListItem Value="10">دهم</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 133px">
                    &nbsp;
                </td>
                <td style="width: 329px">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="بعدا ارسال شود." />
                    <br />
                    <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" 
                        oncheckedchanged="CheckBox3_CheckedChanged" Text="ارسال با دروازه:" />
                    <asp:TextBox ID="GatewayTextBox" runat="server" Height="22px" 
                        style="direction: ltr; margin-left: 0px;" Visible="false" Width="150px"></asp:TextBox>
                    <br />
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                        ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="999,999,999.999" 
                        MaskType="Number" MessageValidatorTip="true" TargetControlID="GatewayTextBox">
                    </asp:MaskedEditExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" 
                        TargetControlID="GatewayTextBox" WatermarkText="آدرس آی پی را اینجا وارد کنید">
                    </asp:TextBoxWatermarkExtender>
                    <asp:Button ID="SendBtn" runat="server" onclick="SendBtn_Click" Text="ارسال" 
                        ValidationGroup="1" />
                </td>
                <td>
                </td>
            </tr>
    </table>
            <asp:ListBox ID="ListBox1" runat="server" Visible="false" AutoPostBack="True" CssClass="aspcontrols" ForeColor="#FF3300" Width="99.5%"></asp:ListBox>
</fieldset>
</ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="ResultLabel" runat="server" Text="Label"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="false">
    <ContentTemplate>
        <asp:BalloonPopupExtender ID="BalloonPopupExtender1" runat="server" TargetControlID="Image1" BalloonSize="Large" BalloonStyle="Rectangle"  BalloonPopupControlID="Panel1">
        </asp:BalloonPopupExtender>
        <asp:Panel ID="Panel1" runat="server">
        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:stringsRes,sendSMSFromFileNotices2 %>">"></asp:Literal>
        </asp:Panel>
<fieldset>
<legend>ارسال پیام های مختلف برای گیرندگان مختلف (نظیر به نظیر)</legend>
    <table style="width: 100%;">
        <tr>
            <td style="width: 145px">
           <asp:Label ID="Label15" runat="server" Text="انتخاب فایل پیام ها :"></asp:Label><br />
           <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/help.png"/>
            </td>
            <td style="width: 329px">
                <asp:FileUpload ID="FileUpload2" runat="server" /><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload2"
                 ErrorMessage="نوع فایل شما فقط میتواند یکی از انواع xls ویا xlsx باشد !" 
                 ValidationExpression="(.+\.([Xx][Ll][Ss][Xx])|.+\.([Xx][Ll][Ss]))"  ValidationGroup="1"></asp:RegularExpressionValidator><br />
                <asp:CheckBox ID="CheckBox7" runat="server" Text="سطر اول بعنوان نام های ستون ها" /><br />
                <asp:CheckBox ID="CheckBox8" runat="server" Text="افزودن شماره ها به تماس ها" /><br />
                <asp:Button ID="Button5" runat="server" onclick="Button5_Click" Text="بالاگذاری" />
            </td>
            <td>
                <asp:Label ID="Label18" runat="server" style="direction: ltr" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 145px">
                <asp:Label ID="Label16" runat="server" Text="نحوه نمایش پیامک :"></asp:Label>
            </td>
            <td style="width: 358px">
                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="aspcontrols" 
                    style="direction: rtl" AutoPostBack="True" 
                    onselectedindexchanged="DropDownList4_Changed">
                    <asp:ListItem Value="1">پیامک عادی</asp:ListItem>
                    <asp:ListItem Value="0">پیامک خبری</asp:ListItem>
                    <asp:ListItem Value="2">ذخیره در حافظه سیم کارت</asp:ListItem>
                    <asp:ListItem Value="3">ذخیره در نرم افزاری خاص</asp:ListItem>
                </asp:DropDownList>&nbsp;<asp:TextBox ID="TextBox2" runat="server" AutoPostBack="True" Visible="false" Height="22px" 
                        style="direction: ltr; margin-left: 0px;" Width="154px"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="TextBox2" WatermarkText="شماره پورت نرم افزار مورد نظر">
                </asp:TextBoxWatermarkExtender>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 145px">
                <asp:Label ID="Label17" runat="server" Text="اولویت ارسال :"></asp:Label>
            </td>
            <td style="width: 358px">
                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="aspcontrols" 
                    style="direction: rtl">
                    <asp:ListItem Value="1">اول</asp:ListItem>
                    <asp:ListItem Value="2">دوم</asp:ListItem>
                    <asp:ListItem Value="3">سوم</asp:ListItem>
                    <asp:ListItem Value="4">چهارم</asp:ListItem>
                    <asp:ListItem Value="5">پنجم</asp:ListItem>
                    <asp:ListItem Value="6">ششم</asp:ListItem>
                    <asp:ListItem Value="7">هفتم</asp:ListItem>
                    <asp:ListItem Value="8">هشتم</asp:ListItem>
                    <asp:ListItem Value="9">نهم</asp:ListItem>
                    <asp:ListItem Value="10">دهم</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 145px">
                &nbsp;</td>
            <td style="width: 358px">
                <asp:CheckBox ID="CheckBox4" runat="server" Text="بعدا ارسال شود." /><br />
                <asp:CheckBox ID="CheckBox5" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBox5_CheckedChanged" Text="ارسال با دروازه:" />
                <asp:TextBox ID="GatewayTextBox2" runat="server" Height="22px" 
                    style="direction: ltr; margin-left: 0px;" Visible="false" Width="150px"></asp:TextBox><br />
                <asp:MaskedEditExtender ID="GatewayTextBox2_MaskedEditExtender" runat="server" 
                    ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="999,999,999.999" 
                    MaskType="Number" MessageValidatorTip="true" TargetControlID="GatewayTextBox2">
                </asp:MaskedEditExtender>
                <asp:TextBoxWatermarkExtender ID="GatewayTextBox0_TextBoxWatermarkExtender" 
                    runat="server" TargetControlID="GatewayTextBox2" 
                    WatermarkText="آدرس آی پی را اینجا وارد کنید">
                </asp:TextBoxWatermarkExtender>
                <asp:Button ID="SendBtn0" runat="server" onclick="SendBtn0_Click" Text="ارسال" 
                    ValidationGroup="1" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
            <asp:ListBox ID="ListBox2" runat="server" Visible="false" AutoPostBack="True" CssClass="aspcontrols" ForeColor="#FF3300" Width="99.5%"></asp:ListBox>
</fieldset>
 </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
</ContentTemplate>
<Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="Button5" />
            <asp:PostBackTrigger ControlID="SendBtn" />
            <asp:PostBackTrigger ControlID="SendBtn0" />
</Triggers>
    </asp:UpdatePanel>
</asp:Content>
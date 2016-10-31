<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendBulkSMS.aspx.cs" Inherits="Administration_ShortMessageService_getProvinces" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<%@ Register Src="~/EasyControls/TopMenu.ascx" TagPrefix="TopMenu" TagName="TopMenu" %>
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

<TopMenu:TopMenu ID="TopMenu1" runat="server" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <fieldset>
    <legend>پیامک های انبوه</legend>
    <asp:RadioButton ID="RadioButton4" runat="server"  GroupName="BulkSelection" 
        Text="افزودن پیامک انبوه" AutoPostBack="True" 
        oncheckedchanged="radioBtn1" Checked="true" Font-Bold="true"/><br />
    <asp:RadioButton ID="RadioButton5" runat="server"  GroupName="BulkSelection" 
        Text="صندوق خروجی پیامک های انبوه" 
        oncheckedchanged="radioBtn2" AutoPostBack="True" Font-Bold="true"/>
    <br />    <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:Panel ID="Panel2" runat="server" CssClass="Panel">   
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:stringsRes, sendSMS_MessageBoxNotices%>" ></asp:Literal>
    </asp:Panel>
    <asp:BalloonPopupExtender ID="BalloonPopupExtender2" runat="server" TargetControlID="Image3" BalloonStyle="Rectangle" BalloonPopupControlID="panel2" BalloonSize="Large">
    </asp:BalloonPopupExtender>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="MessageTextBox" WatermarkText="متن پیامک خود را اینجا تایپ کنید ..." WatermarkCssClass="watermark">
    </asp:TextBoxWatermarkExtender>
<fieldset>
<legend>افزودن پیامک انبوه</legend>
    <table style="width: 100%;">
                <tr>
            <td style="width: 133px">
                <asp:Label ID="Label3" runat="server" Text="متن پیام :"></asp:Label><br />
                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/help.png" />
            </td>
            <td style="width: 329px">
                <asp:TextBox ID="MessageTextBox" runat="server" TextMode="MultiLine" 
                    ValidationGroup="1" ontextchanged="MessageTextBox_TextGhanged" AutoPostBack="True"></asp:TextBox>
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
                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="aspcontrols" 
                    style="direction: rtl" AutoPostBack="True">
                    <asp:ListItem Value="1">پیامک عادی</asp:ListItem>
                    <asp:ListItem Value="0">پیامک خبری</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
            <tr>
                <td style="width: 133px">
                    <asp:Label ID="Label1" runat="server" Text="زمان ارسال :"></asp:Label><asp:Image
                        ID="Image4" runat="server" ImageUrl="~/Images/help.png"/>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:stringsRes, sendBulkSMS_SendTime%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender3" runat="server" BalloonPopupControlID="Panel3" BalloonSize="Medium" TargetControlID="Image4">
                </asp:BalloonPopupExtender>
                </td>
                <td style="width: 339px">
                <Heidarpour:DatePicker ID="DatePicker1" runat="server" DatePersian="1392/07/11" CalendarType="Persian"
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" ShowWeekNumbers="True" AutoPostBack="true"
        ReadOnly="True" FirstDayOfWeek="Saturday" ShowOthers="True" Height="25px" style="text-align:center;"
        onclose="onClose" OnUpdate="onUpdate" LabelText="انتخاب تاریخ :" ></Heidarpour:DatePicker>
        <br />
        <td style="width:130px; vertical-align:middle">        <asp:Label ID="Label2" runat="server" Text="ساعت و دقیقه :"></asp:Label>
                    <MKB:TimeSelector ID="TimeSelector1" runat="server" DisplaySeconds="false" 
                        SelectedTimeFormat="TwentyFour" CssClass="TimeSelector"></MKB:TimeSelector><br /> ابتدا سمت راست ساعت و سمت چپ دقیقه را انتخاب کنید .
</td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
            <td></td>
            <td>
                <asp:Button ID="createBulk" runat="server" Text="ذخیره شود" 
                    onclick="createBulk_Click"  ValidationGroup="1"/>
                    <asp:Image ID="Image2"
                    runat="server" ImageUrl="~/Images/help.png" />
                <asp:Panel ID="Panel5" runat="server">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:stringsRes, sendBulkSMS_Save%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender5" runat="server" BalloonPopupControlID="Panel5" BalloonSize="Large" TargetControlID="Image2">
                </asp:BalloonPopupExtender>
            </td>
            <td></td>
            </tr>
    </table>
</fieldset>
</ContentTemplate>
    </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
     <fieldset>
    <legend>صندوق خروجی پیامکهای انبوه</legend>
         <asp:GridView ID="DataGrid2" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="DataGrid2_SelectedIndexChanged">
             <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
             <Columns>
                 <asp:CommandField ShowSelectButton="True" ButtonType="Button" CancelText="لغو" DeleteText="حذف" EditText="ویرایش" InsertText="درج" NewText="جدید" SelectText="انتخاب" UpdateText="بروزرسانی"></asp:CommandField>
                 <asp:BoundField DataField="ID" HeaderText="کد" ReadOnly="True" SortExpression="ID"></asp:BoundField>
                 <asp:BoundField DataField="BulkID" HeaderText="شناسه پیامک" SortExpression="BulkID"></asp:BoundField>
                 <asp:BoundField DataField="Message" HeaderText="متن پیام" SortExpression="Message"></asp:BoundField>
                 <asp:BoundField DataField="SenderNumber" HeaderText="شماره فرستنده" SortExpression="SenderNumber"></asp:BoundField>
                 <asp:BoundField DataField="SMSType" HeaderText="نوع پیامک" SortExpression="SMSType"></asp:BoundField>
                 <asp:BoundField DataField="BulkDate" HeaderText="تاریخ ارسال" SortExpression="BulkDate"></asp:BoundField>
                 <asp:BoundField DataField="BulkHour" HeaderText="ساعت ارسال" SortExpression="BulkHour"></asp:BoundField>
                 <asp:BoundField DataField="BulkMinute" HeaderText="دقیقه ارسال" SortExpression="BulkMinute"></asp:BoundField>
                 <asp:BoundField DataField="BulkStatus" HeaderText="وضعیت پیامک انبوه" SortExpression="BulkStatus"></asp:BoundField>
                 <asp:BoundField DataField="Comments" HeaderText="توضیحات" SortExpression="Comments"></asp:BoundField>
                 <asp:BoundField DataField="SysDateTime" HeaderText="زمان ثبت در سیستم" SortExpression="SysDateTime"></asp:BoundField>
             </Columns>

             <EditRowStyle BackColor="#2461BF"></EditRowStyle>
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>
             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
             <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>
             <RowStyle BackColor="#EFF3FB"></RowStyle>
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
             <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
             <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>
             <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
             <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
         </asp:GridView>
         <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConflictDetection="CompareAllValues" ConnectionString='<%$ ConnectionStrings:ArvidSMSConnectionString %>' DeleteCommand="DELETE FROM [SendBulkTbl] WHERE [ID] = @original_ID AND [BulkID] = @original_BulkID AND [Message] = @original_Message AND [SenderNumber] = @original_SenderNumber AND [SMSType] = @original_SMSType AND [BulkDate] = @original_BulkDate AND [BulkHour] = @original_BulkHour AND [BulkMinute] = @original_BulkMinute AND (([BulkStatus] = @original_BulkStatus) OR ([BulkStatus] IS NULL AND @original_BulkStatus IS NULL)) AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL)) AND [SysDateTime] = @original_SysDateTime" InsertCommand="INSERT INTO [SendBulkTbl] ([ID], [BulkID], [Message], [SenderNumber], [SMSType], [BulkDate], [BulkHour], [BulkMinute], [BulkStatus], [Comments], [SysDateTime]) VALUES (@ID, @BulkID, @Message, @SenderNumber, @SMSType, @BulkDate, @BulkHour, @BulkMinute, @BulkStatus, @Comments, @SysDateTime)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [SendBulkTbl]" UpdateCommand="UPDATE [SendBulkTbl] SET [BulkID] = @BulkID, [Message] = @Message, [SenderNumber] = @SenderNumber, [SMSType] = @SMSType, [BulkDate] = @BulkDate, [BulkHour] = @BulkHour, [BulkMinute] = @BulkMinute, [BulkStatus] = @BulkStatus, [Comments] = @Comments, [SysDateTime] = @SysDateTime WHERE [ID] = @original_ID AND [BulkID] = @original_BulkID AND [Message] = @original_Message AND [SenderNumber] = @original_SenderNumber AND [SMSType] = @original_SMSType AND [BulkDate] = @original_BulkDate AND [BulkHour] = @original_BulkHour AND [BulkMinute] = @original_BulkMinute AND (([BulkStatus] = @original_BulkStatus) OR ([BulkStatus] IS NULL AND @original_BulkStatus IS NULL)) AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL)) AND [SysDateTime] = @original_SysDateTime">
             <DeleteParameters>
                 <asp:Parameter Name="original_ID" Type="Int32"></asp:Parameter>
                 <asp:Parameter Name="original_BulkID" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_Message" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_SenderNumber" Type="Int64"></asp:Parameter>
                 <asp:Parameter Name="original_SMSType" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_BulkDate" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_BulkHour" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_BulkMinute" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_BulkStatus" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_Comments" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_SysDateTime" Type="String"></asp:Parameter>
             </DeleteParameters>
             <InsertParameters>
                 <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>
                 <asp:Parameter Name="BulkID" Type="String"></asp:Parameter>
                 <asp:Parameter Name="Message" Type="String"></asp:Parameter>
                 <asp:Parameter Name="SenderNumber" Type="Int64"></asp:Parameter>
                 <asp:Parameter Name="SMSType" Type="String"></asp:Parameter>
                 <asp:Parameter Name="BulkDate" Type="String"></asp:Parameter>
                 <asp:Parameter Name="BulkHour" Type="String"></asp:Parameter>
                 <asp:Parameter Name="BulkMinute" Type="String"></asp:Parameter>
                 <asp:Parameter Name="BulkStatus" Type="String"></asp:Parameter>
                 <asp:Parameter Name="Comments" Type="String"></asp:Parameter>
                 <asp:Parameter Name="SysDateTime" Type="String"></asp:Parameter>
             </InsertParameters>
             <UpdateParameters>
                 <asp:Parameter Name="BulkID" Type="String"></asp:Parameter>
                 <asp:Parameter Name="Message" Type="String"></asp:Parameter>
                 <asp:Parameter Name="SenderNumber" Type="Int64"></asp:Parameter>
                 <asp:Parameter Name="SMSType" Type="String"></asp:Parameter>
                 <asp:Parameter Name="BulkDate" Type="String"></asp:Parameter>
                 <asp:Parameter Name="BulkHour" Type="String"></asp:Parameter>
                 <asp:Parameter Name="BulkMinute" Type="String"></asp:Parameter>
                 <asp:Parameter Name="BulkStatus" Type="String"></asp:Parameter>
                 <asp:Parameter Name="Comments" Type="String"></asp:Parameter>
                 <asp:Parameter Name="SysDateTime" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_ID" Type="Int32"></asp:Parameter>
                 <asp:Parameter Name="original_BulkID" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_Message" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_SenderNumber" Type="Int64"></asp:Parameter>
                 <asp:Parameter Name="original_SMSType" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_BulkDate" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_BulkHour" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_BulkMinute" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_BulkStatus" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_Comments" Type="String"></asp:Parameter>
                 <asp:Parameter Name="original_SysDateTime" Type="String"></asp:Parameter>
             </UpdateParameters>
         </asp:SqlDataSource>
    </fieldset>
    <br />
     <fieldset>   
        <asp:Literal ID="Literal8" runat="server" Text="ابتدا پیامک مورد نظر را از فهرست بالا انتخاب نمائید."></asp:Literal><br />
     <asp:Button ID="DataRefresh" runat="server" Text="بازیابی داده ها" onclick="DataRefresh_Click" />&nbsp;
     <asp:Button ID="BulkOpen" runat="server" Text="باز کردن" onclick="BulkOpen_Click" />&nbsp;
     <asp:Button ID="DataDelete" runat="server" Text="حذف" onclick="DataDelete_Click" />&nbsp;
     <asp:Button ID="StatusCheck" runat="server" Text="بررسی وضعیت" onclick="StatusCheck_Click" />&nbsp;
     <asp:Button ID="Button1" runat="server" Text="محاسبه هزینه" onclick="Button1_Click" />&nbsp;
     <asp:Button ID="RecipientsAddBtn" runat="server" Text="گیرندگان"  onclick="RecipientsAddBtn_Click"/>&nbsp;
     <asp:Button ID="SendBtn" runat="server"  Text="ارسال" ValidationGroup="2" onclick="SendBtn_Click" />
     <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/help.png" />
                <asp:Panel ID="Panel7" runat="server">
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:stringsRes, sendBulknotice%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender7" runat="server" BalloonPopupControlID="Panel7" BalloonSize="Small" TargetControlID="Image6">
                </asp:BalloonPopupExtender>
    </fieldset>
    <br />
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <fieldset>
    <legend>گیرندگان</legend>
            <fieldset>
    <legend>فهرست گیرندگان</legend>
                <asp:GridView ID="DataGrid1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" CancelText="لغو" DeleteText="حذف" EditText="ویرایش" InsertText="درج" NewText="جدید" SelectText="انتخاب" UpdateText="بروزرسانی"></asp:CommandField>
                        <asp:BoundField DataField="ID" HeaderText="کد" ReadOnly="True" SortExpression="ID"></asp:BoundField>
                        <asp:BoundField DataField="SendBy" HeaderText="ارسال بر اساس" SortExpression="SendBy"></asp:BoundField>
                        <asp:BoundField DataField="BulkID" HeaderText="شناسه پیامک انبوه" SortExpression="BulkID"></asp:BoundField>
                        <asp:BoundField DataField="Code" HeaderText="نوع ارسال" SortExpression="Code"></asp:BoundField>
                        <asp:BoundField DataField="StartIndex" HeaderText="شماره شروع از فهرست" SortExpression="StartIndex"></asp:BoundField>
                        <asp:BoundField DataField="Count" HeaderText="تعداد" SortExpression="Count"></asp:BoundField>
                        <asp:BoundField DataField="SIMType" HeaderText="نوع سیم کارت" SortExpression="SIMType"></asp:BoundField>
                        <asp:BoundField DataField="Comments" HeaderText="توضیحات" SortExpression="Comments"></asp:BoundField>
                        <asp:BoundField DataField="SysDateTime" HeaderText="زمان ثبت در سیستم" SortExpression="SysDateTime"></asp:BoundField>
                    </Columns>

                    <EditRowStyle BackColor="#2461BF"></EditRowStyle>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>
                    <RowStyle BackColor="#EFF3FB"></RowStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                </asp:GridView>
                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConflictDetection="CompareAllValues" ConnectionString='<%$ ConnectionStrings:ArvidSMSConnectionString %>' DeleteCommand="DELETE FROM [RecipientsTbl] WHERE [ID] = @original_ID AND [SendBy] = @original_SendBy AND [BulkID] = @original_BulkID AND [Code] = @original_Code AND [StartIndex] = @original_StartIndex AND [Count] = @original_Count AND [SIMType] = @original_SIMType AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL)) AND [SysDateTime] = @original_SysDateTime" InsertCommand="INSERT INTO [RecipientsTbl] ([ID], [SendBy], [BulkID], [Code], [StartIndex], [Count], [SIMType], [Comments], [SysDateTime]) VALUES (@ID, @SendBy, @BulkID, @Code, @StartIndex, @Count, @SIMType, @Comments, @SysDateTime)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [RecipientsTbl]" UpdateCommand="UPDATE [RecipientsTbl] SET [SendBy] = @SendBy, [BulkID] = @BulkID, [Code] = @Code, [StartIndex] = @StartIndex, [Count] = @Count, [SIMType] = @SIMType, [Comments] = @Comments, [SysDateTime] = @SysDateTime WHERE [ID] = @original_ID AND [SendBy] = @original_SendBy AND [BulkID] = @original_BulkID AND [Code] = @original_Code AND [StartIndex] = @original_StartIndex AND [Count] = @original_Count AND [SIMType] = @original_SIMType AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL)) AND [SysDateTime] = @original_SysDateTime">
                    <DeleteParameters>
                        <asp:Parameter Name="original_ID" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_SendBy" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_BulkID" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Code" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_StartIndex" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Count" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_SIMType" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Comments" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_SysDateTime" Type="String"></asp:Parameter>
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ID" Type="String"></asp:Parameter>
                        <asp:Parameter Name="SendBy" Type="String"></asp:Parameter>
                        <asp:Parameter Name="BulkID" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Code" Type="String"></asp:Parameter>
                        <asp:Parameter Name="StartIndex" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Count" Type="String"></asp:Parameter>
                        <asp:Parameter Name="SIMType" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Comments" Type="String"></asp:Parameter>
                        <asp:Parameter Name="SysDateTime" Type="String"></asp:Parameter>
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SendBy" Type="String"></asp:Parameter>
                        <asp:Parameter Name="BulkID" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Code" Type="String"></asp:Parameter>
                        <asp:Parameter Name="StartIndex" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Count" Type="String"></asp:Parameter>
                        <asp:Parameter Name="SIMType" Type="String"></asp:Parameter>
                        <asp:Parameter Name="Comments" Type="String"></asp:Parameter>
                        <asp:Parameter Name="SysDateTime" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_ID" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_SendBy" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_BulkID" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Code" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_StartIndex" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Count" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_SIMType" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_Comments" Type="String"></asp:Parameter>
                        <asp:Parameter Name="original_SysDateTime" Type="String"></asp:Parameter>
                    </UpdateParameters>
                </asp:SqlDataSource>
                <br />
        <asp:Literal ID="Literal9" runat="server" Text="ابتدا گیرنده مورد نظر را از فهرست بالا انتخاب نمائید."></asp:Literal><br />
                <asp:Button ID="RecipientsOpBtn1" runat="server" Text="بازیابی داده ها" OnClick="RecipientsOpBtn1_Click" />&nbsp;
                <asp:Button ID="RecipientsOpBtn2" runat="server" Text="حذف"  OnClick="RecipientsOpBtn2_Click" />&nbsp;
    </fieldset>
    <br />
            <fieldset>
    <legend>افزودن گیرندگان</legend>
        <table style="width: 100%;">
            <tr>
                <td style="width: 133px">
                    <asp:Label ID="Label13" runat="server" Text="ارسال بر اساس :"></asp:Label>
                    <asp:Image
                        ID="Image1" runat="server" ImageUrl="~/Images/help.png" />
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:stringsRes, sendBulkSMS_DeliveryOption%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender1" runat="server" BalloonPopupControlID="Panel1" BalloonSize="Large" TargetControlID="Image1">
                </asp:BalloonPopupExtender>
                </td>
                <td style="width: 401px">
                    &nbsp;
                    <asp:RadioButton ID="RadioButton1" runat="server" Text="استان و شهر" 
                        AutoPostBack="true" GroupName="1" 
                        oncheckedchanged="RadioBtn1_CheckedChanged"/>
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="کد پستی" 
                        AutoPostBack="true" GroupName="1" oncheckedchanged="RadioBtn2_CheckedChanged"/>
                    <asp:RadioButton ID="RadioButton3" runat="server" Text="پیش شماره" 
                        AutoPostBack="true" GroupName="1" oncheckedchanged="RadioBtn3_CheckedChanged"/>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 133px">
                    <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 401px">
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="aspcontrols" Visible="false" 
                        AutoPostBack="true" onselectedindexchanged="Drp1_SelectedIndexChanged">
    </asp:DropDownList><asp:DropDownList ID="DropDownList3" runat="server" CssClass="aspcontrols" 
                        Visible="false" AutoPostBack="true" 
                        onselectedindexchanged="Drp3_SelectedIndexChanged">
    </asp:DropDownList><asp:TextBox ID="fourNumbersTxbx" runat="server" Visible="false" 
                        AutoPostBack="true" Width="80px" style="direction: ltr" 
                        ontextchanged="fourNumbersTxbx_TextChanged"></asp:TextBox><asp:FilteredTextBoxExtender
                            ID="FilteredTextBoxExtender1" runat="server" TargetControlID="fourNumbersTxbx" FilterType="Numbers">
                        </asp:FilteredTextBoxExtender>
                </td>
                <td>
                    <asp:Label ID="CityProvinceCountLabel" runat="server" Text=""></asp:Label>&nbsp;<asp:Label ID="CountLabel"
                        runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 133px">
                    <asp:Label ID="Label15" runat="server" Text="از :"></asp:Label>
                                        <asp:Image
                        ID="Image7" runat="server" ImageUrl="~/Images/help.png" />
                <asp:Panel ID="Panel6" runat="server">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:stringsRes, sendBulkSMS_IndexOption%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender6" runat="server" BalloonPopupControlID="Panel6" BalloonSize="Large" TargetControlID="Image7">
                </asp:BalloonPopupExtender>

                </td>
                <td style="width: 401px">
                    <asp:TextBox ID="TextBox2" runat="server" Width="80px" style="direction: ltr"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 133px">
                    <asp:Label ID="Label16" runat="server" Text="به تعداد :"></asp:Label>
                </td>
                <td style="width: 401px">
                    <asp:TextBox ID="TextBox3" runat="server" Width="80px" style="direction: ltr"></asp:TextBox>
                    </td>
                <td>
                   </td>
            </tr>
            <tr>
                <td style="width: 133px">
                    <asp:Label ID="Label17" runat="server" Text="نوع شماره گیرندگان :"></asp:Label>
                </td>
                <td style="width: 401px">
                    <asp:DropDownList ID="DropDownList6" runat="server" CssClass="aspcontrols" 
                        AutoPostBack="True" onselectedindexchanged="Drp6_SelectedIndexChanged">
                        <asp:ListItem Value="0">دائمی و اعتباری</asp:ListItem>
                        <asp:ListItem Value="2">خطوط دائمی</asp:ListItem>
                        <asp:ListItem Value="1">خطوط اعتباری</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                   </td>
            </tr>
            <tr>
                <td style="width: 133px">
                    &nbsp;</td>
                <td style="width: 401px">
                    <asp:Button ID="recipientAddBtn" runat="server" Text="افزودن به فهرست" 
                        onclick="recipientAddBtn_Click"/>
                </td>
                <td>
                    </td>
            </tr>
        </table>
    </fieldset>
    </fieldset>
    </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    </ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="DropDownList1" />
<asp:AsyncPostBackTrigger ControlID="RadioButton1" />
<asp:PostBackTrigger ControlID="createBulk" />
<asp:PostBackTrigger ControlID="recipientAddBtn" />
<asp:PostBackTrigger ControlID="SendBtn" />
<asp:PostBackTrigger ControlID="Button1" />
<asp:PostBackTrigger ControlID="DataRefresh" />
<asp:PostBackTrigger ControlID="DataDelete" />
<asp:PostBackTrigger ControlID="StatusCheck" />
<asp:PostBackTrigger ControlID="BulkOpen" />
<asp:PostBackTrigger ControlID="RecipientsOpBtn1" />
<asp:PostBackTrigger ControlID="RecipientsOpBtn2" />
</Triggers>
    </asp:UpdatePanel>
</asp:Content>
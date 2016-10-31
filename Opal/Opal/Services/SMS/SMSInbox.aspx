<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSInbox.aspx.cs" Inherits="Administration_ShortMessageService_Inbox" MasterPageFile="~/Site.master" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="~/EasyControls/TopMenu.ascx" TagPrefix="TopMenu" TagName="TopMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">

<TopMenu:TopMenu ID="TopMenu1" runat="server" />
<fieldset>
<legend>صندوق ورودی پیامک های متنی</legend>
    <asp:GridView ID="DataGrid1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" AllowSorting="True" BackImageUrl="~/Images/Logo New - Copy.png" AutoGenerateColumns="False" DataKeyNames="ID"  OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ButtonType="Button" CancelText="لغو" DeleteText="حذف" EditText="ویرایش" InsertText="درج" NewText="جدید" SelectText="انتخاب" ShowSelectButton="True" UpdateText="بروزرسانی" />
            <asp:BoundField DataField="ID" HeaderText="شناسه" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="messageBody" HeaderText="متن پیام" SortExpression="messageBody" />
            <asp:BoundField DataField="errorResult" HeaderText="پیام خطا" SortExpression="errorResult" />
            <asp:BoundField DataField="recipientNumber" HeaderText="شماره گیرنده" SortExpression="recipientNumber" />
            <asp:BoundField DataField="msgsenderNumber" HeaderText="شماره فرستنده" SortExpression="msgsenderNumber" />
            <asp:BoundField DataField="date" HeaderText="تاریخ دریافت" SortExpression="date" />
            <asp:BoundField DataField="sysdate" HeaderText="زمان ثبت در سیستم" SortExpression="sysdate" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArvidSMSConnectionString %>" DeleteCommand="DELETE FROM [IncomingTbl] WHERE [ID] = @original_ID AND [messageBody] = @original_messageBody AND (([errorResult] = @original_errorResult) OR ([errorResult] IS NULL AND @original_errorResult IS NULL)) AND [recipientNumber] = @original_recipientNumber AND [msgsenderNumber] = @original_msgsenderNumber AND [date] = @original_date AND [sysdate] = @original_sysdate" InsertCommand="INSERT INTO [IncomingTbl] ([ID], [messageBody], [errorResult], [recipientNumber], [msgsenderNumber], [date], [sysdate]) VALUES (@ID, @messageBody, @errorResult, @recipientNumber, @msgsenderNumber, @date, @sysdate)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [IncomingTbl]" UpdateCommand="UPDATE [IncomingTbl] SET [messageBody] = @messageBody, [errorResult] = @errorResult, [recipientNumber] = @recipientNumber, [msgsenderNumber] = @msgsenderNumber, [date] = @date, [sysdate] = @sysdate WHERE [ID] = @original_ID AND [messageBody] = @original_messageBody AND (([errorResult] = @original_errorResult) OR ([errorResult] IS NULL AND @original_errorResult IS NULL)) AND [recipientNumber] = @original_recipientNumber AND [msgsenderNumber] = @original_msgsenderNumber AND [date] = @original_date AND [sysdate] = @original_sysdate">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_messageBody" Type="String" />
            <asp:Parameter Name="original_errorResult" Type="String" />
            <asp:Parameter Name="original_recipientNumber" Type="String" />
            <asp:Parameter Name="original_msgsenderNumber" Type="String" />
            <asp:Parameter Name="original_date" Type="String" />
            <asp:Parameter Name="original_sysdate" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="messageBody" Type="String" />
            <asp:Parameter Name="errorResult" Type="String" />
            <asp:Parameter Name="recipientNumber" Type="String" />
            <asp:Parameter Name="msgsenderNumber" Type="String" />
            <asp:Parameter Name="date" Type="String" />
            <asp:Parameter Name="sysdate" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="messageBody" Type="String" />
            <asp:Parameter Name="errorResult" Type="String" />
            <asp:Parameter Name="recipientNumber" Type="String" />
            <asp:Parameter Name="msgsenderNumber" Type="String" />
            <asp:Parameter Name="date" Type="String" />
            <asp:Parameter Name="sysdate" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_messageBody" Type="String" />
            <asp:Parameter Name="original_errorResult" Type="String" />
            <asp:Parameter Name="original_recipientNumber" Type="String" />
            <asp:Parameter Name="original_msgsenderNumber" Type="String" />
            <asp:Parameter Name="original_date" Type="String" />
            <asp:Parameter Name="original_sysdate" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
      <asp:Literal ID="Literal8" runat="server" Text="ابتدا پیامک مورد نظر را از فهرست بالا انتخاب نمائید."></asp:Literal><br />
     <asp:Button ID="DataRefresh" runat="server" Text="بازیابی داده ها" onclick="DataRefresh_Click" />&nbsp;
     <asp:Button ID="BulkOpen" runat="server" Text="باز کردن" onclick="MsgOpen_Click" />&nbsp;
     <asp:Button ID="DataDelete" runat="server" Text="حذف" onclick="DataDelete_Click" />&nbsp;
     <asp:Button ID="ExcelExport" runat="server" Text="خروجی به اکسل" onclick="ExcelExport_Click" />&nbsp;
     <asp:Button ID="SendToFolder" runat="server" Text="ارسال به پوشه" onclick="SendToFolder_Click" />&nbsp;
</fieldset>

</asp:Content>

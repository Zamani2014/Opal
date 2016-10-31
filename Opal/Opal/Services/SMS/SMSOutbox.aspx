<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSOutbox.aspx.cs" Inherits="Administration_ShortMessageService_Outbox" MasterPageFile="~/Site.master" %>
<%@ Register Src="~/EasyControls/TopMenu.ascx" TagPrefix="TopMenu" TagName="TopMenu" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<TopMenu:TopMenu ID="TopMenu1" runat="server" />
<fieldset>
<legend>صندوق خروجی پیامک های متنی</legend>
    <asp:GridView ID="DataGrid1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button" CancelText="لغو" DeleteText="حذف" EditText="ویرایش" InsertText="درج" NewText="جدید" SelectText="انتخاب" UpdateText="بروز رسانی"></asp:CommandField>
            <asp:BoundField DataField="ID" HeaderText="شناسه" ReadOnly="True" SortExpression="ID"></asp:BoundField>
            <asp:BoundField DataField="messageBody" HeaderText="متن پیام" SortExpression="messageBody"></asp:BoundField>
            <asp:BoundField DataField="recipientNumber" HeaderText="شماره گیرنده" SortExpression="recipientNumber"></asp:BoundField>
            <asp:BoundField DataField="msgsenderNumber" HeaderText="شماره فرستنده" SortExpression="msgsenderNumber"></asp:BoundField>
            <asp:BoundField DataField="DateTime" HeaderText="تاریخ و زمان" SortExpression="DateTime"></asp:BoundField>
            <asp:BoundField DataField="messageStatus" HeaderText="وضعیت پیام" SortExpression="messageStatus"></asp:BoundField>
            <asp:BoundField DataField="Comments" HeaderText="توضیحات" SortExpression="Comments"></asp:BoundField>
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
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConflictDetection="CompareAllValues" ConnectionString='<%$ ConnectionStrings:ArvidSMSConnectionString %>' DeleteCommand="DELETE FROM [OutBoxTbl] WHERE [ID] = @original_ID AND [messageBody] = @original_messageBody AND [recipientNumber] = @original_recipientNumber AND [msgsenderNumber] = @original_msgsenderNumber AND [DateTime] = @original_DateTime AND [messageStatus] = @original_messageStatus AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL))" InsertCommand="INSERT INTO [OutBoxTbl] ([ID], [messageBody], [recipientNumber], [msgsenderNumber], [DateTime], [messageStatus], [Comments]) VALUES (@ID, @messageBody, @recipientNumber, @msgsenderNumber, @DateTime, @messageStatus, @Comments)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [OutBoxTbl]" UpdateCommand="UPDATE [OutBoxTbl] SET [messageBody] = @messageBody, [recipientNumber] = @recipientNumber, [msgsenderNumber] = @msgsenderNumber, [DateTime] = @DateTime, [messageStatus] = @messageStatus, [Comments] = @Comments WHERE [ID] = @original_ID AND [messageBody] = @original_messageBody AND [recipientNumber] = @original_recipientNumber AND [msgsenderNumber] = @original_msgsenderNumber AND [DateTime] = @original_DateTime AND [messageStatus] = @original_messageStatus AND (([Comments] = @original_Comments) OR ([Comments] IS NULL AND @original_Comments IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int64"></asp:Parameter>
            <asp:Parameter Name="original_messageBody" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_recipientNumber" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_msgsenderNumber" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_DateTime" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_messageStatus" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_Comments" Type="String"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int64"></asp:Parameter>
            <asp:Parameter Name="messageBody" Type="String"></asp:Parameter>
            <asp:Parameter Name="recipientNumber" Type="String"></asp:Parameter>
            <asp:Parameter Name="msgsenderNumber" Type="String"></asp:Parameter>
            <asp:Parameter Name="DateTime" Type="String"></asp:Parameter>
            <asp:Parameter Name="messageStatus" Type="String"></asp:Parameter>
            <asp:Parameter Name="Comments" Type="String"></asp:Parameter>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="messageBody" Type="String"></asp:Parameter>
            <asp:Parameter Name="recipientNumber" Type="String"></asp:Parameter>
            <asp:Parameter Name="msgsenderNumber" Type="String"></asp:Parameter>
            <asp:Parameter Name="DateTime" Type="String"></asp:Parameter>
            <asp:Parameter Name="messageStatus" Type="String"></asp:Parameter>
            <asp:Parameter Name="Comments" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_ID" Type="Int64"></asp:Parameter>
            <asp:Parameter Name="original_messageBody" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_recipientNumber" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_msgsenderNumber" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_DateTime" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_messageStatus" Type="String"></asp:Parameter>
            <asp:Parameter Name="original_Comments" Type="String"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
      <asp:Literal ID="Literal8" runat="server" Text="ابتدا پیامک مورد نظر را از فهرست بالا انتخاب نمائید."></asp:Literal><br />
     <asp:Button ID="DataRefresh" runat="server" Text="بازیابی داده ها" onclick="DataRefresh_Click" />&nbsp;
     <asp:Button ID="BulkOpen" runat="server" Text="باز کردن" onclick="MsgOpen_Click" />&nbsp;
     <asp:Button ID="DataDelete" runat="server" Text="حذف" onclick="DataDelete_Click" />&nbsp;
     <asp:Button ID="ExcelExport" runat="server" Text="خروجی به اکسل" onclick="ExcelExport_Click" />&nbsp;
     <asp:Button ID="StatusCheck" runat="server" Text="بررسی وضعیت" onclick="StatusCheck_Click" />&nbsp;

</fieldset>
</asp:Content>


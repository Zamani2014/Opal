<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LatestNews.aspx.cs" Inherits="Administration_BulletManagement_BookNews" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">

<fieldset dir="rtl">
<legend style="direction: rtl"><strong>فهرست آخرین خبرهای سایت</strong></legend>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        DataKeyNames="ID" DataSourceID="SqlDataSource1" GridLines="Vertical" 
        style="text-align: center">
        <AlternatingRowStyle BackColor="Gainsboro" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="شناسه خبر" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="Subject" HeaderText="موضوع خبر" 
                SortExpression="Subject" />
            <asp:BoundField DataField="News" HeaderText="متن خبر" 
                SortExpression="News" />
            <asp:BoundField DataField="DateTime" HeaderText="زمان ثبت در سیستم" 
                SortExpression="DateTime" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ArvidSMSConnectionString %>" 
        SelectCommand="SELECT * FROM [LatestNews]" 
        ConflictDetection="CompareAllValues" 
        DeleteCommand="DELETE FROM [LatestNews] WHERE [ID] = @original_ID AND [Subject] = @original_Subject AND [News] = @original_News AND [DateTime] = @original_DateTime" 
        InsertCommand="INSERT INTO [LatestNews] ([ID], [Subject], [News], [DateTime]) VALUES (@ID, @Subject, @News, @DateTime)" 
        OldValuesParameterFormatString="original_{0}" 
        
        
        
        UpdateCommand="UPDATE [LatestNews] SET [Subject] = @Subject, [News] = @News, [DateTime] = @DateTime WHERE [ID] = @original_ID AND [Subject] = @original_Subject AND [News] = @original_News AND [DateTime] = @original_DateTime">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Subject" Type="String" />
            <asp:Parameter Name="original_News" Type="String" />
            <asp:Parameter Name="original_DateTime" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Subject" Type="String" />
            <asp:Parameter Name="News" Type="String" />
            <asp:Parameter Name="DateTime" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Subject" Type="String" />
            <asp:Parameter Name="News" Type="String" />
            <asp:Parameter Name="DateTime" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Subject" Type="String" />
            <asp:Parameter Name="original_News" Type="String" />
            <asp:Parameter Name="original_DateTime" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</fieldset>
<br />
<fieldset dir="rtl">
<legend style="direction: rtl"><strong>تراکنش ها</strong></legend>

    <fieldset dir="rtl">
    <legend style="direction: rtl"><strong>درج</strong></legend>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="شماره شناسائی :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" style="direction: ltr"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="وارد کردن شماره شناسائی ضروری است ." ControlToValidate="TextBox3" ValidationGroup="1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="عنوان خبر :"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="وارد کردن عنوان خبر ضروری است ." ControlToValidate="TextBox1" ValidationGroup="1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="متن خبر :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="وارد کردن نشانی وب ضروری است ." ControlToValidate="TextBox2" ValidationGroup="1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" style="text-align: center" 
                        Text="درج کن عزیزم" ValidationGroup="1" onclick="Button1_Click"/>
                </td>
            </tr>
        </table>
    </fieldset>

    <fieldset dir="rtl">
    <legend style="direction: rtl"><strong>حذف</strong></legend>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="شماره شناسائی :"></asp:Label>
                </td>
                <td>
                    &nbsp;<asp:TextBox ID="TextBox4" runat="server" style="direction: ltr"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="وارد کردن شماره شناسائی ضروری است ." ControlToValidate="TextBox4" ValidationGroup="2"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="حذف کن عزیزم" ValidationGroup="2" 
                        onclick="Button2_Click"/>
                </td>
            </tr>
            </table>
    </fieldset>
</fieldset>

</asp:Content>

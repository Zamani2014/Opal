<%@ Page Language="C#" AutoEventWireup="true" CodeFile="success.aspx.cs" Inherits="success" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
        <fieldset>
<legend>پرداخت با موفقیت انجام شد</legend>
    <div style="text-align:center">
        <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/Shetab.jpg"/><br />
        <h1>
    <asp:Label ID="PaymentStatus" runat="server" Text="پرداخت الکترونیکی با موفقیت انجام شد ." style="font-weight: 700; font-size: large"></asp:Label></h1>
        <br />
        <asp:Label ID="Label3" runat="server" Text="کد پیگیری : " style="font-weight: 700"></asp:Label><asp:Label ID="Label1" runat="server" Text="" style="font-weight: 700"></asp:Label>
        <br /><asp:Button ID="Button1" runat="server" Text="بازگشت به صفحه اصلی" OnClick="Button1_Click" />
    </div>
</fieldset>

</asp:Content>
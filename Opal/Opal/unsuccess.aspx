<%@ Page Language="C#" AutoEventWireup="true" CodeFile="unsuccess.aspx.cs" Inherits="unsuccess"  MasterPageFile="~/Site.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <fieldset>
<legend>خطا در پرداخت الکترونیکی</legend>
    <div style="text-align:center">
        <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/Shetab.jpg"/><br />
        <h1>
    <asp:Label ID="PaymentStatus" runat="server" Text="متاسفانه در پرداخت الکترونیکی با کارتهای عضو شتاب مشکلی پیش آمده است ." style="font-weight: 700; font-size: large"></asp:Label></h1>
        <br />
            <asp:Label ID="Label2" runat="server" Text="شرح مشکل : " style="font-weight: 700"></asp:Label><asp:Label ID="ErrorLabel" runat="server" Text="" style="font-weight: 700"></asp:Label>
        <br /><asp:Button ID="Button1" runat="server" Text="بازگشت به صفحه اصلی" OnClick="Button1_Click" />
    </div>
</fieldset>
</asp:Content>
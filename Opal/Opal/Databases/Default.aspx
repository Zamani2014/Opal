<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Databases"  MasterPageFile="~/Site.master"%>
<%@ Register Src="~/EasyControls/TopMenu.ascx" TagPrefix="TopMenu" TagName="TopMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<TopMenu:TopMenu ID="TopMenu1" runat="server" />
<fieldset>
<legend>ارسال بر اساس بانک های اطلاعاتی موجود</legend>
    <table style="width:100%;">
        <tr>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/Images/ImageBtn/traffic lights.png" Width="128" Height="128" OnClick="ImageButton1_Click"/>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/ImageBtn/1020_new_tarhTransparent.png" Width="128" Height="128" OnClick="ImageButton4_Click"/>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/ImageBtn/TehranProvincePCode.png" Width="128" Height="128" OnClick="ImageButton5_Click"/>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/ImageBtn/employer_icon.png" Width="128" Height="128" OnClick="ImageButton6_Click"/>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton10" runat="server"  ImageUrl="~/Images/ImageBtn/TehranDistrict.png" Width="128" Height="128" OnClick="ImageButton10_Click"/>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/ImageBtn/region.png" Width="128" Height="128" OnClick="ImageButton9_Click"/>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton8" runat="server"  ImageUrl="~/Images/ImageBtn/MetroAndBRT.png"  Width="128" Height="128" OnClick="ImageButton8_Click"/>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/ImageBtn/IrancellBank.png" Width="128" Height="128" OnClick="ImageButton7_Click"/>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/ImageBtn/Heterosexual symbol (pink and blue pseudo-3D version).png" Width="128" Height="128" OnClick="ImageButton3_Click"  />
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/ImageBtn/Map_by_Artdesigner.png" Width="128" Height="128" OnClick="ImageButton2_Click" />
            </td>
        </tr>
    </table>
</fieldset>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetCredit.aspx.cs" Inherits="Administration_ShortMessageService_GetCredit" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<fieldset>
<legend>میزان اعتبار باقیمانده</legend>
    <table style="width: 100%;">
        <tr>
            <td style="width: 254px">
                &nbsp;
                <asp:Label ID="Label1" runat="server" Text="میزان اعتبار باقیمانده :"></asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:Label ID="ResultLabel" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 254px">
                &nbsp;
            </td>
            <td>
                &nbsp;
                <asp:Button ID="GetCreditBtn" runat="server" Text="نمایش باقیمانده اعتبار" 
                    onclick="GetCreditBtn_Click" />
            </td>
        </tr>
        </table>

</fieldset>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendByTehranProvincePostalCode.aspx.cs" Inherits="Databases_SendByPostalCode" MasterPageFile="~/Site.master" %>
<%@ Register Src="~/EasyControls/TopMenu.ascx" TagPrefix="TopMenu" TagName="TopMenu" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="DNA.UI.JQuery" Namespace="DNA.UI.JQuery" TagPrefix="DotNetAge" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<TopMenu:TopMenu ID="TopMenu1" runat="server" />
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="MessageTextBox" WatermarkText="متن پیامک خود را اینجا تایپ کنید ..." WatermarkCssClass="watermark">
    </asp:TextBoxWatermarkExtender>
    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="MobileTextBox" FilterType="Numbers" ValidChars=".,;">
    </asp:FilteredTextBoxExtender>
    <asp:BalloonPopupExtender ID="BalloonPopupExtender1" runat="server" TargetControlID="Image1" BalloonStyle="Rectangle"  BalloonPopupControlID="panel1" BalloonSize="Medium">
    </asp:BalloonPopupExtender>
    <asp:BalloonPopupExtender ID="BalloonPopupExtender2" runat="server" TargetControlID="Image2" BalloonStyle="Rectangle" BalloonPopupControlID="panel2" BalloonSize="Large">
    </asp:BalloonPopupExtender>
    <asp:BalloonPopupExtender ID="BalloonPopupExtender5" runat="server" TargetControlID="Image8" BalloonStyle="Rectangle" BalloonPopupControlID="Panel5" BalloonSize="Medium">
    </asp:BalloonPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="Panel">
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:stringsRes, SendByTehranProvincePostalCode %>">"></asp:Literal>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" CssClass="Panel">
        <asp:Literal ID="Literal2" runat="server" Text="<%$  Resources:stringsRes, sendSMS_MessageBoxNotices%>">"></asp:Literal>
    </asp:Panel>
    <asp:Panel ID="Panel5" runat="server" CssClass="Panel">
        <asp:Literal ID="Literal5" runat="server" Text="<%$  Resources:stringsRes, SendByTehranProvincePostalCode2%>">"></asp:Literal>
    </asp:Panel>
<fieldset>
<legend>ارسال بر اساس کد پستی استان تهران</legend>
    <div style="text-align:center">
    <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Images/maps/TehranProvinceDB.png" HotSpotMode="PostBack" ></asp:ImageMap>
    </div>
        <br />
    <table style="width: 100%;" >
        <tr>
            <td style="width: 133px">
                &nbsp;<asp:Label ID="Label1" runat="server" Text="کد پستی مورد نظر :" 
                    style="text-align: center"></asp:Label><br />
                <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/help.png"/>
            </td>
            <td style="width: 180px">
                <asp:TextBox ID="MobileTextBox" runat="server" style="direction: ltr" Width="80px" MaxLength="5" AutoPostBack="true"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="وارد کردن کد پستی پنج رقمی ضروری است ." ControlToValidate="MobileTextBox" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
            <td>
                <br />
                <br />
                <br />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 133px">
                &nbsp;<asp:Label ID="Label2" runat="server" Text="متن پیام"></asp:Label><br />
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/help.png" />
            </td>
            <td style="width: 180px">
                <asp:TextBox ID="MessageTextBox" runat="server" TextMode="MultiLine" 
                    ontextchanged="MessageTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>
                <br />
                [<asp:Label ID="Label6" runat="server" Text="تعداد کاراکتر:"></asp:Label><asp:Label
                    ID="Label7" runat="server" Text="0"></asp:Label>]&nbsp;[<asp:Label ID="Label8"
                        runat="server" Text="تعداد پیامک:"></asp:Label><asp:Label ID="Label9" runat="server"
                            Text="0"></asp:Label>]&nbsp;[<asp:Label ID="Label10" runat="server" Text="زبان :"></asp:Label><asp:Label
                                ID="Label11" runat="server" Text="فارسی"></asp:Label>]
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="وارد کردن متن پیام ضروری است ." ControlToValidate="MessageTextBox" ValidationGroup="1"></asp:RequiredFieldValidator>
                <br />
                <asp:LinkButton ID="LinkButton1" runat="server">ذخیره بعنوان الگو</asp:LinkButton>
            </td>
            <td>
                <asp:Button ID="Button4" runat="server" Text="انتخاب الگوی متن" /><br />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="پیامک ادغامی" /><br />
                <asp:Image ID="Image3" runat="server"  ImageUrl="~/Images/help.png"/>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:stringsRes, sendSMS_MergeOption%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender3" runat="server" BalloonPopupControlID="Panel3" BalloonSize="Large" TargetControlID="Image3">
                </asp:BalloonPopupExtender>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px">
                <asp:Label ID="Label12" runat="server" Text="فیلتر بر اساس پیش شماره :"></asp:Label><br />
                <asp:Image ID="Image8" runat="server"  ImageUrl="~/Images/help.png"/>
            </td>
            <td style="width: 180px">
                <asp:TextBox ID="MobileNumbersFilter" runat="server" Width="80px" MaxLength="5" style="direction: ltr" ></asp:TextBox>
            </td>
            <td style="width:150px">
                    <asp:Label ID="Label15" runat="server" Text="از :"></asp:Label>
                                        <asp:Image
                        ID="Image7" runat="server" ImageUrl="~/Images/help.png" />
                    <asp:TextBox ID="TextBox2" runat="server" Width="80px" style="direction: ltr; float:left" MaxLength="20"></asp:TextBox>
                <asp:Panel ID="Panel6" runat="server">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:stringsRes, sendBulkSMS_IndexOption%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender6" runat="server" BalloonPopupControlID="Panel6" BalloonSize="Large" TargetControlID="Image7">
                </asp:BalloonPopupExtender>                

                <br /><br />
                    <asp:Label ID="Label16" runat="server" Text="به تعداد :"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server" Width="80px" style="direction: ltr;  float:left" MaxLength="20"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px">
                <asp:Label ID="Label4" runat="server" Text="نحوه نمایش پیامک :"></asp:Label>
                <asp:Image ID="Image4" runat="server"  ImageUrl="~/Images/help.png"/>
                <asp:Panel ID="Panel4" runat="server">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:stringsRes, sendSMS_DeliveryOption%>" ></asp:Literal>
                </asp:Panel>
                <asp:BalloonPopupExtender ID="BalloonPopupExtender4" runat="server" BalloonPopupControlID="Panel4" BalloonSize="Large" TargetControlID="Image4">
                </asp:BalloonPopupExtender>
            </td>
            <td style="width: 180px">
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="aspcontrols" 
                    style="direction: rtl" AutoPostBack="True" 
                        onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                    <asp:ListItem Value="1">پیامک عادی</asp:ListItem>
                    <asp:ListItem Value="0">پیامک خبری</asp:ListItem>
                    <asp:ListItem Value="2">ذخیره در حافظه سیم کارت</asp:ListItem>
                    <asp:ListItem Value="3">ذخیره در نرم افزاری خاص</asp:ListItem>
                </asp:DropDownList><asp:TextBox ID="TextBox1" runat="server" Visible="false" style="direction: ltr; margin-left: 0px;" Height="22px" Width="154px"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="TextBox1" WatermarkText="شماره پورت نرم افزار مورد نظر">
                    </asp:TextBoxWatermarkExtender>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px">
                <asp:Label ID="Label5" runat="server" Text="اولویت ارسال :"></asp:Label>
            </td>
            <td style="width: 180px">
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
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 133px">
                &nbsp;
                </td>
            <td style="width: 180px">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="بعدا ارسال شود." oncheckedchanged="CheckBox1_CheckedChanged" AutoPostBack="True" />
<%--                <DotNetAge:DatePicker ID="DatePicker1" runat="server" IsRightToLeft="true" LocID="fa" YearRange="1390:1400"  AnimationOnShow="show" AnotherFormatString="yyyy/MM/dd" CssClass="aspcontrols">
                </DotNetAge:DatePicker>
--%>                
                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/DateIcon.png" Height="15px" Width="15px"/>&nbsp;&nbsp;<asp:Image ID="Image6" runat="server" ImageUrl="~/Images/TimeIcon.png" Height="16px" Width="16px" />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <asp:CheckBox ID="CheckBox3" runat="server" Text="ارسال با دروازه:" 
                        oncheckedchanged="CheckBox3_CheckedChanged" AutoPostBack="True" />
                <asp:TextBox ID="GatewayTextBox" runat="server"  Visible="false" 
    style="direction: ltr; margin-left: 0px;" Height="22px" Width="150px"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="GatewayTextBox" WatermarkText="آدرس آی پی را اینجا وارد کنید" >
                </asp:TextBoxWatermarkExtender>
                </ContentTemplate>
              </asp:UpdatePanel>

                <asp:Button ID="SendBtn" runat="server" Text="ارسال" onclick="SendBtn_Click" ValidationGroup="1" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
            <asp:ListBox ID="ListBox1" runat="server" Visible="false" AutoPostBack="True" CssClass="aspcontrols" ForeColor="#FF3300" Width="99.5%"></asp:ListBox>
</fieldset>
</ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="SendBtn" />
<%--                <asp:AsyncPostBackTrigger ControlID="" />--%>
               </Triggers>
</asp:UpdatePanel>
</asp:Content>
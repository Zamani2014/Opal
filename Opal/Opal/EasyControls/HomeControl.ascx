<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeControl.ascx.cs" Inherits="EasyControls_HomeControl" %>

        <table style="width: 100%;">
            <tr>
                <td>
                    <fieldset dir="rtl"style=" height:200px">
                            <legend style="direction: rtl"><strong>آخرین اخبار</strong></legend>
                        <asp:BulletedList ID="BulletedList1" runat="server" DataSourceID="SqlDataSource1" 
                                DataTextField="Subject" DataValueField="Address" DisplayMode="HyperLink">
                        </asp:BulletedList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:SoftDBConnectionString %>" 
                                SelectCommand="SELECT * FROM [LatestNews]"></asp:SqlDataSource>
                    </fieldset>
                </td>
                <td>
                   <fieldset dir="rtl"style=" height:200px">
                            <legend style="direction: rtl"><strong>دریافت فایل ها و مستندات</strong></legend>
                       <asp:BulletedList ID="BulletedList2" runat="server" style="direction: rtl" 
                                DisplayMode="HyperLink" DataSourceID="SqlDataSource2" DataTextField="Subject" 
                                DataValueField="Address">
                       </asp:BulletedList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:SoftDBConnectionString %>" 
                                SelectCommand="SELECT * FROM [DownloadLinks]"></asp:SqlDataSource>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset dir="rtl" style=" height:200px">
                            <legend style="direction: rtl"><strong>فرم های الکترونیکی</strong></legend>
                        <asp:BulletedList ID="BulletedList3" runat="server" DisplayMode="HyperLink" 
                                DataSourceID="SqlDataSource4" DataTextField="Subject" DataValueField="Address">
                        </asp:BulletedList>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:SoftDBConnectionString %>" 
                                SelectCommand="SELECT * FROM [eforms]"></asp:SqlDataSource>
                    </fieldset>
                </td>
                <td>
                     <fieldset dir="rtl"style=" height:200px">
                            <legend style="direction: rtl"><strong>آخرین امکانات و تغییرات سیستم</strong></legend>
                         <asp:BulletedList ID="BulletedList4" runat="server" DisplayMode="HyperLink" 
                                DataSourceID="SqlDataSource3" DataTextField="Subject" DataValueField="Address">
                         </asp:BulletedList>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:SoftDBConnectionString %>" 
                                SelectCommand="SELECT * FROM [SystemChange]"></asp:SqlDataSource>
                    </fieldset>
                </td>
            </tr>
        </table>

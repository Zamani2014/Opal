<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsTabs.ascx.cs" Inherits="EasyControls_WebUserControl" %>
<%@ Register Assembly="DNA.UI.JQuery" Namespace="DNA.UI.JQuery" TagPrefix="DotNetAge" %>
<%@ Register Src="~/EasyControls/LatestNews.ascx" TagPrefix="LatestNews" TagName="LatestNews" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<!--<script src="../App_Themes/ElasticOrange/PricingPlan/ga.js" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/jquery.js" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/checkOAuth.htm" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/loader-1002.js" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/sf_main.htm" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/sf_preloader.jsp" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/sf_code.jsp" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/base_single_icon.js" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/dojo.js" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/script.js" type="text/javascript" charset="utf-8"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/window.js" charset="utf-8" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/getSupportedSitesJSON.action" charset="utf-8" type="text/javascript" id="sufioIoScript1"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/getCouponsSupportedSites.action" charset="utf-8" type="text/javascript" id="sufioIoScript2"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/get.htm" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/ngram_id_dict.json"  type="text/javascript" id="SF_ngram_call"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/ga_002.js" type="text/javascript"></script>
<script type="text/javascript" src="../App_Themes/ElasticOrange/PricingPlan//woopra.js"></script>
<script type="text/javascript">                                                                                                                                                                                                                                                                                                    var switchTo5x = true;</script>
<script type="text/javascript" src="../App_Themes/ElasticOrange/PricingPlan//buttons.js"></script>
<script type="text/javascript">stLight.options({ publisher: "934e8786-d36f-4688-9db4-41042cefd03d" });</script>
<script type="text/javascript">    window['PricePeepPartnerData'] = { id: '260001', subid: '154_2035', startup_location: 'left' };</script>
<script src="../App_Themes/ElasticOrange/PricingPlan/pricepeep.htm" type="text/javascript"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/aeyJhZmZpZCI6MTAxOCwic3ViYWZmaWQiOjEwMDIsImhyZWYiOiJodHRwOi8v.js"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/a9993e364706816aba3e25717850c26c9cd0d89d.js"></script>
<script src="../App_Themes/ElasticOrange/PricingPlan/banners.js" async=""></script>
<sfmsg data="{&quot;imageCount&quot;:0,&quot;ip&quot;:&quot;1.1.1.1&quot;}" id="sfMsgId"></sfmsg> 
<!-- Start of Woopra Code -->
<script type="text/javascript">
    function woopraReady(tracker) {
        tracker.setDomain('itzurkarthi.com');
        tracker.setIdleTimeout(1800000);
        tracker.track();
        return false;
    }
    (function () {
        var wsc = document.createElement('script');
        wsc.src = document.location.protocol + '//static.woopra.com/js/woopra.js';
        wsc.type = 'text/javascript';
        wsc.async = true;
        var ssc = document.getElementsByTagName('script')[0];
        ssc.parentNode.insertBefore(wsc, ssc);
    })();
</script>
<!-- End of Woopra Code -->
<!-- .wrapper -->
<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-27820211-1']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

		</script>      
<iframe style="display:none;" src="../App_Themes/ElasticOrange/PricingPlan/getSegment.htm" name="stSegmentFrame" id="stSegmentFrame" frameborder="0" height="0px" scrolling="no" width="0px"></iframe>
<DotNetAge:Tabs ID="HomeNewsTabs" runat="server" Height="480px" Width="99.9%" SelectedIndex="2">
<Animations>
    <DotNetAge:AnimationAttribute AnimationType="opacity" Value="toggle" />
    <DotNetAge:AnimationAttribute AnimationType="height" Value="toggle" />
</Animations>
    <Views>
        <DotNetAge:View ID="View1" runat="server" Text="آخرین اخبار">
      <LatestNews:LatestNews ID="LatestNews1" runat="server" style="height:480px" /> 
        </DotNetAge:View>
        <DotNetAge:View ID="View2" runat="server" Text="فرم ها و خدمات الکترونیکی" 
            CssClass="ui-tabs-panel ui-widget-content ui-corner-bottom" ShowHeader="false">
            <table style="width: 100%; height:200px">
                <tr style="text-align:center;">
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Forms/sign-up.png" Width="50" Height="50"/><br />
                       <a href="../UserRegistration.aspx">فرم عمومی ثبت نام در سامانه</a> 
                    </td>
                    <td>
                        <asp:Image ID="Image2" runat="server"  ImageUrl="~/Images/Forms/credit_card_payment.png" Width="50" Height="50"/><br />
                        <a href="../eForms/epayment.aspx">پرداخت الکترونیکی حساب</a> 
                    </td>
                    <td>
                        <asp:Image ID="Image3" runat="server"  ImageUrl="~/Images/Forms/Form.png" Width="50" Height="50"/><br />
                        <a href="../eForms/recieptform.aspx">اطلاعات فیش بانکی</a> 
                    </td>
                   <td>
                        <asp:Image ID="Image4" runat="server"  ImageUrl="~/Images/Forms/web-services.png" Width="50" Height="50"/><br />
                        <a href="../eForms/solutionsform.aspx">سفارش خدمات و راهکارها</a> 
                    </td>

                </tr >
                <tr style="text-align:center;">
                    <td>
                        <asp:Image ID="Image5" runat="server"  ImageUrl="~/Images/Forms/palet_shipping_products_goods_shipment.png" Width="50" Height="50"/><br />
                       <a href="../eForms/productsform.aspx">سفارش محصولات</a> 
                    </td>
                    <td>
                        <asp:Image ID="Image6" runat="server"  ImageUrl="~/Images/Forms/poland_640.png" Width="50" Height="50"/><br />
                     <a href="../eForms/simrecharge.aspx">شارژ الکترونیکی سیم کارت</a>   
                    </td>
                    <td>
                        <asp:Image ID="Image7" runat="server"  ImageUrl="~/Images/Forms/agent-green-256.png" Width="50" Height="50"/><br />
                    <a href="../eForms/agents.aspx"> درخواست نمایندگی فروش</a>   
                    </td>
                     <td>
                        <asp:Image ID="Image8" runat="server"  ImageUrl="~/Images/Forms/Credits.png" Width="50" Height="50"/><br />
                  <a href="../eForms/creditform.aspx">فرم الکترونیکی واریز اعتبار</a>      
                    </td>

                </tr>
                <tr style="text-align:center;">
                    <td>
                        <asp:Image ID="Image9" runat="server"  ImageUrl="~/Images/Forms/voting-box-hi.png" Width="50" Height="50"/><br />
                    <a href="/customervoice.aspx">نظرات انتقادات و پیشنهادات</a>    
                    </td>
                    <td>
                        <asp:Image ID="Image10" runat="server"  ImageUrl="~/Images/Forms/Portal_Catalog.png" Width="50" Height="50"/><br />
                    <a href="../eForms/catalog.aspx">سفارش کاتالوگ و مستندات</a>    
                    </td>
                    <td>
                        <asp:Image ID="Image11" runat="server"  ImageUrl="~/Images/Forms/email.png" Width="50" Height="50"/><br />
                     <a href="/contactus.aspx"> فرم عمومی تماس با ما</a>  
                    </td>
                                        <td>
                        <asp:Image ID="Image15" runat="server"  ImageUrl="~/Images/Forms/Control-Panel-icon.png" Width="50" Height="50"/><br />
                     <a href="../eForms/panelorder.aspx">فرم سفارش پنل های اینترنتی</a>  
                    </td>

                </tr>
                <tr style="text-align:center;">
                <td>
                        <asp:Image ID="Image16" runat="server"  ImageUrl="~/Images/Forms/error_button.png" Width="50" Height="50"/><br />
                     <a href="../eForms/bugtracker.aspx">فرم گزارش خطاهای فنی</a>  
                </td>
                <td></td>
                <td></td>
                <td></td>
                </tr>
            </table>
            </DotNetAge:View>
        <DotNetAge:View ID="View3" runat="server" Text="تعرفه پنل های اینترنتی" 
            CssClass="ui-tabs-panel ui-widget-content ui-corner-bottom" ShowHeader="False">
            <div style="margin:0px auto; padding:0px; width:100%;">
<!--  4 columns -->
<%--    <center>
      <span class="st_fblike_vcount" displaytext="Facebook Like"></span>
      <span class="st_facebook_vcount" displaytext="Facebook"></span>
      <span class="st_twitter_vcount" displaytext="Tweet"></span>
      <span class="st_email_vcount" displaytext="Email"></span>
      <span class="st_linkedin_vcount" displaytext="LinkedIn"></span>
      <span class="st_pinterest_vcount" displaytext="Pinterest"></span>
      <span class="st_sharethis_vcount" displaytext="ShareThis"></span>
    </center>

--%>
<div class="pricing-table-wrapper">
  <div class="pricing-table">
    <ul class="pricing-column">
      <li class="pricing-title">حرفه ای</li>
      <li class="pricing-price">600,000 تومان
        <div class="price-comment">بدون محدودیت زمانی</div>
      </li>
      <li>16,528 پیامک</li>
      <li>20,000 تومان شارژ رایگان</li>
      <li>ارسال بصورت عادی و خبری</li>
      <li>امکان ذخیره در حافظه سیم کارت گیرنده</li>
      <li>ارسال مستقیم از فایلهای اکسل و متنی </li>
      <li>ارسال بصورت نظیر به نظیر از فایل اکسل</li>
      <li>ارسال انبوه بر اساس استان، شهر و پیش شماره</li>
      <li>ارسال انبوه بر اساس کد پستی و مناطق تهران</li>
      <li>شماره 10 رقمی رند اختصاصی</li>
      <li class="pricing-button"><a class="button single-color text-bright size-medium" href="eforms/panelorder.aspx?PlanID=5" id="sc_button_3">سفارش</a></li>
    </ul>
    <ul class="pricing-column">
      <li class="pricing-title">پرمصرف</li>
      <li class="pricing-price">520,000 تومان
        <div class="price-comment">بدون محدودیت زمانی</div>
      </li>
      <li>31,007 پیامک</li>
      <li>20,000 تومان شارژ رایگان</li>
      <li>ارسال بصورت عادی و خبری</li>
      <li>امکان ذخیره در حافظه سیم کارت گیرنده</li>
      <li>ارسال مستقیم از فایلهای اکسل و متنی </li>
      <li>ارسال بصورت نظیر به نظیر از فایل اکسل</li>
      <li>ارسال انبوه بر اساس استان، شهر و پیش شماره</li>
      <li>ارسال انبوه بر اساس کد پستی و مناطق تهران</li>
      <li>شماره 10 رقمی معمولی اختصاصی</li>
      <li class="pricing-button"><a class="button single-color text-bright size-medium" href="eforms/panelorder.aspx?PlanID=4" id="sc_button_1">سفارش</a></li>
    </ul>
    <ul class="pricing-column">
      <li class="pricing-title">مصرف متوسط</li>
      <li class="pricing-price">300,000 تومان
        <div class="price-comment">بدون محدودیت زمانی</div>
      </li>
      <li>14,285 پیامک</li>
      <li>15,000 تومان شارژ رایگان</li>
      <li>ارسال بصورت عادی و خبری</li>
      <li>امکان ذخیره در حافظه سیم کارت گیرنده</li>
      <li>ارسال مستقیم از فایلهای اکسل و متنی </li>
      <li>ارسال بصورت نظیر به نظیر از فایل اکسل</li>
      <li>ارسال انبوه بر اساس استان، شهر و پیش شماره</li>
      <li>ارسال انبوه بر اساس کد پستی و مناطق تهران</li>
      <li>شماره 12 رقمی معمولی اختصاصی</li>
      <li class="pricing-button"><a class="button single-color text-bright size-medium" href="eforms/panelorder.aspx?PlanID=3" id="sc_button_2">سفارش</a></li>
    </ul>
    <ul class="pricing-column">
      <li class="pricing-title">پیشنهاد ویژه</li>
      <li class="pricing-price">180,000 تومان
        <div class="price-comment">بدون محدودیت زمانی</div>
      </li>
      <li>11,538 پیامک</li>
      <li>10,000 تومان شارژ رایگان</li>
      <li>ارسال بصورت عادی و خبری</li>
      <li>امکان ذخیره در حافظه سیم کارت گیرنده</li>
      <li>ارسال مستقیم از فایلهای اکسل و متنی </li>
      <li>ارسال بصورت نظیر به نظیر از فایل اکسل</li>
      <li>ارسال انبوه بر اساس استان، شهر و پیش شماره</li>
      <li>ارسال انبوه بر اساس کد پستی و مناطق تهران</li>
      <li>شماره 14 رقمی معمولی اختصاصی</li>
      <li class="pricing-button"><a class="button single-color text-bright size-medium" href="eforms/panelorder.aspx?PlanID=2" id="sc_button_3">سفارش</a></li>
    </ul>
    <ul class="pricing-column">
      <li class="pricing-title">مبتدی</li>
      <li class="pricing-price">80,000 تومان
        <div class="price-comment">بدون محدودیت زمانی</div>
      </li>
      <li>3,846 پیامک</li>
      <li>10,000 تومان شارژ رایگان</li>
      <li>ارسال بصورت عادی و خبری</li>
      <li>امکان ذخیره در حافظه سیم کارت گیرنده</li>
      <li>ارسال مستقیم از فایلهای اکسل و متنی </li>
      <li>ارسال بصورت نظیر به نظیر از فایل اکسل</li>
      <li>ارسال انبوه بر اساس استان، شهر و پیش شماره</li>
      <li>ارسال انبوه بر اساس کد پستی و مناطق تهران</li>
      <li>شماره 14 رقمی معمولی اختصاصی</li>
      <li class="pricing-button"><a class="button single-color text-bright size-medium" href="eforms/panelorder.aspx?PlanID=1" id="sc_button_3">سفارش</a></li>
    </ul>
   <div class="clear"></div>
  </div>
   <ul style="text-align:right">
   <li>حداقل اعتبار برای خرید پنجاه هزار تومان میباشد .</li>
   <li>هزینه خط اختصاصی جداگانه اخذ میشود .</li>
   <li>به کلیه مبالغ فوق شش درصد مالیات بر ارزش افزوده اضافه میشود .</li>
   </ul>
</div>
</div>
            </DotNetAge:View>
        <DotNetAge:View ID="View4" runat="server" Text="تعرفه خطوط پیامک" 
            CssClass="ui-tabs-panel ui-widget-content ui-corner-bottom" ShowHeader="False">
           
            <div style="text-align:center">
                <asp:Image ID="Image14" runat="server" ImageUrl="~/Images/LinePricing.JPG" />
            </div>
            <ul>
            <li>کلیه قیمت های فوق به ریال میباشد .</li>
            <li>به کلیه مبالغ فوق شش درصد مالیات بر ارزش افزوده اضافه می گردد .</li>
            <li>امکان انتخاب شماره های رند نیز وجود دارد که میتوانید با بخش فروش هماهنگ نمائید .</li>
            <li>برای شماره های رند هزینه ای علاوه بر هزینه های فوق اخذ میشود .</li>
            </ul>
            </DotNetAge:View>
        <DotNetAge:View ID="View9" runat="server" Text="تعرفه ارسال پیامک" 
            CssClass="ui-tabs-panel ui-widget-content ui-corner-bottom" ShowHeader="False">
            <table>
            <tr>
            <td>
            جدول هزینه های ارسال پیامک های فارسی انبوه در سال 92 (بریال)
                <br /><asp:Image ID="Image12" runat="server" ImageUrl="~/Images/PricingSMSFa.JPG" Width="430" Height="170"/>
            </td>
            <td>
            جدول هزینه های ارسال پیامک های انگلیسی انبوه در سال 92 (بریال)
                <br /><asp:Image ID="Image13" runat="server" ImageUrl="~/Images/SMSPricingEn.JPG" Width="430" Height="170"/>
            </td>
            </tr>
            </table>
            </DotNetAge:View>
        <%--        <DotNetAge:View ID="View5" runat="server" Text="تعرفه سرویس های ویژه" 
            CssClass="ui-tabs-panel ui-widget-content ui-corner-bottom" ShowHeader="False">
            در حال حاضر سرویس های ویژه ارائه نمیشوند، خواهشمندیم روزهای آتی پیگیری نمائید .
            </DotNetAge:View>
--%>
        <%--        <DotNetAge:View ID="View6" runat="server" Text="پنل های نمایندگی" 
            CssClass="ui-tabs-panel ui-widget-content ui-corner-bottom" ShowHeader="False">
            در حال حاضر پنل های نمایندگی ارائه نمیشوند، خواهشمندیم روزهای آتی پیگیری نمائید .
            </DotNetAge:View>
--%>      
        <DotNetAge:View ID="View7" runat="server" Text="دریافت فایل ها و مستندات">
        <table>
        <tr>
        <td>
            <asp:BulletedList ID="BulletedList1" runat="server" BulletStyle="CustomImage" BulletImageUrl="~/Images/help - Copy.png"  DisplayMode="HyperLink">
            <asp:ListItem Text="راهنمای نماد اعتماد الکترونیکی فروشگاه های الکترونیکی" Value="~/Files/ecne/ecne.pdf" ></asp:ListItem>
            <asp:ListItem Text="آشنائی با مرکز دولتی صدور گواهی الکترونیکی ریشه" Value="~/Files/RCA Intro/RCAIntro.pdf"></asp:ListItem>
            <asp:ListItem Text="راهنمای کاربری پنل های سامانه خدمات ارزش افزوده تلفن همراه" Value="~"></asp:ListItem>
            <asp:ListItem Text="اطلاعات شماره حساب های بانکی حقیقی ریالی" Value="~/Files/bank/BankAccounts.pdf"></asp:ListItem>
            </asp:BulletedList>
        </td>
        <td>
            <asp:BulletedList ID="BulletedList2" runat="server">
            </asp:BulletedList>
        </td>
        </tr>
        </table>
        <asp:Literal ID="Literal1" runat="server" Text="برای دریافت فایل ها و مستندات کاملتر به بخش امکانات سامانه >> فایل ها و مستندات مراجعه نمائید ."></asp:Literal>
        </DotNetAge:View>
    </Views>
</DotNetAge:Tabs>

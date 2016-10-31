using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using MagfaWebReference;
using SOAPSmsQueue;
using System.Globalization;

namespace ArvidfavaSMS
{
    /// <summary>
    /// Summary description for ShortMessageService
    /// </summary>
    public class ShortMessageService
    {
        public static int MAX_VALUE = 1000;
        public ShortMessageService()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static String getDescriptionForCode(int code)
        {
            switch (code)
            {
                case -1:
                    return "پیامی مطابق با شناسه وارد شده وجود ندارد";
                case 1:
                    return "شماره گیرنده نامعتبر است، لطفا شماره گیرنده و یا گیرنده گان را مجددا بررسی نمائید";
                case 2:
                    return "شماره فرستنده نامعتبر است. لطفا شماره فرستنده را مجددا بررسی نمائید";
                case 3:
                    return "پارامتر رمزنگاری متن پیام نامعنبر است. لطفا صحت و همخوانی متن پیامک را با رمزنگاری انتخابی بررسی نمائید";
                case 4:
                    return "پارامتر نوع دریافت پیامک نامعتبر است. برای حالت عادی و نرمال میتوانید این پارامتر را رها کنید";
                case 6:
                    return "سرآیند تعریف شده توسط کاربر نامعتبر است. برای ارسال پیامکهای ساده این پارامتر را میتوانید رها کنید";
                case 10:
                    return "پارامتر اولویت ارسال پیامک نامعتبر است. تنها اعداد برای این پارامتر معتبر تلقی میشوند";
                case 12:
                    return "اطلاعات حساب کاربری شما نامعتبر است لطفا نام کاربری و گذرواژه خود را بررسی نمائید";
                case 13:
                    return "متن پیامک خود را بررسی نمائید. متن پیامک احیانا خالی است و یا اندازه متن پیامک و پارامتر سرآیند تعریف شده توسط کاربر نامعتبر است";
                case 14:
                    return "حساب شما اعتبار ریالی مورد نیاز برای ارسال پیامک را دارا نمیباشد";
                case 15:
                    return "سرور در هنگام ارسال پیامک مشغول برطرف نمودن ایراد داخلی بوده است. پیام ها دوباره ارسال شوند / ارسال مجدد درخواست";
                case 16:
                    return "حساب شما غیرفعال میباشد برای کسب اطلاعات بیشتر با ارائه دهنده خدمات پیامک خود تماس بگیرید";
                case 17:
                    return "حساب شما منقضی شده است برای کسب اطلاعات بیشتر با ارائه دهنده خدمات پیامک خود تماس بگیرید";
                case 18:
                    return "ترکیب نام کاربری/گذرواژه/نام دامنه نامعتبر است لطفا مجددا سعی نمائید";
                case 19:
                    return "ترکیب نام کاربری و گذرواژه شما نامعتبر میباشد";
                case 20:
                    return "شماره اختصاصی با نام کاربر حساب مطابقت ندارد";
                case 22:
                    return "نوع سرویس درخواستی نامعتبر است، سرویس اختصاص داده شده به حساب با نوع درخواست همخوانی ندارد";
                case 23:
                    return "به دلیل ترافیک بالای سرور آمادگی دریافت پیام جدید را ندارد، لطفا دوباره سعی کنید / ارسال مجدد درخواست";
                case 24:
                    return "شناسه پیامک معتبر نمیباشد. ممکن است شناسه پیامک اشتباه و یا متعلق به پیامکی باشد که بیش از یک روز از ارسال آن گذشته است";
                case 25:
                    return "نام سرویس درخواستی معتبر نمیباشد.";
                case 27:
                    return "شماره گیرنده در فهرست غیرفعال شرکت همراه اول قرار دارد / ارسال پیامکهای تبلیغاتی برای این شماره امکان پذیر نیست";
                case 101:
                    return "طول آرایه پارامتر متن پیامک با طول آرایه گیرندگان تطابق ندارد";
                case 102:
                    return "طول آرایه پارامتر ویژه پارامترهای نوع دریافت پیامک با طول آرایه گیرندگان تطابق ندارد، شما میبایست یک عنصر در آرایه نوع دریافت پیامک برای هر شماره گیرنده تعریف کنید";
                case 103:
                    return "طول آرایه پارامتر شماره های ارسال کنندگان با طول آرایه گیرندگان تطابق ندارد";
                case 104:
                    return "طول آرایه پارامتر سرآیندهای تعریف شده توسط کاربر با طول آرایه گیرندگان تطابق ندارد";
                case 105:
                    return "طول آرایه پارامتر اولویت ارسال پیامک با طول آرایه گیرندگان تطابق ندارد";
                case 106:
                    return "آرایه گیرندگان خالی است";
                case 107:
                    return "حداکثر تعداد گیرندگان برای هر پیامک نود میباشد، طول آرایه پارامتر گیرندگان بیشتر از طول مجاز میباشد";
                case 108:
                    return "آرایه فرستندگان خالی است";
                case 109:
                    return "تعداد شماره های گیرنده با رمزنگاری برای هر پیغام مطابقت ندارد شما باید یک شماره گیرنده برای هر رمزنگاری متن تعریف کنید";
                case 110:
                    return "طول آرایه پارامتر شناسه پیامک کاربر با طول آرایه گیرندگان تطابق ندارد";
                default:
                    return "خطایی رخ داده است";
            }
        }
        public static String getDescriptionForStatusCode(int code)
        {
            switch (code)
            {
                case 1:
                    return "رسیده به گوشی";
                case 2:
                    return "نرسیده به گوشی";
                case 8:
                    return "رسیده به مخابرات";
                case 16:
                    return "نرسیده به مخابرات";
                case 0:
                    return "در صف ارسال";
                default:
                    return "(خطا)";
            }
        }
        public static String getDescriptionForBulkCode(Int64 code)
        {
            switch (code)
            {
                case -1:
                    return "اطلاعات حساب نامعتبر است .";
                case -2:
                    return "بالک متعلق به این حساب نیست .";
                case -3:
                    return "بالک بدون گیرنده است .";
                case -4:
                    return "خطای سرور، ارسال مجدد .";
                case -5:
                    return "پارامتر متن بالک خالی است .";
                case -6:
                    return "شماره فرستنده خالی است .";
                case -7:
                    return "پارامتر نوع بالک نامعتبر است .";
                case -8:
                    return "تاریخ ارسال نامعتبر است .";
                case -9:
                    return "شناسه بالک خالی است .";
                case -10:
                    return "شناسه استان نامعتبر است .";
                case -11:
                    return "شناسه بالک نامعتبر است .";
                case -12:
                    return "پارامتر پیش شماره خالی است .";
                case -13:
                    return "پارامتر کد پستی نامعتبر است .";
                case -14:
                    return "شناسه گیرندگان نامعتبر است .";
                case -15:
                    return "شماره فرستنده نامعتبر است .";
                case -16:
                    return "شناسه شهر نامعتبر است .";
                case -17:
                    return "پارامتر نوع شماره نامعتبر است .";
                case -18:
                    return "مقدار پارامتر -از- نامعتبر است .";
                case -19:
                    return "مقدار پارامتر پیش شماره نامعتبر است .";
                default:
                    return "خطایی رخ داده است .";
            }
        }
        public static String getDescriptionForBulkStatus(string status)
        {
            if ("COMPOSING" == status)
                {
                    return "در حال ایجاد پیامک انبوه";
                }
            if ("PENDING" == status)
                {
                    return "در حالت انتظار جهت تائید";
                }
            if ("ACCEPTED" == status)
                {
                    return "تائید شده و منتظر ارسال به مخابرات";
                }
            if ("REJECTED" == status)
                {
                    return "از طرف مرکز پیامک تائید نشده است";
                }
            if ("STARTED" == status)
                {
                    return "در حال ارسال به مخابرات";
                }
            if ("ّFINISHED" == status)
                {
                    return "ارسال پیامک انبوه پایان یافته";
                }
            if ("HALTED" == status)
                {
                    return "توقف موقت پیامک انبوه";
                }
            if ("CANCELED" == status)
                {
                    return "لغو ارسال پیامک انبوه";
                }
                else
                {
                    return "خطا";
                }
        }
        public static String getDescriptionForPaymentStatusMellat(int code)
        {
            switch (code)
            {
                case 0:
                    return "تراکنش با موفقیت انجام شد .";
                case 11:
                    return "شماره کارت بانکی نامعتبر است .";
                case 12:
                    return "موجودی کافی نیست .";
                case 13:
                    return "رمز نادرست است .";
                case 14:
                    return "تعداد دفعات وارد کردن رمز بیش از حد مجاز است .";
                case 15:
                    return "کارت نامعتبر است .";
                case 16:
                    return "دفعات برداشت وجه بیش از حد مجاز است .";
                case 17:
                    return "کاربر از انجام تراکنش منصرف شده است .";
                case 18:
                    return "تاریخ انقضای کارت گذشته است .";
                case 19:
                    return "مبلغ برداشت وجه بیش از حد مجاز است .";
                case 111:
                    return "صادر کننده کارت نامعتبر است .";
                case 112:
                    return "خطای سوئیچ صادر کننده کارت .";
                case 113:
                    return "پاسخی از صادر کننده کارت دریافت نشد .";
                case 114:
                    return "دارنده کارت مجاز به انجام این تراکنش نیست .";
                case 21:
                    return "پذیرنده نامعتبر است .";
                case 23:
                    return "خطای امنیتی رخ داده است .";
                case 24:
                    return "اطلاعات کاربری پذیرنده نامعتبر است .";
                case 25:
                    return "مبلغ نامعتبر است .";
                case 31:
                    return "پاسخ نامعتبر است .";
                case 32:
                    return "فرمت اطلاعات وارد شده صحیح نمیباشد .";
                case 33:
                    return "حساب نامعتبر است .";
                case 34:
                    return "خطای سیستمی رخ داده است .";
                case 35:
                    return "اطلاعات تاریخ نامعتبر است .";
                case 41:
                    return "شماره درخواست تکراری است .";
                case 42:
                    return "تراکنش فروش یافت نشد .";
                case 43:
                    return "قبلا درخواست بررسی داده شده است .";
                case 44:
                    return "درخواست بررسی یافت نشد .";
                case 45:
                    return "تراکنش واریز شده است .";
                case 46:
                    return "تراکنش واریز نشده است .";
                case 47:
                    return "تراکنش واریز یافت نشد .";
                case 48:
                    return "تراکنش برگشت خورده است .";
                case 49:
                    return "تراکنش بازپرداخت یافت نشد .";
                case 412:
                    return "شناسه قبض نادرست است .";
                case 413:
                    return "شناسه پرداخت نادرست است .";
                case 414:
                    return "سازمان صادر کننده قبض نامعتبر است .";
                case 415:
                    return "زمان جلسه کاری به پایان رسیده است .";
                case 416:
                    return "خطا در ثبت اطلاعات .";
                case 417:
                    return "شناسه پرداخت کننده نامعتبر است .";
                case 418:
                    return "اشکال در تعریف اطلاعات مشتری .";
                case 419:
                    return "تعداد دفعات ورود اطلاعات از حد مجاز گذشته است .";
                case 421:
                    return "نشانی اینترنتی نامعتبر است .";
                case 51:
                    return "تراکنش تکراری است .";
                case 54:
                    return "تراکنش مرجع موجود نیست .";
                case 55:
                    return "تراکنش نامعتبر است .";
                case 61:
                    return "خطا در .واریز رخ داده است .";
                default:
                    return "متاسفانه خطایی رخ داده است .";
            }
        }
        public static String generateDateString()
        {
            return "[" + System.DateTime.Now.ToLongTimeString() + "]  -  ";
        }
        public static String generateDateTimeString()
        {
            PersianCalendar jc = new PersianCalendar();
            DateTime thisDate = DateTime.Now;

            string p_year = jc.GetYear(thisDate).ToString();
            string p_month = jc.GetMonth(thisDate).ToString();
            string p_day = jc.GetDayOfMonth(thisDate).ToString();

            string mytime = jc.GetHour(thisDate).ToString() + ":" + jc.GetMinute(thisDate).ToString() + ":" + jc.GetSecond(thisDate).ToString();
            string mydate = p_year + "/" + p_month + "/" + p_day;
            string mytot = "[" + mytime + "]" + " - " + "[" + mydate + "]";

            return mytot;
        }
    }
}
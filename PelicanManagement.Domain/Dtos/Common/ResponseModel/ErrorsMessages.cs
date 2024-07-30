using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Common.ResponseModel
{
    public class ErrorsMessages
    {
        public static string FailedLogin = "ورود ناموفق، لطفاً مجدداً تلاش فرمایید.";
        public static string InvalidApiResponse = "خطا، پاسخ API معتبر نمی‌باشد.";
        public static string SuccessfulLogin = "ورود با موفقیت انجام شد.";
        public static string InvalidUsernameOrPassword = "نام کاربری یا رمز عبور نادرست است.";
        public static string NotAuthenticated = "کاربر احراز هویت نشده است.";
        public static string Authenticated = "کاربر با موفقیت احراز هویت شد.";
        public static string NotFound = "موردی یافت نشد.";
        public static string SuccessfulRegistration = "ثبت‌نام با موفقیت انجام شد.";
        public static string UserNotFound = "کاربر یافت نشد. لطفاً مجدداً تلاش فرمایید.";
        public static string AccountInactive = "حساب کاربری غیرفعال است.";
        public static string PasswordsDoNotMatch = "رمز عبور و تکرار آن مطابقت ندارند. لطفاً مجدداً بررسی فرمایید.";
        public static string UserAlreadyExists = "نام کاربری انتخابی قبلاً ثبت شده است. لطفاً نام کاربری متفاوتی وارد کنید.";
        public static string RecordAlreadyExists = "رکوردی با این مشخصات از قبل وجود دارد. لطفاً اطلاعات را بررسی نمایید.";
        public static string NullInputs = "مقادیر ورودی نمی‌توانند خالی باشند.";
        public static string OperationSuccessful = "عملیات با موفقیت انجام شد.";
        public static string OperationFailed = "عملیات با شکست مواجه شد.";
        public static string IncorrectOtp = "کد تأیید نادرست است.";
        public static string OtpSent = "کد تأیید به ایمیل شما ارسال شد.";
        public static string PermissionDenied = "دسترسی رد شد.";
        public static string InternalServerError = "خطای داخلی سرور.";
        public static string PhoneNumberAlreadyExists = "شماره تلفن وارد شده قبلاً در سیستم ثبت شده است.";
        public static string UsernameAlreadyExists = "نام کاربری انتخابی قبلاً ثبت شده است. لطفاً نام کاربری متفاوتی وارد کنید.";
        public static string EmailAlreadyExists = "ایمیل وارد شده قبلاً در سیستم ثبت شده است.";
        public static string SmsPanelNotResponding = "ارسال پیامک با خطا مواجه شد. لطفاً مجدداً تلاش فرمایید.";
        public static string EmailNotConfirmed = "ایمیل شما تأیید نشده است. لطفاً ایمیل خود را تأیید فرمایید.";

    }
}

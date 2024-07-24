using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dtos.Common.ResponseModel
{
    public class ErrorsMessages
    {
        public static string FaildLogin = "Error, please try again";
        public static string SuccessLogin = "Login successful";
        public static string UserORPasswrodIsWrong = "Username or password is incorrect";
        public static string NotAuthenticated = "User not authenticated";
        public static string Authenticated = "User authenticated";
        public static string NotFound = "Not found";
        public static string SuccessRegister = "Registration successful";
        public static string UserNotfound = "User not found, please try again";
        public static string NotActive = "The user account is inactive";
        public static string PassswordAndConfrimPassswordIsnotEqueal = "Password and confirm password do not match, please try again";
        public static string UserExist = "A user with these credentials already exists";
        public static string NullInputs = "Input values are empty";
        public static string Success = "Operation successful";
        public static string Faild = "Operation failed";
        public static string OtpIncorrect = "Incorrect OTP";
        public static string EmailSend = "Verification code has been sent to your email.";
    }
}

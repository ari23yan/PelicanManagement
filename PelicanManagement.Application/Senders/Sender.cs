using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shop.Domain.Dtos.Sender;


namespace Shop.Application.Senders
{
    public class Sender : ISender
    {
        private readonly IConfiguration _configuration;
        public Sender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendMailForgetPassword(SendMailDto request)
        {
            try
            {
                var mail = new MailMessage();

                var SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("ariyanrahmaniii@gmail.com", "keyserSoze.org");
                mail.To.Add(request.Email);
                string password = _configuration["Password"];
                mail.Subject = "Forget Password";
                mail.Body = @"<head>
                   <title></title>
                  
                   <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                  <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                 <style type=""text/css"">
                   #outlook a { padding: 0; }
                   .ReadMsgBody { width: 100%; }
                   .ExternalClass { width: 100%; }
                   .ExternalClass * { line-height:100%; }
                   body { margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }
                   table, td { border-collapse:collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; }
                   img { border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }
                   p { display: block; margin: 13px 0; }
                 </style>
                 <style type=""text/css"">
                   @media only screen and (max-width:480px) {
                     @-ms-viewport { width:320px; }
                     @viewport { width:320px; }
                   }
                 </style>
                 <!--<![endif]-->
                 <!--[if mso]>
                 <xml>
                   <o:OfficeDocumentSettings>
                     <o:AllowPNG/>
                     <o:PixelsPerInch>96</o:PixelsPerInch>
                   </o:OfficeDocumentSettings>
                 </xml>
                 <![endif]-->
                 <!--[if lte mso 11]>
                 <style type=""text/css"">
                   .outlook-group-fix {
                     width:100% !important;
                   }
                 </style>
                 <![endif]-->
                 
                 <!--[if !mso]><!-->
                     <link href=""https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700"" rel=""stylesheet"" type=""text/css"">
                     <style type=""text/css"">
                 
                         @import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);
                 
                     </style>
                   <!--<![endif]--><style type=""text/css"">
                   @media only screen and (min-width:480px) {
                     .mj-column-per-100, * [aria-labelledby=""mj-column-per-100""] { width:100%!important; }
                   }
                 </style>
                 </head>
                 <body style=""background: #F9F9F9;"">
                   <div style=""background-color:#F9F9F9;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]-->
                   <style type=""text/css"">
                     html, body, * {
                       -webkit-text-size-adjust: none;
                       text-size-adjust: none;
                     }
                     a {
                       color:#1EB0F4;
                       text-decoration:none;
                     }
                     a:hover {
                       text-decoration:underline;
                     }
                   </style>
                 <div style=""margin:0px auto;max-width:640px;background:transparent;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:transparent;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:40px 0px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><div aria-labelledby=""mj-column-per-100"" class=""mj-column-per-100 outlook-group-fix"" style=""vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tbody><tr><td style=""word-break:break-word;font-size:0px;padding:0px;"" align=""center""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse:collapse;border-spacing:0px;"" align=""center"" border=""0""><tbody><tr><td style=""width:138px;""><a href="""" target=""_blank""></a></td></tr></tbody></table></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--><div style=""max-width:640px;margin:0 auto;box-shadow:0px 1px 5px rgba(0,0,0,0.1);border-radius:4px;overflow:hidden""><div style=""margin:0px auto;max-width:640px;background:#7289DA url(https://cdn.discordapp.com/email_assets/f0a4cc6d7aaa7bdf2a3c15a193c6d224.png) top center / cover no-repeat;""><!--[if mso | IE]>
                       <v:rect xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""true"" stroke=""false"" style=""width:640px;"">
                         <v:fill origin=""0.5, 0"" position=""0.5,0"" type=""tile"" src=""~/Common/titleBackGround.png"" />
                         <v:textbox style=""mso-fit-shape-to-text:true"" inset=""0,0,0,0"">
                       <![endif]--><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:#7289DA url(~/Common/titleBackGround.png) top center / cover no-repeat;"" align=""center"" border=""0"" background=""https://cdn.discordapp.com/email_assets/f0a4cc6d7aaa7bdf2a3c15a193c6d224.png""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:57px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:undefined;width:640px;"">
                       <![endif]--><div style=""cursor:auto;color:white;font-family:Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-size:36px;font-weight:600;line-height:36px;text-align:center;"">Keyser Soze !</div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table><!--[if mso | IE]>
                         </v:textbox>
                       </v:rect>
                       <![endif]--></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--><div style=""margin:0px auto;max-width:640px;background:#ffffff;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:#ffffff;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:40px 70px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><div aria-labelledby=""mj-column-per-100"" class=""mj-column-per-100 outlook-group-fix"" style=""vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tbody><tr><td style=""word-break:break-word;font-size:0px;padding:0px 0px 20px;"" align=""left""><div style=""cursor:auto;color:#737F8D;font-family:Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-size:16px;line-height:24px;text-align:left;"">
                             <p><img src=""https://cdn.discordapp.com/email_assets/127c95bbea39cd4bc1ad87d1500ae27d.png"" alt=""Party Wumpus"" title=""None"" width=""500"" style=""height: auto;""></p>
                             <h2 style=""font-family: Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;"">Hey " + request.Username + @" </h2>
                             <h3>Your Forget Password Request : </h3>
                 
                           </div></td></tr><tr><td style=""word-break:break-word;font-size:0px;padding:10px 25px;"" align=""center""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse:separate;"" align=""center"" border=""0""><tbody><tr><td style=""border:none;border-radius:3px;color:white;cursor:auto;padding:15px 19px;"" align=""center"" valign=""middle"" bgcolor=""#7289DA""><a  style=""text-decoration:none;line-height:100%;background:#7289DA;color:white;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:15px;font-weight:normal;text-transform:none;margin:0px;"" target=""_blank"">
                            Verification Code : " + request.Code +
                                            @" 
                           </a></td></tr></tbody></table></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--></div><div style=""margin:0px auto;max-width:640px;background:transparent;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:transparent;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:0px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><div aria-labelledby=""mj-column-per-100"" class=""mj-column-per-100 outlook-group-fix"" style=""vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tbody><tr><td style=""word-break:break-word;font-size:0px;""><div style=""font-size:1px;line-height:12px;"">&nbsp;</div></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--><div style=""margin:0 auto;max-width:640px;background:#ffffff;box-shadow:0px 1px 5px rgba(0,0,0,0.1);border-radius:4px;overflow:hidden;""><table cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:#ffffff;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;font-size:0px;padding:0px;""><!--[if mso | IE]>
                       <table border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--><div style=""margin:0px auto;max-width:640px;background:transparent;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:transparent;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:20px 0px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><div aria-labelledby=""mj-column-per-100"" class=""mj-column-per-100 outlook-group-fix"" style=""vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tbody><tr><td style=""word-break:break-word;font-size:0px;padding:0px;"" align=""center""><div style=""cursor:auto;color:#99AAB5;font-family:Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-size:12px;line-height:24px;text-align:center;"">
                       Sent by Keyser Soze • <a href=""https://www.keyserSoze.com"" style=""color:#1EB0F4;text-decoration:none;"" target=""_blank"">www.KeyserSoze.com</a>
                     </div></td></tr><tr><td style=""word-break:break-word;font-size:0px;padding:0px;"" align=""center""><div style=""cursor:auto;color:#99AAB5;font-family:Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-size:12px;line-height:24px;text-align:center;"">
                       Iran,Tehran
                     </div></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></div>
                 </body>";
                SmtpServer.EnableSsl = true;
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ariyanrahmaniii@gmail.com", password);
                SmtpServer.Send(mail);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SendMailForSignUp(SendMailDto request)
        {
            try
            {
                var mail = new MailMessage();

                var SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("ariyanrahmaniii@gmail.com", "KeyserSoze.org");
                mail.To.Add(request.Email);
                string password = _configuration["Password"];
                mail.Subject = "Keyser Soze Verification Code";
                mail.Body = @"<head>
                   <title></title>
                  
                   <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                  <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                 <style type=""text/css"">
                   #outlook a { padding: 0; }
                   .ReadMsgBody { width: 100%; }
                   .ExternalClass { width: 100%; }
                   .ExternalClass * { line-height:100%; }
                   body { margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }
                   table, td { border-collapse:collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; }
                   img { border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; }
                   p { display: block; margin: 13px 0; }
                 </style>
                 <style type=""text/css"">
                   @media only screen and (max-width:480px) {
                     @-ms-viewport { width:320px; }
                     @viewport { width:320px; }
                   }
                 </style>
                 <!--<![endif]-->
                 <!--[if mso]>
                 <xml>
                   <o:OfficeDocumentSettings>
                     <o:AllowPNG/>
                     <o:PixelsPerInch>96</o:PixelsPerInch>
                   </o:OfficeDocumentSettings>
                 </xml>
                 <![endif]-->
                 <!--[if lte mso 11]>
                 <style type=""text/css"">
                   .outlook-group-fix {
                     width:100% !important;
                   }
                 </style>
                 <![endif]-->
                 
                 <!--[if !mso]><!-->
                     <link href=""https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700"" rel=""stylesheet"" type=""text/css"">
                     <style type=""text/css"">
                 
                         @import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);
                 
                     </style>
                   <!--<![endif]--><style type=""text/css"">
                   @media only screen and (min-width:480px) {
                     .mj-column-per-100, * [aria-labelledby=""mj-column-per-100""] { width:100%!important; }
                   }
                 </style>
                 </head>
                 <body style=""background: #F9F9F9;"">
                   <div style=""background-color:#F9F9F9;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]-->
                   <style type=""text/css"">
                     html, body, * {
                       -webkit-text-size-adjust: none;
                       text-size-adjust: none;
                     }
                     a {
                       color:#1EB0F4;
                       text-decoration:none;
                     }
                     a:hover {
                       text-decoration:underline;
                     }
                   </style>
                 <div style=""margin:0px auto;max-width:640px;background:transparent;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:transparent;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:40px 0px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><div aria-labelledby=""mj-column-per-100"" class=""mj-column-per-100 outlook-group-fix"" style=""vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tbody><tr><td style=""word-break:break-word;font-size:0px;padding:0px;"" align=""center""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse:collapse;border-spacing:0px;"" align=""center"" border=""0""><tbody><tr><td style=""width:138px;""><a href="""" target=""_blank""></a></td></tr></tbody></table></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--><div style=""max-width:640px;margin:0 auto;box-shadow:0px 1px 5px rgba(0,0,0,0.1);border-radius:4px;overflow:hidden""><div style=""margin:0px auto;max-width:640px;background:#7289DA url(https://cdn.discordapp.com/email_assets/f0a4cc6d7aaa7bdf2a3c15a193c6d224.png) top center / cover no-repeat;""><!--[if mso | IE]>
                       <v:rect xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""true"" stroke=""false"" style=""width:640px;"">
                         <v:fill origin=""0.5, 0"" position=""0.5,0"" type=""tile"" src=""~/Common/titleBackGround.png"" />
                         <v:textbox style=""mso-fit-shape-to-text:true"" inset=""0,0,0,0"">
                       <![endif]--><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:#7289DA url(~/Common/titleBackGround.png) top center / cover no-repeat;"" align=""center"" border=""0"" background=""https://cdn.discordapp.com/email_assets/f0a4cc6d7aaa7bdf2a3c15a193c6d224.png""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:57px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:undefined;width:640px;"">
                       <![endif]--><div style=""cursor:auto;color:white;font-family:Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-size:36px;font-weight:600;line-height:36px;text-align:center;"">Welcome to Keyser Soze !</div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table><!--[if mso | IE]>
                         </v:textbox>
                       </v:rect>
                       <![endif]--></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--><div style=""margin:0px auto;max-width:640px;background:#ffffff;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:#ffffff;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:40px 70px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><div aria-labelledby=""mj-column-per-100"" class=""mj-column-per-100 outlook-group-fix"" style=""vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tbody><tr><td style=""word-break:break-word;font-size:0px;padding:0px 0px 20px;"" align=""left""><div style=""cursor:auto;color:#737F8D;font-family:Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-size:16px;line-height:24px;text-align:left;"">
                             <p><img src=""https://cdn.discordapp.com/email_assets/127c95bbea39cd4bc1ad87d1500ae27d.png"" alt=""Party Wumpus"" title=""None"" width=""500"" style=""height: auto;""></p>
                   <h2 style=""font-family: Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;"">Hey " + request.Username + @" </h2>
                 <p>Wowwee! Thanks for registering an account! You're the coolest person in all the land (and I've met a lot of really cool people).</p>
                 <p>Before we get started, we'll need to verify your email.</p>
                 
                           </div></td></tr><tr><td style=""word-break:break-word;font-size:0px;padding:10px 25px;"" align=""center""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse:separate;"" align=""center"" border=""0""><tbody><tr><td style=""border:none;border-radius:3px;color:white;cursor:auto;padding:15px 19px;"" align=""center"" valign=""middle"" bgcolor=""#7289DA""><a  style=""text-decoration:none;line-height:100%;background:#7289DA;color:white;font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:15px;font-weight:normal;text-transform:none;margin:0px;"" target=""_blank"">
                            Verification Code : " + request.Code +
                            @" 
                           </a></td></tr></tbody></table></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--></div><div style=""margin:0px auto;max-width:640px;background:transparent;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:transparent;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:0px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><div aria-labelledby=""mj-column-per-100"" class=""mj-column-per-100 outlook-group-fix"" style=""vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tbody><tr><td style=""word-break:break-word;font-size:0px;""><div style=""font-size:1px;line-height:12px;"">&nbsp;</div></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--><div style=""margin:0 auto;max-width:640px;background:#ffffff;box-shadow:0px 1px 5px rgba(0,0,0,0.1);border-radius:4px;overflow:hidden;""><table cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:#ffffff;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;font-size:0px;padding:0px;""><!--[if mso | IE]>
                       <table border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]-->
                       <!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" style=""width:640px;"">
                         <tr>
                           <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                       <![endif]--><div style=""margin:0px auto;max-width:640px;background:transparent;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""font-size:0px;width:100%;background:transparent;"" align=""center"" border=""0""><tbody><tr><td style=""text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:20px 0px;""><!--[if mso | IE]>
                       <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""vertical-align:top;width:640px;"">
                       <![endif]--><div aria-labelledby=""mj-column-per-100"" class=""mj-column-per-100 outlook-group-fix"" style=""vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;""><table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0""><tbody><tr><td style=""word-break:break-word;font-size:0px;padding:0px;"" align=""center""><div style=""cursor:auto;color:#99AAB5;font-family:Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-size:12px;line-height:24px;text-align:center;"">
                       Sent by Keyser Soze • <a href=""https://www.keyserSoze.com"" style=""color:#1EB0F4;text-decoration:none;"" target=""_blank"">www.KeyserSoze.com</a>
                     </div></td></tr><tr><td style=""word-break:break-word;font-size:0px;padding:0px;"" align=""center""><div style=""cursor:auto;color:#99AAB5;font-family:Whitney, Helvetica Neue, Helvetica, Arial, Lucida Grande, sans-serif;font-size:12px;line-height:24px;text-align:center;"">
                       Iran,Tehran
                     </div></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></td></tr></tbody></table></div><!--[if mso | IE]>
                       </td></tr></table>
                       <![endif]--></div>
                 </body>";
                SmtpServer.EnableSsl = true;
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ariyanrahmaniii@gmail.com", password);
                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

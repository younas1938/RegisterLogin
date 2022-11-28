using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Models;

namespace UserEntity.Services
{
   public  interface IEmailService
    {
        //Task SendTestEmail(UserEmailOptions userEmailOptions);

        //Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);

        //Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
Task SendEmailAsync(string fromAddress, string toAddress, string subject, string htmlMessage);

    }
}

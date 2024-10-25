using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;
namespace ServiceHost.Helper;

public class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // برای تست فقط لاگ کنید یا هیچ کاری نکنید
        Console.WriteLine($"[FAKE EMAIL] To: {email}, Subject: {subject}, Body: {htmlMessage}");
        return Task.CompletedTask;
    }
}

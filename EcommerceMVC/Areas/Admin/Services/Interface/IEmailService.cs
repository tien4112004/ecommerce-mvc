using System;

namespace EcommerceMVC.Areas.Admin.Services;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}

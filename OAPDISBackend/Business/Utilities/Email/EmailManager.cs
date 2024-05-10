using System.Net;
using System.Net.Mail;
using DataAccess.Repositories.EmailParameterRepository;
using Entities.Concrete;

namespace Business.Utilities.Email;

public class EmailManager : IEmailService
{
    private readonly IEmailParameterDal _emailParameterDal;

    public EmailManager(IEmailParameterDal emailParameterDal)
    {
        _emailParameterDal = emailParameterDal;
    }
    
    public async Task SendEmailAsync(string receiver, string subject, string content)
    {
        EmailParameter emailParameter = await _emailParameterDal.GetFirst();
        var client = new SmtpClient(emailParameter.Smtp, emailParameter.Port)
        {
            Credentials = new NetworkCredential(emailParameter.Email, emailParameter.Password),
            EnableSsl = emailParameter.SSL
        };
        var mail = new MailMessage(emailParameter.Email, receiver)
        {
            Subject = subject,
            Body = content,
            IsBodyHtml = true
        };
        await client.SendMailAsync(mail);
    }
}
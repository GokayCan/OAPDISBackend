namespace Business.Utilities.Email;

public interface IEmailService
{
    Task SendEmailAsync(string receiver, string subject, string content);
}
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using PeladaControladaAPI.Services;

public class EmailMensagemService : IMensagemService
{
    private readonly IConfiguration _configuration;

    public EmailMensagemService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task EnviarAsync(string destino, string mensagem)
    {
        var smtp = _configuration.GetSection("Smtp");

        var client = new SmtpClient(smtp["Host"], int.Parse(smtp["Port"]))
        {
            Credentials = new NetworkCredential(
                smtp["User"],
                smtp["Password"]
            ),
            EnableSsl = true
        };

        var mail = new MailMessage(
            from: smtp["User"],
            to: destino,
            subject: "Seu código de verificação",
            body: mensagem
        );

        await client.SendMailAsync(mail);
    }
}

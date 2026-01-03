using PeladaControladaAPI.Services;
public class ConsoleMensagemService : IMensagemService
{
    public Task EnviarWhatsAppAsync(string telefone, string mensagem)
    {
        // Aqui simulamos o envio mostrando no terminal onde roda a API
        Console.WriteLine($"[WHATSAPP SIMULADO] Para: {telefone} | Mensagem: {mensagem}");
        return Task.CompletedTask;
    }
}
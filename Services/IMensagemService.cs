namespace PeladaControladaAPI.Services
{
    public interface IMensagemService
    {
        Task EnviarWhatsAppAsync(string telefone, string mensagem);
    }
}
namespace PeladaControladaAPI.Services
{
    public interface IMensagemService
    {
        Task EnviarAsync(string destino, string mensagem);
    }
}
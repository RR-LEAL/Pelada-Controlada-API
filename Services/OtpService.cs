using System.Security.Cryptography;
using System.Text;
using PeladaControladaAPI.Model;

public static class OtpService
{
    public static (string Codigo, UsuarioCodigoOtp Otp) Gerar()
    {
        var codigo = RandomNumberGenerator
            .GetInt32(100000, 999999)
            .ToString();

        using var sha = SHA256.Create();
        var hash = Convert.ToBase64String(
            sha.ComputeHash(Encoding.UTF8.GetBytes(codigo))
        );

        return (
            codigo,
            new UsuarioCodigoOtp
            {
                Usuario = null!, // Será atribuído pelo EF Core
                Hash = hash,
                ExpiraEm = DateTime.UtcNow.AddMinutes(10)
            }
        );
    }

    public static bool Validar(string codigoInformado, UsuarioCodigoOtp otp)
    {
        if (DateTime.UtcNow > otp.ExpiraEm)
            return false;

        using var sha = SHA256.Create();
        var hash = Convert.ToBase64String(
            sha.ComputeHash(Encoding.UTF8.GetBytes(codigoInformado))
        );

        return hash == otp.Hash;
    }
}

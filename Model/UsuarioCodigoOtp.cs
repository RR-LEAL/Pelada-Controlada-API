using PeladaControladaAPI.model;

namespace PeladaControladaAPI.Model
{
    public class UsuarioCodigoOtp
    {
        public Guid Id { get; set; }

        public required string Hash { get; set; }
        public DateTime ExpiraEm { get; set; }

        public bool Usado { get; set; } = false;

        public DateTime UsadoEm { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public required Usuario Usuario { get; set; }
    }

}
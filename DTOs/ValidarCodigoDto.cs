using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeladaControladaAPI.DTOs
{
    public class ValidarCodigoDto
{
    public required string Email { get; set; }
    public required string Codigo { get; set; }
}
}
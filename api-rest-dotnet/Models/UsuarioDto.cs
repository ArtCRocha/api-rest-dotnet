using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace api_rest_dotnet.Models
{
    public class UsuarioDto
    {
        public int? Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public Pefil Pefil { get; set; }

    }

}

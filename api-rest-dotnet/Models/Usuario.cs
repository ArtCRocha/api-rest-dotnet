using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_rest_dotnet.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [JsonIgnore]
        public string Senha { get; set;}
        [Required]
        public Pefil Pefil { get; set; }

        public ICollection<VeiculoUsuarios> Veiculos { get; set; }  
    }

    public enum Pefil
    {
        [Display(Name = "Administrador")]
        Administrador,
        [Display(Name = "Usuario")]
        Usuario,

    }
}

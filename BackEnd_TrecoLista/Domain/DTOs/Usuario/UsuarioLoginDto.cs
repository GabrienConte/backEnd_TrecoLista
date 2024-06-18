using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Usuario
{
    public class UsuarioLoginDto
    {
        [Required]
        [StringLength(255)]
        public string EmailOuLogin { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; }
    }
}

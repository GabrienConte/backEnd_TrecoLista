using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Usuario
{
    public class UsuarioUpdateDto
    {
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Login { get; set; }
    }
}

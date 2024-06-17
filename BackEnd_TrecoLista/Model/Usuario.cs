using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Model
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; }

        [Required]
        [StringLength(255)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoUsuario { get; set; }

        public Usuario(int id, string email, string senha, string login, string tipoUsuario)
        {
            Id = id;
            Email = email;
            Senha = senha;
            Login = login;
            TipoUsuario = tipoUsuario;
        }
    }
}

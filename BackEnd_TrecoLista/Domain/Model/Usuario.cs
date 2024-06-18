using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.Model
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("email")]
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Column("senha")]
        [Required]
        [StringLength(255)]
        public string Senha { get; set; }

        [Column("login")]
        [Required]
        [StringLength(255)]
        public string Login { get; set; }

        [Column("tipousuario")]
        [Required]
        [StringLength(50)]
        public string TipoUsuario { get; set; }

        public Usuario()
        {
        }

        public Usuario(int id, string email, string login, string senha)
        {
            Id = id;
            Email = email;
            Login = login;
            Senha = senha;
        }
    }
}

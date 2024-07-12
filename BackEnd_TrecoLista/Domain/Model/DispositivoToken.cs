using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_TrecoLista.Domain.Model
{
    [Table("dispositivotoken")]
    public class DispositivoToken
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("usuario_id")]
        [Required]
        public int UsuarioId { get; set; }

        [Column("token")]
        [Required]
        public string Token { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Produto Produto { get; set; }

        public DispositivoToken(int id, int usuarioId, string token)
        {
            Id = id;
            UsuarioId = usuarioId;
            Token = token;
        }

        public DispositivoToken()
        {
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Model
{
    [Table("favorito")]
    public class Favorito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public bool Aviso { get; set; }

        public int Prioridade { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public Favorito(int id, int produtoId, int usuarioId, bool aviso, int prioridade, Produto produto, Usuario usuario)
        {
            Id = id;
            ProdutoId = produtoId;
            UsuarioId = usuarioId;
            Aviso = aviso;
            Prioridade = prioridade;
            Produto = produto;
            Usuario = usuario;
        }
    }
}

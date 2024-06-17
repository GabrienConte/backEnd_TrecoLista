using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Model
{
    [Table("historicoemail")]
    public class HistoricoEmail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Assunto { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string CorpoEmail { get; set; }

        [Required]
        public DateTime DataEnvio { get; set; }

        [Required]
        public int DestinoId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey("DestinoId")]
        public virtual Usuario Destino { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }

        public HistoricoEmail(int id, string assunto, string corpoEmail, DateTime dataEnvio, int destinoId, int produtoId, Usuario destino, Produto produto)
        {
            Id = id;
            Assunto = assunto;
            CorpoEmail = corpoEmail;
            DataEnvio = dataEnvio;
            DestinoId = destinoId;
            ProdutoId = produtoId;
            Destino = destino;
            Produto = produto;
        }
    }
}

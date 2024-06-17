using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Model
{
    [Table("historicoemail")]
    public class HistoricoEmail
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("assunto")]
        [Required]
        [StringLength(255)]
        public string Assunto { get; set; }

        [Column("corpoemail", TypeName = "text")]
        [Required]
        public string CorpoEmail { get; set; }

        [Column("dataenvio")]
        [Required]
        public DateTime DataEnvio { get; set; }

        [Column("destino_id")]
        [Required]
        public int DestinoId { get; set; }

        [Column("produto_id")]
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
        }

        public HistoricoEmail()
        {
        }
    }
}

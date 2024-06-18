using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.Model
{
    [Table("categoria")]
    public class Categoria
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("descricao")]
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [Column("ativo")]
        [Required]
        public bool Ativo { get; set; }
    }
}

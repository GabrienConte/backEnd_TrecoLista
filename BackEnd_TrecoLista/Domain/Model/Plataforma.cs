using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.Model
{
    [Table("plataforma")]
    public class Plataforma
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("descricao")]
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        public Plataforma()
        {
        }

        public Plataforma(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Model
{
    [Table("plataforma")]
    public class Plataforma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        public Plataforma(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}

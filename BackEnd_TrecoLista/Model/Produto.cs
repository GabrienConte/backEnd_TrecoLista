using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Model
{
    [Table("produto")]
    public class Produto
    {
        public Produto()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [StringLength(255)]
        public string Link { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor { get; set; }

        [StringLength(255)]
        public string ImagemPath { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int PlataformaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        [ForeignKey("PlataformaId")]
        public virtual Plataforma Plataforma { get; set; }

        public Produto(int id, string descricao, string link, decimal valor, string imagemPath, int categoriaId, int plataformaId, Categoria categoria, Plataforma plataforma)
        {
            Id = id;
            Descricao = descricao;
            Link = link;
            Valor = valor;
            ImagemPath = imagemPath;
            CategoriaId = categoriaId;
            PlataformaId = plataformaId;
            Categoria = categoria;
            Plataforma = plataforma;
        }
    }
}

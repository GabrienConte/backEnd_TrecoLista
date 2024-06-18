using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.Model
{
    [Table("produto")]
    public class Produto
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("descricao")]
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [Column("link")]
        [StringLength(255)]
        public string Link { get; set; }

        [Column("valor", TypeName = "decimal(10, 2)")]
        [Required]
        public decimal Valor { get; set; }

        [Column("imagempath")]
        [StringLength(255)]
        public string ImagemPath { get; set; }

        [Column("categoria_id")]
        [Required]
        public int CategoriaId { get; set; }

        [Column("plataforma_id")]
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

        public Produto(int id, string descricao, string link, decimal valor, string imagemPath, int categoriaId, int plataformaId)
        {
            Id = id;
            Descricao = descricao;
            Link = link;
            Valor = valor;
            ImagemPath = imagemPath;
            CategoriaId = categoriaId;
            PlataformaId = plataformaId;
        }
    }
}

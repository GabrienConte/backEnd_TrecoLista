using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Produto
{
    public class ProdutoCreateDto
    {
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [StringLength(255)]
        public string Link { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [StringLength(255)]
        public string ImagemPath { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int PlataformaId { get; set; }
    }
}

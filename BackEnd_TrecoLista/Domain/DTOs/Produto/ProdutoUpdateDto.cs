using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Produto
{
    public class ProdutoUpdateDto
    {
        [StringLength(255)]
        public string Link { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int PlataformaId { get; set; }
    }
}

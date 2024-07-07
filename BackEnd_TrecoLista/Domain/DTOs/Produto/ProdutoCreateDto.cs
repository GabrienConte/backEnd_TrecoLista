using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Produto
{
    public class ProdutoCreateDto
    {
        [Required]
        [StringLength(255)]
        public string Link { get; set; }

        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }

        public string ImagemPath { get; set; }

        public IFormFile? Imagem { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int PlataformaId { get; set; }

        public int Prioridade { get; set; }

        public bool IsAvisado {  get; set; }
    }
}

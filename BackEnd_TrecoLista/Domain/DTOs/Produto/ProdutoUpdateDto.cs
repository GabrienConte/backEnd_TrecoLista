using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Produto
{
    public class ProdutoUpdateDto
    {
        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int PlataformaId { get; set; }
        public int Prioridade { get; set; }

        public bool IsAvisado { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Favorito
{
    public class ProdutoFavoritoDTO
    {
        [Required]
        public int ProdutoId { get; set; }
    }
}

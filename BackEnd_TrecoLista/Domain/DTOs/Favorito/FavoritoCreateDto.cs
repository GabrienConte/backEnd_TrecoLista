using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Favorito
{
    public class FavoritoCreateDto
    {
        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public bool Aviso { get; set; }

        public int Prioridade { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Categoria
{
    public class CategoriaUpdateDto
    {
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Required]
        public bool Ativo { get; set; }
    }
}

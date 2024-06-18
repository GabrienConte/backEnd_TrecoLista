using System.ComponentModel.DataAnnotations;

namespace BackEnd_TrecoLista.Domain.DTOs.Plataforma
{
    public class PlataformaUpdateDto
    {
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
    }
}

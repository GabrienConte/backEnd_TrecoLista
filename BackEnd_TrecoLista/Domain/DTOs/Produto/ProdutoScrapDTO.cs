using System.Text.Json.Serialization;

namespace BackEnd_TrecoLista.Domain.DTOs.Produto
{
    public class ProdutoScrapDTO
    {
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("valor")]
        public string Valor { get; set; }

        public decimal ValorConvertido { get; set; }

        [JsonPropertyName("imagemPath")]
        public string ImagemPath { get; set; }

        [JsonPropertyName("plataforma")]
        public string Plataforma { get; set; }
    }
}

namespace BackEnd_TrecoLista.Domain.DTOs.Produto
{
    public class ProdutoCardDTO
    {
        public int ProdutoId { get; set; }
        public string Link { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string ImagemPath { get; set; }
        public bool IsFavoritado { get; set; }
    }
}

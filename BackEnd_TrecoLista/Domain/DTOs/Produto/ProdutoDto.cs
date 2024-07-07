namespace BackEnd_TrecoLista.Domain.DTOs.Produto
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public decimal Valor { get; set; }
        public string ImagemPath { get; set; }
        public int CategoriaId { get; set; }
        public int PlataformaId { get; set; }
    }
}

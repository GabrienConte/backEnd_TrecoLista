namespace BackEnd_TrecoLista.ViewModel
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public decimal Valor { get; set; }
        public IFormFile Imagem { get; set; }
        public int CategoriaId { get; set; }
        public int PlataformaId { get; set; }
    }
}

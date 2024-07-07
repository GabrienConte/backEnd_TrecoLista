namespace BackEnd_TrecoLista.Domain.DTOs.Produto
{
    public class ProdutoFavoritadoDTO : ProdutoDto
    {
        public int Prioridade { get; set; }
        public bool Aviso { get; set; }
    }
}

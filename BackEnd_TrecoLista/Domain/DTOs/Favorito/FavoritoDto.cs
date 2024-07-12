namespace BackEnd_TrecoLista.Domain.DTOs.Favorito
{
    public class FavoritoDto
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int UsuarioId { get; set; }
        public bool Aviso { get; set; }
        public int Prioridade { get; set; }
        public string ProdutoDescricao { get; set; }
        public string ProdutoLink { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
    }
}

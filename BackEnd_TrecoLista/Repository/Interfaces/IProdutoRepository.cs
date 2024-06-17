using BackEnd_TrecoLista.Model;

namespace BackEnd_TrecoLista.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        void Add(Produto produto);

        List<Produto> Get(int pageNumber, int pageQuantity);

        Produto? Get(int id);
    }
}

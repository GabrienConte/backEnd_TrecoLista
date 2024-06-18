using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        void Add(Produto produto);

        List<Produto> Get(int pageNumber, int pageQuantity);

        Produto? Get(int id);
    }
}

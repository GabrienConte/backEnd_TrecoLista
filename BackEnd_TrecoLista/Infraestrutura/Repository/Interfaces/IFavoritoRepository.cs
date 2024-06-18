using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface IFavoritoRepository
    {
        void Add(Favorito favorito);

        List<Favorito> Get();
    }
}

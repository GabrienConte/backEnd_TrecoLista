using BackEnd_TrecoLista.Model;

namespace BackEnd_TrecoLista.Repository.Interfaces
{
    public interface IFavoritoRepository
    {
        void Add(Favorito favorito);

        List<Favorito> Get();
    }
}

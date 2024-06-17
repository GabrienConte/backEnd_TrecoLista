using BackEnd_TrecoLista.Model;

namespace BackEnd_TrecoLista.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        void Add(Categoria categoria);

        List<Categoria> Get();
    }
}

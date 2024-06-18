using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        void Add(Categoria categoria);

        List<Categoria> Get();
    }
}

using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Categoria;
using BackEnd_TrecoLista.Domain.DTOs.Plataforma;
using BackEnd_TrecoLista.Domain.Model;

namespace BackEnd_TrecoLista.Infraestrutura.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaCreateDto, Categoria>();
            CreateMap<CategoriaUpdateDto, Categoria>();
            CreateMap<Plataforma, PlataformaDto>();
            CreateMap<PlataformaCreateDto, Plataforma>();
            CreateMap<PlataformaUpdateDto, Plataforma>();
        }
    }
}

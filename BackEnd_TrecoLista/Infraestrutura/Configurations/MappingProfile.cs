using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Categoria;
using BackEnd_TrecoLista.Domain.DTOs.Favorito;
using BackEnd_TrecoLista.Domain.DTOs.Plataforma;
using BackEnd_TrecoLista.Domain.DTOs.Produto;
using BackEnd_TrecoLista.Domain.DTOs.Usuario;
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

            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioCreateDto, Usuario>();
            CreateMap<UsuarioUpdateDto, Usuario>();
            CreateMap<UsuarioLoginDto, Usuario>();

            CreateMap<Produto, ProdutoDto>()
           .ForMember(dest => dest.CategoriaDescricao, opt => opt.MapFrom(src => src.Categoria.Descricao))
           .ForMember(dest => dest.PlataformaDescricao, opt => opt.MapFrom(src => src.Plataforma.Descricao));
            CreateMap<ProdutoCreateDto, Produto>();
            CreateMap<ProdutoUpdateDto, Produto>();

            CreateMap<Favorito, FavoritoDto>()
            .ForMember(dest => dest.ProdutoDescricao, opt => opt.MapFrom(src => src.Produto.Descricao))
            .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario.Login));
            CreateMap<FavoritoCreateDto, Favorito>();
            CreateMap<FavoritoUpdateDto, Favorito>();
        }
    }
}

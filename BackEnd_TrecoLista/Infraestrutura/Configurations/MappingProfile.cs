using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.Categoria;
using BackEnd_TrecoLista.Domain.DTOs.DispositivoToken;
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

            CreateMap<Produto, ProdutoDto>();
            CreateMap<Produto, ProdutoFavoritadoDTO>();
            CreateMap<ProdutoCreateDto, Produto>();
            CreateMap<ProdutoUpdateDto, Produto>();

            CreateMap<Favorito, FavoritoDto>()
            .ForMember(dest => dest.ProdutoDescricao, opt => opt.MapFrom(src => src.Produto.Descricao))
            .ForMember(dest => dest.ProdutoLink, opt => opt.MapFrom(src => src.Produto.Link))
            .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario.Login))
            .ForMember(dest => dest.UsuarioEmail, opt => opt.MapFrom(src => src.Usuario.Email));
            CreateMap<FavoritoCreateDto, Favorito>();
            CreateMap<FavoritoUpdateDto, Favorito>();

            CreateMap<DispositivoToken, DispositivoTokenDto>();
            CreateMap<DispositivoTokenDto, DispositivoToken>();
            CreateMap<DispositivoTokenCreateDto, DispositivoToken>();
        }
    }
}

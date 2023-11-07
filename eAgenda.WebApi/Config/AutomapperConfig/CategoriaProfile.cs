using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloCategoria;
using eAgenda.WebApi.ViewModels.ModuloDespesa;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<FormsCategoriaViewModel, Categoria>();

            CreateMap<Categoria, ListarCategoriaViewModel>();

            CreateMap<Categoria, VisualizarCategoriaViewModel>();

            CreateMap<Categoria, CategoriaSelecionadaViewModel>();
        }
    }
}

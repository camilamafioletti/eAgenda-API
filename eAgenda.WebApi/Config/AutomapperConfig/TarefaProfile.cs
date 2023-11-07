using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloTarefa;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class TarefaProfile : Profile
    {

        private TarefaProfile()
        {
            CreateMap<FormsTarefaViewModel, Tarefa>();

            CreateMap<Tarefa, ListarTarefaViewModel>()
                               .ForMember(destino => destino.Prioridade, opt => opt.MapFrom(origem => origem.Prioridade.GetDescription()))
                               .ForMember(destino => destino.Situacao, opt =>
                                   opt.MapFrom(origem => origem.PercentualConcluido == 100 ? "Concluído" : "Pendente"));

            CreateMap<Tarefa, VisualizarTarefaViewModel>()
                .ForMember(destino => destino.Prioridade, opt => opt.MapFrom(origem => origem.Prioridade.GetDescription()))

                .ForMember(destino => destino.Situacao, opt =>
                    opt.MapFrom(origem => origem.PercentualConcluido == 100 ? "Concluído" : "Pendente"))

                .ForMember(destino => destino.QuantidadeItens, opt => opt.MapFrom(origem => origem.Itens.Count));

            CreateMap<ItemTarefa, VisualizarItemTarefaViewModel>()
                .ForMember(destino => destino.Situacao, opt =>
                    opt.MapFrom(origem => origem.Concluido ? "Concluído" : "Pendente"));


            CreateMap<ItemTarefa, FormsItemTarefaViewModel>();
        }       
    }
}

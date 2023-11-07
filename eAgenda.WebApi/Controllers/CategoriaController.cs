using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloCategoria;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        public ServicoCategoria servicoCategoria;
        private IMapper mapeador;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeador)
        {
            this.mapeador = mapeador;
            this.servicoCategoria = servicoCategoria;
        }

        [HttpGet]
        public List<ListarCategoriaViewModel> SelecionarTodos()
        {
            var categorias = servicoCategoria.SelecionarTodos().Value;

            return mapeador.Map<List<ListarCategoriaViewModel>>(categorias);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarCategoriaViewModel SelecionarPorId(Guid id)
        {
            var categoria = servicoCategoria.SelecionarPorId(id).Value;

            return mapeador.Map<VisualizarCategoriaViewModel>(categoria);
        }

        [HttpPost]
        public string Inserir(FormsCategoriaViewModel categoriaViewModel)
        {
            var categoria = mapeador.Map<Categoria>(categoriaViewModel);

            var resultado = servicoCategoria.Inserir(categoria);

            if (resultado.IsSuccess)
                return "Categoria inserida com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormsCategoriaViewModel categoriaViewModel)
        {
            var categoriaEncontrada = servicoCategoria.SelecionarPorId(id).Value;

            var categoria = mapeador.Map(categoriaViewModel, categoriaEncontrada);

            var resultado = servicoCategoria.Editar(categoria);

            if (resultado.IsSuccess)
                return "Categoria editada com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            var resultadoBusca = servicoCategoria.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
            {

                string[] errosBusca = resultadoBusca.Errors.Select(x => x.Message).ToArray();

                return string.Join("\r\n", errosBusca);
            }

            var resultado = servicoCategoria.Excluir(id);

            if (resultado.IsSuccess)
                return "Categoria excluída com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }
    }
}

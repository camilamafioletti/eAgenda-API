using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private ServicoDespesa servicoDespesa;

        private ServicoCategoria servicoCategoria;

        private IMapper mapeador;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeador)
        {
            this.mapeador = mapeador;
            this.servicoDespesa = servicoDespesa;
        }

        [HttpGet]
        public List<ListarDespesaViewModel> SelecionarTodos()
        {
            var despesas = servicoDespesa.SelecionarTodos().Value;

            return mapeador.Map<List<ListarDespesaViewModel>>(despesas);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarDespesaViewModel SelecionarPorId(Guid id)
        {
            var despesa = servicoDespesa.SelecionarPorId(id).Value;

            return mapeador.Map<VisualizarDespesaViewModel>(despesa);
        }

        [HttpPost]
        public string Inserir(FormsDespesaViewModel despesaViewModel)
        {
            Despesa despesa = new Despesa
            {
                Descricao = despesaViewModel.Descricao,
                Valor = despesaViewModel.Valor,
                FormaPagamento = despesaViewModel.FormaPagamento
            };

            foreach (var c in despesaViewModel.CategoriasSelecionadas)
            {
                var resultadoBusca = servicoCategoria.SelecionarPorId(c);

                if (resultadoBusca.IsFailed)
                {

                    string[] errosBusca = resultadoBusca.Errors.Select(x => x.Message).ToArray();

                    return string.Join("\r\n", errosBusca);
                }

                var categoria = resultadoBusca.Value;

                despesa.Categorias.Add(categoria);
            }

            var resultado = servicoDespesa.Inserir(despesa);

            if (resultado.IsSuccess)
                return "Despesa inserida com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormsDespesaViewModel despesaViewModel)
        {
            var resultadoBusca = servicoDespesa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
            {

                string[] errosBusca = resultadoBusca.Errors.Select(x => x.Message).ToArray();

                return string.Join("\r\n", errosBusca);
            }

            var despesa = resultadoBusca.Value;

            despesa.Descricao = despesaViewModel.Descricao;
            despesa.Valor = despesaViewModel.Valor;
            despesa.FormaPagamento = despesaViewModel.FormaPagamento;

            despesa.Categorias.Clear();
            foreach (var c in despesaViewModel.CategoriasSelecionadas)
            {
                var resultadoBuscaCategoria = servicoCategoria.SelecionarPorId(c);

                if (resultadoBuscaCategoria.IsFailed)
                {

                    string[] errosBuscaCategoria = resultadoBuscaCategoria.Errors.Select(x => x.Message).ToArray();

                    return string.Join("\r\n", errosBuscaCategoria);
                }

                var categoria = resultadoBuscaCategoria.Value;

                despesa.Categorias.Add(categoria);
            }

            var resultado = servicoDespesa.Editar(despesa);

            if (resultado.IsSuccess)
                return "Despesa editada com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            var resultadoBusca = servicoDespesa.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
            {

                string[] errosBusca = resultadoBusca.Errors.Select(x => x.Message).ToArray();

                return string.Join("\r\n", errosBusca);
            }

            var resultado = servicoDespesa.Excluir(id);

            if (resultado.IsSuccess)
                return "Despesa excluída com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }
    }
}

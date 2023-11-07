using AutoMapper;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloContato;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompromissoController : ControllerBase
    {
        private ServicoCompromisso servicoCompromisso;

        private ServicoContato servicoContato;

        private IMapper mapeador;

        public CompromissoController(ServicoCompromisso servicoCompromisso, IMapper mapeador)
        {
            this.mapeador = mapeador;
            this.servicoCompromisso = servicoCompromisso;
        }

        [HttpGet]
        public List<ListarCompromissoViewModel> SelecionarTodos()
        {
            var compromissos = servicoCompromisso.SelecionarTodos().Value;

            return mapeador.Map<List<ListarCompromissoViewModel>>(compromissos);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarCompromissoViewModel SelecionarPorIdCompleto(Guid id)
        {
            var compromisso = servicoCompromisso.SelecionarPorId(id).Value;

            var compromissoViewModel = new VisualizarCompromissoViewModel
            {
                Id = compromisso.Id,
                Assunto = compromisso.Assunto,
                Data = compromisso.Data,
                Local = compromisso.Local,
                TipoLocal = compromisso.TipoLocal,
                Link = compromisso.Link,
                HoraInicio = compromisso.HoraInicio.ToString(@"hh\:mm\:ss"),
                HoraTermino = compromisso.HoraTermino.ToString(@"hh\:mm\:ss"),
            };


            if (compromisso.Contato != null)
            {
                var contato = compromisso.Contato;

                var contatoViewModel = new ListarContatoViewModel
                {
                    Id = contato.Id,
                    Nome = contato.Nome,
                    Cargo = contato.Cargo,
                    Empresa = contato.Empresa,
                    Email = contato.Email,
                    Telefone = contato.Telefone,
                };

                compromissoViewModel.Contato = contatoViewModel;
            }

            return compromissoViewModel;
        }

        [HttpPost]
        public string Inserir(FormsCompromissoViewModel compromissoViewModel)
        {
            var compromisso = mapeador.Map<Compromisso>(compromissoViewModel);

            var resultado = servicoCompromisso.Inserir(compromisso);

            if (resultado.IsSuccess)
                return "Compromisso inserido com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }


        [HttpPut("{id}")]
        public string Editar(Guid id, FormsCompromissoViewModel compromissoViewModel)
        {
            var compromissoEncontrado = servicoCompromisso.SelecionarPorId(id).Value;

            var compromisso = mapeador.Map(compromissoViewModel, compromissoEncontrado);

            var resultado = servicoCompromisso.Editar(compromisso);

            if (resultado.IsSuccess)
                return "Compromisso editado com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }


        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            var resultadoBusca = servicoCompromisso.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
            {

                string[] errosBusca = resultadoBusca.Errors.Select(x => x.Message).ToArray();

                return string.Join("\r\n", errosBusca);
            }

            var resultado = servicoCompromisso.Excluir(id);

            if (resultado.IsSuccess)
                return "Compromisso excluído com sucesso";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n", erros);
        }
    }
}

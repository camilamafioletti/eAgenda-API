using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApi.Config
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<IContextoPersistencia, eAgendaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            services.AddTransient<IRepositorioContato, RepositorioContatoOrm>();
            services.AddTransient<ServicoContato>();
        }
    }
}

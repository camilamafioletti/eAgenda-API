namespace eAgenda.WebApi.Config.AutomapperConfig
{
    public static class AutoMapperConfigExtension
    {
        public static void ConfigurarAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<ContatoProfile>();
                opt.AddProfile<CompromissoProfile>();
            });
        }
    }
}

using eAgenda.WebApi.Config;
using eAgenda.WebApi.Config.AutomapperConfig;

namespace eAgenda.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigurarAutoMapper();
            builder.Services.ConfigurarInjecaoDependencia(builder.Configuration);
            builder.Services.ConfigurarSwagger();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
using System;
using API_Calendario_CEC.Data;
using API_Calendario_CEC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API_Calendario_CEC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método é chamado pelo tempo de execução. Use este método para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            //Usando a string de conexão;
            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(Configuration.GetConnectionString("CalendarioConnection")));

            //Fazendo a injeção dos Services;
            services.AddScoped<InstrutorService, InstrutorService>();
            services.AddScoped<TurmaService, TurmaService>();
            services.AddScoped<LocalService, LocalService>();
            services.AddScoped<DisciplinaService, DisciplinaService>();
            services.AddScoped<PilarService, PilarService>();
            services.AddScoped<ReservaService, ReservaService>();
            services.AddScoped<AulaService, AulaService>();
            services.AddScoped<EventoService, EventoService>();

            //Injetando o AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // Este método é chamado pelo tempo de execução. Use este método para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseCors(c =>
                c.WithOrigins("*")
                 .WithMethods("PUT", "DELETE", "GET", "POST")
                 .AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

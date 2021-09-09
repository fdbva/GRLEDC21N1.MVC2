using Data.Data;
using Data.Repositories;
using Data.UoW;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crosscutting.IoC
{
    public static class Bootstrapper
    {

        public static void RegisterServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<BibliotecaContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BibliotecaContext")));

            services.AddTransient<IAutorService, AutorService>();
            services.AddTransient<IAutorRepository, AutorRepository>();
            services.AddTransient<ILivroService, LivroService>();
            services.AddTransient<ILivroRepository, LivroRepository>();
            services.AddTransient<IEstatisticaService, EstatisticaService>();
            services.AddTransient<IEstatisticaRepository, EstatisticaRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

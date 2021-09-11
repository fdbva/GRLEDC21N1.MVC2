using Application.AppServices;
using Application.AppServices.Implementations;
using Application.AutoMapper;
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

            //Application
            services.AddTransient<IAutorAppService, AutorAppService>();
            services.AddTransient<ILivroAppService, LivroAppService>();
            services.AddTransient<IEstatisticaAppService, EstatisticaAppService>();

            //Services
            services.AddTransient<IAutorService, AutorService>();
            services.AddTransient<ILivroService, LivroService>();
            services.AddTransient<IEstatisticaService, EstatisticaService>();

            //Repositories
            services.AddTransient<IAutorRepository, AutorRepository>();
            services.AddTransient<ILivroRepository, LivroRepository>();
            services.AddTransient<IEstatisticaRepository, EstatisticaRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(BibliotecaMappingProfiles));
        }
    }
}

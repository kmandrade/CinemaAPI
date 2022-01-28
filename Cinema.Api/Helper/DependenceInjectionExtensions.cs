using Data.Entities;
using Data.Repository;
using Data.Services.Handlers;
using Domain.Services.Entities;
using Serviços.Services.Entities;
using Serviços.Services.Handlers;

namespace Cinema.Api.Helper
{
    public static class DependenceInjectionExtensions
    {
        public static IServiceCollection AddDependenceInjection (this IServiceCollection services)
        {
            //INTERFACES DATA
            services.AddScoped<IFilmeDao, FilmeComEfCore>();
            services.AddScoped<IDiretorDao, DiretorComEfCore>();
            services.AddScoped<IAtorDao, AtorComEfCore>();
            services.AddScoped<IGeneroDao, GeneroComEfCore>();

            //INTERFACES SERVICES
            services.AddScoped<IFilmeService, FilmeServices>();
            services.AddScoped<IDiretorService, DiretorServices>();
            services.AddScoped<IAtorService, AtorServices>();
            services.AddScoped<IGeneroService, GeneroServices>();

            return services;
        }
    }
}

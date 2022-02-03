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
            services.AddScoped<IGeneroFilme, GeneroFilmeComEfCore>();
            services.AddScoped<IAtorFilme, AtorFilmeComEfCore>();

            //INTERFACES SERVICES
            services.AddScoped<IFilmeService, FilmeServices>();
            services.AddScoped<IDiretorService, DiretorServices>();
            services.AddScoped<IAtorService, AtorServices>();
            services.AddScoped<IGeneroService, GeneroServices>();
            services.AddScoped<IAtorFilmeService, AtorFilmeServices>();
            services.AddScoped<IGeneroFilmeService, GeneroFilmeServices>();
            

            return services;
        }
    }
}

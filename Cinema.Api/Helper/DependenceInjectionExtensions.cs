using Data.Entities;
using Data.Repository;
using Data.Services.Handlers;
using Domain.Services.Entities;

namespace Cinema.Api.Helper
{
    public static class DependenceInjectionExtensions
    {
        public static IServiceCollection AddDependenceInjection (this IServiceCollection services)
        {
            services.AddScoped<IQueryBusca, BuscaFilmePorGeneroDiretorAutor>();
            services.AddScoped<IFilmeDao, FilmeComEfCore>();
            services.AddScoped<IAdminService, DefaultAdminService>();
            return services;
        }
    }
}

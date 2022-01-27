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
            
            services.AddScoped<IFilmeDao, FilmeComEfCore>();
            services.AddScoped<IFilmeService, FilmeServices>();
            services.AddScoped<IDiretorDao, DiretorComEfCore>();
            services.AddScoped<IDiretorService, DiretorServices>();
            return services;
        }
    }
}

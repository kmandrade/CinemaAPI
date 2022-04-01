using Data.InterfacesData;
using Data.Repository;
using Data.Services.Handlers;
using Domain.Services.InterfacesService;
using Servicos.Services.Handlers;
using Servicos.Services.InterfacesService;
using Servicos.Services.Token;
using Utils.Cripitografia;

namespace Cinema.Api.Helper
{
    public static class DependenceInjectionExtensions
    {
        public static IServiceCollection AddDependenceInjection(this IServiceCollection services)
        {
            //INTERFACES DATA
            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<IDiretorRepository, DiretorRepository>();
            services.AddScoped<IAtorRepository, AtorRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IGeneroFilmeRepository, GeneroFilmeComEfCore>();
            services.AddScoped<IAtorFilmeRepository, AtorFilmeRepository>();
            services.AddScoped<IVotosRepository, VotosRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //INTERFACES SERVICES
            services.AddScoped<IFilmeService, FilmeServices>();
            services.AddScoped<IDiretorService, DiretorServices>();
            services.AddScoped<IAtorService, AtorServices>();
            services.AddScoped<IGeneroService, GeneroServices>();
            services.AddScoped<IAtorFilmeService, AtorFilmeServices>();
            services.AddScoped<IGeneroFilmeService, GeneroFilmeServices>();
            services.AddScoped<IVotosService, VotosServices>();
            services.AddScoped<IUsuarioService, UsuarioServices>();
            services.AddScoped<ITokenService, TokenService>();



            //UTILS
            services.AddScoped<IHash, Hash>();

            return services;
        }
    }
}

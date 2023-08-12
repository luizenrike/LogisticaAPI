using LogisticaAPI.ApplicationServices.Services;
using LogisticaAPI.Domain.Interfaces;
using LogisticaAPI.Infra.Repository;

namespace LogisticaAPI.Configurations
{
    public static class RepositoryConfig
    {
        public static void AddRepository(this IServiceCollection service)
        {
            service.AddTransient<IProdutoRepository, ProdutoRepository>();
            service.AddTransient<ILocalizacaoRepository, LocalizacaoRepository>();
            service.AddTransient<IRuaRepository, RuaRepository>();
            service.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
        }

        public static void AddServices(this IServiceCollection service)
        {
            service.AddTransient<ProdutoService>();
            service.AddTransient<RuaService>();
            service.AddTransient<LocalizacaoService>();
            service.AddTransient<FuncionarioService>();
        }
    }
}

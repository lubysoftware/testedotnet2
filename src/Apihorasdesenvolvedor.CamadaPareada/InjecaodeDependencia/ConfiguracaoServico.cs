using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.Desenvolvedor;
using Microsoft.Extensions.DependencyInjection;
using Apihorasdesenvolvedor.Servico.Servicos;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.DesenvolvedorXLancamentohoras;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.DesenvolvedorXProjeto;
using Apihorasdesenvolvedor.Dominio.Interfaces.Servicos.Projeto;

namespace Apihorasdesenvolvedor.CamadaPareada.InjecaodeDependencia
{
    public class ConfiguracaoServico
    {
        public static void ConfiguracaoDependenciaServico(IServiceCollection serviceColletion)
        {
            serviceColletion.AddTransient<IDesenvolvedorService, DesenvolvedorService>();
            serviceColletion.AddTransient<IDesenvolvedorXLancamentohorasService, DesenvolvedorXLancamentohorasService>();
            serviceColletion.AddTransient<IDesenvolvedorXProjetoService, DesenvolvedorXProjetoService>();
            serviceColletion.AddTransient<IProjetoService, ProjetoService>();
        }
    }
}

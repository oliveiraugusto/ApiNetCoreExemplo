using Api.Domain.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            /* 
            Referencia: https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1
            Transient, Scoped, Singleton
            
            Transient: ou Transitório, Serviços temporários de tempo de vida (AddTransient) são criados cada vez que são solicitados pelo contêiner de serviço. 
            Esse tempo de vida funciona melhor para serviços leves e sem estado.

            Scoped: ou Com Escopo, Os serviços com tempo de vida (AddScoped) com escopo são criados uma vez por solicitação de cliente (conexão).

            Singleton:  Serviços de tempo de vida singleton (AddSingleton) são criados na primeira solicitação (ou quando Startup.ConfigureServices é executado e uma instância é especificada com o registro do serviço). 
            Cada solicitação subsequente usa a mesma instância. Se o aplicativo exigir um comportamento de singleton, recomendamos permitir que o contêiner do serviço gerencie o tempo de vida do serviço. 
            Não implemente o padrão de design singleton e forneça o código de usuário para gerenciar o tempo de vida do objeto na classe.

            */
            
            serviceCollection.AddTransient<IUserService, UserService>(); 
        }
    }
}
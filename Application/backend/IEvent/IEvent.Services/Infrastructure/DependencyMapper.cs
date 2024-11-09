using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IEvent.Data.Infrastructure;
using IEvent.Services.UserServices;

namespace IEvent.Services.Infrastructure
{
  public static class DependencyMapper
  {
    public static IServiceCollection AddIEventContextAndServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
      //Initialize the IEvent context
      serviceCollection.InitializeIEventDatabaseContext(configuration);

      //Add other dependency injection services
      serviceCollection.AddScoped<IUserService, UserService>();

      return serviceCollection;
    }
  }
}

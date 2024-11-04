using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IEvent.Data.Infrastructure
{
  public static class DependencyMapper
  {
    public static IServiceCollection InitializeIEventDatabaseContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new ArgumentException("The DatabaseConnectionString is NULL or empty");
      }

      serviceCollection.AddDbContext<IEventContext>(options =>
      {
        options.UseSqlServer(connectionString);
      });

      serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
      serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

      return serviceCollection;
    }
  }
}

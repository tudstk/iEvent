using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IEvent.Data.Infrastructure;
using IEvent.Services.UserServices;
using IEvent.Services.LocationServices;
using IEvent.Services.ArtistServices;
using IEvent.Services.EventServices;
using IEvent.Services.EventTypeServices;
using IEvent.Services.GenreServices;

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
      serviceCollection.AddScoped<ILocationService, LocationService>();
      serviceCollection.AddScoped<IArtistService, ArtistService>();
      serviceCollection.AddScoped<IEventService, EventService>();
      serviceCollection.AddScoped<IEventTypeService, EventTypeService>();
      serviceCollection.AddScoped<IGenreService, GenreService>();

      return serviceCollection;
    }
  }
}

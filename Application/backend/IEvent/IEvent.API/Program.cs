using DotNetEnv;
using IEvent.Services.Infrastructure;
using IEvent.Shared.Settings;

namespace IEvent.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Env.Load();

      var builder = WebApplication.CreateBuilder(args);
      IConfiguration configuration = builder.Configuration;

      //Add the .env file variables to the configuration
      builder.Services.Configure<EnvSettings>(builder.Configuration);

      //Add controllers and swagger
      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      // Add the dbcontext and the services in the di container
      builder.Services.AddIEventContextAndServices(configuration);

      var app = builder.Build();

      app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}

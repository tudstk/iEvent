using IEvent.Services.Infrastructure;

namespace IEvent.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      IConfiguration configuration = builder.Configuration;

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

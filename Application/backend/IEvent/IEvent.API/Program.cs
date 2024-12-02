using DotNetEnv;
using IEvent.Data;
using IEvent.Data.Entities;
using IEvent.Services.Infrastructure;
using IEvent.Shared.Authentication;
using IEvent.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
      builder.Services.AddSwaggerGen(options =>
      {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.Http,
          Scheme = "Bearer"
        });
        
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
            },
            Array.Empty<string>()
          }
        });
      });

      // Add the dbcontext and the services in the di container
      builder.Services.AddIEventContextAndServices(configuration);

      // Add identity user
      builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
      {
        options.Password.RequiredLength = 5;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;

      })
        .AddEntityFrameworkStores<IEventContext>()
        .AddDefaultTokenProviders();

      builder.Services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: configuration["SecretKey"]!)),
          };
        });

      builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy("AdminPolicy", policy => policy.RequireRole(AuthRoles.Admin));
        options.AddPolicy("UserPolicy", policy => policy.RequireRole(AuthRoles.User));
        options.AddPolicy("OrganizerPolicy", policy => policy.RequireRole(AuthRoles.Organizer));
      });

      var app = builder.Build();

      app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthentication();

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }
  }
}

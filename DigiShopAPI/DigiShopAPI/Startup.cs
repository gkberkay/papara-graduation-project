using AutoMapper;
using DigiShop.Base.Log;
using DigiShop.Base.Token;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Bussiness.Mapper;
using DigiShop.Bussiness.Token;
using DigiShop.Bussiness.Validation;
using DigiShop.Data.UnitOfWork;
using DigiShopAPI.Middleware;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace DigiShopAPI;

public class Startup
{
    public IConfiguration Configuration;
    public static JwtConfig jwtConfig { get; private set; }

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.AddSingleton<JwtConfig>(jwtConfig);

        var connectionString = Configuration.GetConnectionString("MsSqlServer");
        services.AddDbContext<DigiShopDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
        services.AddSingleton(config.CreateMapper());

        services.AddScoped<ITokenService, TokenService>();

        services.AddControllers().AddFluentValidation(x =>
        {
            x.RegisterValidatorsFromAssemblyContaining<UserValidator>();
        });

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                ValidAudience = jwtConfig.Audience,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };

        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DigiShopAPI", Version = "v1" });
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "DigiShop Management for IT Company",
                Description = "Enter JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigiShopAPI v1"));
        }
        app.UseMiddleware<HeartbeatMiddleware>();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
        {
            Log.Information("-------------Request-Begin------------");
            Log.Information(requestProfilerModel.Request);
            Log.Information(Environment.NewLine);
            Log.Information(requestProfilerModel.Response);
            Log.Information("-------------Request-End------------");
        };
        app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
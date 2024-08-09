using AutoMapper;
using DigiShop.Bussiness.Cqrs;
using DigiShop.Bussiness.Mapper;
using DigiShop.Bussiness.Validation;
using DigiShop.Data.UnitOfWork;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace DigiShopAPI;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {

        //// Veritabanı bağlantı dizesini alıyoruz
        var connectionString = Configuration.GetConnectionString("MsSqlServer");
        services.AddDbContext<DigiShopDbContext>(options => options.UseSqlServer(connectionString));

        //// IUnitOfWork servisini ekliyoruz
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //// AutoMapper konfigürasyonunu ekliyoruz
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
        services.AddSingleton(config.CreateMapper());


        //// FluentValidation'ı hizmet olarak ekleyin
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


        // Swagger hizmetini ekleyin
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DigiShopAPI", Version = "v1" });
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

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
using DigiShopAPI;

namespace DigiShopAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}


//using DigiShop.Data.UnitOfWork;
//using Microsoft.EntityFrameworkCore;
//using DigiShop.Bussiness.Mapper;
//using DigiShop.Bussiness.Cqrs;
//using MediatR;
//using FluentValidation.AspNetCore;
//using DigiShop.Bussiness.Validation;
//using System.Text.Json.Serialization;
//using DigiShop.Schema;
//using FluentValidation;
//using DigiShop.Bussiness.Command;
//using AutoMapper;
//using DigiShop.Data.Domain;
//using System.Reflection;
//using Microsoft.AspNetCore.Hosting;

//var builder = WebApplication.CreateBuilder(args);

//// Veritabanı bağlantı dizesini alıyoruz
//var connectionString = builder.Configuration.GetConnectionString("MsSqlServer");

//// IUnitOfWork servisini ekliyoruz
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//// AutoMapper konfigürasyonunu ekliyoruz
////builder.Services.AddAutoMapper(typeof(MapperConfig));
//var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
//builder.Services.AddSingleton(config.CreateMapper());

//// FluentValidation'ı hizmet olarak ekleyin
//builder.Services.AddControllers()
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());
////builder.Services.AddScoped<IValidator<User>, UserValidator>();

//// MediatR'ı ekliyoruz ve assembly'yi tarıyoruz
////builder.Services.AddMediatR(typeof(Program).Assembly);

////builder.Services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());




//// FluentValidation'ı ekliyoruz
////builder.Services.AddValidatorsFromAssemblyContaining<BaseValidator>();

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//    options.JsonSerializerOptions.WriteIndented = true;
//    options.JsonSerializerOptions.PropertyNamingPolicy = null;
//});
//// FluentValidation ile AddControllers'ı ekliyoruz
////builder.Services.AddControllers().AddFluentValidation(fv =>
////{
////    fv.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
////});



////builder.Services.AddFluentValidationClientsideAdapters();

//// DbContext'i ekliyoruz
//builder.Services.AddDbContext<DigiShopDbContext>(options =>
//    options.UseSqlServer(connectionString));

//// Diğer servis konfigürasyonları
//builder.Services.AddControllers();

//// Swagger ekliyoruz
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

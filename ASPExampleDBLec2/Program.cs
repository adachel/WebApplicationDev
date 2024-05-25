
using ASPExampleDBLec2.DB;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ASPExampleDBLec2
{

    // NuGet:
    // Autofac.Extensions.DependencyInjection
    // Microsoft.EntityFrameworkCore
    // Microsoft.EntityFrameworkCore.Proxies
    // Microsoft.EntityFrameworkCore.Design
    // Npgsql.EntityFrameworkCore.PostgreSQL
    // Microsoft.AspNetCore.OpenApi

    // для redis
    // Microsoft.Extensions.Caching.StackExchangeRedis

    // Autofac – это контейнер внедрения зависимостей (Dependency Injection, DI) для платформы .NET. 
    // Регистрация зависимостей
    // Разрешение зависимостей
    // Жизненный цикл компонентов
    // Модули
    // Интеграция с различными технологиями








    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // подкл autofac



            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            ///////// для redis
            //builder.Services.AddStackExchangeRedisCache(options =>
            //{
            //    string server = "127.0.0.1";
            //    string port = "6379";
            //    string cnstring = $"{server}:{port}";
            //    options.Configuration = cnstring;
            //});
                    

            /////// для кэша
            builder.Services.AddMemoryCache(options => 
            {
                options.TrackStatistics = true; // собирает статистику
            }); // для кэширования




            // билдим конфигурацию
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.Register(c => new AppDbContext(cfg.GetConnectionString("db"))).InstancePerDependency(); 
                // "db" - ключ в appsettings.json
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // для работы со статичными файлами
            //var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
            //Directory.CreateDirectory(staticFilesPath);

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(staticFilesPath),
            //    RequestPath = "/static"
            //});   


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

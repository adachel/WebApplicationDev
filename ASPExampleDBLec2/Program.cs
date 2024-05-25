
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

    // ��� redis
    // Microsoft.Extensions.Caching.StackExchangeRedis

    // Autofac � ��� ��������� ��������� ������������ (Dependency Injection, DI) ��� ��������� .NET. 
    // ����������� ������������
    // ���������� ������������
    // ��������� ���� �����������
    // ������
    // ���������� � ���������� ������������








    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // ����� autofac



            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            ///////// ��� redis
            //builder.Services.AddStackExchangeRedisCache(options =>
            //{
            //    string server = "127.0.0.1";
            //    string port = "6379";
            //    string cnstring = $"{server}:{port}";
            //    options.Configuration = cnstring;
            //});
                    

            /////// ��� ����
            builder.Services.AddMemoryCache(options => 
            {
                options.TrackStatistics = true; // �������� ����������
            }); // ��� �����������




            // ������ ������������
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.Register(c => new AppDbContext(cfg.GetConnectionString("db"))).InstancePerDependency(); 
                // "db" - ���� � appsettings.json
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ��� ������ �� ���������� �������
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

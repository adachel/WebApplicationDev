
using ASPExampleDBLec2.DB;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace ASPExampleDBLec2
{

    // NuGet:
    // Autofac.Extensions.DependencyInjection
    // Microsoft.EntityFrameworkCore
    // Microsoft.EntityFrameworkCore.Proxies
    // Microsoft.EntityFrameworkCore.Design
    // Npgsql.EntityFrameworkCore.PostgreSQL
    // Microsoft.AspNetCore.OpenApi

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

            builder.Services.AddMemoryCache(); // ��� �����������

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

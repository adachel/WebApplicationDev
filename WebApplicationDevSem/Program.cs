
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.Mapping;
using WebApplicationDevSem.Repo;

namespace WebApplicationDevSem
{
    // nuGet:
    // Microsoft.EntityFrameworkCore
    // Microsoft.EntityFrameworkCore.Design
    // Microsoft.EntityFrameworkCore.Proxies
    // Microsoft.EntityFrameworkCore.SqlServ
    // Npgsql.EntityFrameworkCore.Postgre
    // Autofac.Extensions.DependencyInject
    // AutoMapper


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(MappingProfile)); // 

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache(x => x.TrackStatistics = true); //

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // подключили Autofac
            builder.Host.ConfigureContainer<ContainerBuilder>(x => 
            { 
                x.RegisterType<ProductRepo>().As<IPoductRepo>();
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

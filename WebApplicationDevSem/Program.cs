
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WebApplicationDevSem.Abstraction;
using WebApplicationDevSem.DB;
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

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // подключили Autofac



            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(MappingProfile)); // 

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache(x => x.TrackStatistics = true); // кэш




            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(x => 
            { 
                x.RegisterType<ProductRepo>().As<IPoductRepo>();
                x.RegisterType<ProductGroupRepo>().As<IProductGroupRepo>();
                x.Register(c => new ProductContext(cfg.GetConnectionString("db"))).InstancePerDependency();
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


using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lec3LibraryApi.DB;
using Lec3LibraryApi.DTO.Map;
using Lec3LibraryApi.Repo;

namespace Lec3LibraryApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            ////////////////// сейчас исп не стали, заменив cfg на builder.Configuration в строке с Register, 
            ///т.к. appsettings находится в проекте
            ///
            //var config = new ConfigurationBuilder();
            //config.AddJsonFile("appsettings.json");
            //var cfg = config.Build();

            builder.Services.AddAutoMapper(typeof(MappingProFile));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<LibraryRepo>().As<ILibraryRepo>();
                containerBuilder.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });
            //////////////////////



            var app = builder.Build();

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

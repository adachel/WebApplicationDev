
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lec3ApiClientsBook.DB;
using Lec3ApiClientsBook.DTO;
using Lec3ApiClientsBook.Repo;

namespace Lec3ApiClientsBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProFile));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<ClientBookRepo>().As<IClientBookRepo>();
                containerBuilder.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });



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

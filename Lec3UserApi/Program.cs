
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lec3UserApi.DB;
using Lec3UserApi.DTO;
using Lec3UserApi.Repo;

namespace Lec3UserApi
{
    public class Program
    {
        public static WebApplication BuildWebApp(string[] args)
        { 
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProFile));

            var config = new ConfigurationBuilder(); // это все, если нужно загрузить стороннию конфигурацию
            config.AddJsonFile("appsettings.json");  //
            var cfg = config.Build();                //

            //builder.Configuration.GetConnectionString("db"); // своя конфигурация

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(cd =>
            { 
                cd.RegisterType<UserRepository>().As<IUserRepository>();
                cd.Register(c => new AppDBContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            return builder.Build();
        }
        public static void Main(string[] args)
        {
            

            var app = BuildWebApp(args);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // что бы грамотно отрабатывал поле DateTime

            app.Run();
        }
    }
}

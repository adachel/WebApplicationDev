
using ASPDepLec2.Util;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace ASPDepLec2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<Fibonacci>().As<IFibonacci>().InstancePerLifetimeScope();
            }); // конфигур ContainerBuilder. регистрируем класс фибо в контейнере.
                // через Host обращаемся к объекту, кот конфиг контейнер.
                // ContainerBuilder - из autofac. InstancePerLifetimeScope - время жизни типа.

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


using Microsoft.EntityFrameworkCore;
using Sem4SecurityMarket.Context;

namespace Sem4SecurityMarket
{
    // Создать каталог RSA
    // openssl genpkey -algorithm RSA -out private_key.pem  -- получаем private_key.pem в RSA
    // openssl rsa -pubout -in private_key.pem -out public_key.pem  -- получаем public_key.pem в RSA
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ///
            builder.Services.AddDbContext<AuthContext>(opt =>
            {
                opt.UseNpgsql(builder.Configuration.GetConnectionString("defaultConnectionString"));
            });
            // сделать миграции
            



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

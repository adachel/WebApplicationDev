
using Lec4JWTAuth.AuthorizatoinModel;
using Lec4JWTAuth.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;

namespace Lec4JWTAuth
{
    public class Program
    {
        // Для windows нужно установить OpenSSL (например https://wiki.openssl.org/index.php/Binaries).
        // Открываем каталог RSA(нужно создать) в терминале и командами:
        // openssl genpkey -algorithm RSA -out private_key.pem - генерирует приватный ключ
        //                                                       и сохраняет его в каталог под именем private_key.pem
        // openssl rsa -pubout -in private_key.pem -out public_key.pem - получает публичный ключ на основе информации,
        //                                                               содержащейся в файла private_key.pem,
        //                                                               и сохраняет ее в файл public_key.pem

        public static RSA GetPublicKey()   // для RSA
        {
            var f = File.ReadAllText("RSA/public_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(f);
            return rsa;
        }




        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen(); // вместо этого код ниже

            //////////////////////////////////////////////

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            //builder.Services.AddScoped<IUserAuthenticationService, AuthenticationMock>(); // экземпляр на все объекты одного запроса
            //builder.Services.AddSingleton<IUserAunthenticationService, AunthenticationMock>(); // экземпляр на весь жизненный цикл приложения
            builder.Services.AddTransient<IUserAuthenticationService, AuthenticationMock>(); // на каждый экземпляр свой объект


            

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters  // указываем, как валидируем токен
                {
                    ValidateIssuer = true, // Validate - проверяем
                    ValidateAudience = true,
                    ValidateLifetime = true, // срок действия токена
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"], // доступ к параметрам
                    ValidAudience = builder.Configuration["Jwt:Audience"],


                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) 
                                                                        // полчаем ключ, кот будем проверять. Для HmacSha256

                    IssuerSigningKey = new RsaSecurityKey(GetPublicKey()) // для RSA

                };
            });




            builder.Services.AddSwaggerGen(opt =>  // учим свагер работать с токенами
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // как клиенты должны предоставлять токен
                {
                    In = ParameterLocation.Header, // Указывает, что токен должен передаваться в заголовке HTTP(Header).
                    Description = "Please enter token", // Пояснение, как предоставить токен.
                    Name = "Authorization", // Имя заголовка, в котором будет передаваться токен (обычно"Authorization").
                    Type = SecuritySchemeType.Http, // Тип схемы безопасности (SecuritySchemeType.Http).
                    BearerFormat = "Token", // Формат токена (обычно "Token").

                    Scheme = "bearer" // Название схемы (обычно "bearer").
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement // указывает, что все запросы должны использовать схему
                                                                          // безопасности "Bearer".Это гарантирует,
                                                                          // что каждый запрос к API должен предоставить
                                                                          // токен авторизации
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });














            /////////////////////////////

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //////////////////////////////
            app.UseAuthentication(); // именно в таком порядке
            app.UseAuthorization();
            ///////////////////////////////

            app.MapControllers();

            app.Run();
        }
    }
}

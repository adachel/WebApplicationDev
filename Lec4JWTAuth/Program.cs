
using Lec4JWTAuth.AuthorizatoinModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Lec4JWTAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen(); // вместо этого код ниже

            builder.Services.AddScoped<IUserAuthenticationService, AuthenticationMock>(); // экземпл€р на все объекты одного запроса
            //builder.Services.AddSingleton<IUserAunthenticationService, AunthenticationMock>(); // экземпл€р на весь жизненный цикл приложени€
            //builder.Services.AddTransient<IUserAuthenticationService, AuthenticationMock>(); // на каждый экземпл€р свой объект


            builder.Services.AddSwaggerGen(opt =>  // учим свагер работать с токенами
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Token"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()  // указываем, как валидируем токен
                {
                    ValidateIssuer = true, // Validate - провер€ем
                    ValidateAudience = true,
                    ValidateLifetime = true, // срок действи€ токена
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt: Issuer"],
                    // builder.Configuration["Jwt: Issuer"] - способ доступа к параметрам
                    ValidAudience = builder.Configuration["Jwt: Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes("ze0wwDowm6P6EPzv7IaeVf7nCbUG1CPVm35pLsxgN5oq"))
                    // полчаем ключ, кот будем провер€ть
                };
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
            app.UseAuthentication(); // именно в таком пор€дке
            app.UseAuthorization();
            ///////////////////////////////

            app.MapControllers();

            app.Run();
        }
    }
}


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
            //builder.Services.AddSwaggerGen(); // ������ ����� ��� ����

            builder.Services.AddScoped<IUserAuthenticationService, AuthenticationMock>(); // ��������� �� ��� ������� ������ �������
            //builder.Services.AddSingleton<IUserAunthenticationService, AunthenticationMock>(); // ��������� �� ���� ��������� ���� ����������
            //builder.Services.AddTransient<IUserAuthenticationService, AuthenticationMock>(); // �� ������ ��������� ���� ������


            builder.Services.AddSwaggerGen(opt =>  // ���� ������ �������� � ��������
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
                opt.TokenValidationParameters = new TokenValidationParameters()  // ���������, ��� ���������� �����
                {
                    ValidateIssuer = true, // Validate - ���������
                    ValidateAudience = true,
                    ValidateLifetime = true, // ���� �������� ������
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt: Issuer"],
                    // builder.Configuration["Jwt: Issuer"] - ������ ������� � ����������
                    ValidAudience = builder.Configuration["Jwt: Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes("ze0wwDowm6P6EPzv7IaeVf7nCbUG1CPVm35pLsxgN5oq"))
                    // ������� ����, ��� ����� ���������
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
            app.UseAuthentication(); // ������ � ����� �������
            app.UseAuthorization();
            ///////////////////////////////

            app.MapControllers();

            app.Run();
        }
    }
}

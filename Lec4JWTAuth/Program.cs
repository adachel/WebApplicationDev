
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
        // ��� windows ����� ���������� OpenSSL (�������� https://wiki.openssl.org/index.php/Binaries).
        // ��������� ������� RSA(����� �������) � ��������� � ���������:
        // openssl genpkey -algorithm RSA -out private_key.pem - ���������� ��������� ����
        //                                                       � ��������� ��� � ������� ��� ������ private_key.pem
        // openssl rsa -pubout -in private_key.pem -out public_key.pem - �������� ��������� ���� �� ������ ����������,
        //                                                               ������������ � ����� private_key.pem,
        //                                                               � ��������� �� � ���� public_key.pem

        public static RSA GetPublicKey()   // ��� RSA
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
            //builder.Services.AddSwaggerGen(); // ������ ����� ��� ����

            //////////////////////////////////////////////

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            //builder.Services.AddScoped<IUserAuthenticationService, AuthenticationMock>(); // ��������� �� ��� ������� ������ �������
            //builder.Services.AddSingleton<IUserAunthenticationService, AunthenticationMock>(); // ��������� �� ���� ��������� ���� ����������
            builder.Services.AddTransient<IUserAuthenticationService, AuthenticationMock>(); // �� ������ ��������� ���� ������


            

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters  // ���������, ��� ���������� �����
                {
                    ValidateIssuer = true, // Validate - ���������
                    ValidateAudience = true,
                    ValidateLifetime = true, // ���� �������� ������
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"], // ������ � ����������
                    ValidAudience = builder.Configuration["Jwt:Audience"],


                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) 
                                                                        // ������� ����, ��� ����� ���������. ��� HmacSha256

                    IssuerSigningKey = new RsaSecurityKey(GetPublicKey()) // ��� RSA

                };
            });




            builder.Services.AddSwaggerGen(opt =>  // ���� ������ �������� � ��������
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // ��� ������� ������ ������������� �����
                {
                    In = ParameterLocation.Header, // ���������, ��� ����� ������ ������������ � ��������� HTTP(Header).
                    Description = "Please enter token", // ���������, ��� ������������ �����.
                    Name = "Authorization", // ��� ���������, � ������� ����� ������������ ����� (������"Authorization").
                    Type = SecuritySchemeType.Http, // ��� ����� ������������ (SecuritySchemeType.Http).
                    BearerFormat = "Token", // ������ ������ (������ "Token").

                    Scheme = "bearer" // �������� ����� (������ "bearer").
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement // ���������, ��� ��� ������� ������ ������������ �����
                                                                          // ������������ "Bearer".��� �����������,
                                                                          // ��� ������ ������ � API ������ ������������
                                                                          // ����� �����������
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
            app.UseAuthentication(); // ������ � ����� �������
            app.UseAuthorization();
            ///////////////////////////////

            app.MapControllers();

            app.Run();
        }
    }
}

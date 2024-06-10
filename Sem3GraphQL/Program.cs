
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Sem3GraphQL.Abstraction;
using Sem3GraphQL.DB;
using Sem3GraphQL.GraphServises.Mutation;
using Sem3GraphQL.GraphServises.Query;
using Sem3GraphQL.Mapping;
using Sem3GraphQL.Repo;


// http://localhost:18000/graphql/

namespace Sem3GraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddSingleton<ProductRepo>()
                            .AddSingleton<ProductGroupRepo>()
                            .AddSingleton<StorageRepo>()
                            .AddSingleton<ProductStorageRepo>()
                            .AddGraphQLServer()
                            .AddQueryType<Query>()
                            .AddMutationType<Mutation>();
            
            builder.Services.AddSingleton<IProductRepo, ProductRepo>();
            builder.Services.AddSingleton<IProductGroupRepo, ProductGroupRepo>();

            builder.Services.AddSingleton<IStorageRepo, StorageRepo>();
            builder.Services.AddSingleton<IProductStorageRepo, ProductStorageRepo>();

            builder.Host.ConfigureContainer<ContainerBuilder>(x =>
            {
                x.Register(c => new ProductContext(builder.Configuration
                                                          .GetConnectionString("db")!))
                                                          .InstancePerDependency();
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGraphQL();

            app.Run();
        }
    }
}

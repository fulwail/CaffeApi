using Caffe.Api.Domain.Interceptors;
using Caffe.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Polly;
using System.Data.Common;
using System.Reflection;

namespace Caffe.Api.Domain.Extensions
{
    public static class ContextExtensions
    {
        public static IServiceCollection InitEfDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            void BuildOptions(NpgsqlDbContextOptionsBuilder npgsqlOptions)
            {
                npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: new List<string>());
            }

            services.AddDbContext<ICaffeContext,CaffeContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions =>
                    {
                        BuildOptions(npgsqlOptions);
                        npgsqlOptions.MigrationsAssembly(typeof(ContextExtensions).GetTypeInfo().Assembly.GetName().Name);
                    }
                )
                .AddInterceptors(new SafeDeleteInterceptor());
            });
            AddRepositories(services);
            return services;
        }

        public static async Task<IHost> MigrateEfDbContext(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = ServiceProviderServiceExtensions.GetService<CaffeContext>(services);

                var retry = Policy.Handle<DbException>()
                    .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    });

                await retry.ExecuteAsync(async () => {
                    await context.Database.MigrateAsync();   
                });
                
            }

            return host;
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMenuProductRepository,MenuProductRepository>();
        }

    }
}

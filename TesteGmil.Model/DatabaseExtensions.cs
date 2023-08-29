using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TesteGmil.Model.Context;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<TestGmilContext>(options => options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TestGmilContext).Assembly.FullName)));

            return services;
        }
    }
}

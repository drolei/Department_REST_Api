using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepartmentApi.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

    }
}

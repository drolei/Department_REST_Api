using DepartmentApi.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentApi.Services.Extensions
{
    public static class Extensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

        }
    }
}

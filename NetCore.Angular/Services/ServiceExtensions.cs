using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Angular.Services
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddNetCoreAngular(this IServiceCollection services, 
                                            Action<AngularServiceOptions> configure = null)
        {

            services.AddScoped<AngularService>();
            var options = new AngularServiceOptions {
            };
            configure?.Invoke(options);
            services.AddSingleton(options);
            return services;
        }

    }
}

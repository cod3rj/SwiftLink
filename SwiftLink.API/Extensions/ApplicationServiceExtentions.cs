using System.Globalization;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SwiftLink.API.Core;
using SwiftLink.API.Database;
using SwiftLink.API.Features.Url;
using SwiftLink.API.Services;

namespace SwiftLink.API.Extensions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
                });
            });

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

            services.AddFluentValidationAutoValidation();

            services.AddHttpContextAccessor();

            services.AddScoped<UrlService>();

            return services;
        }
    }
}
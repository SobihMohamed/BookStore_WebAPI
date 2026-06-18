using Application.Abstraction.Authentication;
using Application.Common.Behaviors;
using Application.DTOs.Auth;
using Application.Features.Auth.Commands.RegisterCustomer;
using Application.Services.Authentication;
using FluentValidation;
using MediatR;

namespace BookStore_Web.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtTokenSettings"));

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddValidatorsFromAssembly(typeof(RegisterCustomerCommandValidator).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
using BookStore_Web.API.Extensions;
using BookStore_Web.API.Middlewares;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace BookStore_Web.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Database Connection
            builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Inject Custom Extensions (Identity, Rate Limiting, CORS, JWT)
            builder.Services.InjectIdentityCore();
            builder.Services.InjectRateLimiting();
            builder.Services.AddCustomCors(builder.Configuration);
            builder.Services.AddJwtAuthentication(builder.Configuration, builder.Environment);

            // 3. Controllers & Swagger
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // Custom Exception Handling
            builder.Services.AddCustomExceptionHandling();

            var app = builder.Build();
            // Use Custom Exception Handling Middleware
            app.UseCustomExceptionHandling();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // 4. Middlewares Pipeline 
            app.UseRateLimiter(); 

            app.UseCors("CorsPolicy"); 

            app.UseAuthentication(); 
            app.UseAuthorization();  

            app.MapControllers();
            await app.SeedDatabaseAsync();
            app.Run();
        }
    }
}
using PzuCwiczenia.Infrastructure.ServiceInterfaces;
using PzuCwiczenia.Services.Books;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace PzuCwiczenia.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
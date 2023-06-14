using PzuCwiczenia.Infrastructure.ServiceInterfaces;
using PzuCwiczenia.Services.Books;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Book Api",
                    Description = "Api dostêpowe do serwisu Bibliotekarz",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Leszek Lewandowski",
                        Email = "leszek.lewandowski@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/leszek-lewandowski-nadalasc/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example licence",
                        Url = new Uri("https://example.com/licence")
                    }
                });

                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    //options.InjectStylesheet("/swagger-ui/custom.css");
                });
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
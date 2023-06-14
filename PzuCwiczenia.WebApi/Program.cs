using System.Diagnostics;

namespace PzuCwiczenia.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            //app.MapControllers();

            app.Use(async (context, next) =>
            {
                Debug.WriteLine($"1. Enpoint: {context.GetEndpoint()?.DisplayName ?? (null)}");
                await next();
            });

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                Debug.WriteLine($"2. Enpoint: {context.GetEndpoint()?.DisplayName ?? (null)}");
                await next();
            });

            app.MapGet("/", (HttpContext context) =>
            {
                Debug.WriteLine($"3. Enpoint: {context.GetEndpoint()?.DisplayName ?? (null)}");
                return "Hello world";
            }).WithDisplayName("Hello");

            app.UseEndpoints(route =>
                route.MapControllerRoute("Default", "{controller=Home}/{action=Get}/{id:int?}")
            );

            app.Use(async (context, next) =>
            {
                Debug.WriteLine($"4. Enpoint: {context.GetEndpoint()?.DisplayName ?? (null)}");
                await next();
            });

            app.Run();
        }
    }
}
using PzuCwiczenia.Infrastructure.ServiceInterfaces;
using PzuCwiczenia.Services.Books;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using PzuCwiczenia.WebApi.Authentication;
using PzuCwiczenia.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PzuCwiczenia.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Basic Authentication
            //builder.Services.AddAuthentication("BasicAuthentication")
            //    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // JwtBearer Authentication
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<Infrastructure.ServiceInterfaces.IAuthenticationService, AuhtenticationService>();
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

                //option.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                //{
                //    Name = "Authentication",
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "basic",
                //    In = ParameterLocation.Header,
                //    Description = "Uwierzytelnianie metod¹ Basic Authentication"
                //});

                //option.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "basic"
                //            }
                //        },
                //        Array.Empty<string>()
                //    }
                //});

                option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header,
                    Description = "Uwierzytelnianie metod¹ JWT Token"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
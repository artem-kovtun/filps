using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Filps.Application.Factories;
using Filps.Application.Factories.Contracts;
using Filps.Application.Requests.Users.Queries;
using Filps.Blob.Contracts;
using Filps.Blob.Engines;
using Filps.Blob.Models;
using Filps.Domain.Configurations;
using Filps.Domain.Repositories;
using Filps.Domain.Repositories.Contracts;
using Filps.GoogleServices.Contacts;
using Filps.GoogleServices.Engines;
using Filps.GoogleServices.Models;
using Filps.Helpers.Engines;
using Filps.Helpers.Engines.Contracts;
using Filps.Security.Contracts;
using Filps.Security.Engines;
using Filps.Security.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Filps
{
    public static class Bootstrapper
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            
            services.AddMediatR(typeof(GetUserAuthenticationQuery).GetTypeInfo().Assembly);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddCors(options =>
            {
                options.AddPolicy("Default", 
                    builder => builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
            
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = ".FILPS.Session";
            });
            
            services.AddControllers();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddGoogle(options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClientId = "759758100252-jdqv2p65fa3n2k9tdpargn72lcoe5id2.apps.googleusercontent.com";
                    options.ClientSecret = "qup54dbuAitag4H5dZhvN1tg";
                    options.Scope.Add("profile");
                    options.Scope.Add("https://www.googleapis.com/auth/drive");
                    options.SaveTokens = true;
                    options.Events.OnCreatingTicket = context =>
                    {
                        context.Identity.AddClaim(new Claim("image", context.User.GetProperty("picture").ToString()!));
                        return Task.CompletedTask;
                    };
                });

            services.AddAuthorization();
            
            services.AddSpaStaticFiles(c =>
            {
                c.RootPath = "ClientApp/dist";
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FILPS API", Version = "V1" });
            });
            
            services.BindOptions(configuration);
            services.AddRepositories();
            services.AddEngines();
            services.AddFactories();
        }
        
        private static void BindOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleCredentials>(configuration.GetSection("GoogleCredentials"));
            services.Configure<BlobConfiguration>(configuration.GetSection("BlobConfiguration"));
            services.Configure<DatabaseConfiguration>(configuration.GetSection("DatabaseConfiguration"));
            services.Configure<KeyInformation>(configuration.GetSection("AesEncryptionKeys"));
        }

        private static void AddEngines(this IServiceCollection services)
        {
            services.AddScoped<ISessionEngine, SessionEngine>();
            services.AddScoped<IGoogleDriveEngine, GoogleDriveEngine>();
            services.AddScoped<IGoogleCredentialEngine, GoogleCredentialEngine>();
            services.AddScoped<IGoogleProfileEngine, GoogleProfileEngine>();
            services.AddScoped<IBlobStorageEngine, BlobStorageEngine>();
            services.AddScoped<IAesEncryptionEngine, AesEncryptionEngine>();
        }
        
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFileRepository, FileRepository>();
        }

        private static void AddFactories(this IServiceCollection services)
        {
            services.AddTransient<IDataStoreFactory, DataStoreFactory>();
        }

    }
}
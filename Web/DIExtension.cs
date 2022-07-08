using Api.Contracts;
using DataAccess;
using DataAccess.Context;
using DataAccess.Contracts;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;
using Sieve.Services;
using Business;
using Business.Businesses;
using Model;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace Web
{
    internal static class DIExtension

    {
        internal static IServiceCollection InjectRazorAndApi(this IServiceCollection services) =>
        services.AddRazorPages()
            .Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            })
            .AddApplicationPart(typeof(IBaseController<>).Assembly)
            .Services
            .AddHealthChecks()
            .Services;
            

        internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services)=>
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        internal static IServiceCollection InjectContext(this IServiceCollection services) =>
  services.AddDbContextPool<Context>(options =>
          options.UseInMemoryDatabase("stack"));

        internal static IServiceCollection Injectsieve(this IServiceCollection services) =>
            services.AddScoped<ISieveProcessor, SieveProcessor>();

        internal static IServiceCollection InjectAuthentication(this IServiceCollection services) =>
            services.
            AddAuthorization()
            .AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = option.DefaultChallengeScheme =
                CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(option =>
            {
                option.LoginPath = "/User/Login";
                option.LogoutPath = "User/Logout";
                option.ExpireTimeSpan = TimeSpan.FromDays(1);
                option.AccessDeniedPath = "User/AccessDeny";
            }).Services;

        internal static IServiceCollection InjectBusiness(this IServiceCollection services) =>
            services.AddScoped<IBaseBusiness<User>, UserBusiness>()
            .AddScoped<IBaseBusiness<Post>, PostBusiness>()
            .AddScoped<IBaseBusiness<Comment>, CommentBusiness>();

        internal static IServiceCollection InjectContentCompression(this IServiceCollection services) =>
            services.Configure<GzipCompressionProviderOptions>
            (options => options.Level = System.IO.Compression.CompressionLevel.Fastest)
            .AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());

        internal static IServiceCollection InjectNLog(this IServiceCollection services,
        IWebHostEnvironment environment)
        {
            var factory = NLogBuilder.ConfigureNLog(
                    environment.IsProduction()
                        ? "nlog.config"
                        : $"nlog.{environment.EnvironmentName}.config");
            return services.AddSingleton(_ => factory.GetLogger("Info"))
                .AddSingleton(_ => factory.GetLogger("Error"));
        }




    }
}

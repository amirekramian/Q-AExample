using Api.Contracts;
using DataAccess;
using DataAccess.Context;
using DataAccess.Contracts;
using System.Text.Json.Serialization;




namespace Web
{
    internal static class DIExtension
    {
        internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services)=>
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        internal static IServiceCollection InjectContext(this IServiceCollection services) =>
            services.AddDbContextPool<Context>(option =>
            option.useinmem




    }
}

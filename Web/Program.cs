using DataAccess;
using DataAccess.Context;
using DataAccess.Contracts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore;
using Web;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(config =>
    config.SetBasePath(Directory.GetCurrentDirectory()).AddEnvironmentVariables());
// Add services to the container.
//builder.Services.AddRazorPages();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Host.ConfigureLogging(x => x.ClearProviders().SetMinimumLevel(LogLevel.Trace));
string connectionstring = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<Context>(option =>
{
    option.UseSqlServer(connectionstring);
});

builder.Host.ConfigureLogging(x => x.ClearProviders().SetMinimumLevel(LogLevel.Trace));

builder.Host.UseNLog();

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    builder.Services
        .InjectRazorAndApi()
        .InjectBusiness()
        .InjectUnitOfWork()
        .Injectsieve()
        .InjectContentCompression()
        .InjectAuthentication()
        .AddEndpointsApiExplorer();

    var app = builder.Build();

    await using var scope = app.Services.CreateAsyncScope();

    await using var context = scope.ServiceProvider.GetRequiredService<Context>();

    await context.Database.EnsureCreatedAsync();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

     app.UseEndpoints(endpoints =>
      {
          if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
              endpoints.MapHealthChecks("HealthCheck");
          endpoints.MapControllers();
          endpoints.MapRazorPages().RequireAuthorization();
      });
    logger.Debug("init main");
    app.RunAsync();
}
catch (Exception exception)
{
    logger.Error(exception, "Program Stopped Because of Exception !");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();

}




// Configure the HTTP request pipeline.


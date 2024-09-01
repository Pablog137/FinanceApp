using Finance.API.Data;
using Finance.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;


try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
      .ReadFrom.Configuration(builder.Configuration)
      .CreateLogger();

    Log.Information("Starting web application");

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwagger();
    builder.Services.AddDbContext(builder.Configuration);
    builder.Services.AddIdentity();
    builder.Services.AddJwtAuth(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.AddRepositories();
    builder.Services.AddSerilog();
    builder.Services.AddHealthCheck();

    var app = builder.Build();

    app.MapHealthChecks("/health");

    // Create Database
    //using (var scope = app.Services.CreateScope())
    //{
    //    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //    dbContext.Database.Migrate();
    //}

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}

namespace Finance.API
{
    public partial class Program { }
}
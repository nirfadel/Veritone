using CRMBilling.Core.DB;
using CRMBilling.Core.Repository;
using CRMBilling.Core.Services;
using CRMBilling.Core.Utils.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICRMBillingService, CRMBillingService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IErrorHandler, ErrorHandler>();
builder.Services.AddDbContext<CRMBillingDBContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddLogging(builder =>
 {
     builder.ClearProviders();
     builder.AddConsole();
     builder.AddDebug();
 });

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("conn");
    options.SchemaName = "dbo";
    options.TableName = "DistributedCache";
});

var app = builder.Build();
app.UseCors(configurePolicy: policy =>
{
    policy.WithOrigins(builder.Configuration["AllowedOrigins"]);
}); app.UseSerilogRequestLogging();
app.MapControllers();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.Run();


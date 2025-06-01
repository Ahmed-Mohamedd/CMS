using System.Reflection;
using CMS.Application.Common.Behaviors;
using CMS.Application.Common.Exceptions.Handler;
using CMS.Application.Features.Leaves.Utilities;
using CMS.Application.Features.Persons.Commands.CreatePerson;
using CMS.Application.Features.Persons.Mapping;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using CMS.Infrastructure.Repositories;
using Hangfire;
using HealthChecks.UI.Client;
using Mapster;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Hangfire services
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("MSSQlConnection")));

builder.Services.AddHangfireServer();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQlConnection"));
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IApplicationDbContext), typeof(ApplicationDbContext));

TypeAdapterConfig.GlobalSettings.Scan(typeof(PersonMappingConfig).Assembly); // regitser (mapster) mapping profiles

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreatePersonCommand).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHealthChecks()    // adding health checks for mssqldb
    .AddSqlServer(builder.Configuration.GetConnectionString("MSSQlConnection"));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddScoped<LeaveBalanceResetJob>();

var app = builder.Build();

// Enable Hangfire dashboard 
app.UseHangfireDashboard();

// Schedule the job to run yearly on July 1st at midnight
RecurringJob.AddOrUpdate<LeaveBalanceResetJob>(
    "reset-annual-leaves",
    job => job.ResetAnnualLeaveBalancesAsync(),
    "0 0 1 7 *", // Cron expression
    new RecurringJobOptions
    {
        TimeZone = TimeZoneInfo.Local
    }
);

#region Update db

using var scope = app.Services.CreateScope();     
        var services = scope.ServiceProvider;            

        var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            //Ask CLR To Create Object From ApplicationDbContext Explicitly
            var DbContext = services.GetRequiredService<ApplicationDbContext>();

            await DbContext.Database.MigrateAsync();
            await SeedingData.SeedAsync(DbContext, LoggerFactory);

        }
        catch (Exception ex)
        {
            var logger = LoggerFactory.CreateLogger<Assembly>();
            logger.LogError(ex, "An Error Occurred While Updating Database");
        }
        
    #endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler( options => { });

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

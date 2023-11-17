using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using FinekraApi.Core.Entities;
using FinekraApi.Core.Services;
using FinekraApi.Core.Interfaces;
using FinekraApi.Core.Repositories;
using Microsoft.OpenApi.Models;
using FinekraApi.Core;
using Microsoft.AspNetCore.OData;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.File;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.
builder.Services.AddDbContext<PerfumeDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers().AddOData(opt =>
{
    opt.Count().Filter().OrderBy().Expand().Select().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinekraApi", Version = "v1" });
});

builder.Services.AddScoped<IPerfumeRepository, PerfumeRepository>();
builder.Services.AddScoped<IPerfumeService, PerfumeService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Configure Serilog for logging
builder.Logging.AddSerilog(Log.Logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinekraApi V1");
        c.RoutePrefix = "swagger";
    });

    app.UseODataRouteDebug(); // OData Route Debugging
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

IEdmModel GetEdmModel()
{
    var builder = new Microsoft.OData.ModelBuilder.ODataConventionModelBuilder();
    builder.EntitySet<Perfumes>("Perfumes");
    builder.EntitySet<Orders>("Orders");

    var edmModel = builder.GetEdmModel();
    if (edmModel != null)
    {
        edmModel.SetEdmVersion(new Version(4, 0));
    }

    return edmModel;
}

using ApplicationLayer;
using AutoClassLibrary.Context;
using AutoClassLibrary.Repository;
using AutoMapperDemo.SwaggerHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using static ApplicationLayer.ApplicationLayerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddApplicationServices();
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

//builder.Services.AddApiVersioning(options => {
    //options.ReportApiVersions = true;
    //options.AssumeDefaultVersionWhenUnspecified = true;
    //options.DefaultApiVersion = new ApiVersion(2, 0);

    // Specify additional API versions
   // options.ApiVersionReader = new Microsoft.AspNetCore.Mvc.Versioning.QueryStringApiVersionReader("version");
    //options.AssumeDefaultVersionWhenUnspecified = true;
    //options.ReportApiVersions = true;
    //options.DefaultApiVersion = new ApiVersion(1, 0);

//}
//);
//IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

//builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerVersionedApiExplorer();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/myBeautifulLog-.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();

    IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(
        options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

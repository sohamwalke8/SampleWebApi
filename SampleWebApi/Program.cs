using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SampleWebApi.Application.Implementation;
using SampleWebApi.Application.Interface;
using SampleWebApi.DAL;
using SampleWebApi.GlobalExceptionHandler;
using SampleWebApi.Models;
using SampleWebApi.Repository;
using SampleWebApi.Utility;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;



builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});




builder.Services.AddControllers();
builder.Services.AddScoped<IGenericInterface,GenericRepository>();
//builder.Services.AddScoped<AuthIGenericRepository, AuthGenericRepository>();



builder.Services.AddScoped(typeof(AuthIGenericRepository<>), typeof(AuthGenericRepository<>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<CustomeException>();



//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//// Adding Jwt Bearer
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ClockSkew = TimeSpan.Zero,

//        ValidAudience = configuration["JWT:ValidAudience"],
//        ValidIssuer = configuration["JWT:ValidIssuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
//    };
//});




builder.Services.AddScoped(typeof(AuthIGenericRepository<Student>), typeof(AuthGenericRepository<Student>));

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.Console()
//    .WriteTo.File("logs/myBeautifulLog-.txt", rollingInterval: RollingInterval.Day)
//.CreateLogger();
builder.Services.AddScoped<IDBSeeding, DBSeeding>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbseeding = scope.ServiceProvider.GetRequiredService<IDBSeeding>();
    dbseeding.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => {  });

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();

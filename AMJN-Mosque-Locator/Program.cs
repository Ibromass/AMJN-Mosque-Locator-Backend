using System.Reflection;
using System.Text;
using DotNetEnv;
using AMJN_Mosque_Locator.AppDbContext;
using AMJN_Mosque_Locator.Implementations.Repositories;
using AMJN_Mosque_Locator.Implementations.Services;
using AMJN_Mosque_Locator.Interfaces.Repositories;
using AMJN_Mosque_Locator.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AMJNDbContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("AMJNDbContext"),
        new MySqlServerVersion(new Version(8, 0, 34))
    ));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AMJN Mosque Locator API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token. Example: Bearer eyJhbG..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
Env.Load();
Console.WriteLine("Testing API Key Load...");
Console.WriteLine($"Key found: {!string.IsNullOrEmpty(Env.GetString("GOOGLE_MAPS_API_KEY"))}");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("726FEF51-D266-4AEC-A10C-POI78AA6R00JDJ"))
        };
    });

builder.Services.AddAuthorization();
//builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi 
builder.Services.AddScoped<IMosqueService, MosqueService>();
builder.Services.AddScoped<IAdminauthService, AdminauthService>();
builder.Services.AddScoped<IMosqueRepository, MosqueRepository>();
builder.Services.AddScoped<IAdminauthRepository, AdminauthRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()   // This allows all websites to connect
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AMJN Mosque Locator API V1");
    c.RoutePrefix = string.Empty;
});


//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

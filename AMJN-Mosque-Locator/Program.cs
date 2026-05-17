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
    options.UseNpgsql(builder.Configuration.GetConnectionString("AMJNDbContext")));


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

var jwtTokenKey = builder.Configuration["JwtTokenSettings:TokenKey"];
if (string.IsNullOrWhiteSpace(jwtTokenKey))
{
    throw new InvalidOperationException("JwtTokenSettings:TokenKey is not configured.");
}

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtTokenKey))
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AMJNDbContext>();

    try
    {
        // Try to migrate, but don't fail if tables exist
        db.Database.Migrate();
        Console.WriteLine("Database migration completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration warning: {ex.Message}");
        Console.WriteLine("Continuing with existing database...");
    }
}

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

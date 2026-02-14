using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using YellowKalam.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// ===============================
//        DATABASE CONNECTION
// ===============================
builder.Services.AddDbContext<YellowKalamContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    )
);

// ===============================
//        CONTROLLERS + JSON
// ===============================
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// ===============================
//        JWT AUTHENTICATION
// ===============================
var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

// ===============================
//        AUTHORIZATION POLICIES
// ===============================
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PermissionsAccess", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c =>
                c.Type == "AppUserId" && (c.Value == "1" || c.Value == "2048")
            )
        )
    );
});

// ===============================
//        CORS - ALLOW ALL FOR DEV
// ===============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("Content-Disposition");
    });
});

// ===============================
//        SWAGGER + JWT SUPPORT
// ===============================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "YellowKalam API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {JWT token}"
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

var app = builder.Build();

// ===============================
//        MIDDLEWARE PIPELINE
// ===============================

// CORS MUST be first!
app.UseCors("AllowAll");

// Global exception handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            var ex = error.Error;
            
            // Check if it's a database connection error
            string message = "حدث خطأ في الخادم";
            if (ex.Message.Contains("SQL Server") || ex.Message.Contains("connection"))
            {
                message = "خطأ في الاتصال بقاعدة البيانات";
            }
            
            await context.Response.WriteAsJsonAsync(new { 
                message = message,
                error = app.Environment.IsDevelopment() ? ex.Message : "Internal Server Error",
                details = app.Environment.IsDevelopment() ? ex.StackTrace : null
            });
        }
    });
});

// Swagger enabled for all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "YellowKalam API v1");
    c.RoutePrefix = "swagger";
});

// Redirect root to swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Log startup
Console.WriteLine("===========================================");
Console.WriteLine("YellowKalam API Started!");
Console.WriteLine("Swagger: http://localhost:5299/swagger");
Console.WriteLine("API Base: http://localhost:5299/api");
Console.WriteLine("===========================================");

app.Run();

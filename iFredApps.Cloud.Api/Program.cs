using iFredApps.Cloud.Core.Interfaces.Repository;
using iFredApps.Cloud.Core.Interfaces.Services;
using iFredApps.Cloud.Core.Services;
using iFredApps.Cloud.Data;
using iFredApps.Cloud.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                     new MySqlServerVersion(new Version(8, 0))));

// Reposit�rios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();

// Servi�os
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILicenseService, LicenseService>();

// Configura��o de JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
       options.TokenValidationParameters = new TokenValidationParameters
       {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidAudience = builder.Configuration["Jwt:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       };
    });

builder.Services.AddAuthorization();

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
   options.SwaggerDoc("v1", new OpenApiInfo
   {
      Title = "iFredCloud API",
      Version = "v1",
      Description = "API para autentica��o de usu�rios e valida��o de licen�as.",
   });

   // Adiciona suporte ao JWT no Swagger
   options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
   {
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey,
      Scheme = "Bearer",
      BearerFormat = "JWT",
      In = ParameterLocation.Header,
      Description = "Digite 'Bearer {seu token JWT}' para autenticar-se.",
   });

   options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Configura��o de autoriza��o
//builder.Services.AddAuthorization(options =>
//{
//   options.AddPolicy("Admin", policy => policy.RequireRole("Admin")); // Adiciona pol�tica para o papel 'Admin'
//});

builder.Services.AddControllers();

var app = builder.Build();

// Configura��o de Swagger apenas para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "iFredCloud API v1");
   });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

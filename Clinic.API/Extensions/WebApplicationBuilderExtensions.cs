using Clinic.Domain.Entities;
using Clinic.Domain.Helpers;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Clinic.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentaion(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddSignalR();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", options =>
            {
                // options.AllowAnyHeader()
                // .AllowAnyMethod()
                // .AllowAnyOrigin()
                // .AllowCredentials();
                options.WithOrigins("http://localhost:4200", "http://localhost:61695", "http://localhost:61695", "http://127.0.0.1:4200", "http://localhost:49618", "https://localhost:49618", "http://localhost:55658","https://localhost:55658","http://localhost:64530","https://localhost:64530","http://localhost:64519")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        builder.Services.AddSwaggerGen(c =>
        {
            // Add JWT button to Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
            });

            // Apply Bearer token to all operations
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
                        []
                    }
                });
        });

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;


        }).AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddGoogle(options =>
        {
            var clientId = builder.Configuration["Authentication:Google:ClientId"];
            var clientSecrut = builder.Configuration["Authentication:Google:ClientSecret"];

            if(clientId is null)
                throw new ArgumentNullException(nameof(clientId));

            if(clientSecrut is null)
                throw new ArgumentException(nameof(clientSecrut));


            options.ClientId = clientId;
            options.ClientSecret = clientSecrut;

            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.CallbackPath = "/signin-google";


        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),

                NameClaimType = "name"
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/chat"))
                    {
                        context.Token = accessToken; 
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"JWT failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                }
            };
        });


        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
        builder.Services.AddHttpContextAccessor();
    }
}

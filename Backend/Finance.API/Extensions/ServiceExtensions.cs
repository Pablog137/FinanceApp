using Finance.API.Data;
using Finance.API.Helpers;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Models;
using Finance.API.Repository;
using Finance.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Finance.API.Extensions
{
    public static class ServiceExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ITransferService, TransferService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            // Add repositories
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
        }

        public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                };

            });

        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 2;
                opt.Password.RequireDigit = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


        }
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
        });
            });
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<HealthCheck>("API and DB Check", tags: new[] { "api", "db" });
        }

        public static void AddCors(this IServiceCollection services, string MyAllowSpecificOrigins)
        {

            services.AddCors(options =>
       {
           options.AddPolicy(name: MyAllowSpecificOrigins,
                             policy =>
                             {
                                 policy.WithOrigins("http://localhost:3000")
                                       .AllowAnyHeader()
                                       .AllowAnyMethod();
                             });
       });
        }


    }
}

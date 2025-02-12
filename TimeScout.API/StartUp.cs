using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TimeScout.Application.Services;
using TimeScout.Application.Interfaces;
using TimeScout.Domain.Interfaces;
using TimeScout.Infrastructure.DataAccess;
using TimeScout.Infrastructure.Repository;
using TimeScout.Application.Settings;
using TimeScout.Application.Profiles;
using Microsoft.AspNetCore.RateLimiting;
using TimeScout.Infrastructure.Provider;

namespace TimeScout.API;

public class StartUp
{
    public IConfiguration Configuration { get; }

    public StartUp(IConfiguration config)
    {
        Configuration = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        services.Configure<JwtSetting>(Configuration.GetSection("Jwt"));

        services.AddControllers();
        services.AddHttpContextAccessor();

        // Add repository scoped
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventGroupRepository, EventGroupRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        // Add service scoped
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IEventGroupService, EventGroupService>();
        services.AddScoped<ITagService, TagService>();

        // Add provider scoped
        services.AddScoped<ICurrentUserProvider, CurrentUser>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeScout.API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        services.AddRateLimiter(options => {
            options.AddSlidingWindowLimiter("LoginPolicy", opt => {
                opt.Window = TimeSpan.FromMinutes(1);
                opt.PermitLimit = 5;
                opt.SegmentsPerWindow = 3;
                opt.QueueLimit = 0;

            });
            options.AddFixedWindowLimiter("RegisterPolicy", opt => {
                opt.Window = TimeSpan.FromHours(1);
                opt.PermitLimit = 3;
                opt.QueueLimit = 0;
            });
            options.AddFixedWindowLimiter("RefreshTokenPolicy", opt => {
                opt.Window = TimeSpan.FromHours(1);
                opt.PermitLimit = 10;
                opt.QueueLimit = 0;
            });
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });

        // Add Authentication
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"] ?? throw new ArgumentNullException("No JWT key found in configuration.")))
            };
        });

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<StartUp>();
        services.AddAutoMapper(cfg => {
            cfg.AddProfile<EventGroupProfile>();
            cfg.AddProfile<EventProfile>();
            cfg.AddProfile<TagProfile>();
            cfg.AddProfile<UserProfile>();
        });

        // Configure Database
        services.AddDbContext<TimeScoutDbContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("TimeScoutDb"));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseRateLimiter();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}

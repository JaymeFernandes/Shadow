using System.Text.Json;
using Domain.Models.User;
using Domain.ObjectPool;
using Domain.Options;
using Duende.IdentityServer.Models;
using Infrastructure.Contexts.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class AuthSetup
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:7185",
            ValidateAudience = true,
            ValidAudience = "api1",                 
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:7185"; // IdentityServer URL
                options.Audience = "api1";

                // Aceita tokens de tipo "JWT" e "at+jwt"
                options.TokenValidationParameters = tokenParameters;
                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt", "JWT" };
            });
        

        // Identity (EF + Tokens)
        services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityContext>()
            .AddDefaultTokenProviders();
        
        services.AddScoped<SignInManager<IdentityUser>>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Type = "https://httpstatuses.com/401"
                };

                var json = JsonSerializer.Serialize(problem);

                return context.Response.WriteAsync(json);
            };
            
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Type = "https://httpstatuses.com/403"
                };

                var json = JsonSerializer.Serialize(problem);

                return context.Response.WriteAsync(json);
            };
        });

        // IdentityServer (Duende)
        services.AddIdentityServer(options =>
            {
                options.IssuerUri = "https://localhost:7185";
            })
            .AddAspNetIdentity<IdentityUser>()
            .AddInMemoryClients(IdentityServerConfig.Clients)
            .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
            .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseNpgsql(configuration["Connections:PostgreSql:Identity"],
                        sql => sql.MigrationsAssembly("Api"));
            })
            .AddDeveloperSigningCredential();

        // Authorization policy for scope "api1"
        services.AddAuthorization(options =>
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "api1");
            }));

        return services;
    }

    public static async Task UseAuth(this WebApplication app)
    {
        var users = app.Configuration.GetSection("Users").Get<List<UserSettings>>();

        using var scoped = app.Services.CreateScope();
        var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var mongoPool = scoped.ServiceProvider.GetRequiredService<MongoPool>();

        string[] roles = Enum.GetNames(typeof(Roles));

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        foreach (var user in users)
        {
            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(newUser, user.Password);

                foreach (var role in user.Roles)
                {
                    await userManager.AddToRoleAsync(newUser, role);
                }

                var mongoUser = new MongoUser
                {
                    Code = newUser.Id,
                    Display = user.Display,
                    Name = user.Name,
                    Lang = "en",
                    LastLoginAt = DateTime.Now,
                    isPrivate = false,
                    CachedAt = DateTime.Now
                };

                mongoPool.UsersQueue.Enqueue(mongoUser);
            }
        }
    }
}

public static class IdentityServerConfig
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource("roles", new[] { "role" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("api1", "Main API"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "api_client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AllowedScopes = { "api1", "openid", "profile", "email", "roles", "offline_access" },
                AllowOfflineAccess = true,
                AccessTokenLifetime = 3600,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 2592000
            }
        };
}

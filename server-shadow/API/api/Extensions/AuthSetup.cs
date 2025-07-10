using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models.User;
using Domain.ObjectPool;
using Domain.Options;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Infrastructure.Contexts.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class AuthSetup
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = "api-shadow",
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:7185",
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
                options.Authority = "https://localhost:7185";
                options.Audience = "api-shadow";
                
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = tokenParameters;
                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt", "JWT" };
            });
        
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

        services.AddIdentityServer(options =>
            {
                options.IssuerUri = "https://localhost:7185";
            })
            .AddAspNetIdentity<IdentityUser>()
            .AddProfileService<ProfileService>()
            .AddInMemoryClients(IdentityServerConfig.Clients)
            .AddInMemoryApiResources(IdentityServerConfig.ApiResources)
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
        // services.AddAuthorization(options =>
        //     options.AddPolicy("ApiScope", policy =>
        //     {
        //         policy.RequireAuthenticatedUser();
        //         policy.RequireClaim("scope", "api-shadow");
        //     }));

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
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("api-shadow", "Main API")
        {
            Scopes = { "api-shadow" }
        }
    ];

    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource("roles", new[] { "role" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("api-shadow", "Main API")
    ];

    public static IEnumerable<Client> Clients =>
    [
        new Client
            {
                ClientId = "api_client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AllowedScopes = { "api-shadow", "openid", "profile", "email", "roles", "offline_access" },
                AllowOfflineAccess = true,
                AccessTokenLifetime = 3600,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 2592000,
                
                AccessTokenType = AccessTokenType.Jwt,
                AlwaysIncludeUserClaimsInIdToken = true
            }
    ];
}

public class ProfileService : IProfileService
{
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var userId = context.Subject?.FindFirst("sub")?.Value;
        if (userId == null) return;

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return;

        var userClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
        };

        var roles = await _userManager.GetRolesAsync(user);
        userClaims.AddRange(roles.Select(role => new Claim("role", role)));

        context.IssuedClaims.AddRange(userClaims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var userId = context.Subject?.FindFirst("sub")?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            context.IsActive = false;
            return;
        }
        
        var user = await _userManager.FindByIdAsync(userId);
        context.IsActive = user != null;
    }
}
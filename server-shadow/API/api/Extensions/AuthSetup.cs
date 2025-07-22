using Domain.Models.User;
using Domain.Models.User.Shared;
using Domain.ObjectPool;
using Domain.Options;
using Infrastructure.Contexts.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

namespace Api.Extensions;

public static class AuthSetup
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityContext>()
            .AddDefaultTokenProviders();

        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<AppIdentityContext>();
            })
            .AddServer(options =>
            {
                options.SetTokenEndpointUris("connect/token");
                
                options.AllowClientCredentialsFlow()
                    .AllowRefreshTokenFlow();

                options.AllowPasswordFlow()
                    .AllowRefreshTokenFlow();

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate()
                    .DisableAccessTokenEncryption();

                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableEndSessionEndpointPassthrough()
                    .DisableTransportSecurityRequirement();

                options.SetAccessTokenLifetime(TimeSpan.FromHours(1));
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(30));
            });
        
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            
        })
        .AddJwtBearer(optons =>
        {
            optons.Authority = "https://localhost:7185";
            optons.Audience = "api-shadow";
            
            optons.RequireHttpsMetadata = false;
        
            optons.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
    
    
    
    public static async Task UseAuth(this WebApplication app)
    {
        var users = app.Configuration.GetSection("Users").Get<List<UserSettings>>();

        using var scoped = app.Services.CreateScope();

        var appManager = scoped.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var scopeManager = scoped.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();
        
        var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var mongoPool = scoped.ServiceProvider.GetRequiredService<MongoPool>();
        
        foreach (var value in IdentityServerConfiguration.Applications)
        {
            if (await appManager.FindByClientIdAsync(value.ClientId!) is null)
                await appManager.CreateAsync(value);
        }

        foreach (var value in IdentityServerConfiguration.Scopes)
        {
            if(await scopeManager.FindByNameAsync(value.Name!) is null)
                await scopeManager.CreateAsync(value);
        }

        foreach (var role in Enum.GetNames(typeof(Roles)))
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
                    IsPrivate = false,
                    CachedAt = DateTime.Now
                };

                mongoPool.UsersQueue.Enqueue(mongoUser);
            }
        }
    }

    public static class IdentityServerConfiguration
    {
        public static OpenIddictApplicationDescriptor[] Applications =>
        [
            new()
            {
                ClientId = "api-shadow",
                DisplayName = "API Shadow",
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Scopes.OpenId,
                    OpenIddictConstants.Scopes.Email,
                    OpenIddictConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Permissions.Prefixes.Scope + "api-shadow"
                }
            }
        ];

        public static OpenIddictScopeDescriptor[] Scopes =>
        [
            new()
            {
                Name = "api-shadow",
                DisplayName = "API Shadow",
                Resources =
                {
                    "api-shadow"
                }
            },
            new()
            {
                Name = "roles",
                DisplayName = "roles",
                Resources = { "role" }
            }
        ];
    }
}



